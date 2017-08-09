using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class CompanyDTO
    {
        public long Id { get; set; }

        public DateTime FoundationDate { get; set; }

        public int Staff { get; set; }


        public virtual AccountDTO Account { get; set; }

        public virtual LocationDTO Location { get; set; }

        public virtual ICollection<VendorDTO> Vendors { get; set; }
    }
}
