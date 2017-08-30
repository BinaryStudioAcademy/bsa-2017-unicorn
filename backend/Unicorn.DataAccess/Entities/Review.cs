using System;

using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Review : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public long FromAccountId { get; set; }

        public string From { get; set; }

        public long ToAccountId { get; set; }
        public string To {get; set; }

        public string Avatar { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string WorkName { get; set; }

        public int Grade { get; set; }

        public long BookId { get; set; }
    }
}
