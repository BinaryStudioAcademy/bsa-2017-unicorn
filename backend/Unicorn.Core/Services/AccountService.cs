using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using System;
using System.Data.Entity;

namespace Unicorn.Core.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accountlist = await _unitOfWork.AccountRepository.GetAllAsync();
            List<AccountDTO> allaccountsData = new List<AccountDTO>();
            foreach (var account in accountlist)
            {
                var accountDto = new AccountDTO()
                {
                    Id = account.Id,
                    Avatar = account.Avatar,
                    CroppedAvatar = account.CroppedAvatar,
                    DateCreated = account.DateCreated,
                    Email = account.Email,
                    Location = new LocationDTO()
                    {
                        Id = account.Location.Id,
                        PostIndex = account.Location.PostIndex,
                        Adress = account.Location.Adress,
                        City = account.Location.City,
                        Latitude = account.Location.Latitude,
                        Longitude = account.Location.Longitude
                    },
                    EmailConfirmed = account.EmailConfirmed,
                    SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid = x.Uid}).ToList(),
                    Role = new RoleDTO { Id = account.Role.Id, Name = account.Role.Name }
                };
                allaccountsData.Add(accountDto);
            }
            return allaccountsData;
        }

        public async Task<AccountDTO> GetByIdAsync(long id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            var accountDto = new AccountDTO()
            {
                Id = account.Id,
                Avatar = account.Avatar,
                CroppedAvatar = account.CroppedAvatar,
                DateCreated = account.DateCreated,
                Email = account.Email,
                EmailConfirmed =account.EmailConfirmed,
                Location = new LocationDTO()
                {
                    Id = account.Location.Id,
                    PostIndex = account.Location.PostIndex,
                    Adress = account.Location.Adress,
                    City = account.Location.City,
                    Latitude = account.Location.Latitude,
                    Longitude = account.Location.Longitude
                },
                SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid= x.Uid}).ToList(),
                Role = new RoleDTO { Id = account.Role.Id, Name = account.Role.Name }
            };
            return accountDto;
        }

        public async Task<ShortProfileInfoDTO> GetProfileInfoAsync(long id)
        {
            var account = await _unitOfWork.AccountRepository.Query
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == id);

            var result = new ShortProfileInfoDTO
            {
                Avatar = account.Avatar,
                CroppedAvatar = account.CroppedAvatar,
                Email = account.Email,
                Role = account.Role.Name
            };

            switch (account.Role.Id)
            {
                case 2:
                case 3:
                    var person = await _unitOfWork.PersonRepository.Query
                        .Include(p => p.Account)
                        .SingleAsync(p => p.Account.Id == id);
                    result.Name = $"{person.Name} {person.Surname}";
                    return result;
                    
                case 4: 
                    var company = await _unitOfWork.CompanyRepository.Query
                        .Include(p => p.Account)
                        .SingleAsync(p => p.Account.Id == id);
                    result.Name = company.Name;
                    return result;
                case 5:
                    var adminPerson = await _unitOfWork.PersonRepository.Query
                        .Include(p => p.Account).FirstOrDefaultAsync(x => x.Account.Id == id);
                    if (adminPerson == null)
                    {
                        var adminCompany = await _unitOfWork.CompanyRepository.Query
                            .Include(p => p.Account).SingleAsync(p => p.Account.Id == id);
                        result.Name = adminCompany.Name;
                        return result;
                    }

                    result.Name = $"{adminPerson.Name} {adminPerson.Surname}";
                    return result;

                default:
                    return null;
            }
        }

        public async Task<List<ShortProfileInfoDTO>> SearchByTemplate(string template, int count)
        {
            var persons = await _unitOfWork.PersonRepository.Query
                .Include(p => p.Account)
                .Include(p => p.Account.Role)
                .Where(p => p.Account.Role.Id != 5)
                .Where(p => p.Account.Email.Contains(template) || (p.Name + " " + p.Surname + " " + p.MiddleName).Contains(template))
                .Select(p => new ShortProfileInfoDTO
                {
                    AccountId = p.Account.Id,
                    Avatar = p.Account.Avatar,
                    Email = p.Account.Email,
                    Role = p.Account.Role.Name,
                    Name = p.Name + " " + p.Surname + " " + p.MiddleName
                }).ToListAsync();
            var companies = await _unitOfWork.CompanyRepository.Query
                .Include(c => c.Account)
                .Where(c => c.Account.Email.Contains(template) || c.Name.Contains(template))
                .Select(c => new ShortProfileInfoDTO
                {
                    AccountId = c.Account.Id,
                    Avatar = c.Account.Avatar,
                    Email = c.Account.Email,
                    Role = c.Account.Role.Name,
                    Name = c.Name
                }).ToListAsync();

            return companies
                .Union(persons)
                .OrderBy(p => p.Name).ToList();
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
