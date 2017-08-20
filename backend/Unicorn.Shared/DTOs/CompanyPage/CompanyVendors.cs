using System.Collections.Generic;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyVendors
    {
        public long Id { get; set; }

        public ICollection<CompanyVendor> Vendors { get; set; }
    }
}