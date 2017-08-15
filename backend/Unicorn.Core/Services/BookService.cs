using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Converters;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILocationService _location;

        public BookService(IUnitOfWork unitOfWork, ILocationService location)
        {
            _unitOfWork = unitOfWork;
            _location = location;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            List<BookDTO> datareturn = new List<BookDTO>();
            foreach (var book in books)
            {
                var bookDto = BookDTOConverter.BookToDTO(book);

                if (book.Location != null)
                {
                    bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
                }

                if (book.Vendor != null)
                {
                    bookDto.Vendor = VendorDTOConverter.VendorToDTO(book.Vendor);
                }
                if (book.Company != null)
                {
                    bookDto.Company = CompanyDTOConverter.CompanyToDTO(book.Company);
                }
                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetById(long id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var bookDto = BookDTOConverter.BookToDTO(book);

            if (book.Location != null)
            {
                bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
            }

            if (book.Vendor != null)
            {
                bookDto.Vendor = VendorDTOConverter.VendorToDTO(book.Vendor);
            }
            if (book.Company != null)
            {
                bookDto.Company = CompanyDTOConverter.CompanyToDTO(book.Company);
            }
            return bookDto;
        }
    }
}
