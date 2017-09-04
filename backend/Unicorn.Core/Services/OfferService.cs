using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOffersAsync(IEnumerable<ShortOfferDTO> offers)
        {
            foreach (var offer in offers)
            {
                await CreateOfferAsync(offer);
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetOffersAsync(long vendorId)
        {
            var vendorsQuery = await _unitOfWork.OfferRepository
                .Query
                .Include(o => o.Vendor)
                .Include(o => o.Company)
                .Include(o => o.Company.Account)
                .Where(o => o.Vendor.Id == vendorId && o.Status == OfferStatus.Pending)
                .ToListAsync();

            var vendors = vendorsQuery
                .Select(o => OfferToDto(o));

            return vendors;
        }

        public async Task UpdateOfferAsync(OfferDTO offerDto)
        {
            var offer = await _unitOfWork.OfferRepository
                .Query
                .Include(o => o.Company)
                .Include(o => o.Vendor)
                .SingleOrDefaultAsync(o => o.Id == offerDto.Id);
            if (offer == null)
            {
                throw new Exception();
            }
            if (string.IsNullOrEmpty(offerDto.DeclinedMessage))
            {
                offer.DeclinedMessage = offerDto.DeclinedMessage;
            }
            offer.Status = offerDto.Status;
            _unitOfWork.OfferRepository.Update(offer);
            await _unitOfWork.SaveAsync();
            if (offer.Status == OfferStatus.Accepted)
            {
                await AcceptVendorAsync(offer);
            }
        }

        private async Task AcceptVendorAsync(Offer offer)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(offer.Company.Id);
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(offer.Vendor.Id);
            company.Vendors.Add(vendor);
            vendor.Company = company;
            _unitOfWork.VendorRepository.Update(vendor);
            _unitOfWork.CompanyRepository.Update(company);
            await _unitOfWork.SaveAsync();
        }

        private async Task CreateOfferAsync(ShortOfferDTO offerDto)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(offerDto.VendorId);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(offerDto.CompanyId);
            var offer = new Offer();
            offer.AttachedMessage = offerDto.AttachedMessage;
            offer.Vendor = vendor;
            offer.Company = company;
            offer.Status = OfferStatus.Pending;

            _unitOfWork.OfferRepository.Create(offer);
            await _unitOfWork.SaveAsync();
        }

        private OfferDTO OfferToDto(Offer offer)
        {
            return new OfferDTO
            {
                Id = offer.Id,
                AttachedMessage = offer.AttachedMessage,
                Status = offer.Status,
                DeclinedMessage = offer.DeclinedMessage,
                Company = new CompanyDTO
                {
                    Id = offer.Company.Id,
                    Avatar = offer.Company.Account.Avatar,
                    Name = offer.Company.Name,
                    Rating = CalculateAverageRating(offer.Company.Account.Id)
                }
            };
        }

        private double CalculateAverageRating(long receiverId)
        {
            var select = _unitOfWork.RatingRepository.Query
                .Where(p => p.Reciever.Id == receiverId).Select(z => z.Grade);
            return select.Any() ? select.Average() : 0;

        }

        
    }
}
