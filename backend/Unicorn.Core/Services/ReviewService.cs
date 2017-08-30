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
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);

            return ReviewToDTO(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetByReceiverIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            return reviews
                .Where(r => r.ToAccountId == id)
                .OrderBy(r => r.Date)
                .Select(r => ReviewToDTO(r)).ToList();

        }

       

        public async Task<IEnumerable<ReviewDTO>> GetBySenderIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            return reviews
                .Where(r => r.FromAccountId == id)
                .Select(r => ReviewToDTO(r)).ToList();
        }

        public async Task<IEnumerable<ReviewDTO>> GetByBookIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            return reviews
                .Where(r => r.BookId == id)
                .Select(r => ReviewToDTO(r)).ToList();
        }

        private ReviewDTO ReviewToDTO(Review review)
        {
            return new ReviewDTO()
            {
                Id = review.Id,
                BookId = review.BookId,
                Description = review.Description,
                From = review.From,
                FromAccountId = review.FromAccountId,
                To = review.To,
                ToAccountId = review.ToAccountId,
                Date = review.Date,
                Avatar = review.Avatar,
                Grade = review.Grade,
                WorkName = review.WorkName
            };
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
                review.Avatar = book.Customer.Person.Account.Avatar;
                review.WorkName = book.Work.Name;
                review.From = book.Customer.Person.Name;
                review.FromAccountId = book.Customer.Person.Account.Id;
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
            rating.Sender = book.Customer.Person.Account;
            rating.Reciever = (reviewDto.PerformerType == "vendor" ? book.Vendor.Person.Account : book.Company.Account);

            _unitOfWork.RatingRepository.Create(rating);

            await _unitOfWork.SaveAsync();

            var notification = new NotificationDTO()
            {
                Title = $"New review",
                Description = $"{book.Customer.Person.Name} {book.Customer.Person.Surname} send review for your work {book.Work.Name}.",
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
