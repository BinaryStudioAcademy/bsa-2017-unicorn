using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs
{
    public class CompanyDTO
    {
        public long Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime FoundationDate { get; set; }

        public double Rating { get; set; }

        public string Director { get; set; }

        public LocationDTO Location { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }

        public ICollection<VendorDTO> Vendors { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }





        public AccountDTO Account { get; set; }

        
    }
}
