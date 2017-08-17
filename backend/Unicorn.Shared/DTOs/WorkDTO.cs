using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class WorkDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Subcategory { get; set; }

        public long SubcategoryId { get; set; }
    }
}
