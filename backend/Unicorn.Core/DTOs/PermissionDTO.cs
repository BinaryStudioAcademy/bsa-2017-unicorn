using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class PermissionDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AccountDTO> Accounts { get; set; }
    }
}
