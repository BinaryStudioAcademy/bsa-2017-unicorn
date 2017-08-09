using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Permission : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
