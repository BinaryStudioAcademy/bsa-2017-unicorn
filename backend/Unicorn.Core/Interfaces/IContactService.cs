using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Core.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactProviderDTO>> GetAllProvidersAsync();
        Task<ContactShortDTO> GetByIdAsync(long id);
        Task<ContactShortDTO> CreateAsync(long accountId, ContactShortDTO contactDto);
        Task UpdateAsync(long accountId, ContactShortDTO contactDto);
        Task RemoveAsync(long accountId, long contactId);
    }
}
