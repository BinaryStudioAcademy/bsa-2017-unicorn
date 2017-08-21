using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Unicorn.Core.Interfaces;
using Unicorn.Core.Services.Helpers;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.Vendor;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Book;

namespace Unicorn.Core.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ShortVendorDTO>> GetAllAsync()
        {
            var vendors = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Location)
                .Include(v => v.PortfolioItems)
                .Include(v => v.Works)
                .Include(v => v.Company)
                .ToListAsync();

            return vendors.Select(v => VendorToDTO(v));
        }

        public async Task<ShortVendorDTO> GetByIdAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Location)
                .Include(v => v.PortfolioItems)
                .Include(v => v.Works)
                .Include(v => v.Company)
                .SingleAsync(x => x.Id == id);
            return VendorToDTO(vendor);
        }

        public async Task<IEnumerable<ContactShortDTO>> GetVendorContacts(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .SingleAsync(x => x.Id == id);


            return vendor.Person.Account.Contacts.Select(c => new ContactShortDTO() {
                Id = c.Id,
                Type = c.Provider.Type,
                Provider = c.Provider.Name,
                Value = c.Value
            });
        }

        public async Task<IEnumerable<CategoryDTO>> GetVendorCategoriesAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Works)
                .SingleAsync(x => x.Id == id);

            var works = vendor.Works;
            return works.GroupBy(w => w.Subcategory.Category)
                .Select(g => new CategoryDTO()
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Description = g.Key.Description,
                    Subcategories = g.Key.Subcategories
                        .Select(s => new SubcategoryShortDTO()
                        {
                            Category = g.Key.Name,
                            CategoryId = g.Key.Id,
                            Name = s.Name,
                            Id = s.Id,
                            Description = s.Description
                        }).ToList()
                }).ToList();
        }

        public async Task<long> GetVendorAccountIdAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .SingleAsync(x => x.Id == id);
            return vendor.Person.Account.Id;
        }

        private ShortVendorDTO VendorToDTO(Vendor vendor)
        {
            return new ShortVendorDTO()
            {
                Avatar = vendor.Person.Account.Avatar,
                Background = vendor.Person.Account.Background,
                Company = vendor.Company?.Name,
                CompanyId = vendor.Company?.Id,
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                Name = vendor.Person.Name,
                Surname = vendor.Person.Surname,
                MiddleName = vendor.Person.MiddleName,
                Id = vendor.Id,
                City = vendor.Person.Location.City,
                LocationId = vendor.Person.Location.Id,
                Position = vendor.Position,
                WorkLetter = vendor.WorkLetter,
                Birthday = vendor.Person.Birthday,
                Works = vendor.Works.Select(w => new WorkDTO()
                {
                    Id = w.Id,
                    Description = w.Description,
                    Name = w.Name,
                    Category = w.Subcategory.Category.Name,
                    CategoryId = w.Subcategory.Category.Id,
                    Subcategory = w.Subcategory.Name,
                    SubcategoryId = w.Subcategory.Id
                }).ToList()
            };
        }

        public async Task Create(VendorRegisterDTO ShortVendorDTO)
        {
            var account = new Account();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)AccountRoles.Vendor);
            var permissions = new List<Permission>();
            var socialAccounts = new List<SocialAccount>();
            var socialAccount = new SocialAccount();
            var vendor = new Vendor();
            var person = new Person();

            account.Role = role;
            account.DateCreated = DateTime.Now;
            account.Email = ShortVendorDTO.Email;
            account.Avatar = "http://static.1927.kiev.ua/images/default_avatar.jpg";

            socialAccount.Provider = ShortVendorDTO.Provider;
            socialAccount.Uid = ShortVendorDTO.Uid;
            socialAccount.Account = account;

            socialAccounts.Add(socialAccount);
            account.SocialAccounts = socialAccounts;

            person.Birthday = ShortVendorDTO.Birthday;
            person.Phone = ShortVendorDTO.Phone;
            person.Name = ShortVendorDTO.FirstName;
            person.MiddleName = ShortVendorDTO.MiddleName;
            person.Surname = ShortVendorDTO.LastName;
            person.Account = account;
            person.Location = new Location();

            vendor.Person = person;
            vendor.Experience = ShortVendorDTO.Experience;
            vendor.Position = ShortVendorDTO.Position;
            vendor.ExWork = ShortVendorDTO.Speciality;

            _unitOfWork.VendorRepository.Create(vendor);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(ShortVendorDTO vendorDto)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .Include(v => v.Works)
                .Include(v => v.Company)
                .SingleAsync(x => x.Id == vendorDto.Id);

            vendor.WorkLetter = vendorDto.WorkLetter;
            vendor.Position = vendorDto.Position;
            vendor.Person.Birthday = vendorDto.Birthday.AddDays(1); // Dirty hack, fix this later
            vendor.Person.Name = vendorDto.Name;
            vendor.Person.Surname = vendorDto.Surname;
            vendor.Person.MiddleName = vendorDto.MiddleName;
            vendor.Works = vendorDto.Works.Select(w => _unitOfWork.WorkRepository.Query.Single(x => x.Id == w.Id)).ToList();

            _unitOfWork.VendorRepository.Update(vendor);
            await _unitOfWork.SaveAsync();
        }
    }
}

