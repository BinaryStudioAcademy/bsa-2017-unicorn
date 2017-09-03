using System.Collections.Generic;

using Unicorn.Shared.DTOs.Review;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyReviews
    {
        public long Id { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}