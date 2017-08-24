using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class WorkDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Category { get; set; }

        public long CategoryId { get; set; }

        public string Subcategory { get; set; }

        public long SubcategoryId { get; set; }

        public long VendorId { get; set; }

        public long CompanyId { get; set; }
    }
}
