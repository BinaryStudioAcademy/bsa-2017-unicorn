using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Admin;
using Unicorn.Core.Infrastructure;

namespace Unicorn.Core.Services
{
    public class AdminService : IAdminService
    {
        public AdminService(IUnitOfWorkFactory unitOfWorkFactory, INotificationProxy notificationProxy, IAuthService authService)
        {
            _uowFactory = unitOfWorkFactory;
            _notificationProxy = notificationProxy;
            _authService = authService;
        }

        public async Task BanAccountAsync(long id)
        {
            var uow = _uowFactory.CreateUnitOfWork();

            var account = await uow.AccountRepository.Query
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == id);

            account.IsBanned = true;

            uow.AccountRepository.Update(account);
            await uow.SaveAsync();

            await _notificationProxy.KickAccount(id);
        }

        public async Task UnbanAccountAsync(long id)
        {
            var uow = _uowFactory.CreateUnitOfWork();

            var account = await uow.AccountRepository.Query
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == id);

            account.IsBanned = false;

            uow.AccountRepository.Update(account);
            await uow.SaveAsync();
        }

        public async Task<AccountsPage> GetAccountsPageAsync(IEnumerable<AccountDTO> items, int page, int size)
        {
            return await Task.Run(() => new AccountsPage
            {
                Items = items.Skip(size * (page - 1))
                    .Take(Math.Min(size, items.Count()-size * (page - 1))).ToList(),
                CurrentPage = page,
                PageSize = size,
                TotalCount = items.Count()
            });
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var personsTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.PersonRepository.Query
                    .Include(p => p.Account)
                    .Include(p => p.Account.Role)
                    .Where(p => p.Account.Role.Id != 5)
                    .Select(p => new AccountDTO
                    {
                        Id = p.Account.Id,
                        Avatar = p.Account.Avatar,
                        Email = p.Account.Email,
                        Role = p.Account.Role.Name,
                        Name = p.Name + " " + p.Surname + " " + p.MiddleName,
                        IsBanned = p.Account.IsBanned
                    }).ToList();
            });

            var companiesTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.CompanyRepository.Query
                    .Include(c => c.Account)
                    .Include(p => p.Account.Role)
                    .Where(p => p.Account.Role.Id != 5)
                    .Select(c => new AccountDTO
                    {
                        Id = c.Account.Id,
                        Avatar = c.Account.Avatar,
                        Email = c.Account.Email,
                        Role = c.Account.Role.Name,
                        Name = c.Name,
                        IsBanned = c.Account.IsBanned
                    }).ToList();
            });
            var companies = await companiesTask;
            var persons = await personsTask;

            return companies
                .Union(persons)
                .OrderBy(p => p.Name).ToList();
        }

        public async Task<IEnumerable<AccountDTO>> SearchAsync(string template, bool isBanned, string role)
        {
            var personsTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.PersonRepository.Query
                    .Include(p => p.Account)
                    .Include(p => p.Account.Role)
                    .Where(p => p.Account.Role.Id != 5)
                    .Where(p => p.Account.IsBanned == isBanned)
                    .Where(p => p.Account.Email.Contains(template) || (p.Name + " " + p.Surname + " " + p.MiddleName).Contains(template))
                    .Select(p => new AccountDTO
                    {
                        Id = p.Account.Id,
                        Avatar = p.Account.Avatar,
                        Email = p.Account.Email,
                        Role = p.Account.Role.Name,
                        Name = p.Name + " " + p.Surname + " " + p.MiddleName,
                        IsBanned = p.Account.IsBanned
                    }).ToList();
            });

            var companiesTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.CompanyRepository.Query
                    .Include(c => c.Account)
                    .Include(p => p.Account.Role)
                    .Where(p => p.Account.Role.Id != 5)
                    .Where(p => p.Account.IsBanned == isBanned)
                    .Where(c => c.Account.Email.Contains(template) || c.Name.Contains(template))
                    .Select(c => new AccountDTO
                    {
                        Id = c.Account.Id,
                        Avatar = c.Account.Avatar,
                        Email = c.Account.Email,
                        Role = c.Account.Role.Name,
                        Name = c.Name,
                        IsBanned = c.Account.IsBanned
                    }).ToList();
            });

            var companies = await companiesTask;
            var persons = await personsTask;

            var accounts = companies
                .Union(persons);

            if (!string.IsNullOrEmpty(role) && role != "all")
            {
                accounts = accounts
                    .Where(a => a.Role.ToLower() == role.ToLower());
            }

            return accounts
                .OrderBy(p => p.Name).ToList();
        }

        public async Task<IEnumerable<AccountDTO>> SearchAsync(string template, string role)
        {
            var personsTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.PersonRepository.Query
                    .Include(p => p.Account)
                    .Include(p => p.Account.Role)
                    .Where(p => p.Account.Role.Id != 5)
                    .Where(p => string.IsNullOrEmpty(template) || p.Account.Email.Contains(template) || (p.Name + " " + p.Surname + " " + p.MiddleName).Contains(template))
                    .Select(p => new AccountDTO
                    {
                        Id = p.Account.Id,
                        Avatar = p.Account.Avatar,
                        Email = p.Account.Email,
                        Role = p.Account.Role.Name,
                        Name = p.Name + " " + p.Surname + " " + p.MiddleName,
                        IsBanned = p.Account.IsBanned
                    }).ToList();
            });

            var companiesTask = Task.Run(() =>
            {
                var uow = _uowFactory.CreateUnitOfWork();

                return uow.CompanyRepository.Query
                    .Include(c => c.Account)
                    .Include(c => c.Account.Role)
                    .Where(c => c.Account.Role.Id != 5)
                    .Where(c => string.IsNullOrEmpty(template) || c.Account.Email.Contains(template) || c.Name.Contains(template))
                    .Select(c => new AccountDTO
                    {
                        Id = c.Account.Id,
                        Avatar = c.Account.Avatar,
                        Email = c.Account.Email,
                        Role = c.Account.Role.Name,
                        Name = c.Name,
                        IsBanned = c.Account.IsBanned
                    }).ToList();
            });

            var companies = await companiesTask;
            var persons = await personsTask;

            var accounts = companies
                .Union(persons);

            if (!string.IsNullOrEmpty(role) && role != "all")
            {
                accounts = accounts
                    .Where(a => a.Role.ToLower() == role.ToLower());
            }

            return accounts
                .OrderBy(p => p.Name).ToList();
        }

        public async Task<string> ValidateLogin(string login, string pass)
        {
            AdminAuthConfig adminAuthConfig = AdminAuthConfig.Config;
            if (login == adminAuthConfig.Login && pass == adminAuthConfig.Password)
            {
                return await _authService.GenerateJwtTokenAsync(null, Properties.Settings.Default.PrivateKey); // Token for admin
            }

            return null;
        }

        private readonly IUnitOfWorkFactory _uowFactory;
        private readonly INotificationProxy _notificationProxy;
        private readonly IAuthService _authService;
    }
}
