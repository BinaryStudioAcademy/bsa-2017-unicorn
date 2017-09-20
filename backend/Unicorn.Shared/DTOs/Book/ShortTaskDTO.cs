using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Book
{
    public class ShortTaskDTO
    {
        public long Id { get; set; }

        public long BookId { get; set; }

        public string Description { get; set; }

        public long WorkId { get; set; }

        public long VendorId { get; set; }
    }
}
