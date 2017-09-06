using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs
{
    public class ShortProfileInfoDTO
    {
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string CroppedAvatar { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
