using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;

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
            var books = _unitOfWork.BookRepository.GetByIdAsync(customer.Id);
            var customerDto = new CustomerDTO()
            {
                Id = customer.Id,
                Person = new PersonDTO()
                {
                    Id = person.Id,
                    Name = person.Name,
                    SurnameName = person.SurnameName,
                    MiddleName = person.MiddleName,
                    Gender = person.Gender,
                    Phone = person.Phone
                }
            };
            return customerDto;
        }

        public async Task CreateAsync(CustomerDTO customerDto)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SocialAccountDTO, SocialAccount>();
                cfg.CreateMap<AccountDTO, Account>();
                cfg.CreateMap<PersonDTO, Person>();
                cfg.CreateMap<CustomerDTO, Customer>();
            });
            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            _unitOfWork.CustomerRepository.Create(customer);
            await _unitOfWork.SaveAsync();
        }
    }
}
