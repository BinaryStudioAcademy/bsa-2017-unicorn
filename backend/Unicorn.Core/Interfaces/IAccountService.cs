using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO> GetByIdAsync(long id);
        Task<ShortProfileInfoDTO> GetProfileInfoAsync(long id);
        Task<List<ShortProfileInfoDTO>> SearchByTemplate(string template, int count);
    }
}
