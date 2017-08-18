using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<object> GetById(long id);
        Task CreateAsync(CustomerRegisterDTO customerDto);
    }
}
