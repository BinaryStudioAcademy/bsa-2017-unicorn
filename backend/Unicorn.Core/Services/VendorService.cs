using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VendorDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Vendor, VendorDTO>());
            return Mapper.Map<IEnumerable<Vendor>, List<VendorDTO>>(await _unitOfWork.VendorRepository.GetAllAsync());
        }

        public async Task<VendorDTO> GetById(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var location = await _unitOfWork.LocationRepository.GetByIdAsync(id);
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            var vendorDto = new VendorDTO()
            {
                Id = vendor.Id,
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                Company = new CompanyDTO()
                {
                    Id = company.Id,
                    Location = new LocationDTO()
                    {
                        Id = location.Id,
                        City = location.City,
                        Adress = location.Adress
                    }
                },
                Person = new PersonDTO()
                {
                    Id = person.Id,
                    Name = person.Name,
                    SurnameName = person.SurnameName,
                    Phone = person.Phone
                },
                Works = (ICollection<WorkDTO>)vendor.Works
            };
            return vendorDto;
        }
    }
}

