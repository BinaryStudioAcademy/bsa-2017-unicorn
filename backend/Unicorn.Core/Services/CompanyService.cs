using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable> GetAllCompaniesAsync()
        {
            var result = await GetCompanies();

            return result;
        }

        public async Task<object> GetCompanyByIdAsync(long id)
        {
            var result = await GetCompany(id);

            return result;
        }







        private async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            var companies = await _unitOfWork.CompanyRepository.GetAllAsync();
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            if (companies.Any())
            {
                var companiesDTO = companies.Select(
                    company =>
                        new CompanyDTO
                        {
                            Avatar = company.Account?.Avatar ?? "default",
                            Name = company.Name,
                            Description = company.Description,
                            FoundationDate = company.FoundationDate,
                            Rating = company.Account?.Rating ?? 0,
                            Director = company.Director?.Avatar ?? "default",
                            Location = new LocationDTO
                            {
                                Adress = company.Location?.Adress ?? "default",
                                City = company.Location?.City ?? "default",
                                Latitude = company.Location?.CoordinateX ?? 0,
                                Longitude = company.Location?.CoordinateY ?? 0
                            },
                            Reviews = reviews?.Where(p => p.To == company.Name)
                                .Select(x => new ReviewDTO
                                {
                                    Avatar = x.Avatar,
                                    Date = x.Date,
                                    From = x.From,
                                    To = x.To,
                                    Grade = x.Grade,
                                    Description = x.Description
                                }).ToList(),
                            Vendors = company.Vendors?.Where(v => v.Company.Id == company.Id)
                                .Select(x => new VendorDTO
                                {
                                    Avatar = x.Person?.Account?.Avatar ?? "default",
                                    Experience = x.Experience,
                                    Position = x.Position,
                                    FIO = x.Person?.Name ?? "Name" + " " + x.Person?.MiddleName
                                }).ToList(),
                            Categories = new Collection<CategoryDTO>
                            {
                                new CategoryDTO
                                {
                                    Icon = company.Account?.Avatar ?? "default",
                                    Name = "Category1"
                                },
                                new CategoryDTO
                                {
                                    Icon = company.Account?.Avatar ?? "default",
                                    Name = "Category2"
                                },
                                new CategoryDTO
                                {
                                    Icon = company.Account?.Avatar ?? "default",
                                    Name = "Category3"
                                },
                                new CategoryDTO
                                {
                                    Icon = company.Account?.Avatar ?? "default",
                                    Name = "Category4"
                                }
                            }
                        }).ToList();

                return companiesDTO;
            }
            return null;
        }

        private async Task<object> GetCompany(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            if (company != null)
            {
                var companyDTO = new CompanyDTO
                {
                    Avatar = company.Account?.Avatar ?? "default",
                    Name = company.Name,
                    Description = company.Description,
                    FoundationDate = company.FoundationDate,
                    Rating = company.Account?.Rating ?? 0,
                    Director = company.Director?.Avatar ?? "default",
                    Location = new LocationDTO
                    {
                        Adress = company.Location?.Adress ?? "default",
                        City = company.Location?.City ?? "default",
                        Latitude = company.Location?.CoordinateX ?? 0,
                        Longitude = company.Location?.CoordinateY ?? 0
                    },
                    Reviews = reviews?.Where(p => p.To == company.Name)
                        .Select(x => new ReviewDTO
                        {
                            Avatar = x.Avatar,
                            Date = x.Date,
                            From = x.From,
                            To = x.To,
                            Grade = x.Grade,
                            Description = x.Description
                        }).ToList(),
                    Vendors = company.Vendors?.Where(v => v.Company.Id == company.Id)
                        .Select(x => new VendorDTO
                        {
                            Avatar = x.Person?.Account?.Avatar ?? "default",
                            Experience = x.Experience,
                            Position = x.Position,
                            FIO = x.Person?.Name ?? "Name" + " " + x.Person?.MiddleName
                        }).ToList(),
                    Categories = new Collection<CategoryDTO>
                    {
                        new CategoryDTO
                        {
                            Icon = company.Account?.Avatar ?? "default",
                            Name = "Category1"
                        },
                        new CategoryDTO
                        {
                            Icon = company.Account?.Avatar ?? "default",
                            Name = "Category2"
                        },
                        new CategoryDTO
                        {
                            Icon = company.Account?.Avatar ?? "default",
                            Name = "Category3"
                        },
                        new CategoryDTO
                        {
                            Icon = company.Account?.Avatar ?? "default",
                            Name = "Category4"
                        }
                    }
                };
                return companyDTO;
            }
            return null;


        }
    }
}