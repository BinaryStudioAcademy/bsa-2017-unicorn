using AutoMapper;
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
using Unicorn.Shared.DTOs.Book;
using Unicorn.Shared.DTOs;
using AutoMapper;
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
            account.Location = new Location()
            {
                Adress = customerDto.Location.Adress,
                City = customerDto.Location.City,
                IsDeleted = false,
                Latitude = customerDto.Location.Latitude,
                Longitude = customerDto.Location.Longitude,
                PostIndex = customerDto.Location.PostIndex
            };

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
                customer.Person.Account.Location = new Location()
                {
                    Adress = userDTO.Location.Adress,
                    City = userDTO.Location.City,
                    IsDeleted = false,
                    Latitude = userDTO.Location.Latitude,
                    Longitude = userDTO.Location.Longitude,
                    PostIndex = userDTO.Location.PostIndex
                };
                customer.Person.Phone = userDTO.Phone;
                customer.Person.Birthday = userDTO.Birthday;

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
                Include(c=>c.Person.Account.Location).
                SingleAsync(c => c.Id == id);
            if (customer != null)
            {
                var customerDto = new UserShortDTO()
                {
                    Id = customer.Id,
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
                    Birthday = customer.Person.Birthday,
                    Phone = customer.Person.Phone,
                    Avatar = customer.Person.Account.Avatar,
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
                    }).ToList(),
                    //Books = customer.Books?.Select(x => new BookShortDto()
                    //{
                    //    Address = x.Location?.Adress,
                    //    Date = x.Date,
                    //    Description = x.Description,
                    //    Vendor = x?.Vendor?.Person?.Name,
                    //    Status = x.Status,
                    //    WorkType = x.Work.Subcategory?.Name
                    //}).ToList()
                };
                return customerDto;
            }
            return null;
        }

        public async Task<UserForOrder> GetForOrderAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            return new UserForOrder()
            {
                Location = new Shared.DTOs.LocationDTO()
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
                SingleAsync(c => c.Id == id);
            if (customer != null)
            {
                //var books = await _unitOfWork.BookRepository
                //    .Query
                //    .Include(b => b.Work)
                //    .Include(b => b.Customer)
                //    .Include(b => b.Vendor)
                //    .Include(b => b.Company)
                //    .Include(b => b.Location)
                //    .Where(b => b.Customer.Id == id)
                //    .ToListAsync();
                //var bookDtos = books
                //    .Select(b => new BookDTO
                //    {
                //        Id = b.Id,
                //        Date = b.Date,
                //        Description = b.Description,
                //        Status = b.Status,
                //        Work = new WorkDTO
                //        {
                //            Id = b.Work.Id,
                //            Icon = b.Work.Icon,
                //            Name = b.Work.Name                           
                //        },
                //        Location = new LocationDTO
                //        {
                //            Id = b.Location.Id,
                //            Adress = b.Location.Adress,
                //            City = b.Location.City,
                //            Latitude = b.Location.Latitude,
                //            Longitude = b.Location.Longitude,
                //            PostIndex = b.Location.PostIndex
                //        }
                //    }).ToList();
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
                    }).ToList(),
                    //Books = customer.Books?.Select(x => new BookShortDto()
                    //{
                    //    Address = x.Location?.Adress,
                    //    Date = x.Date,
                    //    Description = x.Description,
                    //    Vendor = x?.Vendor?.Person?.Name,
                    //    Status = x.Status,
                    //    WorkType = x.Work.Subcategory?.Name
                    //}).ToList()
                };
                return customerDto;
            }
            return null;
        }

        public async Task<UserForOrder> GetForOrderAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            return new UserForOrder()
            {
                Location = new Shared.DTOs.LocationDTO()
                {
                    Id = customer.Person.Location.Id,
                    Adress = customer.Person.Location.Adress,
                    City = customer.Person.Location.City,
                    Latitude = customer.Person.Location.Latitude,
                    Longitude = customer.Person.Location.Longitude,
                    PostIndex = customer.Person.Location.PostIndex
                },
                Phone = customer.Person.Phone
            };
        }
    }
}
