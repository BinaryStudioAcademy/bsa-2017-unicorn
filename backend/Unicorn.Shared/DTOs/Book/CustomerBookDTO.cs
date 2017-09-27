using System;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Review;

namespace Unicorn.Shared.DTOs.Book
{
    public class CustomerBookDTO
    {
        public long Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public string DeclinedReason { get; set; }

        public string Performer { get; set; }

        public long PerformerId { get; set; }

        public string PerformerType { get; set; }

        public int Rating { get; set; }

        public bool IsHidden { get; set; }

        public WorkDTO Work { get; set; }

        public ReviewDTO Review { get; set; }

        public LocationDTO Location { get; set; }
    }
}
