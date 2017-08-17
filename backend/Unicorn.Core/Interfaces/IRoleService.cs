using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task <RoleDTO> GetByIdAsync(long id);
        Task<RoleDTO> GetByUserId(long uid);
    }
}
