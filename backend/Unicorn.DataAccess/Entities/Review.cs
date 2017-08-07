using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public Vendor Vengor { get; set; }
        public Customer Customer {get; set; }
        public double Grade { get; set; }
        public string Description { get; set; }
    }
}
