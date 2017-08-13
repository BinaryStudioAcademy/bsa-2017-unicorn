using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;

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
                var work = await _unitOfWork.WorkRepository.GetByIdAsync(book.Id);
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(book.Work.Subcategory.Id);
                var bookDto = new BookDTO()
                {
                    Id = book.Id,
                    Date = book.Date,
                    Status = book.Status,
                    Description = book.Description,
                    Location = await _location.GetByIdAsync(book.Id),
                    Work = new WorkDTO()
                    {
                        Id = work.Id,
                        Name = work.Name,
                        Description = work.Description,
                        Subcategory = new SubcategoryDTO()
                        {
                            Id = subcategory.Id,
                            Name = subcategory.Name,
                            Description = subcategory.Description
                        }
                    }
                };

                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetById(long id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(book.Work.Id);
            var customer = await _unitOfWork.CategoryRepository.GetByIdAsync(book.Customer.Id);
            var personcust = await _unitOfWork.PersonRepository.GetByIdAsync(book.Customer.Person.Id);
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(book.Vendor.Id);
            var vendorCompany = await _unitOfWork.CompanyRepository.GetByIdAsync(book.Vendor.Company.Id);
            var vendorComAcc = await _unitOfWork.AccountRepository.GetByIdAsync(vendorCompany.Account.Id);
            var vendorPerson = await _unitOfWork.PersonRepository.GetByIdAsync(vendor.Person.Id);
            var vendorComPerAcc = await _unitOfWork.AccountRepository.GetByIdAsync(vendor.Person.Account.Id);
            var bookDto = new BookDTO()
            {
                Id = book.Id,
                Date = book.Date,
                Status = book.Status,
                Description = book.Description,
                Location = await _location.GetByIdAsync(id),
                Work = new WorkDTO()
                {
                    Id = work.Id,
                    Name = work.Name,
                    Description = work.Description
                },
                Customer = new CustomerDTO()
                {
                    Id = customer.Id,
                    Person = new PersonDTO() { Id = personcust.Id, Name = personcust.Name, SurnameName = personcust.SurnameName, Phone = personcust.Phone }           
                },
                Vendor = new VendorDTO()
                {
                    Id = vendor.Id,
                    Person = new PersonDTO() { Id = vendorPerson.Id, Account = new AccountDTO() { Id = vendorComPerAcc.Id, Rating = vendorComPerAcc.Rating }, Name = vendorPerson.Name, SurnameName = vendor.Person.SurnameName, Phone = vendor.Person.Phone },
                    Company = new CompanyDTO() { Id = vendor.Id, Account = new AccountDTO() { Id = vendorComAcc.Id, DateCreated = vendorComAcc.DateCreated, Rating = vendorComAcc.Rating } },
                    Experience = vendor.Experience             
                }
            };
            return bookDto;
        }

    }
}
