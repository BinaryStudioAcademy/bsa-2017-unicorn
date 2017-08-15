using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO> GetById(long id);
        Task CreateAsync(CustomerRegisterDTO customerDto);
    }
}
