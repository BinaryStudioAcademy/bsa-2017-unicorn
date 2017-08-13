using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Core.DTOs;
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

        public async Task<LocationDTO> GetByIdAsync(long id)
        {
            var location = await _unitOfWork.LocationRepository.GetByIdAsync(id);
            LocationDTO locationDto = new LocationDTO()
            {
                Id = location.Id,
                Adress = location.Adress,
                City = location.City,
                CoordinateX = location.CoordinateX,
                CoordinateY = location.CoordinateY,
                PostIndex = location.PostIndex           
            };
            return locationDto;
        }
    }
}
