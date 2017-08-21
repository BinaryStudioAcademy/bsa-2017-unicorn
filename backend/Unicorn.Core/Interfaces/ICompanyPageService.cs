using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyPageService
    {
        Task<ICollection<ShortCompanyDTO>> GetAllCompanies();

        Task<ShortCompanyDTO> GetCompanyShort(long id);

        Task<CompanyDetails> GetCompanyDetails(long id);

        Task SaveCompanyDetails(CompanyDetails companyDTO);

        Task<CompanyReviews> GetCompanyReviews(long id);

        Task SaveCompanyReviews(CompanyReviews companyDTO);

        Task<CompanyVendors> GetCompanyVendors(long id);

        Task SaveCompanyVendors(CompanyVendors companyDTO);

        Task<CompanyContacts> GetCompanyContacts(long id);

        Task SaveCompanyContacts(CompanyContacts companyDTO);
    }
}