using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Notification;
using Unicorn.Shared.DTOs.Review;

namespace Unicorn.Core.Services
{
    public class ReviewService : IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<ReviewDTO> GetByIdAsync(long id)
        {
            var review = await _unitOfWork.ReviewRepository.Query
                .Include(r => r.Sender)
                .SingleAsync(r => r.Id == id);

            return ReviewToDTO(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetByReceiverIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.Query
                .Include(r => r.Sender)
                .Where(r => r.ToAccountId == id)
                .ToListAsync();

            return reviews
                .OrderBy(r => r.Date)
                .Select(r => ReviewToDTO(r)).ToList();

        }

        public async Task<IEnumerable<ReviewDTO>> GetBySenderIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.Query
                .Include(r => r.Sender)
                .Where(r => r.Sender.Id == id)
                .ToListAsync();
            return reviews
                .Select(r => ReviewToDTO(r)).ToList();
        }

        public async Task<ReviewDTO> GetByBookIdAsync(long id)
        {
            var review = await _unitOfWork.ReviewRepository.Query
                .Include(r => r.Sender)
                .SingleAsync(r => r.BookId == id);

            return ReviewToDTO(review);
        }

        public ReviewDTO GetByBookId(long id)
        {
            var review = _unitOfWork.ReviewRepository.Query
                .Include(r => r.Sender)
                .SingleOrDefault(r => r.BookId == id);

            if (review != null)
                return ReviewToDTO(review);
            else
                return null;
        }

        private ReviewDTO ReviewToDTO(Review review)
        {
            var work = _unitOfWork.BookRepository
                .Query
                .Include(b => b.Work)
                .Where(b => b.Id == review.BookId)
                .Select(b => b.Work.Name)
                .FirstOrDefault();
                
            var reviewDto = new ReviewDTO()
            {
                Id = review.Id,
                BookId = review.BookId,
                Description = review.Description,
                Avatar = review.Sender.Avatar,
                FromAccountId = review.Sender.Id,
                To = review.To,
                ToAccountId = review.ToAccountId,
                Date = review.Date,
                Grade = review.Grade,
                WorkName = review.WorkName ?? work
            };

            switch (review.Sender.Role.Id)
            {
                case 2:
                case 3:
                    var customer = _unitOfWork.CustomerRepository.Query
                        .Include(p => p.Person)
                        .Include(p => p.Person.Account)
                        .Single(p => p.Person.Account.Id == review.Sender.Id);
                    reviewDto.From = $"{customer.Person.Name} {customer.Person.Surname}";
                    reviewDto.FromProfileId = customer.Id;
                    break;
                case 4:
                    var company = _unitOfWork.CompanyRepository.Query
                        .Include(p => p.Account)
                        .Single(p => p.Account.Id == review.Sender.Id);
                    reviewDto.From = company.Name;
                    break;
                case 5:
                    var adminPerson = _unitOfWork.PersonRepository.Query
                        .Include(p => p.Account)
                        .FirstOrDefault(x => x.Account.Id == review.Sender.Id);
                    if (adminPerson == null)
                    {
                        var adminCompany = _unitOfWork.CompanyRepository.Query
                            .Include(p => p.Account)
                            .Single(p => p.Account.Id == review.Sender.Id);
                    }

                    reviewDto.From = $"{adminPerson.Name} {adminPerson.Surname}";
                    break;
                default:
                    return null;
            }

            return reviewDto;
        }

        public async Task SaveReview(ShortReviewDTO reviewDto)
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
                .SingleAsync(b => b.Id == reviewDto.BookId);

            if(!string.IsNullOrEmpty(reviewDto.Text.Trim()))
            {
                var review = new Review();
                review.BookId = reviewDto.BookId;
                review.Description = reviewDto.Text;
                review.Grade = reviewDto.Grade;
                review.WorkName = book.Work.Name;
                if (book.IsCompanyTask)
                {
                    review.Sender = book.Company.Account;
                }
                else
                {
                    review.Sender = book.Customer.Person.Account;
                }
                if (reviewDto.PerformerType == "vendor")
                {
                    review.To = book.Vendor.Person.Name;
                    review.ToAccountId = book.Vendor.Person.Account.Id;
                }
                else
                {
                    review.To = book.Company.Name;
                    review.ToAccountId = book.Company.Account.Id;
                }
                review.Date = DateTime.Now;

                _unitOfWork.ReviewRepository.Create(review);
            }
            
            book.Status = BookStatus.Confirmed;
            _unitOfWork.BookRepository.Update(book);

            var rating = new Rating();
            rating.Book = book;
            rating.Grade = reviewDto.Grade;
            if (book.IsCompanyTask)
            {
                rating.Sender = book.Company.Account;
            }
            else
            {
                rating.Sender = book.Customer.Person.Account;
            }
            rating.Reciever = (reviewDto.PerformerType == "vendor" ? book.Vendor.Person.Account : book.Company.Account);

            _unitOfWork.RatingRepository.Create(rating);

            await _unitOfWork.SaveAsync();

            string senderName;
            if (book.IsCompanyTask)
            {
                senderName = book.Company.Name;
            }
            else
            {
                senderName = $"{book.Customer.Person.Name} {book.Customer.Person.Surname}";
            }
            var notification = new NotificationDTO()
            {
                Title = $"New review",
                Description = $"{senderName} send review for your work {book.Work.Name}.",
                SourceItemId = book.Id,
                Time = DateTime.Now,
                Type = NotificationType.TaskNotification
            };

            var receiverId = rating.Reciever.Id;
            await _notificationService.CreateAsync(receiverId, notification);

        }

        private IUnitOfWork _unitOfWork;
        private INotificationService _notificationService;
    }
}
