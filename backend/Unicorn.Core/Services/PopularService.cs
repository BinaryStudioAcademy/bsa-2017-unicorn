using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Popular;

namespace Unicorn.Core.Services
{
    public class PopularService : IPopularService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRatingService _ratingService;

        public PopularService(IUnitOfWork uow, IRatingService ratingService)
        {
            _uow = uow;
            _ratingService = ratingService;
        }

        public async Task<List<FullPerformerDTO>> GetPerformersByFilterAsync(
            string city, string name, string role, double? rating, string ratingCondition, bool withReviews, string categoriesString,
            string subcategoriesString, double? latitude, double? longitude, double? distance, string sort, int page, int pagesize
            )
        {
            var reviews = await _uow.ReviewRepository.GetAllAsync();
            var categories = !string.IsNullOrEmpty(categoriesString) ? categoriesString.Split(' ').Select(c => Int64.Parse(c)).ToList() : new List<long>();
            var subcategories = !string.IsNullOrEmpty(subcategoriesString) ? subcategoriesString.Split(' ').Select(c => Int64.Parse(c)).ToList() : new List<long>();

            IQueryable<Vendor> vendorsQuery = null;
            IQueryable<Company> companiesQuery = null;

            if (role == "vendor" || role == "all")
            {
                vendorsQuery = _uow.VendorRepository
                    .Query
                    .Include(v => v.Works
                        .Select(w => w.Subcategory.Category))
                    .Include(v => v.Person)
                    .Include(v => v.Person.Account)
                    .Include(v => v.Person.Account.Location);
            }
            if (role == "company" || role == "all")
            {
                companiesQuery = _uow.CompanyRepository
                .Query
                .Include(c => c.Works)
                .Include(c => c.Account)
                .Include(c => c.Account.Location);
            }
            //if (!string.IsNullOrEmpty(city))
            //{
            //    vendorsQuery = vendorsQuery?
            //        .Where(v => v.Person.Account.Location.City.Contains(city));
            //    companiesQuery = companiesQuery?
            //        .Where(c => c.Account.Location.City.Contains(city));
            //}
            if (!string.IsNullOrEmpty(name))
            {
                vendorsQuery = vendorsQuery?
                    .Where(v => v.Person.Name.Contains(name));
                companiesQuery = companiesQuery?
                    .Where(c => c.Name.Contains(name));
            }

            var vendorsCategoryQuery = vendorsQuery;
            var companiesCategoryQuery = companiesQuery;

            foreach (var categoryId in categories)
            {
                vendorsCategoryQuery = vendorsCategoryQuery?
                    .Where(v => v.Works.Any(w => w.Subcategory.Category.Id == categoryId));
                companiesCategoryQuery = companiesCategoryQuery?
                    .Where(c => c.Works.Any(w => w.Subcategory.Category.Id == categoryId));
            }

            foreach (var subcategoryId in subcategories)
            {
                vendorsQuery = vendorsQuery?
                    .Where(v => v.Works.Any(w => w.Subcategory.Id == subcategoryId));
                companiesQuery = companiesQuery?
                    .Where(c => c.Works.Any(w => w.Subcategory.Id == subcategoryId));
            }

            var vendorsList = vendorsQuery != null ? await vendorsQuery.Intersect(vendorsCategoryQuery).ToListAsync() : new List<Vendor>();
            var vendors = vendorsList
                .Select(v => VendorToFullPerformer(v, reviews, longitude, latitude))
                .ToList();
            var companiesList = companiesQuery != null ? await companiesQuery.Intersect(companiesCategoryQuery).ToListAsync() : new List<Company>();
            var companies = companiesList
                .Select(c => CompanyToFullPerformer(c, reviews, longitude, latitude))
                .ToList();

            var performersQuery = ConcatPerformers(vendors, companies).AsQueryable();

            if (rating != null)
            {
                switch (ratingCondition)
                {
                    case "lower":
                        performersQuery = performersQuery.Where(p => p.Rating <= rating);
                        break;
                    case "grater":
                    default:
                        performersQuery = performersQuery.Where(p => p.Rating >= rating);
                        break;
                }
            }

            if (distance == 0 || distance == null)
            {
                if (!string.IsNullOrEmpty(city))
                    performersQuery = performersQuery?
                        .Where(p => p.Location.City.Contains(city));
            }
            else
            {
                performersQuery = performersQuery?
                    .Where(p => p.Distance <= distance);
            }

            performersQuery = performersQuery
                .Where(p => !withReviews || p.ReviewsCount > 0);

            switch (sort)
            {
                case "rating":
                    performersQuery = performersQuery.OrderByDescending(x => x.Rating);
                    break;
                case "name":
                    performersQuery = performersQuery.OrderBy(x => x.Name);
                    break;
                case "distance":
                    performersQuery = performersQuery.OrderBy(x => x.Distance);
                    break;
                default:
                    performersQuery = performersQuery.OrderByDescending(x => x.Rating);
                    break;
            }

            return performersQuery.ToList()
                .Skip(pagesize * (page - 1))
                .Take(pagesize).ToList();
        }
        public async Task<List<FullPerformerDTO>> GetAllPerformersAsync()
        {
            var reviews = await _uow.ReviewRepository.GetAllAsync();

            var vendorsList = await _uow.VendorRepository
                .Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .Include(v => v.Person.Account.Location)
                .ToListAsync();

            var vendors = vendorsList
                .Select(v => VendorToFullPerformer(v, reviews, null, null)).ToList();

            var companiesList = await _uow.CompanyRepository
                .Query
                .Include(c => c.Account)
                .Include(c => c.Account.Location)
                .ToListAsync();

            var companies = companiesList
                .Select(c => CompanyToFullPerformer(c, reviews, null, null)).ToList();

            return ConcatPerformers(vendors, companies);
        }

        private List<FullPerformerDTO> ConcatPerformers(List<FullPerformerDTO> p1, List<FullPerformerDTO> p2)
        {
            return p1.Concat(p2)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();
        }

        public async Task<List<PopularCategoryDTO>> GetPopularCategories()
        {
            var works = await _uow.WorkRepository.Query
                .Include(w => w.Subcategory)
                .Include(w => w.Subcategory.Category)
                .OrderByDescending(w => w.Orders).ToListAsync();

            return works
                .Select(w => w.Subcategory.Category)
                .Distinct()
                .Take(3)
                .Select(c => new PopularCategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public async Task<List<PerformerDTO>> GetPopularPerformers()
        {
            var reviews = await _uow.ReviewRepository.GetAllAsync();

            var vendorsList = await _uow.VendorRepository
                .Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .Include(v => v.Person.Account.Location)
                .ToListAsync();

            var vendors = vendorsList
                .Select(v => new PerformerDTO
                {
                    Id = v.Id,
                    Avatar = v.Person.Account.Avatar,
                    Name = v.Person.Name,
                    Rating = CalculateRating(v.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == v.Person.Account.Id),
                    PerformerType = "vendor",
                    Link = "vendor/" + v.Id,
                    City = v.Person.Account.Location.City
                }).ToList();

            var companiesList = await _uow.CompanyRepository
                .Query
                .Include(c => c.Account)
                .Include(c => c.Account.Location)
                .ToListAsync();

            var companies = companiesList
                .Select(c => new PerformerDTO
                {
                    Id = c.Id,
                    Avatar = c.Account.Avatar,
                    Name = c.Name,
                    Rating = CalculateRating(c.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == c.Account.Id),
                    PerformerType = "company",
                    Link = "company/" + c.Id,
                    City = c.Account.Location.City
                }).ToList();

            var performers = vendors
                .Concat(companies)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .Take(5).ToList();

            return performers;
        }

        public async Task<List<PerformerDTO>> GetPopularPerformers(long id)
        {
            var reviews = await _uow.ReviewRepository.GetAllAsync();

            var works = await _uow.WorkRepository
                .Query
                .Where(w => w.Subcategory.Category.Id == id)
                .Include(w => w.Subcategory)
                .Include(w => w.Subcategory.Category)
                .Include(w => w.Vendor)
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .Include(w => w.Company)
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();


            var vendors = works
                .Where(v => v.Vendor != null)
                .Select(v => new PerformerDTO
                {
                    Id = v.Vendor.Id,
                    Avatar = v.Vendor.Person.Account.Avatar,
                    Name = v.Vendor.Person.Name,
                    Rating = CalculateRating(v.Vendor.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == v.Vendor.Person.Account.Id),
                    PerformerType = "vendor",
                    Link = $"vendor/" + v.Vendor.Id,
                    City = v.Vendor.Person.Account.Location.City
                });

            var companies = works
                .Where(c => c.Company != null)
                .Select(c => new PerformerDTO
                {
                    Id = c.Company.Id,
                    Avatar = c.Company.Account.Avatar,
                    Name = c.Company.Name,
                    Rating = CalculateRating(c.Company.Account.Id),//_ratingService.GetAvarageByRecieverId(c.Company.Account.Id).Result,
                    ReviewsCount = reviews.Count(r => r.ToAccountId == c.Company.Account.Id),
                    PerformerType = "company",
                    Link = "company/" + c.Company.Id,
                    City = c.Company.Account.Location.City
                });

            var performers = vendors
                .Concat(companies)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .Take(5)
                .ToList();

            return performers;
        }

        private FullPerformerDTO CompanyToFullPerformer(Company c, IEnumerable<Review> reviews, double? longitude, double? latitude)
        {
            return new FullPerformerDTO
            {
                Id = c.Id,
                Avatar = c.Account.Avatar,
                Name = c.Name,
                Description = c.Description,
                Rating = CalculateRating(c.Account.Id),
                ReviewsCount = reviews.Count(r => r.ToAccountId == c.Account.Id),
                PerformerType = "company",
                Link = "company/" + c.Id,
                Location = new LocationDTO
                {
                    Id = c.Account.Location.Id,
                    City = c.Account.Location.City,
                    Adress = c.Account.Location.Adress,
                    Latitude = c.Account.Location.Latitude,
                    Longitude = c.Account.Location.Longitude,
                    PostIndex = c.Account.Location.PostIndex
                },
                Distance = CalculateDistance(c.Account.Location.Latitude, c.Account.Location.Longitude, latitude, longitude)
            };
        }


        private FullPerformerDTO VendorToFullPerformer(Vendor v, IEnumerable<Review> reviews, double? longitude, double? latitude)
        {
            return new FullPerformerDTO
            {
                Id = v.Id,
                Avatar = v.Person.Account.Avatar,
                Name = v.Person.Name,
                Description = v.Position,
                Rating = CalculateRating(v.Person.Account.Id),
                ReviewsCount = reviews.Count(r => r.ToAccountId == v.Person.Account.Id),
                PerformerType = "vendor",
                Link = "vendor/" + v.Id,
                Location = new LocationDTO
                {
                    Id = v.Person.Account.Location.Id,
                    City = v.Person.Account.Location.City,
                    Adress = v.Person.Account.Location.Adress,
                    Latitude = v.Person.Account.Location.Latitude,
                    Longitude = v.Person.Account.Location.Longitude,
                    PostIndex = v.Person.Account.Location.PostIndex
                },
                Distance = CalculateDistance(v.Person.Account.Location.Latitude, v.Person.Account.Location.Longitude, latitude, longitude)
            };
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

        private double CalculateRating(long recieverId)
        {
            var ratings = _uow.RatingRepository.Query
                  .Include(y => y.Reciever)
                  .Where(p => p.Reciever.Id == recieverId)
                  .ToList();

            return ratings.Any() ? ratings.Average(z => z.Grade) : 0;
        }
    }
}
