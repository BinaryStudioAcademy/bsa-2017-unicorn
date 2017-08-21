using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Entities;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioItemDTO> GetItemByIdAsync(long id);
        Task<IEnumerable<PortfolioItemDTO>> GetItemsByVendorIdAsync(long id);
        Task CreateAsync(long vendorId, PortfolioItemDTO itemDto);
    }
}
