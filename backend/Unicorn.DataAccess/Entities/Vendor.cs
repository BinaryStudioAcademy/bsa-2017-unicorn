using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Vendor : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public double Experience { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }

        public string WorkLetter { get; set; }

        public virtual Company Company { get; set; }

        public virtual Person Person { get; set; }

        public virtual Calendar Calendar { get; set; }

        public virtual ICollection<Work> Works { get; set; }

        public virtual ICollection<PortfolioItem> PortfolioItems { get; set; }
    }
}