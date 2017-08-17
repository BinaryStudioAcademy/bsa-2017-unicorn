using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Permission, PermissionDTO>());
            return Mapper.Map<IEnumerable<Permission>, List<PermissionDTO>>(await _unitOfWork.PermissionRepository.GetAllAsync());
        }

        public async Task<PermissionDTO> GetById(int id)
        {
            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(id);
            return new PermissionDTO
            {
                Id = permission.Id,
                Name = permission.Name,
            };
        }
    }
}
