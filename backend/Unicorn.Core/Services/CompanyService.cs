using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Services.Helpers;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.Vendor;

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
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)AccountRoles.Company);
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var company = new Company();

            account.Role = role;
            account.Avatar = "http://www.yupptv.in/images/banner-default.jpg";
            account.DateCreated = DateTime.Now;
            account.Email = companyDto.Email;           

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
            company.Location = new Location();

            _unitOfWork.CompanyRepository.Create(company);
            await _unitOfWork.SaveAsync();
        }
    }
}

