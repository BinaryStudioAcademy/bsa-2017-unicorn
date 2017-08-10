using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO> GetById(int id);
    }
}
