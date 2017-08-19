using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Vendor;

namespace Unicorn.Core.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HistoryDTO>> GetAllAsync()
        {
            var history = await _unitOfWork.HistoryRepository.GetAllAsync();

            List<HistoryDTO> datareturn = new List<HistoryDTO>();

            foreach (var element in history)
            {
                var historyDto = new HistoryDTO()
                {
                    Id = element.Id,
                    Date = element.Date,
                    DateFinished = element.DateFinished,
                    BookDescription = element.BookDescription,
                    CategoryName = element.CategoryName,
                    SubcategoryName = element.SubcategoryName,
                    WorkDescription =element.WorkDescription,
                   
                    Customer = new CustomerDTO()
                    {
                        Id = element.Customer.Id,
                        Person = new PersonDTO()
                        {   Id = element.Customer.Person.Id,
                            Name = element.Customer.Person.Name,
                            Surname = element.Customer.Person.Surname,
                            Phone = element.Customer.Person.Phone
                        }
                    }
                };

                

                if (element.Vendor != null)
                {
                    historyDto.Vendor = new VendorDTO()
                    {
                        Id = element.Vendor.Id,
                        Experience = element.Vendor.Experience
                    };
                }
                if (element.Company != null)
                {
                    historyDto.Company = new CompanyDTO()
                    {
                        Id = element.Company.Id,
                        Account = new AccountDTO() {
                            Id = element.Company.Account.Id,
                            DateCreated = element.Company.Account.DateCreated,
                            Rating = element.Company.Account.Rating },
                            Vendors = element.Company.Vendors.Select
                            (x => new VendorDTO { Id = x.Id, Person = new PersonDTO()
                            { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } })
                            .ToList()
                    };
                }
                datareturn.Add(historyDto);
            }
            return datareturn;
        }

        public async Task<HistoryDTO> GetById(long id)
        {
            var history = await _unitOfWork.HistoryRepository.GetByIdAsync(id);
            var historyDto = new HistoryDTO()
            {
                Id = history.Id,
                Date = history.Date,
                DateFinished = history.DateFinished,
                WorkDescription = history.WorkDescription,
                BookDescription = history.BookDescription,
                CategoryName = history.CategoryName,
                SubcategoryName = history.SubcategoryName,

                Customer = new CustomerDTO()
                {
                    Id = history.Customer.Id,
                    Person = new PersonDTO() { Id = history.Customer.Person.Id,
                        Name = history.Customer.Person.Name,
                        Surname = history.Customer.Person.Surname,
                        Phone = history.Customer.Person.Phone }
                }
            };

            if (history.Review != null)
            {
                historyDto.Review = new ReviewDTO()
                {
                    Id = history.Review.Id,
                    From = history.Review.From,
                    FromAccountId = history.Review.FromAccountId,
                    Avatar = history.Review.Avatar,
                    Date = history.Review.Date,
                    Description = history.Review.Description,
                    BookId = history.Review.BookId,
                    Grade = history.Review.Grade,
                    To = history.Review.To,
                    ToAccountId = history.Review.ToAccountId
                };
            }
            if (history.Vendor != null)
            {
                historyDto.Vendor = new VendorDTO()
                {
                    Id = history.Vendor.Id,
                    Experience = history.Vendor.Experience
                };
            }
            if (history.Company != null)
            {
                historyDto.Company = new CompanyDTO()
                {
                    Id = history.Company.Id,
                    Account = new AccountDTO() { Id = history.Company.Account.Id,
                        DateCreated = history.Company.Account.DateCreated,
                        Rating = history.Company.Account.Rating },
                    Vendors = history.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                };
            }
            return historyDto;
        }
    }
}
