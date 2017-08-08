using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    public class Subcategory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public virtual Category Category { get; set; }

        public ICollection<Work> Works { get; set; }
    }
}
