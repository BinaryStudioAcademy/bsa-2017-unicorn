using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Vendor;
using Unicorn.Shared.DTOs.Book;
using System.Data.Entity;
using System;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILocationService _locationService;

        public BookService(IUnitOfWork unitOfWork, ILocationService location)
        {
            _unitOfWork = unitOfWork;
            _locationService = location;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            List<BookDTO> datareturn = new List<BookDTO>();
            foreach (var book in books)
            {
                var bookDto = new BookDTO()
                {
                    Id = book.Id,
                    Date = book.Date,
                    Status = book.Status,
                    Description = book.Description,
                    Work = new WorkDTO()
                    {
                        Id = book.Work.Id,
                        Name = book.Work.Name,
                        Description = book.Work.Description,
                        Subcategory = book.Work.Subcategory.Name,
                        SubcategoryId = book.Work.Subcategory.Id
                    },
                    Customer = new CustomerDTO()
                    {
                        Id = book.Customer.Id,
                        Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, Surname = book.Customer.Person.Surname, Phone = book.Customer.Person.Phone }
                    }
                };

                if (book.Location != null)
                {
                    bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
                }

                if (book.Vendor != null)
                {
                    bookDto.Vendor = new VendorDTO()
                    {
                        Id = book.Vendor.Id,
                        Experience = book.Vendor.Experience
                    };
                }
                if (book.Company != null)
                {
                    bookDto.Company = new CompanyDTO()
                    {
                        Id = book.Company.Id,
                        Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated, Rating = book.Company.Account.Rating },
                        Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                    };
                }
                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetByIdAsync(long id)
        {
            //var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var book = await _unitOfWork.BookRepository.Query
                .Include(b => b.Location)
                .Include(b => b.Vendor)
                .Include(b => b.Vendor.Person)
                .Include(b => b.Work)
                .Include(b => b.Work.Subcategory)
                .Include(b => b.Customer)
                .Include(b => b.Customer.Person)
                .Include(b => b.Company)
                .SingleAsync(b => b.Id == id);
            var bookDto = new BookDTO()
            {
                Id = book.Id,
                Date = book.Date,
                Status = book.Status,
                Description = book.Description,
                Work = new WorkDTO()
                {
                    Id = book.Work.Id,
                    Name = book.Work.Name,
                    Description = book.Work.Description,
                    Subcategory = book.Work.Subcategory.Name,
                    SubcategoryId = book.Work.Subcategory.Id
                },
                Customer = new CustomerDTO()
                {
                    Id = book.Customer.Id,
                    Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, Surname = book.Customer.Person.Surname, Phone = book.Customer.Person.Phone }
                }
            };

            if (book.Location != null)
            {
                bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
            }

            if (book.Vendor != null)
            {
                bookDto.Vendor = new VendorDTO()
                {
                    Id = book.Vendor.Id,
                    Experience = book.Vendor.Experience
                };
            }
            if (book.Company != null)
            {
                bookDto.Company = new CompanyDTO()
                {
                    Id = book.Company.Id,
                    Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated, Rating = book.Company.Account.Rating },
                    Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                };
            }
            return bookDto;
        }

        public async Task Create(BookOrderDTO book)
        {
            Work work = await _unitOfWork.WorkRepository.GetByIdAsync(book.WorkId);
            Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(book.CustomerId);
            Location location = await _unitOfWork.LocationRepository.GetByIdAsync(book.Adress.Id); // TODO: if not exists or changed by user?

            Company company = null;
            Vendor vendor = null;

            if (book.Profile.ToLower() == "company")
            {
                company = await _unitOfWork.CompanyRepository.GetByIdAsync(book.ProfileId);
            }
            else
            {
                vendor = await _unitOfWork.VendorRepository.GetByIdAsync(book.ProfileId);
            }

            Book _book = new Book()
            {
                IsDeleted = false,
                Company = company,
                Customer = customer,
                Date = book.Date,
                Description = book.Description,
                Location = location,
                Status  = BookStatus.Accepted,
                Vendor = vendor,
                Work = work
            };

            _unitOfWork.BookRepository.Create(_book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<VendorBookDTO>> GetVendorOrdersAsync(long vendorId)
        {
            return await _unitOfWork.BookRepository.Query
                .Include(b => b.Vendor)
                .Include(b => b.Work)
                .Include(b => b.Work.Subcategory)
                .Include(b => b.Work.Subcategory.Category)
                .Include(b => b.Location)
                .Include(b => b.Customer)
                .Include(b => b.Customer.Person)
                .Where(b => b.Vendor != null)
                .Where(b => b.Vendor.Id == vendorId)
                .Select(b => new VendorBookDTO()
                {
                    Id = b.Id,
                    Customer = b.Customer.Person.Name + " " + b.Customer.Person.Surname,
                    CustomerId = b.Customer.Id,
                    Date = b.Date,
                    Description = b.Description,
                    Location = new LocationDTO()
                    {
                        Id = b.Location.Id,
                        Adress = b.Location.Adress,
                        City = b.Location.City,
                        Latitude = b.Location.Latitude,
                        Longitude = b.Location.Longitude
                    },
                    Status = b.Status,
                    Work = new WorkDTO()
                    {
                        Id = b.Work.Id,
                        Name = b.Work.Name,
                        Description = b.Work.Description,
                        Category = b.Work.Subcategory.Category.Name,
                        CategoryId = b.Work.Subcategory.Category.Id,
                        Subcategory = b.Work.Subcategory.Name,
                        SubcategoryId = b.Work.Subcategory.Id
                    }
                }).ToListAsync();

        }

        public async Task Update(BookDTO bookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);
            book.Status = bookDto.Status;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();
        }
    }
}
