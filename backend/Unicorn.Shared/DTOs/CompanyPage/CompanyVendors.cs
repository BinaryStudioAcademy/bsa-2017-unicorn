using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyVendors
    {
        public long Id { get; set; }

        public ICollection<Task<CompanyVendor>> Vendors { get; set; }

        public ICollection<Task<CompanyVendor>> AllVendors { get; set; }
    }
}