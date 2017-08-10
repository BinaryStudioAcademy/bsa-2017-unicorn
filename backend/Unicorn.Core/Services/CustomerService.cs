using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
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

        public async Task<CustomerDTO> GetById(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            var books = _bookservice.GetAllAsync();

            var customerDto = new CustomerDTO()
            {
                Id = customer.Id,
                Books = (ICollection<BookDTO>)books,
                Person = new PersonDTO() {
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


    }
}
