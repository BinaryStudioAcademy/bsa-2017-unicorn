using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class PortfolioItem : IEntity
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public Subcategory Subcategory { get; set; }
        public Work WorkType { get; set; }

        public long HistoryEntryId { get; set; }
        public Vendor Vendor { get; set; }

        public bool IsDeleted { get; set; }
    }
}
