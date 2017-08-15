using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }

        public virtual PersonDTO Person { get; set; }

        public virtual ICollection<BookDTO> Books { get; set; }
    }
}
