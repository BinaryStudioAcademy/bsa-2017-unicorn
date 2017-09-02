using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.History
{
    public class VendorHistoryDTO
    {
        public long Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BookDescription { get; set; }
        public string WorkDescription { get; set; }
        public long WorkId { get; set; }
        public string Subcategory { get; set; }
        public string Category { get; set; }
        public string Label { get; set; }
    }
}
