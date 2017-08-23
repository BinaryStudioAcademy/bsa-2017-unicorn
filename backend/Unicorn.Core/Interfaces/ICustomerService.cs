using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.User;

namespace Unicorn.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<object> GetById(long id);
        Task<UserForOrder> GetForOrderAsync(long id);
        Task CreateAsync(CustomerRegisterDTO customerDto);
        Task UpdateCustomerAsync(UserShortDTO userDTO);
        Task<long> GetUserAccountIdAsync(long id);
    }
}
