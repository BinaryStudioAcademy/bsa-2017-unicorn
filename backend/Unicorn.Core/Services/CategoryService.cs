using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
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
                    Tags = c.Tags,
                    Icon = c.Icon,
                    Subcategories = c.Subcategories
                        .Where(s => !s.IsDeleted)    
                        .Select(s => new SubcategoryShortDTO()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Category = c.Name,
                            CategoryId = c.Id,
                            Description = s.Description,
                            Tags = s.Tags,
                            Icon = s.Icon
                        }).ToList()
                }).ToListAsync();
        }

        public Task<CategoryDTO> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Description = categoryDTO.Description,
                Icon = categoryDTO.Icon,
                IsDeleted = false,
                Name = categoryDTO.Name,
                Subcategories = new List<Subcategory>(),
                Tags = ""
            };

            _unitOfWork.CategoryRepository.Create(category);
            await _unitOfWork.SaveAsync();

            categoryDTO.Id = category.Id;

            return categoryDTO;
        }

        public async Task RemoveAsync(long id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryDTO.Id);

            category.Description = categoryDTO.Description;
            category.Icon = categoryDTO.Icon;
            category.Name = categoryDTO.Name;
            category.Tags = categoryDTO.Tags;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();

            return categoryDTO;
        }

        public async Task<List<CategoryDTO>> SearchByNameAsync(string template)
        {
            return await _unitOfWork.CategoryRepository.Query
                .Include(c => c.Subcategories)
                .Where(c => c.Name.ToLower().Contains(template.ToLower()))
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Tags = c.Tags,
                    Icon = c.Icon,
                    Subcategories = c.Subcategories
                        .Where(s => !s.IsDeleted)
                        .Select(s => new SubcategoryShortDTO()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Category = c.Name,
                            CategoryId = c.Id,
                            Description = s.Description,
                            Tags = s.Tags,
                            Icon = s.Icon
                        }).ToList()
                }).ToListAsync();
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
