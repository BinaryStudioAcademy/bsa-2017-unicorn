using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO> GetById(long id);
    }
}
