using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.Query
                .Include(c => c.Subcategories)
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Icon = c.Icon,
                    Subcategories = c.Subcategories.Select(s => new SubcategoryShortDTO()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Category = c.Name,
                        CategoryId = c.Id,
                        Description = s.Description,
                        Icon = s.Icon
                    }).ToList()
                }).ToListAsync();
        }

        public Task<CategoryDTO> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
