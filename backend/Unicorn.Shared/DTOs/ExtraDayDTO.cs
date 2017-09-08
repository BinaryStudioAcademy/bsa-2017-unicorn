using System;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Shared.DTOs
{
    public class ExtraDayDTO
    {
        public long Id { get; set; }

        public DateTime Day { get; set; }

        public bool DayOff { get; set; }

        public long CalendarId { get; set; }
    }
}