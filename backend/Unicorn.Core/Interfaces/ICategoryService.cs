using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(long id);
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task RemoveAsync(long id);
        Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO);
        Task<List<CategoryDTO>> SearchByNameAsync(string template);
    }
}
