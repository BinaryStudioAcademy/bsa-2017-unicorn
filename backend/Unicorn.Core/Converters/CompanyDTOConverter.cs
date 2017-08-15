using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class CompanyDTOConverter
    {
        public static CompanyDTO CompanyToDTO(Company company)
        {
            return new CompanyDTO()
            {
                Id = company.Id,
                Account = new AccountDTO() { Id = company.Account.Id, DateCreated = company.Account.DateCreated, Rating = company.Account.Rating },
                Staff = company.Staff,
                Vendors = company.Vendors.Select(v => VendorDTOConverter.VendorToDTO(v))
            };
        }
    }
}
