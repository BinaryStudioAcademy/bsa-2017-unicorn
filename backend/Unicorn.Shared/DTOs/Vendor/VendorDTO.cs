using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Vendor
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public string Avatar { get; set; }

        public string CroppedAvatar { get; set; }

        public double Experience { get; set; }

        public string Position { get; set; }

        public string FIO { get; set; }

        public double Rating { get; set; }

        public virtual CompanyDTO Company { get; set; }

        public virtual PersonDTO Person { get; set; }

        public virtual ICollection<WorkDTO> Works { get; set; }
    }
}
