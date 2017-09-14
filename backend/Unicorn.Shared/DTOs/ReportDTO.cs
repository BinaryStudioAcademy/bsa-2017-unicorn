using System;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Shared.DTOs
{
    public class ReportDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public ReportType Type { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public long? ProfileId { get; set; }
        public string ProfileType { get; set; }
    }
}
