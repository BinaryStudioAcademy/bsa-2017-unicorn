using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<WorkDTO>> GetAllAsync()
        {
            var workList = await _unitOfWork.WorkRepository.GetAllAsync();
            return workList.Select(w => WorkToDTO(w));
        }

        public async Task<WorkDTO> GetByIdAsync(long id)
        {
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(id);
            return WorkToDTO(work);
        }

        private WorkDTO WorkToDTO(Work work)
        {
            return new WorkDTO()
            {
                Name = work.Name,
                Description = work.Description,
                Subcategory = work.Subcategory.Name,
                SubcategoryId = work.Subcategory.Id,
                Id = work.Id
            };
        }
    }
}