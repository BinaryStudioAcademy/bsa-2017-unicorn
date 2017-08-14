using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkDTO>> GetAllAsync();
        Task<WorkDTO> GetById(long id);
    }
}
