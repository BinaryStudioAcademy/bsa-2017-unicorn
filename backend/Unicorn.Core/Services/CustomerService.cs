using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Services.Helpers;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookservice;

        public CustomerService(IUnitOfWork unitOfWork, IBookService bookservice)
        {
            _unitOfWork = unitOfWork;
            _bookservice = bookservice;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Customer, CustomerDTO>());
            return Mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(await _unitOfWork.CustomerRepository.GetAllAsync());
        }

        public async Task<CustomerDTO> GetById(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(customer.Person.Id);

            var books = await _unitOfWork.BookRepository.GetAllAsync();
            var sortedBooks = books.Where(x => x.Customer.Id == id);

            var customerDto = new CustomerDTO()
            {
                Id = customer.Id,
                Person = new PersonDTO()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Surname = person.Surname,
                    MiddleName = person.MiddleName,
                    Gender = person.Gender,
                    Phone = person.Phone
                },
                Books = Mapper.Map<List<BookDTO>>(sortedBooks)
            };
            return customerDto;
        }

        public async Task CreateAsync(CustomerRegisterDTO customerDto)
        {
            var account = new Account();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)AccountRoles.Customer);
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var customer = new Customer();
            var person = new Person();

            account.Role = role;
            account.DateCreated = DateTime.Now;
            account.Email = customerDto.Email;

            socialAccount.Provider = customerDto.Provider;
            socialAccount.Uid = customerDto.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);
            account.SocialAccounts = socialAccounts;

            person.Birthday = customerDto.Birthday;
            person.Phone = customerDto.Phone;
            person.Name = customerDto.FirstName;
            person.MiddleName = customerDto.MiddleName;
            person.Surname = customerDto.LastName;
            person.Account = account;
            person.Location = new Location();

            customer.Person = person;
            customer.Books = new List<Book>();

            _unitOfWork.CustomerRepository.Create(customer);
            await _unitOfWork.SaveAsync();
        }
    }
}
