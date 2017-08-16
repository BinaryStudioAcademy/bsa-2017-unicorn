using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDTO>> GetAllAsync();
        Task<VendorDTO> GetByIdAsync(long id);
        Task<long> GetVendorAccountIdAsync(long id);
        Task<IEnumerable<SubcategoryShortDTO>> GetVendorCategoriesAsync(long id);
        Task<IEnumerable<ContactDTO>> GetVendorContacts(long id);
        Task<VendorDTO> GetById(long id);
        Task Create(VendorRegisterDTO vendorDto);
    }
}
