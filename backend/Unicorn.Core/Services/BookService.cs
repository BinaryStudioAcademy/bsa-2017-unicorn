using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;
using System.Linq;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILocationService _location;

        public BookService(IUnitOfWork unitOfWork, ILocationService location)
        {
            _unitOfWork = unitOfWork;
            _location = location;
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
                        Subcategory = new SubcategoryDTO() { Id = book.Work.Subcategory.Id, Category = new CategoryDTO() { Id = book.Work.Subcategory.Category.Id, Name = book.Work.Subcategory.Category.Name } }
                    },
                    Customer = new CustomerDTO()
                    {
                        Id = book.Customer.Id,
                        Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, SurnameName = book.Customer.Person.SurnameName, Phone = book.Customer.Person.Phone }
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
                        Person = new PersonDTO()
                        {
                            Id = book.Vendor.Person.Id,
                            Account = new AccountDTO()
                            {
                                Id = book.Vendor.Person.Account.Id,
                                Rating = book.Vendor.Person.Account.Rating,
                                Avatar = book.Vendor.Person.Account.Avatar
                            },
                            Name = book.Vendor.Person.Name,
                            SurnameName = book.Vendor.Person.SurnameName,
                            Phone = book.Vendor.Person.Phone
                        },
                        Experience = book.Vendor.Experience
                    };
                }
                if (book.Company != null)
                {
                    bookDto.Company = new CompanyDTO()
                    {
                        Id = book.Company.Id,
                        Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated, Rating = book.Company.Account.Rating },
                        Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, SurnameName = x.Person.SurnameName } }).ToList()
                    };
                }
                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetById(long id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
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
                    Subcategory = new SubcategoryDTO() { Id = book.Work.Subcategory.Id, Category = new CategoryDTO() { Id = book.Work.Subcategory.Category.Id, Name = book.Work.Subcategory.Category.Name } }
                },
                Customer = new CustomerDTO()
                {
                    Id = book.Customer.Id,
                    Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, SurnameName = book.Customer.Person.SurnameName, Phone = book.Customer.Person.Phone }
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
                    Person = new PersonDTO()
                    {
                        Id = book.Vendor.Person.Id,
                        Account = new AccountDTO()
                        {
                            Id = book.Vendor.Person.Account.Id,
                            Rating = book.Vendor.Person.Account.Rating,
                            Avatar = book.Vendor.Person.Account.Avatar
                        },
                        Name = book.Vendor.Person.Name,
                        SurnameName = book.Vendor.Person.SurnameName,
                        Phone = book.Vendor.Person.Phone
                    },
                    Experience = book.Vendor.Experience
                };
            }
            if (book.Company != null)
            {
                bookDto.Company = new CompanyDTO()
                {
                    Id = book.Company.Id,
                    Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated, Rating = book.Company.Account.Rating },
                    Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, SurnameName = x.Person.SurnameName } }).ToList()
                };
            }
            return bookDto;
        }
    }
}
