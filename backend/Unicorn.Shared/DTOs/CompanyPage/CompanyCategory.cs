using System.Collections.Generic;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyCategory
    {
        public long Id { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CompanySubcategory> Subcategories { get; set; }
    }
}