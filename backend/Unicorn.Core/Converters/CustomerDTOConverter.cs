using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class CustomerDTOConverter
    {
        public static CustomerDTO CustomerToDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                Id = customer.Id,
                Person = new PersonDTO() { Id = customer.Person.Id, Name = customer.Person.Name, SurnameName = customer.Person.Surname, Phone = customer.Person.Phone }
            };
        }
    }
}
