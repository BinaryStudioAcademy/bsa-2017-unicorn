using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Review;

namespace Unicorn.Core.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDTO> GetByBookIdAsync(long id);
        ReviewDTO GetByBookId(long id);
        Task<ReviewDTO> GetByIdAsync(long id);
        Task<IEnumerable<ReviewDTO>> GetByReceiverIdAsync(long id);
        Task<IEnumerable<ReviewDTO>> GetBySenderIdAsync(long id);
        Task SaveReview(ShortReviewDTO review);
    }
}
