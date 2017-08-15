using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO> GetByIdAsync(long id);
    }
}