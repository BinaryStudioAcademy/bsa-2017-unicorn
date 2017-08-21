using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(long id);
    }
}
