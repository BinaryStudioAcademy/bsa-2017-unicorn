﻿using System;
using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Account
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public string Avatar { get; set; }

        public int Rating { get; set; }

        
        public virtual Role Role { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}