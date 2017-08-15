using System.Linq;

using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class BookDTOConverter
    {
        public static BookDTO BookToDTO(Book book)
        {
            return new BookDTO()
            {
                Id = book.Id,
                Date = book.Date,
                Status = book.Status,
                Description = book.Description,
                Work = WorkDTOConverter.WorkToDTO(book.Work),
                Customer = CustomerDTOConverter.CustomerToDTO(book.Customer)
            };
        }
    }
}
