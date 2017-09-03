using System;

using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Review : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public Account Sender { get; set; }

        public long ToAccountId { get; set; }
        public string To {get; set; }

        public DateTimeOffset Date { get; set; }

        public string Description { get; set; }

        public string WorkName { get; set; }

        public int Grade { get; set; }

        public long BookId { get; set; }
    }
}
