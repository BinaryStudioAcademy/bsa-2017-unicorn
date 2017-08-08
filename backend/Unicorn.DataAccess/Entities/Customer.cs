using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Customer
    {
        public long Id { get; set; }


        public virtual Person Person { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}