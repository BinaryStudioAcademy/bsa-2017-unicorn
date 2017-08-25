using System;
using System.Collections.Generic;
using Unicorn.Shared.DTOs.Contact;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyDetails
    {
        public long Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public DateTime FoundationDate { get; set; }

        public string Director { get; set; }

        public string City { get; set; }

        public int ReviewsCount { get; set; }

        public LocationDTO Location { get; set; }

        public ICollection<CompanyWork> Works { get; set; }
    }
}