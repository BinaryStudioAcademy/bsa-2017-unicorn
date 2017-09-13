using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Chart;

namespace Unicorn.Core.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Dictionary<int, string> Months = new Dictionary<int, string>
        {
            { 1, "January"},
            { 2, "February"},
            { 3, "March"},
            { 4, "April"},
            { 5, "May"},
            { 6, "June"},
            { 7, "July"},
            { 8, "August"},
            { 9, "September"},
            { 10, "October"},
            { 11, "November"},
            { 12, "December"},
        };


        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AnalyticsDTO> GetCompanyAnalytics(long companyId)
        {
            var analytics = new AnalyticsDTO();
            var accepted = new List<ChartPointDTO>();
            var declined = new List<ChartPointDTO>();
            var date = GetMonthAndYear();
            var month = date.Item1;
            var year = date.Item2;
            for (int i = 0; i < 12; i++)
            {
                accepted.Add(new ChartPointDTO
                {
                    Name = Months[month],
                    Value = GetCompanyBooksCount(month, year, BookStatus.Accepted, companyId)
                        + GetCompanyBooksCount(month, year, BookStatus.Confirmed, companyId)
                        + GetCompanyBooksCount(month, year, BookStatus.Finished, companyId)
                });
                declined.Add(new ChartPointDTO
                {
                    Name = Months[month],
                    Value = GetCompanyBooksCount(month, year, BookStatus.Declined, companyId)
                });
                month++;
                if (month > 12)
                {
                    year++;
                    month -= 12;
                }
            }
            analytics.BooksAccepted = new LineChartDTO
            {
                Name = "Accepted books",
                Series = accepted
            };
            analytics.BooksDeclined = new LineChartDTO
            {
                Name = "Declined books",
                Series = declined
            };
            var popularWorkPoints = await GetCompanyPopularWorks(companyId);
            analytics.PopularWorks = new PieChartDTO
            {
                Points = popularWorkPoints
            };
            var confirmedWorks = await GetCompanyConfirmedWorks(companyId);
            analytics.ConfirmedWorks = new PieChartDTO
            {
                Points = confirmedWorks
            };
            var decliendWorks = await GetCompanyDeclinedWorks(companyId);
            analytics.DeclinedWorks = new PieChartDTO
            {
                Points = decliendWorks
            };

            return analytics;
        }

        public async Task<AnalyticsDTO> GetVendorAnalytics(long vendorId)
        {
            throw new NotImplementedException();
        }

        private Tuple<int, int> GetMonthAndYear()
        {
            var month = (DateTime.Now.Month + 12) % 12 + 1;
            var year = DateTime.Now.Year - 1;
            if (month == 1)
            {
                year++;
            }
            return new Tuple<int, int>(month, year);
        }

        private int GetCompanyBooksCount(int month, int year, BookStatus status, long id)
        {
            return _unitOfWork.BookRepository
                .Query
                .Where(b => b.Company.Id == id && b.Status == status && b.Date.Month == month && b.Date.Year == year)
                .Count();
        }

        private int GetVendorsBooksCount(int month, int year, BookStatus status, long id)
        {
            return _unitOfWork.BookRepository
                .Query
                .Where(b => b.Vendor.Id == id && b.Status == status && b.Date.Month == month && b.Date.Year == year)
                .Count();
        }

        private async Task<List<ChartPointDTO>> GetCompanyPopularWorks(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = Convert.ToInt32(w.Orders)
            }).ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyConfirmedWorks(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = _unitOfWork.BookRepository
                            .Query
                            .Where(b => b.Status == BookStatus.Confirmed && b.Work.Id == w.Id)
                            .Count()
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyDeclinedWorks(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = _unitOfWork.BookRepository
                            .Query
                            .Where(b => b.Status == BookStatus.Declined && b.Work.Id == w.Id)
                            .Count()
            })
            .OrderBy(p => p.Value)
            .ToList();
        }
    }
}
