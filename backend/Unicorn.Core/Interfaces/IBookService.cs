using System.Collections.Generic;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO> GetByIdAsync(long id);
        Task Create(BookOrderDTO book);
        Task Update(VendorBookDTO book);
        Task Update(BookDTO book);
        Task<IEnumerable<VendorBookDTO>> GetOrdersAsync(string role, long id);
        Task<IEnumerable<VendorBookDTO>> GetPendingOrdersAsync(string role, long id);
        Task<IEnumerable<VendorBookDTO>> GetAcceptedOrdersAsync(string role, long id);
        Task<IEnumerable<VendorBookDTO>> GetFinishedOrdersAsync(string role, long id);
        Task<IEnumerable<CustomerBookDTO>> GetCustomerBooks(long id);
        Task DeleteBook(long id);

        Task CreateTasks(List<ShortTaskDTO> tasks, long companyId);
        Task<List<BookDTO>> GetCompanyTasks(long companyId);
        Task ReassignCompanyTask(ShortTaskDTO task, long companyId);
        Task DeleteCompanyTask(long taskId);
    }
}
