using System;

namespace Unicorn.Shared.DTOs.Admin
{
    public class BannedAccountDTO
    {
        public long Id { get; set; }
        public long AccountId { get; set; }

        public string Role { get; set; }
        public string Email { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
