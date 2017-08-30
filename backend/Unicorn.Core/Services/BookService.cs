using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Vendor;
using Unicorn.Shared.DTOs.Book;
using System.Data.Entity;
using System;
using Unicorn.Shared.DTOs.Review;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        ILocationService _locationService;
        private INotificationService _notificationService;

        public BookService(IUnitOfWork unitOfWork, ILocationService location, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _locationService = location;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            List<BookDTO> datareturn = new List<BookDTO>();
            foreach (var book in books)
            {
                var bookDto = new BookDTO()
                {
                    Id = book.Id,
                    Date = book.Date,
                    Status = book.Status,
                    Description = book.Description,
                    Work = new WorkDTO()
                    {
                        Id = book.Work.Id,
                        Name = book.Work.Name,
                        Description = book.Work.Description,
                        Subcategory = book.Work.Subcategory.Name,
                        SubcategoryId = book.Work.Subcategory.Id
                    },
                    Customer = new CustomerDTO()
                    {
                        Id = book.Customer.Id,
                        Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, Surname = book.Customer.Person.Surname, Phone = book.Customer.Person.Phone }
                    }
                };

                if (book.Location != null)
                {
                    bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
                }

                if (book.Vendor != null)
                {
                    bookDto.Vendor = new VendorDTO()
                    {
                        Id = book.Vendor.Id,
                        Experience = book.Vendor.Experience
                    };
                }
                if (book.Company != null)
                {
                    bookDto.Company = new CompanyDTO()
                    {
                        Id = book.Company.Id,
                        Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated },
                        Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                    };
                }
                datareturn.Add(bookDto);
            }
            return datareturn;
        }

        public async Task<BookDTO> GetByIdAsync(long id)
        {
            //var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            var book = await _unitOfWork.BookRepository.Query
                .Include(b => b.Location)
                .Include(b => b.Vendor)
                .Include(b => b.Vendor.Person)
                .Include(b => b.Vendor.Person.Account)
                .Include(b => b.Work)
                .Include(b => b.Work.Subcategory)
                .Include(b => b.Customer)
                .Include(b => b.Customer.Person)
                .Include(b => b.Customer.Person.Account)
                .Include(b => b.Company)
                .Include(b => b.Company.Account)
                .SingleAsync(b => b.Id == id);
            var bookDto = new BookDTO()
            {
                Id = book.Id,
                Date = book.Date,
                Status = book.Status,
                Description = book.Description,
                Work = new WorkDTO()
                {
                    Id = book.Work.Id,
                    Name = book.Work.Name,
                    Description = book.Work.Description,
                    Subcategory = book.Work.Subcategory.Name,
                    SubcategoryId = book.Work.Subcategory.Id
                },
                Customer = new CustomerDTO()
                {
                    Id = book.Customer.Id,
                    Person = new PersonDTO() { Id = book.Customer.Person.Id, Name = book.Customer.Person.Name, Surname = book.Customer.Person.Surname, Phone = book.Customer.Person.Phone }
                }
            };

            if (book.Location != null)
            {
                bookDto.Location = new LocationDTO() { Id = book.Location.Id, Adress = book.Location.Adress, City = book.Location.City };
            }

            if (book.Vendor != null)
            {
                bookDto.Vendor = new VendorDTO()
                {
                    Id = book.Vendor.Id,
                    Experience = book.Vendor.Experience
                };
            }
            if (book.Company != null)
            {
                bookDto.Company = new CompanyDTO()
                {
                    Id = book.Company.Id,
                    Account = new AccountDTO() { Id = book.Company.Account.Id, DateCreated = book.Company.Account.DateCreated },
                    Vendors = book.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                };
            }
            return bookDto;
        }

        public async Task Create(BookOrderDTO book)
        {
            // Work work = await _unitOfWork.WorkRepository.GetByIdAsync(book.WorkId);
            Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(book.CustomerId);
            Location location = null;

            if (book.Location.Id == -1)
            {
                location = new Location()
                {
                    IsDeleted = false,
                    City = book.Location.City,
                    Adress = book.Location.Adress,
                    Latitude = book.Location.Latitude,
                    Longitude = book.Location.Longitude,
                    PostIndex = book.Location.PostIndex
                };
            }
            else
            {
                location = await _unitOfWork.LocationRepository.GetByIdAsync(book.Location.Id);
            }

            Company company = null;
            Vendor vendor = null;

            if (book.Profile.ToLower() == "company")
            {
                company = await _unitOfWork.CompanyRepository.GetByIdAsync(book.ProfileId);
            }
            else
            {
                vendor = await _unitOfWork.VendorRepository.Query
                    .Include(v => v.Person)
                    .Include(v => v.Person.Account)
                    .SingleAsync(v => v.Id == book.ProfileId);
            }

            var mockSubcategory = await _unitOfWork.SubcategoryRepository.GetByIdAsync(1);

            Work work = new Work()
            {
                Vendor = vendor,
                Company = company,
                Icon = "http://www.freeiconspng.com/uploads/pictures-icon-11.gif",
                Description = "It\'s temporary mock",
                IsDeleted = false,
                Name = "Mock work",
                Orders = 0,
                Subcategory = mockSubcategory,
            };

            Book _book = new Book()
            {
                IsDeleted = false,
                Company = company,
                Customer = customer,
                CustomerPhone = book.CustomerPhone,
                Date = book.Date,
                Description = book.Description,
                Location = location,
                Status = BookStatus.Pending,
                Vendor = vendor,
                Work = work
            };

            _unitOfWork.BookRepository.Create(_book);
            await _unitOfWork.SaveAsync();

            var notification = new NotificationDTO()
            {
                Title = $"New order for {_book.Work.Name}",
                Description = $"{_book.Customer.Person.Name} {_book.Customer.Person.Surname} booked {_book.Work?.Name}. Check your dashboard to find out details.",
                SourceItemId = _book.Id,
                Time = DateTime.Now,
                Type = NotificationType.TaskNotification
            };

            var receiverId = vendor != null ? vendor.Person.Account.Id : company.Account.Id;
            await _notificationService.CreateAsync(receiverId, notification);
        }

        private int GetRatingByBookId(long id)
        {
            var ratings = _unitOfWork.RatingRepository
                .Query
                .Include(r => r.Book)
                .ToList();

            var rating = ratings
                .Where(r => r.Book != null)
                .FirstOrDefault(r => r.Book.Id == id);

            return rating == null ? 0 : rating.Grade;
        }

        public async Task<IEnumerable<VendorBookDTO>> GetOrdersAsync(string role, long id)
        {
            var query = _unitOfWork.BookRepository
                .Query
                .Include(b => b.Vendor)
                .Include(b => b.Company)
                .Include(b => b.Work)
                .Include(b => b.Work.Subcategory)
                .Include(b => b.Work.Subcategory.Category)
                .Include(b => b.Location)
                .Include(b => b.Customer)
                .Include(b => b.Customer.Person);

            switch (role)
            {
                case "vendor":
                    {
                        query = query
                            .Where(b => b.Vendor != null)
                            .Where(b => b.Vendor.Id == id);
                        break;
                    }
                case "company":
                    {
                        query = query
                            .Where(b => b.Company != null)
                            .Where(b => b.Company.Id == id);
                        break;
                    }
                default: throw new Exception("not supported role");
            }

            var books = await query.ToListAsync();

            return books
                .Select(b => new VendorBookDTO()
                {
                    Id = b.Id,
                    Customer = b.Customer.Person.Name + " " + b.Customer.Person.Surname,
                    CustomerId = b.Customer.Id,
                    CustomerPhone = b.CustomerPhone,
                    Date = b.Date,
                    Description = b.Description,
                    Rating = GetRatingByBookId(b.Id),
                    Review = GetReviewDtoByBookId(b.Id),
                    IsHidden = b.IsHidden,
                    Location = new LocationDTO()
                    {
                        Id = b.Location.Id,
                        Adress = b.Location.Adress,
                        City = b.Location.City,
                        Latitude = b.Location.Latitude,
                        Longitude = b.Location.Longitude
                    },
                    Status = b.Status,
                    Work = new WorkDTO()
                    {
                        Id = b.Work.Id,
                        Name = b.Work.Name,
                        Description = b.Work.Description,
                        Category = b.Work.Subcategory.Category.Name,
                        CategoryId = b.Work.Subcategory.Category.Id,
                        Subcategory = b.Work.Subcategory.Name,
                        SubcategoryId = b.Work.Subcategory.Id,
                        Icon = b.Work.Icon
                    }
                }).ToList();
        }

        public async Task<IEnumerable<CustomerBookDTO>> GetCustomerBooks(long id)
        {
            var books = await _unitOfWork.BookRepository
                .Query
                .Include(b => b.Customer)
                .Include(b => b.Vendor)
                .Include(b => b.Vendor.Person)
                .Include(b => b.Company)
                .Include(b => b.Work)
                .Include(b => b.Work.Subcategory)
                .Include(b => b.Work.Subcategory.Category)
                .Include(b => b.Location)
                .Where(b => b.Customer.Id == id)
                .ToListAsync();

            var customerBooks = books
                .Select(b => BookToCustomerBookDTO(b));

            return customerBooks;
        }

        private ReviewDTO GetReviewDtoByBookId(long id)
        {
            return _unitOfWork.ReviewRepository
                .Query
                .Where(r => r.BookId == id)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Avatar = r.Avatar,
                    BookId = id,
                    Date = r.Date,
                    Description = r.Description,
                    From = r.From,
                    FromAccountId = r.FromAccountId,
                    To = r.To,
                    ToAccountId = r.ToAccountId,
                    Grade = r.Grade,
                    WorkName = r.WorkName
                }).SingleOrDefault();
        }

        private CustomerBookDTO BookToCustomerBookDTO(Book b)
        {
            return new CustomerBookDTO
            {
                Id = b.Id,
                PerformerType = (b.Vendor == null ? "company" : "vendor"),
                Performer = (b.Vendor == null ? b.Company.Name : b.Vendor.Person.Name + " " + b.Vendor.Person.Surname),
                PerformerId = (b.Vendor == null ? b.Company.Id : b.Vendor.Id),
                Date = b.Date,
                Description = b.Description,
                Rating = GetRatingByBookId(b.Id),
                Status = b.Status,
                IsHidden = b.IsHidden,
                Review = GetReviewDtoByBookId(b.Id),
                Location = new LocationDTO
                {
                    Id = b.Location.Id,
                    Adress = b.Location.Adress,
                    City = b.Location.City,
                    Latitude = b.Location.Latitude,
                    Longitude = b.Location.Longitude
                },
                Work = new WorkDTO()
                {
                    Id = b.Work.Id,
                    Name = b.Work.Name,
                    Description = b.Work.Description,
                    Category = b.Work.Subcategory.Category.Name,
                    CategoryId = b.Work.Subcategory.Category.Id,
                    Subcategory = b.Work.Subcategory.Name,
                    SubcategoryId = b.Work.Subcategory.Id,
                    Icon = b.Work.Icon
                }
            };
        }

        public async Task Update(VendorBookDTO bookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);
            book.Status = bookDto.Status;
            book.IsHidden = bookDto.IsHidden;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();

            var notification = new NotificationDTO();
            string performerName = book.Vendor != null ? $"{book.Vendor.Person.Name} {book.Vendor.Person.Surname}" : book.Company.Name;
            long receiverId = book.Customer.Person.Account.Id;

            switch (book.Status)
            {
                case BookStatus.Accepted:
                    notification.Title = "Order accepted";
                    notification.Description = $"{performerName} accepted your order ({book.Work.Name}).";
                    break;
                case BookStatus.Finished:
                    notification.Title = "Work was finished";
                    notification.Description = $"{performerName} finished {book.Work.Name} and waiting for your confirmation.";
                    break;
                case BookStatus.Confirmed:
                    receiverId = book.Vendor != null ? book.Vendor.Person.Account.Id : book.Company.Account.Id;
                    notification.Title = "Work confirmed";
                    notification.Description = $"{book.Work.Name} was confirmed  by {book.Customer.Person.Name} {book.Customer.Person.Surname}.";
                    break;
                case BookStatus.Declined:
                    notification.Title = "Order was declined";
                    notification.Description = $"{performerName} decline your order ({book.Work.Name}).";
                    break;
            }

            notification.Time = DateTime.Now;
            await _notificationService.CreateAsync(receiverId, notification);
        }

        public async Task Update(BookDTO bookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);
            book.Status = bookDto.Status;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<VendorBookDTO>> GetPendingOrdersAsync(string role, long id)
        {
            return await GetOrdersByStatus(role, id, BookStatus.Pending);
        }

        public async Task<IEnumerable<VendorBookDTO>> GetAcceptedOrdersAsync(string role, long id)
        {
            return await GetOrdersByStatus(role, id, BookStatus.Accepted);
        }

        public async Task<IEnumerable<VendorBookDTO>> GetFinishedOrdersAsync(string role, long id)
        {
            var books = await GetOrdersAsync(role, id);
            if (books == null)
            {
                return Enumerable.Empty<VendorBookDTO>();
            }
            return books.Where(b => b.Status == BookStatus.Finished || b.Status == BookStatus.Confirmed);
        }

        private async Task<IEnumerable<VendorBookDTO>> GetOrdersByStatus(string role, long id, BookStatus status)
        {
            var books = await GetOrdersAsync(role, id);
            if (books == null)
            {
                return Enumerable.Empty<VendorBookDTO>();
            }
            return books.Where(b => b.Status == status);
        }
    }
}
