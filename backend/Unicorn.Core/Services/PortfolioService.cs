using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

            return PortfolioItemToDTO(item);
        }

        public async Task<IEnumerable<PortfolioItemDTO>> GetItemsByVendorIdAsync(long id)
        {
            var items = await _unitOfWork.PortfolioRepository.Query
                .Include(i => i.Vendor)
                .Include(i => i.Subcategory)
                .Include(i => i.WorkType)
                .Where(i => i.Vendor.Id == id)
                .ToListAsync();

            return items.Select(i => PortfolioItemToDTO(i)).ToList();
        }

        private PortfolioItemDTO PortfolioItemToDTO(PortfolioItem item)
        {
            return new PortfolioItemDTO()
            {
                Id = item.Id,
                HistoryEntryId = item.HistoryEntryId,
                Image = item.Image,
                Category = item.Subcategory.Category.Name,
                WorkType = item.WorkType.Name
            };
        }

        public async Task CreateAsync(long vendorId, PortfolioItemDTO itemDto)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(vendorId);
            var history = await _unitOfWork.HistoryRepository.GetByIdAsync(itemDto.HistoryEntryId);
            var work = await _unitOfWork.WorkRepository.Query
                .Include(w => w.Subcategory)
                .SingleAsync(w => w.Id == history.WorkId);

            var item = new PortfolioItem()
            {
                HistoryEntryId = itemDto.HistoryEntryId,
                Vendor = vendor,
                Image = itemDto.Image,
                Subcategory = work.Subcategory,
                WorkType = work
            };

            _unitOfWork.PortfolioRepository.Create(item);
            await _unitOfWork.SaveAsync();
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
