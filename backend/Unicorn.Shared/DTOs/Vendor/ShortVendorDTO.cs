using System;
using System.Collections.Generic;

using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Shared.DTOs.Vendor
{
    public class ShortVendorDTO
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string Avatar { get; set; }

        public string CroppedAvatar { get; set; }

        public string Background { get; set; }

        public string City { get; set; }

        public LocationDTO Location { get; set; }

        public double Experience { get; set; }

        public string WorkLetter { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public long? CompanyId { get; set; }

        public DateTime Birthday { get; set; }

        public ICollection<ContactShortDTO> Contacts { get; set; }
    }
}
