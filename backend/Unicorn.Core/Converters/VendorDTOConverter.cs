using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class VendorDTOConverter
    {
        public static VendorDTO VendorToDTO(Vendor vendor)
        {
            return new VendorDTO()
            {
                FIO = $"{vendor.Person.Name} {vendor.Person.Surname}",
                AvatarUrl = vendor.Person.Account.Avatar,
                Contacts = vendor.Contacts.Select(c => ContactDTOConverter.ContactToDTO(c)),
                CoordinateX = vendor.Person.Location.CoordinateX,
                CoordinateY = vendor.Person.Location.CoordinateY,
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                Id = vendor.Id,
                PortfolioItems = vendor.PortfolioItems.Select(i => PortfolioItemDTOConverter.PortfolioItemToDTO(i)),
                Position = vendor.Position,
                WorkLetter = vendor.WorkLetter,
                Works = vendor.Works.Select(w => WorkDTOConverter.WorkToDTO(w))
            };
        }
    }
}
