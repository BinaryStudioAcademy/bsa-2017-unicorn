using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyWorks
    {
        public long Id { get; set; }

        public virtual ICollection<CompanyWork> Works { get; set; }

        public virtual ICollection<CompanyCategory> AllCategories { get; set; }
    }
}