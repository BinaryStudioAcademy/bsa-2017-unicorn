using System.Collections.Generic;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities 
{
    public class Role : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public RoleType Type { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
