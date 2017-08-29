using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Review
{
    public class ShortReviewDTO
    {
        public long BookId { get; set; }

        public long PerformerId { get; set; }

        public string PerformerType { get; set; }

        public string Text { get; set; }

        public int Grade { get; set; }
       
    }
}
