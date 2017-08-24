using System;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyBook
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public BookStatus Status { get; set; }

        public string Description { get; set; }

        public string Customer { get; set; }

        public long CustomerId { get; set; }

        public virtual CompanyWork Work { get; set; }

        public virtual LocationDTO Location { get; set; }
    }
}