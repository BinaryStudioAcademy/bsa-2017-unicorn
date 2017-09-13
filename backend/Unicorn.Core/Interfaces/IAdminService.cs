using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs.Admin;

namespace Unicorn.Core.Interfaces
{
    public interface IAdminService
    {

        Task<BannedAccountDTO> BanAccountAsync(long id, DateTimeOffset endTime);

        Task<BannedAccountDTO> UpdateBanTime(long entryId, DateTimeOffset endTime);

        Task LiftBanAsync(long entryId);

        Task LiftBanByAccountAsync(long accountId);

        Task<List<BannedAccountDTO>> GetAllBannedAccountsAsync();

        Task<List<BannedAccountDTO>> SearchAccountsAsync(string template);

        Task<BannedAccountsPage> GetBannedAccountsPageAsync(int page, int pageSize, IEnumerable<BannedAccountDTO> items);
    }
}
