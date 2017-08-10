using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs
{
    public class SocialAccountDTO
    {
        public long Id { get; set; }
        public long FacebookUID { get; set; }
        public long GoogleUID { get; set; }
    }
}
