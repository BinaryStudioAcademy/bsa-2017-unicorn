using System.Collections.Generic;

namespace Unicorn.Core.DTOs
{
    public class WorkDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SubcategoryDTO Subcategory { get; set; }

        public ICollection<VendorDTO> Vendors { get; set; }
    }
}
