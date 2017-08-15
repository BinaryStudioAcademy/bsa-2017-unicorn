using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class ContactDTOConverter
    {
        public static ContactDTO ContactToDTO(Contact contact)
        {
            return new ContactDTO()
            {
                Id = contact.Id,
                Type = contact.Type,
                Value = contact.Value
            };
        }
    }
}
