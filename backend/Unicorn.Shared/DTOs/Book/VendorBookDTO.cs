﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;

namespace Unicorn.Shared.DTOs.Book
{
    public class VendorBookDTO
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public string CustomerPhone { get; set; }

        public string Customer { get; set; }

        public long CustomerId { get; set; }

        public int Rating { get; set; }

        public bool IsHidden { get; set; }

        public ReviewDTO Review { get; set; }

        public WorkDTO Work { get; set; }

        public LocationDTO Location { get; set; }
    }
}
