﻿using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class RoleDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AccountDTO> Accounts { get; set; }
    }
}