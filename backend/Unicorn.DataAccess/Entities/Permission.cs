using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
