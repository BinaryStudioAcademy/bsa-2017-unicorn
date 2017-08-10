using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Person, PersonDTO>());
            return Mapper.Map<IEnumerable<Person>, List<PersonDTO>>(await _unitOfWork.PersonRepository.GetAllAsync());
        }

        public async Task<PersonDTO> GetById(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);

            var persontDto = new PersonDTO()
            {
                Id = person.Id,
                Name = person.Name,
                SurnameName = person.SurnameName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Gender = person.Gender,
                Phone = person.Phone
            };
            return persontDto;
        }
    }
}
