using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.User;
using System.Data.Entity;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookservice;
        private readonly IHistoryService _historyService;

        public CustomerService(IUnitOfWork unitOfWork, IBookService bookservice, IHistoryService historyService)
        {
            _unitOfWork = unitOfWork;
            _bookservice = bookservice;
            _historyService = historyService;
        }

        public async Task<object> GetById(long id)
        {
            var result = await GetCustomerAsync(id);
            return result;
        }

        public async Task CreateAsync(CustomerRegisterDTO customerDto)
        {
            var account = new Account();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)RoleType.Customer);
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var customer = new Customer();
            var person = new Person();

            account.Role = role;
            account.DateCreated = DateTime.Now;
            account.Email = customerDto.Email;
            account.Avatar = customerDto.Image;
            account.DateCreated = DateTime.Now;

            socialAccount.Provider = customerDto.Provider;
            socialAccount.Uid = customerDto.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);
            account.SocialAccounts = socialAccounts;
            account.Location = new Location()
            {
                Adress = customerDto.Location.Adress,
                City = customerDto.Location.City,
                IsDeleted = false,
                Latitude = customerDto.Location.Latitude,
                Longitude = customerDto.Location.Longitude,
                PostIndex = customerDto.Location.PostIndex
            };
            person.Birthday = customerDto.Birthday;
            person.Phone = customerDto.Phone;
            person.Name = customerDto.FirstName;
            person.MiddleName = customerDto.MiddleName;
            person.Surname = customerDto.LastName;
            person.Account = account;

            customer.Person = person;
            customer.Books = new List<Book>();
            customer.History = new List<History>();
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
                customer.Person.Account.Location = new Location()
                {
                    Adress = userDTO.Location.Adress,
                    City = userDTO.Location.City,
                    IsDeleted = false,
                    Latitude = userDTO.Location.Latitude,
                    Longitude = userDTO.Location.Longitude,
                    PostIndex = userDTO.Location.PostIndex
                };
                _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();
            }
        }
        public async Task<long> GetUserAccountIdAsync(long id)
        {
            var user = await _unitOfWork.CustomerRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .SingleAsync(x => x.Id == id);
            return user.Person.Account.Id;
        }
        public async Task<object> GetCustomerAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.
                Query.Include(c => c.History.Select(h => h.Vendor.Person)).
                Include(c => c.Books.Select(h => h.Vendor.Person)).
                Include(c => c.Person.Account.Location).
                SingleAsync(c => c.Id == id);
            if (customer != null)
            {
                var customerDto = new UserShortDTO()
                {
                    Id = customer.Id,
                    AccountId = customer.Person.Account.Id,
                    Name = customer.Person.Name,
                    SurName = customer.Person.Surname,
                    MiddleName = customer.Person.MiddleName,
                    Location = new LocationDTO()
                    {
                        Adress = customer.Person.Account.Location.Adress,
                        City = customer.Person.Account.Location.City,
                        Latitude = customer.Person.Account.Location.Latitude,
                        Longitude = customer.Person.Account.Location.Longitude,
                        PostIndex = customer.Person.Account.Location.PostIndex
                    },
                    DateCreated = customer.Person.Account.DateCreated,
                    Birthday = customer.Person.Birthday,
                    Phone = customer.Person.Phone,
                    Avatar = customer.Person.Account.Avatar,
                    CroppedAvatar = customer.Person.Account.CroppedAvatar,
                    Background = customer.Person.Account.Background,
                    Email = customer.Person.Account.Email,
                    History = customer.History.Select(x => new HistoryShortDto()
                    {
                        bookDescription = x.BookDescription,
                        categoryName = x.CategoryName,
                        date = x.Date,
                        dateFinished = x.DateFinished,
                        subcategoryName = x.SubcategoryName,
                        vendor = x?.Vendor?.Person?.Name,
                        workDescription = x.WorkDescription
                    }).ToList()
                };
                return customerDto;
            }
            return null;
        }

        public async Task<UserForOrder> GetForOrderAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository
                .Query
                .Include(x => x.Person.Account.Location)
                .FirstAsync(x => x.Id == id);

            return new UserForOrder()
            {
                Location = new LocationDTO()
                {
                    Id = customer.Person.Account.Location.Id,
                    Adress = customer.Person.Account.Location.Adress,
                    City = customer.Person.Account.Location.City,
                    Latitude = customer.Person.Account.Location.Latitude,
                    Longitude = customer.Person.Account.Location.Longitude,
                    PostIndex = customer.Person.Account.Location.PostIndex
                },
                Phone = customer.Person.Phone
            };
        }
    }
}