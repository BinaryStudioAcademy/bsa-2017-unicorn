using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Core.Services
{
    public class ContactService : IContactService
    {
        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ContactProviderDTO>> GetAllProvidersAsync()
        {
            var providers = await _unitOfWork.ContactProviderRepository.GetAllAsync();

            return providers.Select(p => new ContactProviderDTO()
            {
                Id = p.Id,
                Type = p.Type,
                Name = p.Name
            }).ToList();
        }

        public async Task<ContactShortDTO> GetByIdAsync(long id)
        {
            var contact = await _unitOfWork.ContactRepository.Query
                .Include(c => c.Provider)
                .SingleAsync(c => c.Id == id);

            return new ContactShortDTO()
            {
                Id = contact.Id,
                Provider = contact.Provider.Name,
                ProviderId = contact.Provider.Id,
                Type = contact.Provider.Type,
                Value = contact.Value
            };
        }

        public async Task<ContactShortDTO> CreateAsync(long accountId, ContactShortDTO contactDto)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            var provider = await _unitOfWork.ContactProviderRepository.GetByIdAsync(contactDto.ProviderId);
            var contact = new Contact()
            {
                Account = account,
                IsDeleted = false,
                Provider = provider,
                Value = contactDto.Value
            };

            _unitOfWork.ContactRepository.Create(contact);
            await _unitOfWork.SaveAsync();

            var createdContact = GetByIdAsync(contact.Id);

            return await createdContact;
        }

        public async Task UpdateAsync(long accountId, ContactShortDTO contactDto)
        {
            var contact = await _unitOfWork.ContactRepository.GetByIdAsync(contactDto.Id);

            contact.Value = contactDto.Value;

            _unitOfWork.ContactRepository.Update(contact);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(long accountId, long contactId)
        {
            _unitOfWork.ContactRepository.Delete(contactId);
            await _unitOfWork.SaveAsync();
        }

        private IUnitOfWork _unitOfWork;
    }
}
