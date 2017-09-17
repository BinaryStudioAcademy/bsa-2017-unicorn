using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        public SubcategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SubcategoryShortDTO> CreateAsync(long categoryId, SubcategoryShortDTO subcategoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);

            var subcategory = new Subcategory
            {
                Description = subcategoryDTO.Description,
                Icon = subcategoryDTO.Icon,
                IsDeleted = false,
                Name = subcategoryDTO.Name,
                Works = new List<Work>(),
                Tags = "",
                Category = category
            };

            _unitOfWork.SubcategoryRepository.Create(subcategory);
            await _unitOfWork.SaveAsync();

            subcategoryDTO.Id = subcategory.Id;

            return subcategoryDTO;
        }

        public async Task<IEnumerable<SubcategoryShortDTO>> GetByCategoryId(long id)
        {
            return await _unitOfWork.SubcategoryRepository.Query
                .Include(s => s.Category)
                .Where(s => !s.IsDeleted)
                .Where(s => s.Category.Id == id)
                .Select(s => new SubcategoryShortDTO
                {
                    Category = s.Category.Name,
                    Name = s.Name,
                    CategoryId = s.Category.Id,
                    Icon = s.Icon,
                    Id = s.Id,
                    Description = s.Description,
                    Tags = s.Tags
                })
                .ToListAsync();
        }

        public async Task<SubcategoryShortDTO> GetByIdAsync(long id)
        {
            var subcategoryDto = await _unitOfWork.SubcategoryRepository.Query
                .Include(s => s.Category)
                .Where(s => !s.IsDeleted)
                .SingleOrDefaultAsync(s => s.Id == id);

            return new SubcategoryShortDTO
            {
                Category = subcategoryDto.Category.Name,
                Name = subcategoryDto.Name,
                CategoryId = subcategoryDto.Category.Id,
                Icon = subcategoryDto.Icon,
                Id = subcategoryDto.Id,
                Description = subcategoryDto.Description,
                Tags = subcategoryDto.Tags
            }; ;
        }

        public async Task RemoveAsync(long id)
        {
            _unitOfWork.SubcategoryRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<SubcategoryShortDTO> UpdateAsync(SubcategoryShortDTO subcategoryDTO)
        {
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(subcategoryDTO.Id);

            subcategory.Icon = subcategoryDTO.Icon;
            subcategory.Name = subcategoryDTO.Name;
            subcategory.Description = subcategoryDTO.Description;
            subcategory.Tags = subcategoryDTO.Tags;

            _unitOfWork.SubcategoryRepository.Update(subcategory);
            await _unitOfWork.SaveAsync();

            return subcategoryDTO;
        }

        private IUnitOfWork _unitOfWork;
    }
}
