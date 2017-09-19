using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Search;
using System.Device.Location;
using System;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }    
        
        //private DateTime? ParseTheDate(string date)
        //{            
        //    if(date == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var partsOfTheTime = date.Split('-');
        //        var year = int.Parse(partsOfTheTime[0]);
        //        var month = int.Parse(partsOfTheTime[1]);
        //        var day = int.Parse(partsOfTheTime[2].Split('T')[0]);
        //        return new DateTime(year, month, day);
        //    }
        //}

        private bool IsVendorWorkingOnThisDate(long id, DateTimeOffset? date)
        {
            date = date != null ?
                date.GetValueOrDefault().ToUniversalTime() : date;

            if (date == null)
            {
                return false;
            }           
            
            var books = _unitOfWork.BookRepository.Query.Where(x => x.Vendor.Id == id &&
            x.Status != DataAccess.Entities.Enum.BookStatus.Finished && x.Status != DataAccess.Entities.Enum.BookStatus.Declined
                && x.Status != DataAccess.Entities.Enum.BookStatus.Confirmed);

            foreach (var book in books)
            {
                var bookDate = book.Date;
                var bookEndDate = book.EndDate;

                if (date >= bookDate && date <= bookEndDate)
                {
                    return true;
                }

            }

            return false;
        }

        private bool IsCompanyWorkingOnThisDate(long id, DateTimeOffset? date)
        {
            date = date != null ?
                date.GetValueOrDefault().ToUniversalTime() : date;

            if (date == null)
            {
                return false;
            }

            var books = _unitOfWork.BookRepository.Query.Where(x => x.Company.Id == id &&
            x.Status != DataAccess.Entities.Enum.BookStatus.Finished && x.Status != DataAccess.Entities.Enum.BookStatus.Declined
                && x.Status != DataAccess.Entities.Enum.BookStatus.Confirmed);

            foreach (var book in books)
            {
                var bookDate = book.Date;
                var bookEndDate = book.EndDate;
                if (date >= bookDate && date <= bookEndDate)
                {
                    return true;
                }                
            }
            return false;            
        }

        private bool SynchronizeWorkDateWithVendorsWorkDays(Calendar calendar, DateTimeOffset? date, bool isWorkingOnThisDate, int timeZone)
        {    
            if (date == null)
            {
                return true;
            }
            var calendarStartDate = calendar.StartDate.Date;
            var calendarEndDate = calendar.EndDate != null ? 
                calendar.EndDate.GetValueOrDefault().Date : calendar.EndDate;

            var weekendDate = date.GetValueOrDefault().AddHours(-(timeZone / 60));

            if (calendarStartDate <= date && (calendarEndDate == null || date <= calendarEndDate))
            {
                if (calendar.ExtraWorkDays.FirstOrDefault(x => x.Day.Date == date.GetValueOrDefault().Date) != null)
                {
                    return true;
                }
                if (calendar.ExtraDayOffs.FirstOrDefault(x => x.Day.Date == date.GetValueOrDefault().Date) == null)
                {
                    if (calendar.SeveralTaskPerDay)
                    {
                        if (weekendDate.DayOfWeek == DayOfWeek.Saturday || weekendDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (calendar.WorkOnWeekend)
                            {
                                return true;
                            }
                            return false;                            
                        }
                        return true;                       
                    }
                    else
                    {
                        if (date.GetValueOrDefault().DayOfWeek == DayOfWeek.Saturday || date.GetValueOrDefault().DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (calendar.WorkOnWeekend && !isWorkingOnThisDate)
                            {
                                return true;
                            }
                            return false;
                        }
                        if (!isWorkingOnThisDate)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            return false;  
        }

        public async Task<List<SearchWorkDTO>> GetWorksByFilters(  string category, string subcategory, DateTimeOffset? date, int timeZone,
                                                                   string vendor, string ratingcompare, double? rating, bool? reviews,
                                                                   double? latitude, double? longitude, double? distance,
                                                                   string[] categories, string[] subcategories, string city,
                                                                   int? sort  )
        {          
            var reviewsList = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)                
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Where(w => string.IsNullOrEmpty(vendor) || w.Vendor.Person.Name.Contains(vendor))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .ToListAsync();

            var vendorsWorksSyncWithDate = vendorsWorksList.Where(x => SynchronizeWorkDateWithVendorsWorkDays(x.Vendor.Calendar, date, IsVendorWorkingOnThisDate(x.Vendor.Id, date), timeZone)).ToList();

            var vendorsWorks = CreateVendorsWorksAdv(vendorsWorksSyncWithDate, reviewsList, 
                ratingcompare, rating, reviews, latitude, longitude, distance, categories, subcategories, city);


            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Where(w => string.IsNullOrEmpty(vendor) || w.Company.Name.Contains(vendor))
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();

            var companiesWorksSyncWithDate = companiesWorksList.Where(x => SynchronizeWorkDateWithVendorsWorkDays(x.Company.Calendar, date, IsCompanyWorkingOnThisDate(x.Company.Id, date), timeZone)).ToList();

            var companiesWorks = CreateCompaniesWorksAdv(companiesWorksSyncWithDate, reviewsList,
                ratingcompare, rating, reviews, latitude, longitude, distance, categories, subcategories, city);

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .Distinct()
                .ToList();

            var searchWorksOrdered = new List<SearchWorkDTO>();

            switch (sort)
            {
                case (int?)Sort.RATING:
                    searchWorksOrdered = searchWorks.OrderByDescending(x => x.Rating).ToList();
                    break;
                case (int?)Sort.NAME:
                    searchWorksOrdered = searchWorks.OrderBy(x => x.Name).ToList();
                    break;
                case (int?)Sort.DISTANCE:
                    searchWorksOrdered = searchWorks.OrderBy(x => x.Distance).ToList();
                    break;
                default:
                    searchWorksOrdered = searchWorks.OrderByDescending(x => x.Rating).ToList();
                    break;
            }

            return searchWorksOrdered;
        }

        private List<SearchWorkDTO> CreateVendorsWorksAdv(List<Work> works, IEnumerable<Review> reviewsList,
                                                        string ratingcompare, double? rating, bool? reviews,
                                                        double? latitude, double? longitude, double? distance,
                                                        string[] categories, string[] subcategories, string city)
        {
            var worksQuery = works
                .Where(w => string.IsNullOrEmpty(ratingcompare) || rating == null ||
                           (ratingcompare.Equals("ge") && (CalculateRating(w.Vendor.Person.Account.Id) >= rating)) ||
                           (ratingcompare.Equals("le") && (CalculateRating(w.Vendor.Person.Account.Id) <= rating)))
                .Where(w => reviews == null || reviews == false || reviewsList.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id) > 0)
                .Where(w => categories == null || categories.Length < 1 || string.IsNullOrEmpty(categories[0]) || categories.Any(c => c.Equals(w.Subcategory.Category.Name)))
                .Where(w => subcategories == null || subcategories.Length < 1 || string.IsNullOrEmpty(subcategories[0]) || subcategories.Any(c => c.Equals(w.Subcategory.Name)))
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = string.IsNullOrEmpty(w.Icon) ? w.Subcategory.Category.Icon : w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Vendor.Person.Account.Id),
                    ReviewsCount = reviewsList.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id),
                    PerformerType = "vendor",
                    PerformerName = $"{w.Vendor.Person.Name} ({w.Vendor.Position})",
                    Link = "vendor/" + w.Vendor.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Vendor.Person.Account.Location.Id,
                        City = w.Vendor.Person.Account.Location.City,
                        Adress = w.Vendor.Person.Account.Location.Adress,
                        Latitude = w.Vendor.Person.Account.Location.Latitude,
                        Longitude = w.Vendor.Person.Account.Location.Longitude,
                        PostIndex = w.Vendor.Person.Account.Location.PostIndex
                    },
                    Distance = CalculateDistance(w.Vendor.Person.Account.Location.Latitude, w.Vendor.Person.Account.Location.Longitude,
                                                 latitude, longitude)
                });

            if (distance == 0 || distance == null)
            {
                if (!string.IsNullOrEmpty(city))
                    worksQuery = worksQuery?
                        .Where(p => p.Location.City.Contains(city));
            }
            else
            {
                worksQuery = worksQuery?
                    .Where(p => p.Distance <= distance);
            }

            return worksQuery.ToList();
        }

        private List<SearchWorkDTO> CreateCompaniesWorksAdv( List<Work> works, IEnumerable<Review> reviewsList,
                                                          string ratingcompare, double? rating, bool? reviews,
                                                          double? latitude, double? longitude, double? distance,
                                                          string[] categories, string[] subcategories, string city)
        {
            var worksQuery = works
                .Where(w => string.IsNullOrEmpty(ratingcompare) || rating == null ||
                            (ratingcompare.Equals("ge") && (CalculateRating(w.Company.Account.Id) >= rating)) ||
                            (ratingcompare.Equals("le") && (CalculateRating(w.Company.Account.Id) <= rating)))
                .Where(w => reviews == null || reviews == false || reviewsList.Count(r => r.ToAccountId == w.Company.Account.Id) > 0)
                .Where(w => categories == null || categories.Length < 1 || string.IsNullOrEmpty(categories[0]) || categories.Any(c => c.Equals(w.Subcategory.Category.Name)))
                .Where(w => subcategories == null || subcategories.Length < 1 || string.IsNullOrEmpty(subcategories[0]) || subcategories.Any(c => c.Equals(w.Subcategory.Name)))
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = string.IsNullOrEmpty(w.Icon) ? w.Subcategory.Category.Icon : w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Company.Account.Id),
                    ReviewsCount = reviewsList.Count(r => r.ToAccountId == w.Company.Account.Id),
                    PerformerType = "company",
                    PerformerName = $"{w.Company.Name}",
                    Link = "company/" + w.Company.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Company.Account.Location.Id,
                        City = w.Company.Account.Location.City,
                        Adress = w.Company.Account.Location.Adress,
                        Latitude = w.Company.Account.Location.Latitude,
                        Longitude = w.Company.Account.Location.Longitude,
                        PostIndex = w.Company.Account.Location.PostIndex
                    },
                    Distance = CalculateDistance(w.Company.Account.Location.Latitude, w.Company.Account.Location.Longitude,
                                                 latitude, longitude)
                });

            if (distance == 0 || distance == null)
            {
                if (!string.IsNullOrEmpty(city))
                    worksQuery = worksQuery?
                        .Where(p => p.Location.City.Contains(city));
            }
            else
            {
                worksQuery = worksQuery?
                    .Where(p => p.Distance <= distance);
            }

            return worksQuery.ToList();
        }

        private double CalculateRating(long recieverId)
        {
            var ratings = _unitOfWork.RatingRepository.Query
                  .Include(y => y.Reciever)
                  .Where(p => p.Reciever.Id == recieverId)
                  .ToList();

            return ratings.Any() ? ratings.Average(z => z.Grade) : 0;
        }

        private double CalculateDistance(double lat1, double long1, double? lat2, double? long2)
        {
            if (lat2 == null || long2 == null)
                return 0;
            var coord1 = new GeoCoordinate(lat1, long1);
            var coord2 = new GeoCoordinate((double)lat2, (double)long2);
            var distance = coord1.GetDistanceTo(coord2) / 1000;
            return distance;
        }

        public async Task<List<SearchWorkDTO>> GetAllWorks()
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .ToListAsync();

            var vendorsWorks = CreateVendorsWorks(vendorsWorksList, reviews);

            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Include(w => w.Company.Account)
                .ToListAsync();

            var companiesWorks = CreateCompaniesWorks(companiesWorksList, reviews);

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchWorks;
        }

        public async Task<List<SearchWorkDTO>> GetWorksByBaseFilters(string category, string subcategory, DateTimeOffset? date, int timeZone)
        {  
            var reviewsList = await _unitOfWork.ReviewRepository.GetAllAsync();

            var vendorsWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Vendor != null)
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Include(w => w.Vendor.Person)
                .Include(w => w.Vendor.Person.Account)
                .Include(w => w.Vendor.Person.Account.Location)
                .ToListAsync();

            var vendorsWorksSyncWithDate = vendorsWorksList.Where(x => SynchronizeWorkDateWithVendorsWorkDays(x.Vendor.Calendar, date, IsVendorWorkingOnThisDate(x.Vendor.Id, date), timeZone)).ToList();

            var vendorsWorks = CreateVendorsWorks(vendorsWorksSyncWithDate, reviewsList);

            var companiesWorksList = await _unitOfWork.WorkRepository
                .Query
                .Where(w => w.Company != null)
                .Where(x => string.IsNullOrEmpty(category) || (x.Subcategory.Category.Name.Contains(category) || x.Subcategory.Category.Tags.Contains(category)))
                .Where(x => string.IsNullOrEmpty(subcategory) || (x.Subcategory.Name.Contains(subcategory) || x.Subcategory.Tags.Contains(subcategory)))
                .Include(w => w.Company.Account)
                .Include(w => w.Company.Account.Location)
                .ToListAsync();

            var companiesWorksSyncWithDate = companiesWorksList.Where(x => SynchronizeWorkDateWithVendorsWorkDays(x.Company.Calendar, date, IsCompanyWorkingOnThisDate(x.Company.Id, date), timeZone)).ToList();

            var companiesWorks = CreateCompaniesWorks(companiesWorksSyncWithDate, reviewsList);

            var searchWorks = vendorsWorks
                .Concat(companiesWorks)
                .OrderByDescending(p => p.Rating)
                .Distinct()
                .ToList();

            return searchWorks;
        }

        private List<SearchWorkDTO> CreateVendorsWorks(List<Work> works, IEnumerable<Review> reviews)
        {
            return works
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = string.IsNullOrEmpty(w.Icon) ? w.Subcategory.Category.Icon : w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Vendor.Person.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Vendor.Person.Account.Id),
                    PerformerType = "vendor",
                    PerformerName = $"{w.Vendor.Person.Name} ({w.Vendor.Position})",
                    Link = "vendor/" + w.Vendor.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Vendor.Person.Account.Location.Id,
                        City = w.Vendor.Person.Account.Location.City,
                        Adress = w.Vendor.Person.Account.Location.Adress,
                        Latitude = w.Vendor.Person.Account.Location.Latitude,
                        Longitude = w.Vendor.Person.Account.Location.Longitude,
                        PostIndex = w.Vendor.Person.Account.Location.PostIndex
                    },
                }).ToList();
        }

        private List<SearchWorkDTO> CreateCompaniesWorks(List<Work> works, IEnumerable<Review> reviews)
        {
            return works
                .Select(w => new SearchWorkDTO
                {
                    Id = w.Id,
                    Avatar = string.IsNullOrEmpty(w.Icon) ? w.Subcategory.Category.Icon : w.Icon,
                    Name = w.Name,
                    Rating = CalculateRating(w.Company.Account.Id),
                    ReviewsCount = reviews.Count(r => r.ToAccountId == w.Company.Account.Id),
                    PerformerType = "company",
                    PerformerName = $"{w.Company.Name}",
                    Link = "company/" + w.Company.Id,
                    Location = new LocationDTO
                    {
                        Id = w.Company.Account.Location.Id,
                        City = w.Company.Account.Location.City,
                        Adress = w.Company.Account.Location.Adress,
                        Latitude = w.Company.Account.Location.Latitude,
                        Longitude = w.Company.Account.Location.Longitude,
                        PostIndex = w.Company.Account.Location.PostIndex
                    },
                }).ToList();
        }
    }
}
