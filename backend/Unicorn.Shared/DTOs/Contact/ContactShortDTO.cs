using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Contact
{
    public class ContactShortDTO
    {
        public long Id { get; set; }
        public string Provider { get; set; }
        public long ProviderId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
