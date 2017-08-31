using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Customer : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<History> History { get; set; }
    }
}