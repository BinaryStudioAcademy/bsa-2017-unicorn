using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task Create(CompanyRegisterDTO companyDto)
        {
            var account = new Account();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)RoleType.Company);
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var company = new Company();

            account.Role = role;
            account.Avatar = companyDto.Image;
            account.DateCreated = DateTime.Now;
            account.Email = companyDto.Email;

            account.Location = new Location()
            {
                Adress = companyDto.Location.Adress,
                City = companyDto.Location.City,
                IsDeleted = false,
                Latitude = companyDto.Location.Latitude,
                Longitude = companyDto.Location.Longitude,
                PostIndex = companyDto.Location.PostIndex
            };
            socialAccount.Provider = companyDto.Provider;
            socialAccount.Uid = companyDto.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);
            account.SocialAccounts = socialAccounts;

            company.Staff = companyDto.Staff;
            company.Name = companyDto.Name;
            company.Description = companyDto.Description;
            company.Account = account;
            company.FoundationDate = companyDto.Foundation;

            _unitOfWork.CompanyRepository.Create(company);
            await _unitOfWork.SaveAsync();
        }
    }
}

 