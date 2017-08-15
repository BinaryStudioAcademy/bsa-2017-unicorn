using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTO>> GetAllAsync();
        Task<PermissionDTO> GetById(int id);
    }
}
