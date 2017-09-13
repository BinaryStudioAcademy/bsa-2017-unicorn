using System;
using System.Collections.Generic;
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

        public async Task<List<BannedAccountDTO>> GetAllBannedAccountsAsync()
        {
            return await _unitOfWork.BannedAccountRepository.Query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Account)
                .Include(a => a.Account.Role)
                .Select(a => ToBannedAccountDTO(a))
                .ToListAsync();
        }

        public async Task<BannedAccountsPage> GetBannedAccountsPageAsync(int page, int pageSize, IEnumerable<BannedAccountDTO> items)
        {
            return await Task.Run(() => new BannedAccountsPage
            {
                Items = items.Skip(pageSize * (page - 1))
                    .Take(pageSize).ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = items.Count()
            });
        }

        public async Task<List<BannedAccountDTO>> SearchAccountsAsync(string template)
        {
            var persons = await _unitOfWork.PersonRepository.Query
                .Include(p => p.Account)
                .Where(p => p.Account.Email.Contains(template) || (p.Name + " " + p.Surname + " " + p.MiddleName).Contains(template))
                .Select(p => new BannedAccountDTO
                {
                    AccountId = p.Account.Id,
                    Email = p.Account.Email,
                    Role = p.Account.Role.Name,
                }).ToListAsync();
            var companies = await _unitOfWork.CompanyRepository.Query
                .Include(c => c.Account)
                .Where(c => c.Account.Email.Contains(template) || c.Name.Contains(template))
                .Select(c => new BannedAccountDTO
                {
                    AccountId = c.Account.Id,
                    Email = c.Account.Email,
                    Role = c.Account.Role.Name,
                }).ToListAsync();

            var accounts = companies
                .Union(persons)
                .ToList();

            var banned = await _unitOfWork.BannedAccountRepository.Query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Account)
                .Include(a => a.Account.Role)
                .Select(a => ToBannedAccountDTO(a))
                .ToListAsync();

            return accounts
                .Union(banned)
                .GroupBy(a => a.AccountId)
                .Select(g => g.First())
                .ToList();
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
