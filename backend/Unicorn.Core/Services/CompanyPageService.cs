﻿using System.Collections.Generic;
using System.Data.Entity;
﻿using System;
 using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Core.Services
{
    public class CompanyPageService:ICompanyPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRatingService _ratingService;

        #region PublicMethods

        public CompanyPageService(IUnitOfWork unitOfWork, IRatingService ratingService)
        {
            _unitOfWork = unitOfWork;
            _ratingService = ratingService;
        }

        public async Task<ICollection<ShortCompanyDTO>> GetAllCompanies()
        {
            return await GetAllCompaniesMethod();
        }

        public async Task<ShortCompanyDTO> GetCompanyShort(long id)
        {
            return await GetCompanyShortMethod(id);
        }

        public async Task<CompanyDetails> GetCompanyDetails(long id)
        {
            return await GetCompanyDetailsMethod(id);
        }

        public async Task SaveCompanyDetails(CompanyDetails companyDTO)
        {
            await SaveCompanyDetailsMethod(companyDTO);
        }

        public async Task<CompanyReviews> GetCompanyReviews(long id)
        {
            return await GetCompanyReviewsMethod(id);
        }

        public async Task SaveCompanyReviews(CompanyReviews companyDTO)
        {
            await SaveCompanyReviewsMethod(companyDTO);
        }

        public async Task<CompanyVendors> GetCompanyVendors(long id)
        {
            return await GetCompanyVendorsMethod(id);
        }

        public async Task SaveCompanyVendors(CompanyVendors companyDTO)
        {
            await SaveCompanyVendorsMethod(companyDTO);
        }

        public async Task<CompanyContacts> GetCompanyContacts(long id)
        {
            return await GetCompanyContactsMethod(id);
        }

        public async Task SaveCompanyContacts(CompanyContacts companyDTO)
        {
            await SaveCompanyContactsMethod(companyDTO);
        }

        public async Task<CompanyWorks> GetCompanyWorks(long id)
        {
            return await GetCompanyWorksMethod(id);
        }

        public async Task SaveCompanyWorks(CompanyWorks companyDTO)
        {
            await SaveCompanyWorksMethod(companyDTO);
        }

        public async Task<CompanyBooks> GetCompanyBooks(long id)
        {
            return await GetCompanyBooksMethod(id);
        }

        public async Task SaveCompanyBooks(CompanyBooks companyDTO)
        {
            await SaveCompanyBooksMethod(companyDTO);
        }

        public async Task<ICollection<CompanyDetails>> GetSearchCompanies(string category, string subcategory, int? date)
        {
            return await GetSearchCompaniesMethod(category, subcategory, date);
        }

        #endregion

        #region PrivateMethods

        private async Task<ICollection<ShortCompanyDTO>> GetAllCompaniesMethod()
        {
            var companies = await _unitOfWork.CompanyRepository.GetAllAsync();

            if (companies.Any())
            {
                var companiesDTO = companies.Select(
                    company =>
                        new ShortCompanyDTO
                        {
                            Id = company.Id,
                            Avatar = company.Account.Avatar,
                            Name = company.Name,
                            Location = new LocationDTO
                            {
                                Id = company.Location.Id,
                                City = company.Location.City,
                                Adress = company.Location.Adress,
                                Longitude = company.Location.Longitude,
                                Latitude = company.Location.Latitude
                            }
                        }).ToList();
                return companiesDTO;
            }
            return null;
        }

        private async Task<ShortCompanyDTO> GetCompanyShortMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);

            if (company != null)
            {
                var companyDTO = new ShortCompanyDTO
                        {
                            Id = company.Id,
                            Avatar = company.Account.Avatar,
                            Name = company.Name,
                            Location = new LocationDTO
                            {
                                Id = company.Location.Id,
                                City = company.Location.City,
                                Adress = company.Location.Adress,
                                Longitude = company.Location.Longitude,
                                Latitude = company.Location.Latitude
                            }
                };
                return companyDTO;
            }
            return null;
        }

        private async Task<CompanyDetails> GetCompanyDetailsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            if (company != null)
            {
                var companyDetails = new CompanyDetails
                {
                    Id = company.Id,
                    Avatar = company.Account?.Avatar ?? "default",
                    Name = company.Name,
                    Description = company.Description,
                    FoundationDate = company.FoundationDate,
                    Director = company.Director,
                    City = company.Location.City,
                    ReviewsCount = reviews.Count(p => p.ToAccountId == company.Account.Id),
                    Works = company.Works.Select(z => new CompanyWork()
                    {
                        Id = z.Id,
                        Description = z.Description,
                        Name = z.Name,
                        
                        Subcategory = new CompanySubcategory
                        {
                            Id = z.Subcategory.Id,
                            Name = z.Subcategory.Name,
                            Description = z.Subcategory.Description,
                            Category = new CompanyCategory
                            {
                                Id = z.Subcategory.Category.Id,
                                Description = z.Subcategory.Category.Description,
                                Icon = z.Subcategory.Category.Icon,
                                Name = z.Subcategory.Category.Name,
                            }
                        }
                    }).ToList(),
                    Location = new LocationDTO
                    {
                        Id = company.Location.Id,
                        Adress = company.Location?.Adress ?? "default",
                        City = company.Location?.City ?? "default",
                        Latitude = company.Location?.Latitude ?? 0,
                        Longitude = company.Location?.Longitude ?? 0
                    },
                };

                return companyDetails;
            }

            return null;
        }

        private async Task SaveCompanyDetailsMethod(CompanyDetails companyDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyDTO.Id);

            if (company != null)
            {
                company.Account.Avatar = companyDTO.Avatar;
                company.Name = companyDTO.Name;
                company.Director = companyDTO.Director;
                company.Description = companyDTO.Description;
                company.FoundationDate = companyDTO.FoundationDate;
                company.Location = new Location
                {
                    Adress = companyDTO.Location.Adress,
                    City = companyDTO.Location.City,
                    Longitude = companyDTO.Location.Longitude,
                    Latitude = companyDTO.Location.Latitude
                };
                

                _unitOfWork.CompanyRepository.Update(company);
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<CompanyReviews> GetCompanyReviewsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            if (reviews.Any())
            {
                var companyReviews = new CompanyReviews()
                {
                    Reviews = reviews.Where(p => p.ToAccountId == company.Account.Id)
                        .Select(x => new ReviewDTO
                        {
                            Id = x.Id,
                            Avatar = x.Avatar,
                            Date = x.Date,
                            From = x.From,
                            FromAccountId = x.FromAccountId,
                            To = x.To,
                            ToAccountId = x.ToAccountId,
                            Description = x.Description,
                            BookId = x.BookId
                        }).ToList()
                };

                return companyReviews;
            }

            return null;
        }

        private Task SaveCompanyReviewsMethod(CompanyReviews companyDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<CompanyVendors> GetCompanyVendorsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var allVendors = await _unitOfWork.VendorRepository.GetAllAsync();
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            

            if (company != null )
            {
                var companyVendors = new CompanyVendors
                {
                    Id = company.Id,
                    Vendors = CreateCompanyVendor(company.Vendors?.Where(v => v.Company.Id == company.Id), reviews, company),
                    AllVendors = CreateCompanyVendor( allVendors.Where(x => x.Company == null && x.Company?.Id != company.Id), reviews, company)
                           
                };

                return companyVendors;
            }

            return null;
        }
        private IList<CompanyVendor> CreateCompanyVendor(IEnumerable<Vendor> vendors, IEnumerable<Review> reviews, Company company)
        {

            return vendors.Select(x => new CompanyVendor
            {
                Id = x.Id,
                Avatar = x.Person?.Account?.Avatar ?? "default",
                Experience = x.Experience,
                Position = x.Position,
                FIO = x.Person?.Name ?? "Name" + " " + x.Person?.MiddleName,
                Reviews = reviews.Count(p => p.ToAccountId == company.Account.Id),
                Rating = CalculateAverageRating(x.Id)
            }).ToList();
        }
        private double CalculateAverageRating(long receiverId)
        {
            var select = _unitOfWork.RatingRepository.Query
                .Where(p => p.Reciever.Id == receiverId).Select(z => z.Grade);
            return select.Any() ? select.Average() : 0;

        }

        private async Task SaveCompanyVendorsMethod(CompanyVendors companyDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyDTO.Id);

            if (company != null)
            {
                company.Vendors.Clear();
                foreach (var companyDtoVendor in companyDTO.Vendors)
                {
                    var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(companyDtoVendor.Id);
                    if (vendor != null)
                    {
                        company.Vendors.Add(vendor);
                    }
                }
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<CompanyContacts> GetCompanyContactsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);

            if (company != null)
            {
                var companyContacts = new CompanyContacts
                {
                    Id = company.Id,
                    Title = company.Name,
                    Contacts = company.Account?.Contacts.Select(x => new ContactShortDTO
                    {
                        Id = x.Id,
                        Provider = x.Provider.Name,
                        Type = x.Provider.Type,
                        Value = x.Value
                    }).ToList(),
                    Location = new LocationDTO
                    {
                        Id = company.Location.Id,
                        Adress = company.Location?.Adress ?? "default",
                        City = company.Location?.City ?? "default",
                        Latitude = company.Location?.Latitude ?? 0,
                        Longitude = company.Location?.Longitude ?? 0
                    }
                };

                return companyContacts;
            }

            return null;
        }

        private async Task SaveCompanyContactsMethod(CompanyContacts companyDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyDTO.Id);

            if (company != null)
            {
                company.Account.Contacts.Clear();
                foreach (var companyDtoContact in companyDTO.Contacts)
                {
                    var contact = await _unitOfWork.ContactRepository.GetByIdAsync(companyDtoContact.Id);
                    if (contact != null)
                    {
                        contact.Provider.Name = companyDtoContact.Provider;
                        contact.Value = companyDtoContact.Value;
                        contact.Provider.Type = companyDtoContact.Type;

                        company.Account.Contacts.Add(contact);
                    }
                }
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<CompanyWorks> GetCompanyWorksMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var allCategories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (company != null)
            {
                var companyWorks = new CompanyWorks()
                {
                    Id = company.Id,
                    Works = company.Works.Select(z => new CompanyWork()
                    {
                        Id = z.Id,
                        Description = z.Description,
                        Name = z.Name,

                        Subcategory = new CompanySubcategory
                        {
                            Id = z.Subcategory.Id,
                            Name = z.Subcategory.Name,
                            Description = z.Subcategory.Description,
                            Category = new CompanyCategory
                            {
                                Id = z.Subcategory.Category.Id,
                                Description = z.Subcategory.Category.Description,
                                Icon = z.Subcategory.Category.Icon,
                                Name = z.Subcategory.Category.Name,
                            }
                        }
                    }).ToList(),
                    AllCategories = allCategories.Select(x => new CompanyCategory()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Icon = x.Icon,
                        Name = x.Name,
                        Subcategories = x.Subcategories.Select(y => new CompanySubcategory()
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Description = y.Description,
                            Works = y.Works.Select(z => new CompanyWork
                            {
                                Id = z.Id,
                                Description = z.Description,
                                Name = z.Name
                            }).ToList()
                        }).ToList(),
                    }).ToList(),
                };

                return companyWorks;
            }
            return null;
        }

        private async Task SaveCompanyWorksMethod(CompanyWorks companyDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyDTO.Id);

            if (company != null)
            {
                company.Works.Clear();
                foreach (var companyDtoWork in companyDTO.Works)
                {
                    var work = await _unitOfWork.WorkRepository.GetByIdAsync(companyDtoWork.Id);
                    if (work != null)
                    {
                        work.Description = companyDtoWork.Description;
                        work.Name = companyDtoWork.Name;

                        var category =
                            await _unitOfWork.CategoryRepository.GetByIdAsync(companyDtoWork.Subcategory.Category.Id);
                        var subcategory =
                            await _unitOfWork.SubcategoryRepository.GetByIdAsync(companyDtoWork.Subcategory.Id);
                        subcategory.Category = category;
                        work.Subcategory = subcategory;
                    }
                    else
                    {
                        var category =
                            await _unitOfWork.CategoryRepository.GetByIdAsync(companyDtoWork.Subcategory.Category.Id);
                        var subcategory =
                            await _unitOfWork.SubcategoryRepository.GetByIdAsync(companyDtoWork.Subcategory.Id);
                        subcategory.Category = category;
                        work = new Work
                        {
                            Description = companyDtoWork.Description,
                            Name = companyDtoWork.Name,
                            Subcategory = subcategory
                        };
                    }
                    company.Works.Add(work);
                }
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<CompanyBooks> GetCompanyBooksMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            
            if (company != null)
            {
                var companyBooks = new CompanyBooks
                {
                    Id = company.Id,
                    Books = books.Where(x => x.Company != null && x.Company.Id == company.Id).Select(book => new CompanyBook
                    {
                        Id = book.Id,
                        Customer = book.Customer.Person.Name + " " + book.Customer.Person.MiddleName,
                        CustomerId = book.Customer.Id,
                        Date = book.Date,
                        Description = book.Description,
                        Location = new LocationDTO{
                            Id = book.Location.Id,
                            Adress = book.Location.Adress,
                            City = book.Location.City,
                            Latitude = book.Location.Latitude,
                            Longitude = book.Location.Longitude
                        },
                        Status = book.Status,
                        Work = new CompanyWork
                        {
                            Id = book.Work.Id,
                            Description = book.Work.Description,
                            Name = book.Work.Name,

                            Subcategory = new CompanySubcategory
                            {
                                Id = book.Work.Subcategory.Id,
                                Name = book.Work.Subcategory.Name,
                                Description = book.Work.Subcategory.Description,
                                Category = new CompanyCategory
                                {
                                    Id = book.Work.Subcategory.Category.Id,
                                    Description = book.Work.Subcategory.Category.Description,
                                    Icon = book.Work.Subcategory.Category.Icon,
                                    Name = book.Work.Subcategory.Category.Name,
                                }
                            }
                        }
                    }).ToList()
                };
                

                return companyBooks;
            }
            return null;
        }

        private async Task SaveCompanyBooksMethod(CompanyBooks companyDTO)
        {
            foreach (var companyDtoBook in companyDTO.Books)
            {
                var book = await _unitOfWork.BookRepository.GetByIdAsync(companyDtoBook.Id);
                book.Status = companyDtoBook.Status;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<long> GetCompanyAccountId(long id)
        {
            var company = await _unitOfWork.CompanyRepository.Query
               .Include(v => v.Account)
               .SingleAsync(x => x.Id == id);
            return company.Account.Id;
        }

        private async Task<ICollection<CompanyDetails>> GetSearchCompaniesMethod(string category, string subcategory, int? date)
        {
            var companies = await _unitOfWork.CompanyRepository.GetAllAsync();
            var details = new List<CompanyDetails>();

            foreach (var company in companies)
            {
                var detail = await GetCompanyDetailsMethod(company.Id);
                if (detail != null)
                    details.Add(detail);
            }
            return details;            
        }

        #endregion
    }
}