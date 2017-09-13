using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Interfaces
{
    public interface ISubcategoryService
    {
        Task<SubcategoryShortDTO> GetByIdAsync(long id);
        Task<IEnumerable<SubcategoryShortDTO>> GetByCategoryId(long id);
        Task<SubcategoryShortDTO> CreateAsync(long categoryId, SubcategoryShortDTO subcategoryDTO);
        Task RemoveAsync(long id);
        Task<SubcategoryShortDTO> UpdateAsync(SubcategoryShortDTO subcategoryDTO);
    }
}
