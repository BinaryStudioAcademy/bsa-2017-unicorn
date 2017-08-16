using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Register;

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
        
        public async Task Create(CompanyRegisterDTO companyDto)
        {  
            var account = new Account();
            var role = new Role();
            var permissions = new List<Permission>();
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var company = new Company();

            account.Role = role;
            account.Permissions = permissions;
            account.DateCreated = DateTime.Now;
            account.Email = companyDto.Email;
            account.SocialAccounts = socialAccounts;

            role.Name = "company";

            socialAccount.Provider = companyDto.Provider;
            socialAccount.Uid = companyDto.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);

            company.Staff = companyDto.Staff;
            company.Name = companyDto.Name;
            company.Description = companyDto.Description;
            company.Account = account;
            company.FoundationDate = companyDto.Foundation;
            company.Location = new Location();

            _unitOfWork.CompanyRepository.Create(company);
            await _unitOfWork.SaveAsync();
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

