using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Core.Converters;

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

            return vendors.Select(v => VendorDTOConverter.VendorToDTO(v));
        }

        public async Task<VendorDTO> GetById(long id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
            var location = await _unitOfWork.LocationRepository.GetByIdAsync(id);
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            var vendorDto = VendorDTOConverter.VendorToDTO(vendor);
            return vendorDto;
        }
    }
}

