using System;

namespace Unicorn.Shared.DTOs.Admin
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public bool IsBanned { get; set; }
    }
}
