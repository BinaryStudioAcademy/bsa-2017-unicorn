using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public string FIO { get; set; }

        public string Avatar { get; set; }

        public string City { get; set; }

        public long LocationId { get; set; }

        public double Experience { get; set; }

        public string WorkLetter { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public long? CompanyId { get; set; }
    }
}
