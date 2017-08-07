using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    class Account
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public string Avatar { get; set; }

        public int Rating { get; set; }
    }
}
