using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
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
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            List<RoleDTO> rolesdata = new List<RoleDTO>();
            foreach (var role in roles)
            {
                var roleDto = new RoleDTO()
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                rolesdata.Add(roleDto);
            }
            return rolesdata;
        }

        public async Task<RoleDTO> GetByIdAsync(long id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            var roleDto = new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return roleDto;
        }
    }
}
