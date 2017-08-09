using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class WorkDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual SubcategoryDTO Subcategory { get; set; }

        public virtual ICollection<VendorDTO> Vendors { get; set; }
    }
}
