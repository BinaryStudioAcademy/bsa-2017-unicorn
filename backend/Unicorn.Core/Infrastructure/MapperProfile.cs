using System.Collections.Generic;
using AutoMapper;
using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Infrastructure
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(c => c.Director, opt => opt
                    .MapFrom(s => Mapper.Map<Account, AccountDTO>(s.Director)))
                .ForMember(c => c.Account, opt => opt
                    .MapFrom(s => Mapper.Map<Account, AccountDTO>(s.Account)))
                .ForMember(c => c.Location, opt => opt
                    .MapFrom(s => Mapper.Map<Location, LocationDTO>(s.Location)))
                .ForMember(c => c.Vendors, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Vendor>, ICollection<VendorDTO>>(s.Vendors)));

            CreateMap<Account, AccountDTO>()
                .ForMember(c => c.Role, opt => opt
                    .MapFrom(s => Mapper.Map<Role, RoleDTO>(s.Role)))
                .ForMember(c => c.SocialAccounts, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<SocialAccount>, ICollection<SocialAccountDTO>>(s.SocialAccounts)))
                .ForMember(c => c.Permissions, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Permission>, ICollection<PermissionDTO>>(s.Permissions)));

            CreateMap<Book, BookDTO>()
                .ForMember(c => c.Customer, opt => opt
                    .MapFrom(s => Mapper.Map<Customer, CustomerDTO>(s.Customer)))
                .ForMember(c => c.Vendor, opt => opt
                    .MapFrom(s => Mapper.Map<Vendor, VendorDTO>(s.Vendor)))
                .ForMember(c => c.Company, opt => opt
                    .MapFrom(s => Mapper.Map<Company, CompanyDTO>(s.Company)))
                .ForMember(c => c.Work, opt => opt
                    .MapFrom(s => Mapper.Map<Work, WorkDTO>(s.Work)))
                .ForMember(c => c.Location, opt => opt
                    .MapFrom(s => Mapper.Map<Location, LocationDTO>(s.Location)));

            CreateMap<Category, CategoryDTO>()
                .ForMember(c => c.Subcategories, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Subcategory>, ICollection<SubcategoryDTO>>(s.Subcategories)));

            CreateMap<Customer, CustomerDTO>()
                .ForMember(c => c.Person, opt => opt
                    .MapFrom(s => Mapper.Map<Person, PersonDTO>(s.Person)))
                .ForMember(c => c.Books, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Book>, ICollection<BookDTO>>(s.Books)));

            CreateMap<Permission, PermissionDTO>()
                .ForMember(c => c.Accounts, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Account>, ICollection<AccountDTO>>(s.Accounts)));

            CreateMap<Person, PersonDTO>()
                .ForMember(c => c.Account, opt => opt
                    .MapFrom(s => Mapper.Map<Account, AccountDTO>(s.Account)))
                .ForMember(c => c.Location, opt => opt
                    .MapFrom(s => Mapper.Map<Location, LocationDTO>(s.Location)));

            CreateMap<Role, RoleDTO>()
                .ForMember(c => c.Accounts, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Account>, ICollection<AccountDTO>>(s.Accounts)));

            CreateMap<SocialAccount, SocialAccountDTO>()
                .ForMember(c => c.Account, opt => opt
                    .MapFrom(s => Mapper.Map<Account, AccountDTO>(s.Account)));

            CreateMap<Subcategory, SubcategoryDTO>()
                .ForMember(c => c.Category, opt => opt
                    .MapFrom(s => Mapper.Map<Category, CategoryDTO>(s.Category)))
                .ForMember(c => c.Works, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Work>, ICollection<WorkDTO>>(s.Works)));

            CreateMap<Vendor, VendorDTO>()
                .ForMember(c => c.Company, opt => opt
                    .MapFrom(s => Mapper.Map<Company, CompanyDTO>(s.Company)))
                .ForMember(c => c.Works, opt => opt
                    .MapFrom(s => Mapper.Map<ICollection<Work>, ICollection<WorkDTO>>(s.Works)));

            CreateMap<Work, WorkDTO>()
                .ForMember(c => c.Subcategory, opt => opt
                    .MapFrom(s => Mapper.Map<Subcategory, SubcategoryDTO>(s.Subcategory)));

            CreateMap<History, HistoryDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<Review, ReviewDTO>();
        }
    }
}