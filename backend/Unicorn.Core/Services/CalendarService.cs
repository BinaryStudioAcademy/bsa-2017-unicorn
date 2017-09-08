using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class CalendarService:ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Calendar CreateClndr()
        {
            return new Calendar
            {
                StartDate = DateTime.Today,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false
            };
        }

        public async Task<CalendarDTO> CreateCalendar(long accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            var calendar = CreateClndr();
            _unitOfWork.CalendarRepository.Create(calendar);
            await _unitOfWork.SaveAsync();

            switch (account.Role.Type)
            {
                case RoleType.Company:
                    var company = _unitOfWork.CompanyRepository.Query.SingleOrDefault(x => x.Account.Id == accountId);
                    company.Calendar = calendar;
                    _unitOfWork.CompanyRepository.Update(company);
                    await _unitOfWork.SaveAsync();
                    break;
                case RoleType.Vendor:
                    var vendor = _unitOfWork.VendorRepository.Query.SingleOrDefault(x => x.Person.Account.Id == accountId);
                    vendor.Calendar = calendar;
                    _unitOfWork.VendorRepository.Update(vendor);
                    await _unitOfWork.SaveAsync();
                    break;
                default:
                    return null;
            }
            return new CalendarDTO
            {
                StartDate = calendar.StartDate,
                EndDate = calendar.EndDate,
                ExtraDayOffs = new List<ExtraDayDTO>(),
                ExtraWorkDays = new List<ExtraDayDTO>(),
                WorkOnWeekend = false
            };
        }

        public async Task SaveCalendar(CalendarDTO calendarDTO)
        {
            var calendar = await _unitOfWork.CalendarRepository.GetByIdAsync(calendarDTO.Id);

            calendar.StartDate = calendarDTO.StartDate;
            calendar.EndDate = calendarDTO.EndDate;
            calendar.WorkOnWeekend = calendarDTO.WorkOnWeekend;

            calendar.ExtraDayOffs = calendarDTO.ExtraDayOffs.Select(x => new ExtraDay
            {
                Calendar = calendar,
                Day = x.Day,
                DayOff = x.DayOff
            }).ToList();

            calendar.ExtraWorkDays = calendarDTO.ExtraWorkDays.Select(x => new ExtraDay
            {
                Calendar = calendar,
                Day = x.Day,
                DayOff = x.DayOff
            }).ToList();

            _unitOfWork.CalendarRepository.Update(calendar);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CalendarDTO> GetCalendarById(long calendarId)
        {
            var calendar = await _unitOfWork.CalendarRepository.GetByIdAsync(calendarId);

            return new CalendarDTO
            {
                StartDate = calendar.StartDate,
                EndDate = calendar.EndDate,
                ExtraDayOffs = calendar.ExtraDayOffs.Select(x => new ExtraDayDTO
                {
                    CalendarId = x.Calendar.Id,
                    Day = x.Day,
                    DayOff = x.DayOff,
                    Id = x.Id
                }).ToList(),
                ExtraWorkDays = calendar.ExtraWorkDays.Select(x => new ExtraDayDTO
                {
                    CalendarId = x.Calendar.Id,
                    Day = x.Day,
                    DayOff = x.DayOff,
                    Id = x.Id
                }).ToList(),
                WorkOnWeekend = false
            };
        }

        public async Task<CalendarDTO> GetCalendarByAccountId(long accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            CalendarDTO calendar;

            switch (account.Role.Type)
            {
                case RoleType.Company:
                    var company = _unitOfWork.CompanyRepository.Query.SingleOrDefault(x => x.Account.Id == accountId);
                    calendar = new CalendarDTO
                    {
                        StartDate = company.Calendar.StartDate,
                        EndDate = company.Calendar.EndDate,
                        ExtraDayOffs = company.Calendar.ExtraDayOffs.Select(x => new ExtraDayDTO
                        {
                            CalendarId = company.Calendar.Id,
                            Day = x.Day,
                            DayOff = x.DayOff
                        }).ToList(),
                        ExtraWorkDays = company.Calendar.ExtraWorkDays.Select(x => new ExtraDayDTO
                        {
                            CalendarId = company.Calendar.Id,
                            Day = x.Day,
                            DayOff = x.DayOff
                        }).ToList(),
                        WorkOnWeekend = company.Calendar.WorkOnWeekend
                    };
                    return calendar;
                case RoleType.Vendor:
                    var vendor = _unitOfWork.VendorRepository.Query.SingleOrDefault(x => x.Person.Account.Id == accountId);
                    calendar = new CalendarDTO
                    {
                        StartDate = vendor.Calendar.StartDate,
                        EndDate = vendor.Calendar.EndDate,
                        ExtraDayOffs = vendor.Calendar.ExtraDayOffs.Select(x => new ExtraDayDTO
                        {
                            CalendarId = vendor.Calendar.Id,
                            Day = x.Day,
                            DayOff = x.DayOff
                        }).ToList(),
                        ExtraWorkDays = vendor.Calendar.ExtraWorkDays.Select(x => new ExtraDayDTO
                        {
                            CalendarId = vendor.Calendar.Id,
                            Day = x.Day,
                            DayOff = x.DayOff
                        }).ToList(),
                        WorkOnWeekend = vendor.Calendar.WorkOnWeekend
                    };
                    return calendar;
                default:
                    return null;
            }
        }


    }
}