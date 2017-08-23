using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

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
                    Rating = account.Rating,
                    DateCreated = account.DateCreated,
                    Email = account.Email,
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
                Rating = account.Rating,
                DateCreated = account.DateCreated,
                Email = account.Email,
                EmailConfirmed =account.EmailConfirmed,
                SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid= x.Uid}).ToList(),
                Role = new RoleDTO { Id = account.Role.Id, Name = account.Role.Name }
            };
            return accountDto;
        }      
    }
}
