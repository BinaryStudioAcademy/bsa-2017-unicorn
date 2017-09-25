namespace Unicorn.Shared.DTOs.Book
{
    public class ShortTaskDTO
    {
        public long Id { get; set; }

        public long BookId { get; set; }

        public string Description { get; set; }

        public long WorkId { get; set; }

        public long VendorId { get; set; }
    }
}
