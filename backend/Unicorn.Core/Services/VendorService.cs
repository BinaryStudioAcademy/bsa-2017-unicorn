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

        public async Task<IEnumerable<SubcategoryShortDTO>> GetVendorCategoriesAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.Query
                .Include(v => v.Works)
                .SingleAsync(x => x.Id == id);

            var works = vendor.Works;
            return works.GroupBy(w => w.Subcategory)
                .Select(g => new SubcategoryShortDTO()
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Category = g.Key.Category.Name,
                    CategoryId = g.Key.Category.Id,
                    Description = g.Key.Description
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
                Works = vendor.Works.Select(w => new WorkDTO()
                {
                    Id = w.Id,
                    Description = w.Description,
                    Name = w.Name,
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

        public Task<ShortVendorDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}

