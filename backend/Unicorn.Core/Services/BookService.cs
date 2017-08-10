using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDTO>());
            return Mapper.Map<IEnumerable<Book>, List<BookDTO>>(await _unitOfWork.BookRepository.GetAllAsync());
        }

        public async Task<BookDTO> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var bookDto = new BookDTO() { Id = book.Id, Date = book.Date, Status = book.Status, Description = book.Description };
            return bookDto;
        }

    }
}
