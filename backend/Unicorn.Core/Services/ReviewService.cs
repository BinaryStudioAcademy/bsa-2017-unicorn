using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class ReviewService : IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                .Select(r => ReviewToDTO(r));

        }

        public async Task<RatingDTO> GetReceiverRatingAsync(long id)
        {
            var reviews = await GetByReceiverIdAsync(id);

            var average = reviews.Average(r => r.Grade);
            return new RatingDTO()
            {
                Average = average,
                ReviewsCount = reviews.Count()
            };

        }

        public async Task<IEnumerable<ReviewDTO>> GetBySenderIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            return reviews
                .Where(r => r.FromAccountId == id)
                .Select(r => ReviewToDTO(r));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByBookIdAsync(long id)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();
            return reviews
                .Where(r => r.BookId == id)
                .Select(r => ReviewToDTO(r));
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
                Grade = review.Grade
            };
        }

        private IUnitOfWork _unitOfWork;
    }
}
