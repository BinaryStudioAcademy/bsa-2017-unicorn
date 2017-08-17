using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAllAsync();
        Task<LocationDTO> GetByIdAsync(long id);
    }
}
