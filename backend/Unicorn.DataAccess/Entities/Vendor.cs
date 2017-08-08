using System.Collections.Generic;

namespace Unicorn.DataAccess.Entities
{
    public class Vendor
    {
        public long Id { get; set; }

        public double Experience { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }


        public virtual Company Company { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}