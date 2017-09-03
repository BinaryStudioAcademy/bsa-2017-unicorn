using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Search;
using System.Device.Location;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SearchWorkDTO>> GetWorksByFilters(  string category, string subcategory, int? date,
                                                                   string vendor, string ratingcompare, double? rating, bool? reviews,
                                                                   double? latitude, double? longitude, double? distance,
                                                                   string[] categories, string[] subcategories,
                                                                   string sort  )
        {
            var reviewsList = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)                
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Where(w => string.IsNullOrEmpty(vendor) || w.Vendor.Person.Name.Contains(vendor))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .ToListAsync();

            var vendorsWorks = CreateVendorsWorks(vendorsWorksList, reviewsList, 
                ratingcompare, rating, reviews, latitude, longitude, distance, categories, subcategories);


            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Where(w => string.IsNullOrEmpty(vendor) || w.Company.Name.Contains(vendor))
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();

            var companiesWorks = CreateCompaniesWorks(companiesWorksList, reviewsList,
                ratingcompare, rating, reviews, latitude, longitude, distance, categories, subcategories);

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .Distinct()
                .ToList();

            var searchWorksOrdered = new List<SearchWorkDTO>();

            switch (sort)
            {
                case "rating":
                    searchWorksOrdered = searchWorks.OrderByDescending(x => x.Rating).ToList();
                    break;
                case "name":
                    searchWorksOrdered = searchWorks.OrderBy(x => x.Name).ToList();
                    break;
                case "distance":
                    searchWorksOrdered = searchWorks.OrderBy(x => x.Distance).ToList();
                    break;
                default:
                    searchWorksOrdered = searchWorks.OrderByDescending(x => x.Rating).ToList();
                    break;
            }

            return searchWorksOrdered;
        }

        private List<SearchWorkDTO> CreateVendorsWorks( List<Work> works, IEnumerable<Review> reviewsList, 
                                                        string ratingcompare, double? rating, bool? reviews,
                                                        double? latitude, double? longitude, double? distance,
                                                        string[] categories, string[] subcategories )
        {
            return works
                .Where(w => string.IsNullOrEmpty(ratingcompare) || rating == null ||
                           (ratingcompare.Equals("ge") && (CalculateRating(w.Vendor.Person.Account.Id) >= rating)) ||
                           (ratingcompare.Equals("le") && (CalculateRating(w.Vendor.Person.Account.Id) <= rating)))
                .Where(w => reviews == null || reviewsList.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id) > 0)
                .Where(w => distance == null || CalculateDistance(w.Vendor.Person.Account.Location.Latitude, w.Vendor.Person.Account.Location.Longitude, 
                            latitude, longitude) <= distance)
                .Where(w => categories == null || string.IsNullOrEmpty(categories[0]) || categories.Any(c => c.Equals(w.Subcategory.Category.Name)))
                .Where(w => subcategories == null || string.IsNullOrEmpty(subcategories[0]) || subcategories.Any(c => c.Equals(w.Subcategory.Name)))
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Vendor.Person.Account.Id),
                    ReviewsCount = reviewsList.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id),
                    PerformerType = "vendor",
                    PerformerName = $"{w.Vendor.Person.Name} ({w.Vendor.Position})",
                    Link = "vendor/" + w.Vendor.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Vendor.Person.Account.Location.Id,
                        City = w.Vendor.Person.Account.Location.City,
                        Adress = w.Vendor.Person.Account.Location.Adress,
                        Latitude = w.Vendor.Person.Account.Location.Latitude,
                        Longitude = w.Vendor.Person.Account.Location.Longitude,
                        PostIndex = w.Vendor.Person.Account.Location.PostIndex
                    },
                    Distance = CalculateDistance(w.Vendor.Person.Account.Location.Latitude, w.Vendor.Person.Account.Location.Longitude,
                                                 latitude, longitude)
                }).ToList();
        }

        private List<SearchWorkDTO> CreateCompaniesWorks( List<Work> works, IEnumerable<Review> reviewsList,
                                                          string ratingcompare, double? rating, bool? reviews,
                                                          double? latitude, double? longitude, double? distance,
                                                          string[] categories, string[] subcategories )
        {
            return works
                .Where(w => string.IsNullOrEmpty(ratingcompare) || rating == null ||
                            (ratingcompare.Equals("ge") && (CalculateRating(w.Company.Account.Id) >= rating)) ||
                            (ratingcompare.Equals("le") && (CalculateRating(w.Company.Account.Id) <= rating)))
                .Where(w => reviews == null || reviewsList.Count(r => r.ToAccountId == w.Company.Account.Id) > 0)
                .Where(w => distance == null || CalculateDistance(w.Company.Account.Location.Latitude, w.Company.Account.Location.Longitude,
                            latitude, longitude) <= distance)
                .Where(w => categories == null || string.IsNullOrEmpty(categories[0]) || categories.Any(c => c.Equals(w.Subcategory.Category.Name)))
                .Where(w => subcategories == null || string.IsNullOrEmpty(subcategories[0]) || subcategories.Any(c => c.Equals(w.Subcategory.Name)))
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Company.Account.Id),
                    ReviewsCount = reviewsList.Count(r => r.ToAccountId == w.Company.Account.Id),
                    PerformerType = "company",
                    PerformerName = $"{w.Company.Name}",
                    Link = "company/" + w.Company.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Company.Account.Location.Id,
                        City = w.Company.Account.Location.City,
                        Adress = w.Company.Account.Location.Adress,
                        Latitude = w.Company.Account.Location.Latitude,
                        Longitude = w.Company.Account.Location.Longitude,
                        PostIndex = w.Company.Account.Location.PostIndex
                    },
                    Distance = CalculateDistance(w.Company.Account.Location.Latitude, w.Company.Account.Location.Longitude,
                                                 latitude, longitude)
                }).ToList();
        }

        private double CalculateRating(long recieverId)
        {
            var ratings = _unitOfWork.RatingRepository.Query
                  .Include(y => y.Reciever)
                  .Where(p => p.Reciever.Id == recieverId)
                  .ToList();

            return ratings.Any() ? ratings.Average(z => z.Grade) : 0;
        }

        private double CalculateDistance(double lat1, double long1, double? lat2, double? long2)
        {
            if (lat2 == null || long2 == null)
                return 0;
            var coord1 = new GeoCoordinate(lat1, long1);
            var coord2 = new GeoCoordinate((double)lat2, (double)long2);
            var distance = coord1.GetDistanceTo(coord2) / 1000;
            return distance;
        }




        //public async Task<List<SearchWorkDTO>> GetAllWorks()
        //{
        //    var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

        //    var vendorsWorksList = await _unitOfWork.WorkRepository
        //        .Query
        //        .Where(w => w.Vendor != null)
        //        .Include(w => w.Vendor.Person)
        //        .Include(w => w.Vendor.Person.Account)
        //        .ToListAsync();

        //    var vendorsWorks = CreateVendorsWorks(vendorsWorksList, reviews);

        //    var companiesWorksList = await _unitOfWork.WorkRepository
        //        .Query
        //        .Where(w => w.Company != null)
        //        .Include(w => w.Company.Account)
        //        .ToListAsync();

        //    var companiesWorks = CreateCompaniesWorks(companiesWorksList, reviews);

        //    var searchWorks = vendorsWorks
        //        .Concat(companiesWorks)
        //        .OrderByDescending(p => p.Rating)
        //        .Distinct()
        //        .ToList();

        //    return searchWorks;
        //}




        //public async Task<List<SearchWorkDTO>> GetWorksByBaseFilters(string category, string subcategory, int? date)
        //{
        //    var reviewsList = await _unitOfWork.ReviewRepository.GetAllAsync();

        //    var vendorsWorksList = await _unitOfWork.WorkRepository
        //        .Query
        //        .Where(w => w.Vendor != null)
        //        .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
        //        .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
        //        .Include(w => w.Vendor.Person)
        //        .Include(w => w.Vendor.Person.Account)
        //        .Include(w => w.Vendor.Person.Account.Location)
        //        .ToListAsync();

        //    var vendorsWorks = CreateVendorsWorks(vendorsWorksList, reviewsList);

        //    var companiesWorksList = await _unitOfWork.WorkRepository
        //        .Query
        //        .Where(w => w.Company != null)
        //    .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
        //    .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
        //    .Include(w => w.Company.Account)
        //    .Include(w => w.Company.Account.Location)
        //    .ToListAsync();

        //    var companiesWorks = CreateCompaniesWorks(companiesWorksList, reviewsList);

        //    var searchWorks = vendorsWorks
        //        .Concat(companiesWorks)
        //        .OrderByDescending(p => p.Rating)
        //        .Distinct()
        //        .ToList();

        //    return searchWorks;
        //}
    }
}
