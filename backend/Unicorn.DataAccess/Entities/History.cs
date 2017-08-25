using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class History : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateFinished { get; set; }

        public string BookDescription { get; set; }

        public long WorkId { get; set; }

        public string WorkDescription { get; set; }

        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }

        public virtual Review Review { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Company Company { get; set; }

       
    }
}