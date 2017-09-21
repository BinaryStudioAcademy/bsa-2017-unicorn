using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Core.Services
{
    public class CalendarService:ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }     

        private Calendar CreateClndr(DateTimeOffset date)
        {
            return new Calendar
            {
                StartDate = date,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };
        }

        private CalendarDTO ReturnCalendar(Calendar calendar, ICollection<Book> events)
        {
            return new CalendarDTO
            {
                Id = calendar.Id,
                StartDate = calendar.StartDate,
                EndDate = calendar.EndDate,
                ExtraDayOffs = new List<ExtraDayDTO>(),
                ExtraWorkDays = new List<ExtraDayDTO>(),
                Events = events.Select(x => new VendorBookDTO
                {
                    Status = x.Status,
                    Customer = x.Customer.Person.Name + " " + x.Customer.Person.Surname,
                    CustomerPhone = x.CustomerPhone,
                    Date = x.Date,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Work = new WorkDTO
                    {
                        Id = x.Work.Id,
                        Icon = x.Work.Icon == "" ? x.Work.Subcategory.Category.Icon : x.Work.Icon,
                        Name = x.Work.Name
                    }
                }).ToList(),
                WorkOnWeekend = false,
                SeveralTasksPerDay = true
            };
        }

        public async Task<CalendarDTO> CreateCalendar(long accountId, CalendarDTO _calendar)
        {       
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            var calendar = CreateClndr(_calendar.StartDate);
            _unitOfWork.CalendarRepository.Create(calendar);
            await _unitOfWork.SaveAsync();

            List<Book> events;

            switch (account.Role.Type)
            {
                case RoleType.Company:
                    var company = _unitOfWork.CompanyRepository.Query.SingleOrDefault(x => x.Account.Id == accountId);
                    events =
                        _unitOfWork.BookRepository.Query.Where(x => x.Company.Account.Id == company.Account.Id).ToList();
                    company.Calendar = calendar;
                    _unitOfWork.CompanyRepository.Update(company);
                    await _unitOfWork.SaveAsync();

                    return ReturnCalendar(calendar, events);
                    
                case RoleType.Vendor:
                    var vendor = _unitOfWork.VendorRepository.Query.SingleOrDefault(x => x.Person.Account.Id == accountId);
                    events =
                        _unitOfWork.BookRepository.Query.Where(x => x.Vendor.Person.Account.Id == vendor.Person.Account.Id).ToList();
                    vendor.Calendar = calendar;
                    _unitOfWork.VendorRepository.Update(vendor);
                    await _unitOfWork.SaveAsync();

                    return ReturnCalendar(calendar, events);

                default:
                    return null;
            }
            

        }

        public async Task SaveCalendar(CalendarDTO calendarDTO)
        {
            var calendar = await _unitOfWork.CalendarRepository.GetByIdAsync(calendarDTO.Id);

            calendar.StartDate = calendarDTO.StartDate;
            calendar.EndDate = calendarDTO.EndDate;
            calendar.WorkOnWeekend = calendarDTO.WorkOnWeekend;
            calendar.SeveralTaskPerDay = calendarDTO.SeveralTasksPerDay;

            calendar.ExtraDayOffs.Clear();
            calendar.ExtraWorkDays.Clear();

            calendar.ExtraDayOffs = calendarDTO.ExtraDayOffs.Select(x => new ExtraDay
            {                
                Day = x.Day,
                DayOff = x.DayOff
            }).ToList();

            calendar.ExtraWorkDays = calendarDTO.ExtraWorkDays.Select(x => new ExtraDay
            {                
                Day = x.Day,
                DayOff = x.DayOff
            }).ToList();

            _unitOfWork.CalendarRepository.Update(calendar);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CalendarDTO> GetCalendarByAccountId(long accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            CalendarDTO calendar;

            switch (account.Role.Type)
            {
                case RoleType.Company:
                    var company = _unitOfWork.CompanyRepository.Query.SingleOrDefault(x => x.Account.Id == accountId);
                    var companyEvents =
                        _unitOfWork.BookRepository.Query.Where(x => x.Company.Account.Id == company.Account.Id);
                    calendar = new CalendarDTO
                    {
                        Id = company.Calendar.Id,
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
                        Events = companyEvents.Select(x => new VendorBookDTO
                        {
                            Status = x.Status,
                            Customer = x.Customer.Person.Name + " " + x.Customer.Person.Surname,
                            CustomerPhone = x.CustomerPhone,
                            Date = x.Date,
                            EndDate = x.EndDate,
                            Description = x.Description,
                            Work = new WorkDTO
                            {
                                Id = x.Work.Id,
                                Icon = String.IsNullOrEmpty(x.Work.Icon) ? x.Work.Subcategory.Category.Icon : x.Work.Icon,
                                Name = x.Work.Name
                            }
                        }).ToList(),
                        WorkOnWeekend = company.Calendar.WorkOnWeekend,
                        SeveralTasksPerDay = company.Calendar.SeveralTaskPerDay
                    };
                    return calendar;
                case RoleType.Vendor:
                    var vendor = _unitOfWork.VendorRepository.Query.SingleOrDefault(x => x.Person.Account.Id == accountId);
                    var vendorEvents =
                        _unitOfWork.BookRepository.Query.Where(x => x.Vendor.Person.Account.Id == vendor.Person.Account.Id);
                    calendar = new CalendarDTO
                    {
                        Id = vendor.Calendar.Id,
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
                        Events = vendorEvents.Select(x => new VendorBookDTO
                        {
                            Status = x.Status,
                            Customer = x.Customer.Person.Name + " " + x.Customer.Person.Surname,
                            CustomerPhone = x.CustomerPhone,
                            Date = x.Date,
                            EndDate = x.EndDate,
                            Description = x.Description,
                            Work = new WorkDTO
                            {
                                Id = x.Work.Id,
                                Icon = String.IsNullOrEmpty(x.Work.Icon) ? x.Work.Subcategory.Category.Icon : x.Work.Icon,
                                Name = x.Work.Name
                            }
                        }).ToList(),
                        WorkOnWeekend = vendor.Calendar.WorkOnWeekend,
                        SeveralTasksPerDay = vendor.Calendar.SeveralTaskPerDay
                    };
                    return calendar;
                default:
                    return null;
            }
        }


    }
}