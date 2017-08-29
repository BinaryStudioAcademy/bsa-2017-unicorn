using System;

namespace Unicorn.Shared.DTOs
{
    public class ReviewDTO
    {
        public long Id { get; set; }

        public string Avatar { get; set; }

        public long FromAccountId { get; set; }

        public string From { get; set; }

        public long ToAccountId { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public long BookId { get; set; }

        public int Grade { get; set; }
    }
}
