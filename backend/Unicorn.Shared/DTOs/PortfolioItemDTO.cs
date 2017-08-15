namespace Unicorn.Shared.DTOs
{
    public class PortfolioItemDTO
    {
        public long Id { get; set; }
        public long HistoryEntryId { get; set; }

        public string Image { get; set; }
        public string Category { get; set; }
        public string WorkType { get; set; }
    }
}
