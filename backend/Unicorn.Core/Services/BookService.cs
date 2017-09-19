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
using Unicorn.Shared.DTOs.Email;

namespace Unicorn.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocationService _locationService;
        private readonly INotificationService _notificationService;
        private readonly IReviewService _reviewService;
        private readonly IMailService _mailService;

        public BookService(IUnitOfWork unitOfWork, ILocationService location, INotificationService notificationService, IReviewService reviewService, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _locationService = location;
            _notificationService = notificationService;
            _mailService = mailService;
            _reviewService = reviewService;
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
                    EndDate = book.Date,
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
                EndDate = book.EndDate,
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
            Work work = await _unitOfWork.WorkRepository.GetByIdAsync(book.WorkId);
            Customer customer = await _unitOfWork.CustomerRepository.GetByIdAsync(book.CustomerId);
            Location location = null;

            work.Orders = work.Orders + 1;
            _unitOfWork.WorkRepository.Update(work);

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

            Book _book = new Book()
            {
                IsDeleted = false,
                Company = company,
                Customer = customer,
                CustomerPhone = book.CustomerPhone,
                Date = book.Date,
                EndDate = book.EndDate,
                Description = book.Description,
                Location = location,
                Status = BookStatus.Pending,
                Vendor = vendor,
                Work = work
            };

            _unitOfWork.BookRepository.Create(_book);
            await _unitOfWork.SaveAsync();

            /* Send Notification */
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


            /* Send Message */
            string msg = EmailTemplate.NewOrderTemplate(_book.Customer.Person.Name, _book.Customer.Person.Surname, _book.Work?.Name, book.CustomerId);
            string receiverEmail = vendor != null ? vendor.Person.Account.Email : company.Account.Email;
            _mailService.Send(new EmailMessage
            {
                ReceiverEmail = receiverEmail,
                Subject = "You have a new order",
                Body = msg,
                IsHtml = true
            });
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
                            .Where(b => b.Company != null && !b.IsCompanyTask)
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
                    EndDate = b.EndDate,
                    Description = b.Description,
                    Rating = GetRatingByBookId(b.Id),
                    Review = _reviewService.GetByBookId(b.Id),
                    DeclinedReason = b.DeclinedReason,
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
                        Icon = string.IsNullOrEmpty(b.Work.Icon) ? b.Work.Subcategory.Category.Icon : b.Work.Icon
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
                .Where(b => b.Customer.Id == id && !b.IsCompanyTask)
                .ToListAsync();

            var customerBooks = books
                .Select(b => BookToCustomerBookDTO(b));

            return customerBooks;
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
                EndDate = b.EndDate,
                Description = b.Description,
                Rating = GetRatingByBookId(b.Id),
                Status = b.Status,
                IsHidden = b.IsHidden,
                Review = _reviewService.GetByBookId(b.Id),
                DeclinedReason = b.DeclinedReason,
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
                    Icon = string.IsNullOrEmpty(b.Work.Icon) ? b.Work.Subcategory.Category.Icon : b.Work.Icon 
                }
            };
        }

        public async Task Update(VendorBookDTO bookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);

            var isStatusChanged = !(bookDto.Status == book.Status);

            book.Status = bookDto.Status;
            book.IsHidden = bookDto.IsHidden;
            if (bookDto.DeclinedReason != null)
            {
                book.DeclinedReason = bookDto.DeclinedReason;
            }

            _unitOfWork.BookRepository.Update(book);
            await CheckBooks(book);
            await _unitOfWork.SaveAsync();

            if (isStatusChanged)
            {               
                /* Send Notification */
                var notification = new NotificationDTO();
                string performerName = book.Vendor != null ? $"{book.Vendor.Person.Name} {book.Vendor.Person.Surname}" : book.Company.Name;
                long receiverId = book.Customer.Person.Account.Id;

                string newBookStatus = null;
                switch (book.Status)
                {
                    case BookStatus.Accepted:                        
                        VendorBookDTO _event = new VendorBookDTO
                        {
                            Status = bookDto.Status,
                            Customer = bookDto.Customer,
                            Date = bookDto.Date,
                            EndDate = bookDto.EndDate,
                            Description = bookDto.Description,
                            Work = new WorkDTO
                            {
                                Id = bookDto.Work.Id,
                                Icon = bookDto.Work.Icon,
                                Name = bookDto.Work.Name
                            }
                        };
                        await _notificationService.CreateAsync(receiverId, _event);
                        notification.Title = newBookStatus = "Order accepted";
                        notification.Description = $"{performerName} accepted your order ({book.Work.Name}).";
                        break;
                    case BookStatus.Finished:
                        notification.Title = newBookStatus = "Work was finished";
                        notification.Description = $"{performerName} finished {book.Work.Name} and waiting for your confirmation.";
                        break;
                    case BookStatus.Confirmed:
                        receiverId = book.Vendor != null ? book.Vendor.Person.Account.Id : book.Company.Account.Id;
                        notification.Title = "Work confirmed";
                        notification.Description = $"{book.Work.Name} was confirmed  by {book.Customer.Person.Name} {book.Customer.Person.Surname}.";
                        break;
                    case BookStatus.Declined:
                        notification.Title = newBookStatus = "Order was declined";
                        notification.Description = $"{performerName} decline your order ({book.Work.Name}).";
                        break;
                }

                notification.Time = DateTime.Now;
                await _notificationService.CreateAsync(receiverId, notification);

                /* Send Message */
                //if (book.Status != BookStatus.Confirmed) // Only for customer events
                //{
                //    string msg = EmailTemplate.OrderStatusChanged(book.Work.Name, newBookStatus, book.Customer.Id);
                //    string receiverEmail = book.Vendor != null ? book.Vendor.Person.Account.Email : book.Company.Account.Email;
                //    _mailService.Send(new EmailMessage
                //    {
                //        ReceiverEmail = receiverEmail,
                //        Subject = "Work status changed",
                //        Body = msg,
                //        IsHtml = true
                //    });
                //}
            }
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


        private void CheckBooks(List<Book> _books, ref IEnumerable<VendorBookDTO> books)
        {
            foreach (Book _book in _books)
            {
                foreach (Book book in _books)
                {
                    if (book.Id != _book.Id)
                        if ((_book.Status == BookStatus.Pending && book.Status != BookStatus.Pending
                        && book.Status != BookStatus.Declined && book.Status != BookStatus.Finished
                        && book.Status != BookStatus.Confirmed
                        && ((_book.Date.Date >= book.Date.Date && _book.Date.Date <= book.EndDate.Date)
                        || (_book.EndDate.Date <= book.EndDate.Date && _book.EndDate.Date >= book.Date.Date)))
                        || (_book.Status == BookStatus.Pending
                        && (book.Status == BookStatus.Accepted || book.Status == BookStatus.InProgress)
                        && ((book.Date.Date >= _book.Date.Date && book.Date.Date <= _book.EndDate.Date)
                        || (book.EndDate.Date <= _book.EndDate.Date && book.EndDate.Date >= _book.Date.Date))))
                        {
                            foreach (var b in books)
                            {
                                if (b.Id == _book.Id)
                                {
                                    b.MoreTasksPerDay = true;
                                }
                            }
                        }
                }
            }
        }


        private async Task<IEnumerable<VendorBookDTO>> GetOrdersByStatus(string role, long id, BookStatus status)
        {
            var booksDTO = await GetOrdersAsync(role, id);            

            List<Book> _booksEntity = new List<Book>();


            if(role == "vendor")
            {
                var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
                if (vendor != null && vendor.Calendar != null && !vendor.Calendar.SeveralTaskPerDay)
                {
                    _booksEntity = _unitOfWork.BookRepository.Query.Where(x => x.Vendor.Id == id).ToList();
                    CheckBooks(_booksEntity, ref booksDTO);
                }
            }
            else if(role == "company")
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
                if (company != null && company.Calendar != null && !company.Calendar.SeveralTaskPerDay)
                {
                    _booksEntity = _unitOfWork.BookRepository.Query.Where(x => x.Company.Id == id).ToList();
                    CheckBooks(_booksEntity, ref booksDTO);
                }
            }          

            if (booksDTO == null)
            {
                return Enumerable.Empty<VendorBookDTO>();
            }
            return booksDTO.Where(b => b.Status == status);
        }

        public async Task DeleteBook(long id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            book.IsDeleted = true;
            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateTasks(List<ShortTaskDTO> tasks, long companyId)
        {
            foreach (var task in tasks)
            {
                await CreateTask(task, companyId);
            }
        }

        private async Task CreateTask(ShortTaskDTO task, long companyId)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(task.VendorId);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var book = await _unitOfWork.BookRepository.GetByIdAsync(task.BookId);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(task.WorkId);

            var companyBook = new Book();
            companyBook.Date = book.Date;
            companyBook.EndDate = book.EndDate;
            companyBook.Description = task.Description;
            companyBook.Status = BookStatus.Pending;
            companyBook.IsCompanyTask = true;
            companyBook.Vendor = vendor;
            companyBook.Location = book.Location;
            companyBook.Customer = book.Customer;
            companyBook.CustomerPhone = book.CustomerPhone;
            companyBook.ParentBookId = book.Id;
            companyBook.Work = work;
            companyBook.Company = company;

            _unitOfWork.BookRepository.Create(companyBook);

            await _unitOfWork.SaveAsync();
        }

        private async Task CheckBooks(Book book)
        {
            if (!book.IsCompanyTask)
            {
                return;
            }
            if (book.Status == BookStatus.Declined)
            {

            }
            else
            {
                var taskBooks = _unitOfWork.BookRepository.Query.Where(b => b.ParentBookId == book.ParentBookId).ToList();
                if (taskBooks.Count(b => b.Status != book.Status) == 0)
                {
                    var parentBook = await _unitOfWork.BookRepository.GetByIdAsync(book.ParentBookId);
                    parentBook.Status = book.Status;
                    _unitOfWork.BookRepository.Update(parentBook);
                    await _unitOfWork.SaveAsync();
                }
            }
            
        }

        public async Task<List<BookDTO>> GetCompanyTasks(long companyId)
        {
            var tasks = await _unitOfWork.BookRepository
                .Query
                .Include(b => b.Vendor)
                .Include(b => b.Vendor.Person)
                .Include(b => b.Vendor.Person.Account)
                .Include(b => b.Work)
                .Where(b => b.IsCompanyTask && b.Company.Id == companyId)
                .ToListAsync();
            return tasks.Select(b => new BookDTO
            {
                Id = b.Id,
                ParentBookId = b.ParentBookId,
                Status = b.Status,
                IsCompanyTask = b.IsCompanyTask,
                DeclinedReason = b.DeclinedReason,
                Vendor = new VendorDTO
                {
                    Id = b.Vendor.Id,
                    FIO = $"{b.Vendor.Person.Name} {b.Vendor.Person.Surname}",
                    Avatar = b.Vendor.Person.Account.Avatar
                },
                Work = new WorkDTO
                {
                    Id = b.Work.Id,
                    Name = b.Work.Name
                }
            }).ToList();
        }

        public async Task ReassignCompanyTask(ShortTaskDTO task, long companyId)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(task.VendorId);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var book = await _unitOfWork.BookRepository.GetByIdAsync(task.BookId);
            var work = await _unitOfWork.WorkRepository.GetByIdAsync(task.WorkId);

            var companyTask = await _unitOfWork.BookRepository.GetByIdAsync(task.Id);
            companyTask.ParentBookId = 0;

            var companyBook = new Book();
            companyBook.Date = book.Date;
            companyBook.EndDate = book.EndDate;
            companyBook.Description = task.Description;
            companyBook.Status = BookStatus.Pending;
            companyBook.IsCompanyTask = true;
            companyBook.Vendor = vendor;
            companyBook.Location = book.Location;
            companyBook.Customer = book.Customer;
            companyBook.CustomerPhone = book.CustomerPhone;
            companyBook.ParentBookId = book.Id;
            companyBook.Work = work;
            companyBook.Company = company;

            _unitOfWork.BookRepository.Update(companyTask);
            _unitOfWork.BookRepository.Create(companyBook);

            await _unitOfWork.SaveAsync();
        }
    }
}
