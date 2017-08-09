using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class AccountDTO
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public string Avatar { get; set; }

        public int Rating { get; set; }


        public virtual RoleDTO Role { get; set; }

        public virtual ICollection<PermissionDTO> Permissions { get; set; }
    }
}
