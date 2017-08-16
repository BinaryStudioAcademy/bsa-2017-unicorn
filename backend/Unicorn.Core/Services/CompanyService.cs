using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
        {
            var companies = await _unitOfWork.CompanyRepository.GetAllAsync();

            List<CompanyDTO> companiesDTO = new List<CompanyDTO>();

            foreach (var company in companies)
            {
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(company.Account.Id);
                var director = await _unitOfWork.AccountRepository.GetByIdAsync(company.Director.Id);
                var location = await _unitOfWork.LocationRepository.GetByIdAsync(company.Location.Id);
                var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
                var companyDTO = new CompanyDTO
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description,
                    FoundationDate = company.FoundationDate,
                    Staff = company.Staff,

                    Account = new AccountDTO
                    {
                        Avatar = account.Avatar,
                        DateCreated = account.DateCreated,
                        Email = account.Email,
                        Id = account.Id,
                        Rating = account.Rating,
                        EmailConfirmed = account.EmailConfirmed,
                        Permissions = account.Permissions.Select(x => new PermissionDTO { Id = x.Id, Name = x.Name }).ToList(),
                        Role = new RoleDTO {Id = account.Role.Id, Name = account.Role.Name},
                        SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid = x.Uid }).ToList()
                    },

                    Director = new AccountDTO {
                        Avatar = director.Avatar,
                        DateCreated = director.DateCreated,
                        Email = director.Email,
                        Id = director.Id,
                        Rating = director.Rating,
                        EmailConfirmed = director.EmailConfirmed,
                        Permissions = director.Permissions.Select(x => new PermissionDTO { Id = x.Id, Name = x.Name }).ToList(),
                        Role = new RoleDTO { Id = director.Role.Id, Name = director.Role.Name },
                        SocialAccounts = director.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid = x.Uid }).ToList()
                    },

                    Location = new LocationDTO()
                    {
                        Id = location.Id,
                        Adress = location.Adress,
                        City = location.City,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        PostIndex = location.PostIndex
                    },

                    Vendors = vendors.Where(v => v.Company != null && v.Company.Id == company.Id).Select(x => new VendorDTO
                    {
                        Id = x.Id,
                        Experience = x.Experience,
                        ExWork = x.ExWork,
                        Position = x.Position
                    }).ToList()


                };
                companiesDTO.Add(companyDTO);
            }

            return companiesDTO;
        }

        public async Task<CompanyDTO> GetByIdAsync(long id)
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<Company, CompanyDTO>());
            //return Mapper.Map<Company, CompanyDTO>(await _unitOfWork.CompanyRepository.GetByIdAsync(id));
            return null;


            //var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            //var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            //var location = await _unitOfWork.LocationRepository.GetByIdAsync(id);
            //var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            //var vendorDto = new VendorDTO()
            //{
            //    Id = vendor.Id,
            //    Experience = vendor.Experience,
            //    ExWork = vendor.ExWork,
            //    Company = new CompanyDTO()
            //    {
            //        Id = company.Id,
            //        Location = new LocationDTO()
            //        {
            //            Id = location.Id,
            //            City = location.City,
            //            Adress = location.Adress
            //        }
            //    },
            //    Person = new PersonDTO()
            //    {
            //        Id = person.Id,
            //        Name = person.Name,
            //        SurnameName = person.SurnameName,
            //        Phone = person.Phone
            //    },
            //    Works = (ICollection<WorkDTO>)vendor.Works
            //};
            //return vendorDto;
        }
    }
}