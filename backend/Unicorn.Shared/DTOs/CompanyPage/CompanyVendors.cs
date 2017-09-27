using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyVendors
    {
        public long Id { get; set; }

        public ICollection<CompanyVendor> Vendors { get; set; }

        public ICollection<CompanyVendor> AllVendors { get; set; }
    }
}