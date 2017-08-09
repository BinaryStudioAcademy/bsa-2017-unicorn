using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public double Experience { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }


        public virtual CompanyDTO Company { get; set; }

        public virtual PersonDTO Person { get; set; }

        public virtual ICollection<WorkDTO> Works { get; set; }
    }
}
