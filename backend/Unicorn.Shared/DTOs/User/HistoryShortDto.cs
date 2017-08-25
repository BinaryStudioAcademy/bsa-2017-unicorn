using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs.User
{
    public class HistoryShortDto
    {

        public DateTime date { get; set; }
        public DateTime dateFinished { get; set; }
        public string bookDescription { get; set; }
        public string workDescription { get; set; }
        public string vendor { get; set; }
        public string categoryName { get; set; }
        public string subcategoryName { get; set; }
    }
}
