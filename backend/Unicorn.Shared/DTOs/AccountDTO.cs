using System;
using System.Collections.Generic;

namespace Unicorn.Core.DTOs
{
    public class AccountDTO
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime DateCreated { get; set; }

        public string Avatar { get; set; }

        public double Rating { get; set; }

        public virtual RoleDTO Role { get; set; }

        public virtual ICollection<SocialAccountDTO> SocialAccounts { get; set; }
    }
}
