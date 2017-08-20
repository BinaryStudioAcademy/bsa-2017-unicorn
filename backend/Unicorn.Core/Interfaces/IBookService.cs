using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO> GetById(long id);
        Task<IEnumerable<VendorBookDTO>> GetVendorOrdersAsync(long vendorId);
    }
}
