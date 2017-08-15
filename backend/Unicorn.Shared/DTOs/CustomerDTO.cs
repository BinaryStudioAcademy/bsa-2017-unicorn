using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }

        public PersonDTO Person { get; set; }

        public ICollection<BookDTO> Books { get; set; }
    }
}
