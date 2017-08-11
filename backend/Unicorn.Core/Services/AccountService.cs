using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;
using System.Linq;
using System.Data.Entity;

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
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountDTO>());
            return Mapper.Map<IEnumerable<Account>, List<AccountDTO>>(await _unitOfWork.AccountRepository.GetAllAsync());
        }

        public async Task<AccountDTO> GetById(int id)
        {
            var account = await _unitOfWork.AccountRepository.Query.Include(x => x.SocialAccounts).SingleOrDefaultAsync(x => x.Id == id);
            var accountDto = new AccountDTO()
            {
                Id = account.Id,
                Avatar = account.Avatar,
                Rating = account.Rating,
                SocialAccounts = account.SocialAccounts

                //Permissions =  (ICollection<PermissionDTO>)account.Permissions,
            };
            return accountDto;
        }


        
    }
}
