using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Company;
using Unicorn.Shared.DTOs.CompanyPage;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Core.Interfaces
{
    public interface ICompanyPageService
    {
        Task<ICollection<ShortCompanyDTO>> GetAllCompanies();


        Task<ShortCompanyDTO> GetCompanyShort(long id);
        
        Task<CompanyDetails> GetCompanyDetails(long id);

        Task SaveCompanyDetails(CompanyDetails companyDetailsDTO);



        Task<CompanyReviews> GetCompanyReviews(long id);

        Task AddCompanyReviews(CompanyReviews companyReviewsDTO);



        Task<CompanyVendors> GetCompanyVendors(long id);

        Task AddCompanyVendors(CompanyVendors companyVendorsDTO);

        Task DeleteCompanyVendor(long companyId, long vendorId);



        Task<CompanyContacts> GetCompanyContacts(long id);

        Task SaveCompanyContact(ContactShortDTO companyContactDTO);

        Task AddCompanyContact(long companyId, ContactShortDTO companyContactDTO);

        Task DeleteCompanyContact(long companyId, long contactId);



        Task<CompanyWorks> GetCompanyWorks(long id);

        Task SaveCompanyWork(CompanyWork companyWorkDTO);

        Task AddCompanyWork(long companyId, CompanyWork companyWorkDTO);

        Task DeleteCompanyWork(long companyId, long workId);



        Task<CompanyBooks> GetCompanyBooks(long id);

        Task SaveCompanyBook(CompanyBook companyBookDTO);

        

        Task<long> GetCompanyAccountId(long id);

        Task<ICollection<CompanyDetails>> GetSearchCompanies(string category, string subcategory, int? date);

    }
}