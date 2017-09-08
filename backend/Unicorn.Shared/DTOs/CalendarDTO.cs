using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Shared.DTOs
{
    public class CalendarDTO
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool WorkOnWeekend { get; set; }

        public ICollection<ExtraDayDTO> ExtraDayOffs { get; set; }

        public ICollection<ExtraDayDTO> ExtraWorkDays { get; set; }
    }
}