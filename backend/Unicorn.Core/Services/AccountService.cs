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
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            var accountDto = new AccountDTO()
            {
                Id = account.Id,
                Avatar = account.Avatar,
                Rating = account.Rating,
                DateCreated = account.DateCreated,
                Email = account.Email,
                EmailConfirmed =account.EmailConfirmed,
                SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid= x.Uid}).ToList(),
                Permissions = account.Permissions.Select(x => new PermissionDTO { Id = x.Id, Name =x.Name}).ToList(),
                Role = new RoleDTO {Id = role.Id, Name = role.Name}
                

                
                //Permissions =  (ICollection<PermissionDTO>)account.Permissions,
            };
            return accountDto;
        }


        
    }
}
