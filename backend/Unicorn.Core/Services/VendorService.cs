using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VendorDTO>> GetAllAsync()
        {
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();

            return vendors.Select(v => VendorToDTO(v));
        }

        public async Task<VendorDTO> GetByIdAsync(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            return VendorToDTO(vendor);
        }

        private VendorDTO VendorToDTO(Vendor vendor)
        {
            return new VendorDTO()
            {
                AvatarUrl = vendor.Person.Account.Avatar,
                Company = vendor.Company?.Name,
                CompanyId = vendor.Company?.Id,
                Experience = vendor.Experience,
                ExWork = vendor.ExWork,
                FIO = $"{vendor.Person.Name} {vendor.Person.Surname}",
                Id = vendor.Id,
                City = vendor.Person.Location.City,
                LocationId = vendor.Person.Location.Id,
                Position = vendor.Position,
                WorkLetter = vendor.WorkLetter
            };
        }
    }
}

