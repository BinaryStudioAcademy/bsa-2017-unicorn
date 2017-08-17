using System.Collections;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable> GetAllCompaniesAsync();
        Task<object> GetCompanyByIdAsync(long id);
        Task Create(CompanyRegisterDTO companyDto);
    }
}