using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.History;
using Unicorn.Shared.DTOs.Review;
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
                    historyDto.Vendor = new ShortVendorDTO()
                    {
                        Id = element.Vendor.Id,
                        Birthday = element.Vendor.Person.Birthday,
                        Avatar = element.Vendor.Person.Account.Avatar,
                        Name = element.Vendor.Person.Name
                    };
                }

                if (element.Company != null)
                {
                    historyDto.Company = new CompanyDTO()
                    {
                        Id = element.Company.Id,
                        Account = new AccountDTO() {
                            Id = element.Company.Account.Id,
                            DateCreated = element.Company.Account.DateCreated
                            },
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
                    Person = new PersonDTO()
                    {
                        Id = history.Customer.Person.Id,
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

                    FromAccountId = history.Review.Sender.Id,
                    Avatar = history.Review.Sender.Avatar,
                    From = $"{history.Customer.Person.Name} {history.Customer.Person.Surname}",
                    Date = history.Review.Date,
                    Description = history.Review.Description,
                    BookId = history.Review.BookId,
                    To = history.Review.To,
                    ToAccountId = history.Review.ToAccountId
                };
            }
            if (history.Vendor != null)
            {
                historyDto.Vendor = new ShortVendorDTO()
                {
                    Name = "ololo"
                };
            }
            if (history.Company != null)
            {
                historyDto.Company = new CompanyDTO()
                {
                    Id = history.Company.Id,
                    Account = new AccountDTO() { Id = history.Company.Account.Id,
                        DateCreated = history.Company.Account.DateCreated
                        },
                    Vendors = history.Company.Vendors.Select(x => new VendorDTO { Id = x.Id, Person = new PersonDTO() { Id = x.Person.Id, Name = x.Person.Name, Surname = x.Person.Surname } }).ToList()
                };
            }
            return historyDto;
        }

        public async Task<IEnumerable<VendorHistoryDTO>> GetVendorHistoryAsync(long vendorId)
        {
            var history = await _unitOfWork.HistoryRepository.Query
                 .Include(h => h.Vendor)
                 .Where(h => h.Vendor.Id == vendorId).ToListAsync();
            return history
                    .Select(h => new VendorHistoryDTO()
                    {
                        Id = h.Id,
                        BookDescription = h.BookDescription,
                        Category = h.CategoryName,
                        Date = h.Date,
                        Subcategory = h.SubcategoryName,
                        WorkDescription = h.WorkDescription,
                        WorkId = h.WorkId,
                        Label = $"{h.WorkDescription}({h.Date})"
                    }).ToList();
        }
    }
}
