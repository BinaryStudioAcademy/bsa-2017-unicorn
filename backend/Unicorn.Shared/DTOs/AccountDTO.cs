using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class AccountDTO
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime DateCreated { get; set; }

        public string Avatar { get; set; }

        public int Rating { get; set; }

        public virtual RoleDTO Role { get; set; }

        public virtual ICollection<SocialAccountDTO> SocialAccounts { get; set; }

        public virtual ICollection<PermissionDTO> Permissions { get; set; }
    }
}
