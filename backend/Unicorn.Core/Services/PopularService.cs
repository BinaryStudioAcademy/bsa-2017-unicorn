using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
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
                .ToListAsync();

            var vendors = vendorsList
                .Select(v => new PerformerDTO
                {
                    Id = v.Id,
                    Avatar = v.Person.Account.Avatar,
                    Name = v.Person.Name,
                    Rating = _ratingService.GetAvarageByRecieverId(v.Person.Account.Id).Result,
                    ReviewsCount = reviews.Count(r => r.ToAccountId == v.Person.Account.Id),
                    PerformerType = "vendor",
                    Link = "vendor/" + v.Id,
                    City = v.Person.Location.City
                }).ToList();

            var companiesList = await _uow.CompanyRepository
                .Query
                .Include(c => c.Account)
                .ToListAsync();

            var companies = companiesList
                .Select(c => new PerformerDTO
                {
                    Id = c.Id,
                    Avatar = c.Account.Avatar,
                    Name = c.Name,
                    Rating = _ratingService.GetAvarageByRecieverId(c.Account.Id).Result,
                    ReviewsCount = reviews.Count(r => r.ToAccountId == c.Account.Id),
                    PerformerType = "company",
                    Link = "company/" + c.Id,
                    City = c.Location.City
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
                .Include(w => w.Company)
                .Include(w => w.Company.Account)
                .ToListAsync();


            var vendors = works
                .Where(v => v.Vendor != null)
                .Select(v => new PerformerDTO
                {
                    Id = v.Vendor.Id,
                    Avatar = v.Vendor.Person.Account.Avatar,
                    Name = v.Vendor.Person.Name,
                    Rating = _ratingService.GetAvarageByRecieverId(v.Vendor.Person.Account.Id).Result,
                    ReviewsCount = reviews.Count(r => r.ToAccountId == v.Vendor.Person.Account.Id),
                    PerformerType = "vendor",
                    Link = $"vendor/" + v.Vendor.Id,
                    City = v.Vendor.Person.Location.City
                });

            var companies = works
                .Where(c => c.Company != null)
                .Select(c => new PerformerDTO
                {
                    Id = c.Company.Id,
                    Avatar = c.Company.Account.Avatar,
                    Name = c.Company.Name,
                    Rating = _ratingService.GetAvarageByRecieverId(c.Company.Account.Id).Result,
                    ReviewsCount = reviews.Count(r => r.ToAccountId == c.Company.Account.Id),
                    PerformerType = "company",
                    Link = "company/" + c.Company.Id,
                    City = c.Company.Location.City
                });

            var performers = vendors
                .Concat(companies)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return performers;
        }
    }
}
