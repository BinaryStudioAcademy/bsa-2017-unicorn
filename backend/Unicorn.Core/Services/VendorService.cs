using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;
using Unicorn.Shared.DTOs.Register;
using Unicorn.Shared.DTOs.Vendor;
using Unicorn.Shared.DTOs.Contact;
using Unicorn.Shared.DTOs.Book;
using Unicorn.DataAccess.Entities.Enum;

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
                .Include(v => v.Person.Account.Location)
                .Include(v => v.PortfolioItems)
                .Include(v => v.Company)
                .ToListAsync();

            return vendors.Select(v => VendorToDTO(v));
        }

        public async Task<ShortVendorDTO> GetByIdAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account.Location)
                .Include(v => v.PortfolioItems)
                .Include(v => v.Company)
                .SingleAsync(x => x.Id == id);
            return VendorToDTO(vendor);
        }

        public async Task<IEnumerable<ContactShortDTO>> GetVendorContactsAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .SingleAsync(x => x.Id == id);


            return vendor.Person.Account.Contacts
                .Where(c => !c.IsDeleted)
                .Select(c => new ContactShortDTO() {
                    Id = c.Id,
                    Type = c.Provider.Type,
                    Provider = c.Provider.Name,
                    ProviderId = c.Provider.Id,
                    Value = c.Value
                }).ToList();
        }

        public async Task<IEnumerable<CategoryDTO>> GetVendorCategoriesAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Works)
                .SingleAsync(x => x.Id == id);

            var works = vendor.Works.Where(w => !w.IsDeleted);
            return works.GroupBy(w => w.Subcategory.Category)
                .Select(g => new CategoryDTO()
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Description = g.Key.Description,
                    Icon = g.Key.Icon,
                    Subcategories = g.Key.Subcategories
                        .Select(s => new SubcategoryShortDTO()
                        {
                            Category = g.Key.Name,
                            CategoryId = g.Key.Id,
                            Name = s.Name,
                            Id = s.Id,
                            Description = s.Description,
                            Icon = s.Icon
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
                CroppedAvatar = vendor.Person.Account.CroppedAvatar,
                Background = vendor.Person.Account.Background,
                Company = vendor.Company?.Name,
                CompanyId = vendor.Company?.Id,
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                Name = vendor.Person.Name,
                Surname = vendor.Person.Surname,
                MiddleName = vendor.Person.MiddleName,
                Id = vendor.Id,
                AccountId = vendor.Person.Account.Id,
                City = vendor.Person.Account.Location.City,
                Location = new LocationDTO()
                {
                    Id = vendor.Person.Account.Location.Id,
                    Adress = vendor.Person.Account.Location.Adress,
                    City = vendor.Person.Account.Location.City,
                    Latitude = vendor.Person.Account.Location.Latitude,
                    Longitude = vendor.Person.Account.Location.Longitude,
                    PostIndex = vendor.Person.Account.Location.PostIndex
                },
                Position = vendor.Position,
                WorkLetter = vendor.WorkLetter,
                Birthday = vendor.Person.Birthday
            };
        }

        public async Task CreateAsync(VendorRegisterDTO ShortVendorDTO)
        {
            var account = new Account();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync((long)RoleType.Vendor);            
            var socialAccounts = new List<SocialAccount>();            
            var socialAccount = new SocialAccount();
            var vendor = new Vendor();
            var person = new Person();

            account.Role = role;
            account.DateCreated = DateTime.Now;
            account.Email = ShortVendorDTO.Email;
            account.Avatar = ShortVendorDTO.Image;
            account.Location = new Location()
            {
                Adress = ShortVendorDTO.Location.Adress,
                IsDeleted = false,
                City = ShortVendorDTO.Location.City,
                Latitude = ShortVendorDTO.Location.Latitude,
                Longitude = ShortVendorDTO.Location.Longitude,
                PostIndex = ShortVendorDTO.Location.PostIndex
            };

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

            vendor.Person = person;
            vendor.Experience = ShortVendorDTO.Experience;
            vendor.Position = ShortVendorDTO.Position;
            vendor.ExWork = ShortVendorDTO.Speciality;

            _unitOfWork.VendorRepository.Create(vendor);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ShortVendorDTO vendorDto)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Person)
                .Include(v => v.Person.Account)
                .Include(v => v.Person.Account.Location)
                .Include(v => v.Works)
                .Include(v => v.Company)
                .SingleAsync(x => x.Id == vendorDto.Id);
            vendor.Person.Account.Location = new Location()
            {
                Adress = vendorDto.Location.Adress,
                City = vendorDto.Location.City,
                IsDeleted = false,
                Latitude = vendorDto.Location.Latitude,
                Longitude = vendorDto.Location.Longitude,
                PostIndex = vendorDto.Location.PostIndex
            };
            vendor.WorkLetter = vendorDto.WorkLetter;
            vendor.Position = vendorDto.Position;
            vendor.Person.Birthday = vendorDto.Birthday; // Dirty hack, fix this later
            vendor.Person.Name = vendorDto.Name;
            vendor.Person.Surname = vendorDto.Surname;
            vendor.Person.MiddleName = vendorDto.MiddleName;

            _unitOfWork.VendorRepository.Update(vendor);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<WorkDTO>> GetVendorWorksAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Works)
                .SingleAsync(v => v.Id == id);
            return vendor.Works.Where(w => !w.IsDeleted).Select(w => new WorkDTO()
            {
                Id = w.Id,
                Category = w.Subcategory.Category.Name,
                CategoryId = w.Subcategory.Category.Id,
                Subcategory = w.Subcategory.Name,
                SubcategoryId = w.Subcategory.Id,
                Name = w.Name,
                Description = w.Description,
                Icon = string.IsNullOrEmpty(w.Icon) ? w.Subcategory.Category.Icon : w.Icon
            }).ToList();
        }
    }
}

