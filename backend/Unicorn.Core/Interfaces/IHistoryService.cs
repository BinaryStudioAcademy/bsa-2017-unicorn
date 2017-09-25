using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.History;

namespace Unicorn.Core.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryDTO>> GetAllAsync();
        Task<HistoryDTO> GetById(long id);
        Task<IEnumerable<VendorHistoryDTO>> GetVendorHistoryAsync(long vendorId);
    }
}
