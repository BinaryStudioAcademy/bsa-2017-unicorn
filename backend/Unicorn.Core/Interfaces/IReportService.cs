using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.DataAccess.Entities;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDTO>> GetAllAsync();
        Task<ReportDTO> GetByIdAsync(long id);
        Task<Report> CreateAsync(ReportDTO contactDto);
        Task UpdateAsync(ReportDTO contactDto);
        Task DeleteAsync(long id);
    }
}
