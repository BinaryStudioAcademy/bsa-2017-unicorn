using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs.Admin;

namespace Unicorn.Core.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<IEnumerable<AccountDTO>> SearchAsync(string template, bool isBanned, string role);
        Task<IEnumerable<AccountDTO>> SearchAsync(string template, string role);

        Task<AccountsPage> GetAccountsPageAsync(IEnumerable<AccountDTO> items, int page, int size);

        Task BanAccountAsync(long id);
        Task UnbanAccountAsync(long id);

        Task<string> ValidateLogin(string login, string pass);
    }
}
