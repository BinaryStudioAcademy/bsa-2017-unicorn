using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Book : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public Customer Customer { get; set; }

        public string CustomerPhone { get; set; }

        public Vendor Vendor { get; set; }

        public Company Company { get; set; } 

        public Work Work { get; set; }

        public Location Location { get; set; }
    }

    public enum BookStatus
    {
        Accepted,
        InProgress,
        Finished,
        Confirmed
    }
}