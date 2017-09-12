using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Admin;

namespace Unicorn.Core.Services
{
    public class AdminService : IAdminService
    {
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BannedAccountDTO> BanAccountAsync(long id, DateTimeOffset endTime)
        {
            var entry = _unitOfWork.BannedAccountRepository.Query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Account)
                .Include(a => a.Account.Role)
                .SingleOrDefault(a => a.Account.Id == id);

            var accountTask = _unitOfWork.AccountRepository.GetByIdAsync(id);

            if (entry != null)
            {
                if (entry.EndTime <= DateTimeOffset.Now)
                {
                    _unitOfWork.BannedAccountRepository.Delete(entry.Id);

                }
                else
                {
                    return ToBannedAccountDTO(entry);   
                }
            }

            var account = await accountTask;
            entry = new BannedAccount
            {
                Account = account,
                StartTime = DateTimeOffset.Now,
                EndTime = endTime,
                IsDeleted = false
            };

            _unitOfWork.BannedAccountRepository.Create(entry);
            await _unitOfWork.SaveAsync();

            return ToBannedAccountDTO(entry);
        }

        public async Task<BannedAccountDTO> UpdateBanTime(long entryId, DateTimeOffset endTime)
        {
            var entry = await _unitOfWork.BannedAccountRepository.Query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Account)
                .Include(a => a.Account.Role)
                .SingleOrDefaultAsync(a => a.Id == entryId);

            entry.EndTime = endTime;

            _unitOfWork.BannedAccountRepository.Update(entry);
            await _unitOfWork.SaveAsync();

            return ToBannedAccountDTO(entry);
        }

        public async Task LiftBanAsync(long entryId)
        {
            await Task.Run(() => _unitOfWork.BannedAccountRepository.Delete(entryId));
        }

        public async Task LiftBanByAccountAsync(long accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            var entry = await _unitOfWork.BannedAccountRepository.Query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Account)
                .Include(a => a.Account.Role)
                .SingleOrDefaultAsync(a => a.Account.Id == accountId);

            if (entry != null)
                _unitOfWork.BannedAccountRepository.Delete(entry.Id);
        }

        private BannedAccountDTO ToBannedAccountDTO(BannedAccount entry)
        {
            return new BannedAccountDTO
            {
                Id = entry.Id,
                AccountId = entry.Account.Id,
                Email = entry.Account.Email,
                Role = entry.Account.Role.Name,
                StartTime = entry.StartTime,
                EndTime = entry.EndTime
            };
        }

        private IUnitOfWork _unitOfWork;
    }
}
