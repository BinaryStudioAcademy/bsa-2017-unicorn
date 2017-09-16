using System;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface ICalendarService
    {
        Task<CalendarDTO> CreateCalendar(long accountId, DateTime date);
        Task SaveCalendar(CalendarDTO calendar);
        Task<CalendarDTO> GetCalendarByAccountId(long accountId);
    }
}