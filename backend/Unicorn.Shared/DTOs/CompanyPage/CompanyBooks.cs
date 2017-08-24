using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.CompanyPage
{
    public class CompanyBooks
    {
        public long Id { get; set; }

        public virtual ICollection<CompanyBook> Books { get; set; }
    }
}