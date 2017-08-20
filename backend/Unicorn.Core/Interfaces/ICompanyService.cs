using System.Collections;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable> GetAllCompaniesAsync();
        Task<CompanyDTO> GetCompanyByIdAsync(long id);
        Task SaveCompany(ShortCompanyDTO companyDTO);
        Task Create(CompanyRegisterDTO companyDto);
    }
}