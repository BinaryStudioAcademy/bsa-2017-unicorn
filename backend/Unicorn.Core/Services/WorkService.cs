using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
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
            Mapper.Initialize(cfg => cfg.CreateMap<Work, WorkDTO>());
            return Mapper.Map<IEnumerable<Work>, List<WorkDTO>>(await _unitOfWork.WorkRepository.GetAllAsync());
        }

        public async Task<WorkDTO> GetById(int id)
        {
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(id);
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(id);

            var workDto = new WorkDTO()
            {
                Id = work.Id,
                Name = work.Name,
                Description = work.Description,

                Subcategory = new SubcategoryDTO() {
                    Id = subcategory.Id ,
                    Name = subcategory.Name
                }
            };
            return workDto;
        }

    }
}
