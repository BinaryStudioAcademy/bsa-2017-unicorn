using System.Collections.Generic;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyContacts
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public ICollection<ContactShortDTO> Contacts { get; set; }

        public LocationDTO Location { get; set; }
    }
}