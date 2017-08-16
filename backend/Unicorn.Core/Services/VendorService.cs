using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;
using Unicorn.Shared.DTOs.Register;
using System;

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

        public async Task Create(VendorRegisterDTO vendorDto)
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<LocationDTO, Location>();
            //    cfg.CreateMap<PermissionDTO, Permission>();
            //    cfg.CreateMap<RoleDTO, Role>();
            //    cfg.CreateMap<SocialAccountDTO, SocialAccount>();
            //    cfg.CreateMap<AccountDTO, Account>();
            //    cfg.CreateMap<PersonDTO, Person>();
            //    cfg.CreateMap<VendorDTO, Vendor>();
            //});
            var account = new Account();
            var role = new Role();
            var permissions = new List<Permission>();
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var vendor = new Vendor();
            var person = new Person();

            account.Role = role;
            account.Permissions = permissions;
            account.DateCreated = DateTime.Now;
            account.Email = vendorDto.Email;
            account.SocialAccounts = socialAccounts;

            role.Name = "vendor";

            socialAccount.Provider = vendorDto.Provider;
            socialAccount.Uid = vendorDto.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);

            person.Birthday = vendorDto.Birthday;
            person.Phone = vendorDto.Phone;
            person.Name = vendorDto.FirstName;
            person.MiddleName = vendorDto.MiddleName;
            person.SurnameName = vendorDto.LastName;
            person.Account = account;
            person.Location = new Location();

            vendor.Person = person;
            vendor.Experience = vendorDto.Experience;
            vendor.Position = vendorDto.Position;
            vendor.ExWork = vendorDto.Speciality;

            _unitOfWork.VendorRepository.Create(vendor);
            await _unitOfWork.SaveAsync();
        }
    }
}

