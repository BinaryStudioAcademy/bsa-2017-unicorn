using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
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
