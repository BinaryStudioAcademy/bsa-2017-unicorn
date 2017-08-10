using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

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
            var socialAccount = await _unitOfWork.SocialAccountRepository.GetByIdAsync(id);
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);

            var accountDto = new AccountDTO() {
                Id = account.Id,
                Avatar = account.Avatar,
                SocialAccount = new SocialAccountDTO()
                    { Id = account.Id,
                    FacebookUID = socialAccount.FacebookUID,
                    GoogleUID = socialAccount.GoogleUID },
                Rating = account.Rating  };
            return accountDto;
        }
    }
}
