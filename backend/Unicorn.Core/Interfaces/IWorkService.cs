using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkDTO>> GetAllAsync();
        Task<WorkDTO> GetByIdAsync(long id);
        Task RemoveByIdAsync(long id);
        Task<WorkDTO> CreateAsync(WorkDTO dto);
        Task UpdateAsync(WorkDTO dto);
    }
}
