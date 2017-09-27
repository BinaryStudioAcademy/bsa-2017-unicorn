using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Admin
{
    public class AccountsPage
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<AccountDTO> Items { get; set; }
    }
}
