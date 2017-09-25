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
using Unicorn.Shared.DTOs.Email;
using Unicorn.Shared.DTOs.Notification;
using Unicorn.Shared.DTOs.Offer;

namespace Unicorn.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notifyService;
        private readonly IMailService _mailService;

        public OfferService(IUnitOfWork unitOfWork, INotificationService notifyService, IMailService mail)
        {
            _unitOfWork = unitOfWork;
            _notifyService = notifyService;
            _mailService = mail;
        }

        public async Task CreateOffersAsync(IEnumerable<ShortOfferDTO> offers)
        {
            foreach (var offerDto in offers)
            {
                var vendor = await _unitOfWork.VendorRepository
                    .Query
                    .Include(v => v.Person)
                    .Include(v => v.Person.Account)
                    .SingleOrDefaultAsync(v => v.Id == offerDto.VendorId);
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(offerDto.CompanyId);
                var offer = new Offer();
                offer.AttachedMessage = offerDto.AttachedMessage;
                offer.Vendor = vendor;
                offer.Company = company;
                offer.Status = OfferStatus.Pending;

                _unitOfWork.OfferRepository.Create(offer);
                await _unitOfWork.SaveAsync();

                /*Send message*/

                string msg = EmailTemplate.NewOfferTemplate(company.Name, company.Id);
                string receiverEmail = vendor.Person.Account.Email;

                _mailService.Send(new EmailMessage
                {
                    ReceiverEmail = receiverEmail,
                    Subject = "You have a new offer",
                    Body = msg,
                    IsHtml = true
                });

                /*Send notification*/

                var notification = new NotificationDTO()
                {
                    Title = $"New offer from {company.Name}",
                    Description = $"You are offered to join {company.Name} team. Check your dashboard to find out details.",
                    SourceItemId = offer.Id,
                    Time = DateTime.Now,
                    Type = NotificationType.OfferNotification
                };

                var receiverId = vendor.Person.Account.Id;
                await _notifyService.CreateAsync(receiverId, notification);
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetCompanyOffersAsync(long companyId)
        {
            var companiesQuery = await _unitOfWork.OfferRepository
                .Query
                .Include(o => o.Vendor)
                .Include(o => o.Vendor.Person)
                .Include(o => o.Vendor.Person.Account)
                .Include(o => o.Company)
                .Include(o => o.Company.Account)
                .Where(o => o.Company.Id == companyId)
                .ToListAsync();

            var vendors = companiesQuery
                .Select(o => OfferToDto(o));

            return vendors;
        }

        public async Task<IEnumerable<OfferDTO>> GetVendorOffersAsync(long vendorId)
        {
            var vendorsQuery = await _unitOfWork.OfferRepository
                .Query
                .Include(o => o.Vendor)
                .Include(o => o.Vendor.Person)
                .Include(o => o.Vendor.Person.Account)
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
                .Include(o => o.Company.Account)
                .Include(o => o.Vendor)
                .Include(o => o.Vendor.Person)
                .SingleOrDefaultAsync(o => o.Id == offerDto.Id);

            if (offer == null)
            {
                throw new Exception();
            }

            if (!string.IsNullOrEmpty(offerDto.DeclinedMessage))
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

            var status = offer.Status == OfferStatus.Accepted ? "accepted" : "declined";

            /*Send message*/

            string msg = EmailTemplate.OfferStatusChanged(offer.Vendor.Person.Name, status, offer.Company.Id, offer.Vendor.Id);
            string receiverEmail = offer.Company.Account.Email;

            _mailService.Send(new EmailMessage
            {
                ReceiverEmail = receiverEmail,
                Subject = "Offer status changed",
                Body = msg,
                IsHtml = true
            });

            /*Send notification*/

            var notification = new NotificationDTO()
            {
                Title = $"Offer status changed",
                Description = $"{offer.Vendor.Person.Name} {status} your offer. Check your vendors page to find out details.",
                SourceItemId = offer.Id,
                Time = DateTime.Now,
                Type = NotificationType.OfferNotification
            };

            var receiverId = offer.Company.Account.Id;
            await _notifyService.CreateAsync(receiverId, notification);
        }

        public async Task DeleteOfferAsync(long id)
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(id);
            offer.IsDeleted = true;
            _unitOfWork.OfferRepository.Update(offer);

            await _unitOfWork.SaveAsync();
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
                },
                Vendor = new Shared.DTOs.Vendor.VendorDTO
                {
                    Id = offer.Vendor.Id,
                    Avatar = offer.Vendor.Person.Account.Avatar,
                    FIO = offer.Vendor.Person.Name,
                    Experience = offer.Vendor.Experience,
                    Position = offer.Vendor.Position,
                    Rating = CalculateAverageRating(offer.Vendor.Person.Account.Id)
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
