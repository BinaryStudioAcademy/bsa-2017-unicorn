using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class ReviewDTO
    {
        public long Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Grade { get; set; }

        public string Description { get; set; }
    }
}
