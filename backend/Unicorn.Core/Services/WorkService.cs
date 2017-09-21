using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using System;

namespace Unicorn.Core.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkDTO> CreateAsync(WorkDTO dto)
        {
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(dto.SubcategoryId);
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(dto.VendorId);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(dto.CompanyId);       

            var work = new Work()
            {
                Name = dto.Name,
                Description = dto.Description,
                Icon = dto.Icon,
                IsDeleted = false,
                Subcategory = subcategory,
                Vendor = vendor,
                Company = company
            };

            _unitOfWork.WorkRepository.Create(work);
            await _unitOfWork.SaveAsync();

            dto.Id = work.Id;

            return dto;
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

        public async Task RemoveByIdAsync(long id)
        {
            try
            {
                _unitOfWork.WorkRepository.Delete(id);
                await _unitOfWork.SaveAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(WorkDTO dto)
        {
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(dto.SubcategoryId);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(dto.Id);

            work.Name = dto.Name;
            work.Description = dto.Description;
            work.Icon = dto.Icon;
            work.Subcategory = subcategory;

            _unitOfWork.WorkRepository.Update(work);
            await _unitOfWork.SaveAsync();
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