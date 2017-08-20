using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyReviews
    {
        public long Id { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}