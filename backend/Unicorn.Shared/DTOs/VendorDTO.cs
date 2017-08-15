using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public string FIO { get; set; }

        public string AvatarUrl { get; set; }

        public double CoordinateX { get; set; }

        public double CoordinateY { get; set; }

        public double Experience { get; set; }

        public string WorkLetter { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }

        public long CompanyId { get; set; }

        public string Company { get; set; }

        public virtual IEnumerable<WorkDTO> Works { get; set; }

        public IEnumerable<ContactDTO> Contacts { get; set; }

        public IEnumerable<PortfolioItemDTO> PortfolioItems { get; set; }
    }
}
