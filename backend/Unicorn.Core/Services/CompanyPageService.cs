﻿﻿using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Linq;
 using System.Runtime.InteropServices;
 using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Review;
using Unicorn.Shared.DTOs.Notification;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Core.Services
{
    public class CompanyPageService:ICompanyPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewService _reviewService;
        private readonly INotificationService _notifyService;

        #region PublicMethods

        public CompanyPageService(IUnitOfWork unitOfWork, IReviewService reviewService, INotificationService notifyService)
        {
            _unitOfWork = unitOfWork;
            _reviewService = reviewService;
            _notifyService = notifyService;
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

        public async Task SaveCompanyDetails(CompanyDetails companyDetailsDTO)
        {
            await SaveCompanyDetailsMethod(companyDetailsDTO);
        }



        public async Task<CompanyReviews> GetCompanyReviews(long id)
        {
            return await GetCompanyReviewsMethod(id);
        }

        public async Task AddCompanyReviews(CompanyReviews companyReviewsDTO)
        {
            await AddCompanyReviewsMethod(companyReviewsDTO);
        }



        public async Task<CompanyVendors> GetCompanyVendors(long id)
        {
            return await GetCompanyVendorsMethod(id);
        }

        public async Task AddCompanyVendors(CompanyVendors companyVendorsDTO)
        {
            await AddCompanyVendorsMethod(companyVendorsDTO);
        }

        public async Task DeleteCompanyVendor(long companyId, long vendorId)
        {
            await DeleteCompanyVendorMethod(companyId, vendorId);
        }



        public async Task<CompanyContacts> GetCompanyContacts(long id)
        {
            return await GetCompanyContactsMethod(id);
        }

        public async Task SaveCompanyContact(ContactShortDTO companyContactDTO)
        {
            await SaveCompanyContactMethod(companyContactDTO);
        }

        public async Task AddCompanyContact(long companyId, ContactShortDTO companyContactDTO)
        {
            await AddCompanyContactMethod(companyId, companyContactDTO);
        }

        public async Task DeleteCompanyContact(long companyId, long contactId)
        {
            await DeleteCompanyContactMethod(companyId, contactId);
        }
        
        

        public async Task<CompanyWorks> GetCompanyWorks(long id)
        {
            return await GetCompanyWorksMethod(id);
        }

        public async Task SaveCompanyWork(CompanyWork companyWorkDTO)
        {
            await SaveCompanyWorkMethod(companyWorkDTO);
        }

        public async Task AddCompanyWork(long companyId, CompanyWork companyWorkDTO)
        {
            await AddCompanyWorkMethod(companyId, companyWorkDTO);
        }

        public async Task DeleteCompanyWork(long companyId, long workId)
        {
            await DeleteCompanyWorkMethod(companyId, workId);
        }



        public async Task<CompanyBooks> GetCompanyBooks(long id)
        {
            return await GetCompanyBooksMethod(id);
        }

        public async Task SaveCompanyBook(CompanyBook companyBookDTO)
        {
            await SaveCompanyBookMethod(companyBookDTO);
        }


        #endregion

        #region PrivateMethods

        private async Task<ICollection<ShortCompanyDTO>> GetAllCompaniesMethod()
        {
            var companies = await _unitOfWork.CompanyRepository.Query.
                  Include(c => c.Account.Location).ToListAsync();
            if (companies.Any())
            {
                var companiesDTO = companies.Select(
                    company =>
                        new ShortCompanyDTO
                        {
                            Id = company.Id,
                            AccountId = company.Account.Id,
                            Avatar = company.Account.Avatar,
                            Name = company.Name,
                            Location = new LocationDTO
                            {
                                Id = company.Account.Location.Id,
                                City = company.Account.Location.City,
                                Adress = company.Account.Location.Adress,
                                Longitude = company.Account.Location.Longitude,
                                Latitude = company.Account.Location.Latitude
                            }
                        }).ToList();
                return companiesDTO;
            }
            return null;
        }



        private async Task<ShortCompanyDTO> GetCompanyShortMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.Query.
                Include(c => c.Account.Location).SingleAsync(c => c.Id == id);

            if (company != null)
            {
                var companyDTO = new ShortCompanyDTO
                        {
                            Id = company.Id,
                            AccountId = company.Account.Id,
                            Avatar = company.Account.Avatar,
                            Name = company.Name,
                            Location = new LocationDTO
                            {
                                Id = company.Account.Location.Id,
                                City = company.Account.Location.City,
                                Adress = company.Account.Location.Adress,
                                Longitude = company.Account.Location.Longitude,
                                Latitude = company.Account.Location.Latitude
                            }
                };
                return companyDTO;
            }
            return null;
        }

        private async Task<CompanyDetails> GetCompanyDetailsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.Query.
                Include(c => c.Account.Location).SingleAsync(c => c.Id == id);
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
                    City = company.Account.Location.City,
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
                        Id = company.Account.Location.Id,
                        Adress = company.Account.Location?.Adress ?? "default",
                        City = company.Account.Location?.City ?? "default",
                        Latitude = company.Account.Location?.Latitude ?? 0,
                        Longitude = company.Account.Location?.Longitude ?? 0
                    },
                };

                return companyDetails;
            }

            return null;
        }

        private async Task SaveCompanyDetailsMethod(CompanyDetails companyDetailsDTO)
        {
            var company = await _unitOfWork.CompanyRepository.Query.
                Include(c => c.Account.Location).SingleAsync(c => c.Id == companyDetailsDTO.Id);

            if (company != null)
            {
                company.Account.Avatar = companyDetailsDTO.Avatar;
                company.Name = companyDetailsDTO.Name;
                company.Director = companyDetailsDTO.Director;
                company.Description = companyDetailsDTO.Description;
                company.FoundationDate = companyDetailsDTO.FoundationDate;
                company.Account.Location = new Location
                {
                    Adress = companyDetailsDTO.Location.Adress,
                    City = companyDetailsDTO.Location.City,
                    Longitude = companyDetailsDTO.Location.Longitude,
                    Latitude = companyDetailsDTO.Location.Latitude
                };
                

                _unitOfWork.CompanyRepository.Update(company);
                await _unitOfWork.SaveAsync();
            }
        }



        private async Task<CompanyReviews> GetCompanyReviewsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var reviews = await _reviewService.GetByReceiverIdAsync(company.Account.Id);

            if (reviews.Any())
            {
                var companyReviews = new CompanyReviews()
                {
                    Reviews = reviews.OrderByDescending(x => x.Date).ToList()
                };

                return companyReviews;
            }

            return new CompanyReviews
            {
                Reviews = Enumerable.Empty<ReviewDTO>().ToList()
            };
        }

        private int GetRatingByBookId(long id)
        {
            var rating = _unitOfWork.RatingRepository
                .Query
                .Include(r => r.Book)
                .Where(r => r.Book != null)
                .FirstOrDefault(r => r.Book.Id == id);

            return rating == null ? 0 : rating.Grade;
        }

        private Task AddCompanyReviewsMethod(CompanyReviews companyReviewsDTO)
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

        public async Task<List<VendorDTO>> GetCompanyVendorsWithWorks(long companyId)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var vendors = await _unitOfWork.VendorRepository
                .Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .Include(v => v.Works)
                .Include(v => v.Company)
                .Where(v => v.Company != null && v.Company.Id == companyId)
                .ToListAsync();

            return vendors.Select(v => new VendorDTO
            {
                Id = v.Id,
                FIO = $"{v.Person.Name} {v.Person.Surname}",
                Avatar = v.Person.Account.Avatar,
                Works = v.Works.Select(w => new WorkDTO
                {
                    Id = w.Id,
                    Name = w.Name
                }).ToList()
            }).ToList();
        }

        private async Task AddCompanyVendorsMethod(CompanyVendors companyVendorsDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyVendorsDTO.Id);

            if (company != null)
            {
                foreach (var companyDtoVendor in companyVendorsDTO.Vendors)
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

        private async Task DeleteCompanyVendorMethod(long companyId, long vendorId)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(vendorId);

            if (company != null && vendor != null)
            {
                company.Vendors.Remove(vendor);
                await _unitOfWork.SaveAsync();

                var notification = new NotificationDTO()
                {
                    Title = $"New info from {company.Name}",
                    Description = $"You are dismissed from {company.Name} team.",
                    SourceItemId = company.Id,
                    Time = DateTime.Now,
                    Type = NotificationType.OfferNotification
                };

                var receiverId = vendor.Person.Account.Id;
                await _notifyService.CreateAsync(receiverId, notification);
            }
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
                Rating = CalculateAverageRating(x.Person.Account.Id)
            }).ToList();
        }
        private double CalculateAverageRating(long receiverId)
        {
            var select = _unitOfWork.RatingRepository.Query
                .Where(p => p.Reciever.Id == receiverId).Select(z => z.Grade);
            return select.Any() ? select.Average() : 0;

        }
        


        private async Task<CompanyContacts> GetCompanyContactsMethod(long id)
        {
            var company = await _unitOfWork.CompanyRepository.Query.
                Include(c => c.Account.Location).SingleAsync(c=>c.Id==id);

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
                        Id = company.Account.Location.Id,
                        Adress = company.Account.Location?.Adress ?? "default",
                        City = company.Account.Location?.City ?? "default",
                        Latitude = company.Account.Location?.Latitude ?? 0,
                        Longitude = company.Account.Location?.Longitude ?? 0
                    }
                };

                return companyContacts;
            }

            return null;
        }

        private async Task SaveCompanyContactMethod(ContactShortDTO companyContactDTO)
        {
            var contact = await _unitOfWork.ContactRepository.GetByIdAsync(companyContactDTO.Id);

            if (contact != null)
            {
                contact.Value = companyContactDTO.Value;
                _unitOfWork.ContactRepository.Update(contact);
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task AddCompanyContactMethod(long companyId, ContactShortDTO companyContactDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var provider = await _unitOfWork.ContactProviderRepository.GetByIdAsync(companyContactDTO.ProviderId);

            if (company != null)
            {
                _unitOfWork.ContactRepository.Create(new Contact
                {
                    Account = company.Account,
                    Provider = provider,
                    Value = companyContactDTO.Value,
                    IsDeleted = false
                });

                await _unitOfWork.SaveAsync();
            }
        }

        private async Task DeleteCompanyContactMethod(long companyId, long contactId)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var contact = await _unitOfWork.ContactRepository.GetByIdAsync(contactId);

            if (company != null && contact != null)
            {
                company.Account.Contacts.Remove(contact);
                _unitOfWork.ContactRepository.Delete(contactId);
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
                        Icon = string.IsNullOrEmpty(z.Icon) ? z.Subcategory.Category.Icon : z.Icon,
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

        private async Task SaveCompanyWorkMethod(CompanyWork companyWorkDTO)
        {
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(companyWorkDTO.Id);
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(companyWorkDTO.Subcategory.Id);

            if (work != null && subcategory != null)
            {
                work.Name = companyWorkDTO.Name;
                work.Description = companyWorkDTO.Description;
                work.Subcategory = subcategory;
                work.Icon = companyWorkDTO.Icon;
                _unitOfWork.WorkRepository.Update(work);
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task AddCompanyWorkMethod(long companyId, CompanyWork companyWorkDTO)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(companyWorkDTO.Subcategory.Id);

            if (company != null && subcategory != null)
            {
                _unitOfWork.WorkRepository.Create(new Work
                {
                   Company = company,
                   Description = companyWorkDTO.Description,
                   Icon = companyWorkDTO.Icon,
                   Name = companyWorkDTO.Name,
                   Subcategory = subcategory,
                   IsDeleted = false
                });

                await _unitOfWork.SaveAsync();
            }
        }

        private async Task DeleteCompanyWorkMethod(long companyId, long workId)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(workId);

            if (company != null && work != null)
            {
                company.Works.Remove(work);
                _unitOfWork.WorkRepository.Delete(workId);
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

        private async Task SaveCompanyBookMethod(CompanyBook companyBookDTO)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(companyBookDTO.Id);

            if (book != null)
            {
                book.Status = companyBookDTO.Status;
                _unitOfWork.BookRepository.Update(book);
                await _unitOfWork.SaveAsync();
            }
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
            var companies = await _unitOfWork.CompanyRepository.Query.Include(c => c.Account.Location).ToListAsync();
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            if (companies.Any())
            {
                var companiesDetail = companies.Select(
                    company =>
                        new CompanyDetails
                        {
                            Id = company.Id,
                            Avatar = company.Account?.Avatar ?? "default",
                            Name = company.Name,
                            Description = company.Description,
                            Rating = CalculateCompanyRating(company.Account.Id),
                            FoundationDate = company.FoundationDate,
                            Director = company.Director,
                            City = company.Account.Location.City,
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
                                Id = company.Account.Location.Id,
                                Adress = company.Account.Location?.Adress ?? "default",
                                City = company.Account.Location?.City ?? "default",
                                Latitude = company.Account.Location?.Latitude ?? 0,
                                Longitude = company.Account.Location?.Longitude ?? 0
                            },
                        }).ToList();
                return companiesDetail;
            }
            return null;           
        }

        private double CalculateCompanyRating(long recieverId)
        {
            var ratings = _unitOfWork.RatingRepository.Query
                  .Include(y => y.Reciever)
                  .Where(p => p.Reciever.Id == recieverId)
                  .ToList();

            return ratings.Any() ? ratings.Average(z => z.Grade) : 0;
        }

        #endregion
    }
}