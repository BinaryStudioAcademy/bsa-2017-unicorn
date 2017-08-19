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
using Unicorn.Shared.DTOs.User;

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

        public async Task<object> GetById(long id)
        {
            var result = await GetCustomerAsync(id);
            return result;
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

        public async Task UpdateCustomerAsync(UserShortDTO userDTO)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(userDTO.Id);

            if (customer != null)
            {
                customer.Person.Name = userDTO.Name;
                customer.Person.Surname = userDTO.SurName;
                customer.Person.MiddleName = userDTO.MiddleName;
                customer.Person.Account.Email = userDTO.Email;
                customer.Person.Phone = userDTO.Phone;
                customer.Person.Birthday = userDTO.Birthday;

                _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<object> GetCustomerAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                var customerDto = new UserShortDTO()
                {
                    Id = customer.Id,
                    Name = customer.Person.Name,
                    SurName = customer.Person.Surname,
                    MiddleName = customer.Person.MiddleName,
                    LocationId = customer.Person.Location.Id,
                    Birthday = customer.Person.Birthday,
                    Phone = customer.Person.Phone,
                    Avatar = customer.Person.Account.Avatar,
                    Email = customer.Person.Account.Email,
                    Books = customer.Books.Select(x => new BookShortDto()
                    {
                        address = x.Location?.Adress,
                        date = x.Date,
                        description = x.Description,
                        vendor = x?.Vendor?.Person?.Surname,
                        status = x.Status,
                        workType = x.Work.Subcategory?.Name
                    }).ToList()
                };
                return customerDto;
            }
            return null;
        }
    }
}
