using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class PortfolioItemDTOConverter
    {
        public static PortfolioItemDTO PortfolioItemToDTO(PortfolioItem item)
        {
            return new PortfolioItemDTO()
            {
                Id = item.Id,
                Category = $"{item.HistoryEntry.CategoryName}, {item.HistoryEntry.CategoryName}",
                Image = item.Image,
                WorkType = item.HistoryEntry.WorkDescription,
                HistoryEntryId = item.HistoryEntry.Id
            };
        }
    }
}
