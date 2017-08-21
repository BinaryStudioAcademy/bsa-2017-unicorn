using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyWorks
    {
        public long Id { get; set; }

        public ICollection<CompanyWork> Works { get; set; }

        public ICollection<CompanyCategory> AllCategories { get; set; }
    }
}