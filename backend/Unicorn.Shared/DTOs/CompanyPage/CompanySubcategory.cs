using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanySubcategory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual CompanyCategory Category { get; set; }

        public virtual ICollection<CompanyWork> Works { get; set; }
    }
}