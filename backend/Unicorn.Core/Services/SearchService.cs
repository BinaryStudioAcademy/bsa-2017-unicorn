using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Search;

namespace Unicorn.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SearchPerformerDTO>> GetSearchPerformers()
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsList = await _unitOfWork.VendorRepository
                .Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .ToListAsync();

            var vendors = vendorsList
                .Select(v => new SearchPerformerDTO
                {
                    Id = v.Id,
                    Avatar = v.Person.Account.Avatar,
                    Name = v.Person.Name,
                    Rating = CalculateRating(v.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == v.Person.Account.Id),
                    PerformerType = "vendor",
                    Link = "vendor/" + v.Id,
                    City = v.Person.Location.City
                }).ToList();

            var companiesList = await _unitOfWork.CompanyRepository
                .Query
                .Include(c => c.Account)
                .ToListAsync();

            var companies = companiesList
                .Select(c => new SearchPerformerDTO
                {
                    Id = c.Id,
                    Avatar = c.Account.Avatar,
                    Name = c.Name,
                    Rating = CalculateRating(c.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == c.Account.Id),
                    PerformerType = "company",
                    Link = "company/" + c.Id,
                    City = c.Location.City
                }).ToList();

            var searchPerformers = vendors
                .Concat(companies)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchPerformers;
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
