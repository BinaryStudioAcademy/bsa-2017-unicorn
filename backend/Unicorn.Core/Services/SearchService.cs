using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Search;
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

        public async Task<List<SearchWorkDTO>> GetAllWorks()
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .ToListAsync();

            var vendorsWorks = vendorsWorksList
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,                    
                    Rating = CalculateRating(w.Vendor.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id),
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
                }).ToList();

            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();

            var companiesWorks = companiesWorksList
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Company.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Company.Account.Id),
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
                }).ToList();

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchWorks;
        }




        public async Task<List<SearchWorkDTO>> GetWorksByBaseFilters(string category, string subcategory, int date)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query                
                .Where(w => w.Vendor != null &&
                    (w.Subcategory.Name.Contains(subcategory) || w.Subcategory.Tags.Contains(subcategory)) &&
                    (w.Subcategory.Category.Name.Contains(category) || w.Subcategory.Category.Tags.Contains(category)))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .ToListAsync();


            var vendorsWorks = vendorsWorksList
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Vendor.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id),
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
                }).ToList();

            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null &&
                    (w.Subcategory.Name.Contains(subcategory) || w.Subcategory.Tags.Contains(subcategory)) &&
                    (w.Subcategory.Category.Name.Contains(category) || w.Subcategory.Category.Tags.Contains(category)))
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();

            var companiesWorks = companiesWorksList
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Company.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Company.Account.Id),
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
                }).ToList();

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchWorks;
        }

        private double CalculateRating(long recieverId)
        {
            var ratings = _unitOfWork.RatingRepository.Query
                  .Include(y => y.Reciever)
                  .Where(p => p.Reciever.Id == recieverId)
                  .ToList();

            return ratings.Any() ? ratings.Average(z => z.Grade) : 0;
        }
    }
}
