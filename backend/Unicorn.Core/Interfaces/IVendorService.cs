using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDTO>> GetAllAsync();
        Task<VendorDTO> GetById(long id);
        Task Create(VendorRegisterDTO vendorDto);
    }
}
