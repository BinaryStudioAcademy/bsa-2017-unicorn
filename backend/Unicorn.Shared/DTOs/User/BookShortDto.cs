using System;

namespace Unicorn.Shared.DTOs.User
{
    public class BookShortDto
    {
        public DateTime date { get; set; }
        public string address { get; set; }
        public string workType { get; set; }
        public string description { get; set; }
        public string vendor { get; set; }
        public string status { get; set; }
    }
}
