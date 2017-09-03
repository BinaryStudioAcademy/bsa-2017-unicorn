using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Entities;
using Unicorn.Shared.DTOs;
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
