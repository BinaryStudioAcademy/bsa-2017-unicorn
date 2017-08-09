using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }


        public virtual PersonDTO Person { get; set; }

        public virtual ICollection<BookDTO> Books { get; set; }
    }
}
