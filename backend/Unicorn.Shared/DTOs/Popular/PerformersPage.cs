using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Popular
{
    public class PerformersPage
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<FullPerformerDTO> Items { get; set; }
    }
}
