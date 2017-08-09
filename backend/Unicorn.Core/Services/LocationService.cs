using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;
using Unicorn.Core.Interfaces;

namespace Unicorn.Core.Services
{
    class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Location, LocationDTO > ());
            return Mapper.Map<IEnumerable<Location>, List<LocationDTO>>(await _unitOfWork.LocationRepository.GetAllAsync());
        }
    }
}
