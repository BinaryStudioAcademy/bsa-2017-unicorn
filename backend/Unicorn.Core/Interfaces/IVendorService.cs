using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDTO>> GetAllAsync();
        Task<VendorDTO> GetByIdAsync(long id);
        Task<long> GetVendorAccountIdAsync(long id);
    }
}
