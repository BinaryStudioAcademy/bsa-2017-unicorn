using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
