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
            var locations = await _unitOfWork.LocationRepository.GetAllAsync();
            var datareturn = new List<LocationDTO>();
            foreach (var location in locations)
            {
                LocationDTO locationDto = new LocationDTO()
                {
                    Id = location.Id,
                    Adress = location.Adress,
                    City = location.City,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                };
                datareturn.Add(locationDto);
            }
            return datareturn;
        }

        public async Task<LocationDTO> GetByIdAsync(long id)
        {
            var location = await _unitOfWork.LocationRepository.GetByIdAsync(id);
            LocationDTO locationDto = new LocationDTO()
            {
                Id = location.Id,
                Adress = location.Adress,
                City = location.City,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
            };
            return locationDto;
        }
    }
}
