using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyService
    {
        Task Create(CompanyRegisterDTO companyDto);
    }
}