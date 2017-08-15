using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class PortfolioService : IPortfolioService
    {
        public PortfolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PortfolioItemDTO> GetItemByIdAsync(long id)
        {
            var item = await _unitOfWork.PortfolioRepository.GetByIdAsync(id);

            return await PortfolioItemToDTO(item);
        }

        public Task<IEnumerable<PortfolioItemDTO>> GetItemsByVendorIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        private async Task<PortfolioItemDTO> PortfolioItemToDTO(PortfolioItem item)
        {
            var historyEntry = await _unitOfWork.HistoryRepository.GetByIdAsync(item.HistoryEntryId);

            return new PortfolioItemDTO()
            {
                Id = item.Id,
                Category = historyEntry.CategoryName,
                WorkType = historyEntry.WorkDescription,
                HistoryEntryId = item.HistoryEntryId,
                Image = item.Image
            };
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
