﻿using System;
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

        //Company

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
            var companyVendorsRating = await GetCompanyVendorsByRating(companyId);
            analytics.VendorsByRating = new PieChartDTO
            {
                Points = companyVendorsRating
            };
            var companyVendorsBooks = await GetCompanyVendorsByOrders(companyId);
            analytics.VendorsByOrders = new PieChartDTO
            {
                Points = companyVendorsBooks
            };
            var companyVendorsFinished = await GetCompanyVendorsByFinished(companyId);
            analytics.VendorsByFinished = new PieChartDTO
            {
                Points = companyVendorsFinished
            };

            return analytics;
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

        private int GetVendorBooksCount(int month, int year, BookStatus status, long id)
        {
            return _unitOfWork.BookRepository
                .Query
                .Where(b => b.Vendor.Id == id && b.Status == status && b.Date.Month == month && b.Date.Year == year)
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
            })
            .OrderBy(w => w.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetVendorPopularWorks(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            return vendor.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = Convert.ToInt32(w.Orders)
            })
            .OrderBy(w => w.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyConfirmedWorks(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = _unitOfWork.BookRepository
                            .Query
                            .Where(b => b.Status == BookStatus.Confirmed && b.Work.Id == w.Id && b.Company.Id == id)
                            .Count()
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetVendorConfirmedWorks(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            return vendor.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = _unitOfWork.BookRepository
                            .Query
                            .Where(b => b.Status == BookStatus.Confirmed && b.Work.Id == w.Id && b.Vendor.Id == id)
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
                            .Where(b => b.Status == BookStatus.Declined && b.Work.Id == w.Id && b.Company.Id == id)
                            .Count()
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetVendorDeclinedWorks(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            return vendor.Works.Select(w => new ChartPointDTO
            {
                Name = w.Name,
                Value = _unitOfWork.BookRepository
                            .Query
                            .Where(b => b.Status == BookStatus.Declined && b.Work.Id == w.Id && b.Vendor.Id == id)
                            .Count()
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyVendorsByRating(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Vendors.Select(v => new ChartPointDTO
            {
                Name = v.Person.Name,
                Value = GetVendorRating(v.Person.Account.Id)
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyVendorsByOrders(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Vendors.Select(v => new ChartPointDTO
            {
                Name = v.Person.Name,
                Value = GetVendorBooksCount(v.Id)
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private async Task<List<ChartPointDTO>> GetCompanyVendorsByFinished(long id)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            return company.Vendors.Select(v => new ChartPointDTO
            {
                Name = v.Person.Name,
                Value = GetVendorBooksCount(v.Id, BookStatus.Confirmed)
            })
            .OrderBy(p => p.Value)
            .ToList();
        }

        private int GetVendorBooksCount(long id, BookStatus status)
        {
            return _unitOfWork.BookRepository
                .Query
                .Count(b => b.Vendor.Id == id && b.Status == status);
        }

        private int GetVendorBooksCount(long id)
        {
            return _unitOfWork.BookRepository
                .Query
                .Count(b => b.Vendor.Id == id);
        }

        private int GetVendorRating(long id)
        {
            var rating = _unitOfWork.RatingRepository
                .Query
                .Where(r => r.Reciever.Id == id);

            return rating.Any() ? Convert.ToInt32(rating.Average(r => r.Grade)) : 0;
        }


        //Vendor

        public async Task<AnalyticsDTO> GetVendorAnalytics(long vendorId)
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
                    Value = GetVendorBooksCount(month, year, BookStatus.Accepted, vendorId)
                        + GetVendorBooksCount(month, year, BookStatus.Confirmed, vendorId)
                        + GetVendorBooksCount(month, year, BookStatus.Finished, vendorId)
                });
                declined.Add(new ChartPointDTO
                {
                    Name = Months[month],
                    Value = GetVendorBooksCount(month, year, BookStatus.Declined, vendorId)
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
            var popularWorkPoints = await GetVendorPopularWorks(vendorId);
            analytics.PopularWorks = new PieChartDTO
            {
                Points = popularWorkPoints
            };
            var confirmedWorks = await GetVendorConfirmedWorks(vendorId);
            analytics.ConfirmedWorks = new PieChartDTO
            {
                Points = confirmedWorks
            };
            var decliendWorks = await GetVendorDeclinedWorks(vendorId);
            analytics.DeclinedWorks = new PieChartDTO
            {
                Points = decliendWorks
            };

            return analytics;
        }
    }
}