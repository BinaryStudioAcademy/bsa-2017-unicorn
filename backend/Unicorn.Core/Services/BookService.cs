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
                var work = await _unitOfWork.WorkRepository.GetByIdAsync(book.Id);
                var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(book.Id);
                var bookDto = new BookDTO()
                {
                    Id = book.Id,
                    Date = book.Date,
                    Status = book.Status,
                    Description = book.Description,
                    Location = await _location.GetByIdAsync(book.Id),
                    Work = new WorkDTO()
                    {
                        Id = work.Id,
                        Name = work.Name,
                        Description = work.Description,
                        Subcategory = new SubcategoryDTO()
                        {
                            Id = subcategory.Id,
                            Name = subcategory.Name,
                            Description = subcategory.Description
                        }
                    }
                };

                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetById(long id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(id);
            var subcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(id);
            var bookDto = new BookDTO()
            {
                Id = book.Id,
                Date = book.Date,
                Status = book.Status,
                Description = book.Description,
                Location = await _location.GetByIdAsync(id),
                Work = new WorkDTO()
                {
                    Id = work.Id,
                    Name = work.Name,
                    Description = work.Description,
                    Subcategory = new SubcategoryDTO()
                    {
                        Id = subcategory.Id,
                        Name = subcategory.Name,
                        Description = subcategory.Description
                    }
                }
            };
            return bookDto;
        }

    }
}
