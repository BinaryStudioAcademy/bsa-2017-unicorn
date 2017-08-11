using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Role, RoleDTO>());
            return Mapper.Map<IEnumerable<Role>, List<RoleDTO>>(await _unitOfWork.RoleRepository.GetAllAsync());
        }

        public async Task<RoleDTO> GetById(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);

            var roleDto = new RoleDTO()
            {
               Id = role.Id,
               Name = role.Name,
               Accounts = (ICollection<AccountDTO>)role.Accounts,
            };
            return roleDto;
        }
    }
}
