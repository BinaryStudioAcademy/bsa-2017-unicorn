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

        public Company Company { get; set; }

        public Person Person { get; set; }

        public ICollection<Work> Works { get; set; }

        public ICollection<PortfolioItem> PortfolioItems { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}