using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
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
                        Id = w.Vendor.Person.Location.Id,
                        City = w.Vendor.Person.Location.City,
                        Adress = w.Vendor.Person.Location.Adress,
                        Latitude = w.Vendor.Person.Location.Latitude,
                        Longitude = w.Vendor.Person.Location.Longitude,
                        PostIndex = w.Vendor.Person.Location.PostIndex
                    },
                }).ToList();

            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Include(w => w.Company.Account)
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
                        Id = w.Company.Location.Id,
                        City = w.Company.Location.City,
                        Adress = w.Company.Location.Adress,
                        Latitude = w.Company.Location.Latitude,
                        Longitude = w.Company.Location.Longitude,
                        PostIndex = w.Company.Location.PostIndex
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

            var vendorsWorksList = new List<Work>();

            if (category != null && subcategory == null)
            {
                vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Where(w => category != null && (
                        w.Subcategory.Category.Name.Contains(category) ||
                        w.Subcategory.Category.Tags.Contains(category)
                     ))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .ToListAsync();
            }

            if (category == null && subcategory != null)
            {
                vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Where(w => subcategory != null && (
                        w.Subcategory.Name.Contains(subcategory) ||
                        w.Subcategory.Tags.Contains(subcategory)
                    ))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .ToListAsync();
            }

            if (category != null && subcategory != null)
            {
                vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Where(w => category != null && (
                        w.Subcategory.Category.Name.Contains(category) ||
                        w.Subcategory.Category.Tags.Contains(category)
                     ))
                .Where(w => subcategory != null && (
                        w.Subcategory.Name.Contains(subcategory) ||
                        w.Subcategory.Tags.Contains(subcategory)
                    ))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .ToListAsync();
            }
            

            var vendorsWorks = CreateVendorsWorks(vendorsWorksList, reviews);

            


            var companiesWorksList = new List<Work>();

            if (category != null && subcategory == null)
            {
                companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(w => category != null && (
                        w.Subcategory.Category.Name.Contains(category) ||
                        w.Subcategory.Category.Tags.Contains(category)
                     ))
                .Include(w => w.Company.Account)
                .ToListAsync();
            }

            if (category == null && subcategory != null)
            {
                companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(w => subcategory != null && (
                        w.Subcategory.Name.Contains(subcategory) ||
                        w.Subcategory.Tags.Contains(subcategory)
                    ))
                .Include(w => w.Company.Account)
                .ToListAsync();
            }

            if (category != null && subcategory != null)
            {
                companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(w => category != null && (
                        w.Subcategory.Category.Name.Contains(category) ||
                        w.Subcategory.Category.Tags.Contains(category)
                     ))
                .Where(w => subcategory != null && (
                        w.Subcategory.Name.Contains(subcategory) ||
                        w.Subcategory.Tags.Contains(subcategory)
                    ))
                .Include(w => w.Company.Account)
                .ToListAsync();
            }

            var companiesWorks = CreateCompaniesWorks(companiesWorksList, reviews);

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchWorks;
        }

        private List<SearchWorkDTO> CreateVendorsWorks(List<Work> works, IEnumerable<Review> reviews)
        {
            return works
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
                        Id = w.Vendor.Person.Location.Id,
                        City = w.Vendor.Person.Location.City,
                        Adress = w.Vendor.Person.Location.Adress,
                        Latitude = w.Vendor.Person.Location.Latitude,
                        Longitude = w.Vendor.Person.Location.Longitude,
                        PostIndex = w.Vendor.Person.Location.PostIndex
                    },
                }).ToList();
        }

        private List<SearchWorkDTO> CreateCompaniesWorks(List<Work> works, IEnumerable<Review> reviews)
        {
            return works
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
                        Id = w.Company.Location.Id,
                        City = w.Company.Location.City,
                        Adress = w.Company.Location.Adress,
                        Latitude = w.Company.Location.Latitude,
                        Longitude = w.Company.Location.Longitude,
                        PostIndex = w.Company.Location.PostIndex
                    },
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
    }
}
