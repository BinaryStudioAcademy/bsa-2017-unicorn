using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Entities;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Shared.DTOs
{
    public class CalendarDTO
    {
        public long Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public bool WorkOnWeekend { get; set; }

        public bool SeveralTasksPerDay { get; set; }

        public ICollection<VendorBookDTO> Events { get; set; }

        public ICollection<ExtraDayDTO> ExtraDayOffs { get; set; }

        public ICollection<ExtraDayDTO> ExtraWorkDays { get; set; }
    }
}