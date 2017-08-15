using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public string FIO { get; set; }

        public string AvatarUrl { get; set; }

        public LocationDTO Location { get; set; }

        public double Experience { get; set; }

        public string WorkLetter { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }

        public ICollection<ContactDTO> Contacts { get; set; }

        public ICollection<PortfolioItemDTO> PortfolioItems { get; set; }

        public CompanyDTO Company { get; set; }

        public ICollection<WorkDTO> Works { get; set; }
    }
}
