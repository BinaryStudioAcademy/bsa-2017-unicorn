using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.Vendor;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Core.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<ShortVendorDTO>> GetAllAsync();
        Task<ShortVendorDTO> GetByIdAsync(long id);
        Task<long> GetVendorAccountIdAsync(long id);
        Task<IEnumerable<CategoryDTO>> GetVendorCategoriesAsync(long id);
        Task<IEnumerable<ContactShortDTO>> GetVendorContactsAsync(long id);
        Task<IEnumerable<WorkDTO>> GetVendorWorksAsync(long id);
        Task CreateAsync(VendorRegisterDTO vendorDto);
        Task UpdateAsync(ShortVendorDTO vendorDto);
    }
}
