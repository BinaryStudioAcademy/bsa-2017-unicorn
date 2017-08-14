using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDTO>> GetAllAsync();
        Task<VendorDTO> GetById(long id);
    }
}
