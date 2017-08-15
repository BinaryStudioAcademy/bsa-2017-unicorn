using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.Converters;

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

        public async Task<WorkDTO> GetById(long id)
        {
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(id);
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(id);

            var workDto = WorkDTOConverter.WorkToDTO(work);
            return workDto;
        }

    }
}
