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
                Contacts = vendor.Contacts.Select(c => ContactDTOConverter.ContactToDTO(c)).ToList(),
                Location = new LocationDTO()
                {
                    Adress = vendor.Person.Location.Adress,
                    City = vendor.Person.Location.City,
                    CoordinateX = vendor.Person.Location.CoordinateX,
                    CoordinateY = vendor.Person.Location.CoordinateY,
                    PostIndex = vendor.Person.Location.PostIndex
                },
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                Id = vendor.Id,
                PortfolioItems = vendor.PortfolioItems.Select(i => PortfolioItemDTOConverter.PortfolioItemToDTO(i)).ToList(),
                Position = vendor.Position,
                WorkLetter = vendor.WorkLetter,
                Works = vendor.Works.Select(w => WorkDTOConverter.WorkToDTO(w)).ToList()
            };
        }
    }
}
