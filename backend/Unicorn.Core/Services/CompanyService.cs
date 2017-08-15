using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CompanyDTO companyDto)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LocationDTO, Location>();
                cfg.CreateMap<PermissionDTO, Permission>();
                cfg.CreateMap<RoleDTO, Role>();
                cfg.CreateMap<SocialAccountDTO, SocialAccount>();
                cfg.CreateMap<AccountDTO, Account>();
                cfg.CreateMap<PersonDTO, Person>();
                cfg.CreateMap<CompanyDTO, Company>();
            });
            var company = Mapper.Map<CompanyDTO, Company>(companyDto);
            _unitOfWork.CompanyRepository.Create(company);
            await _unitOfWork.SaveAsync();
        }
    }
}
