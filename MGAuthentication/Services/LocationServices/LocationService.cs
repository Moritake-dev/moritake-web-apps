using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Location;
using MGAuthentication.Respositories.CurrentLocationRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly ICurrentLocationRepository _locationRepository;

        public LocationService(ICurrentLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<CurrentLocation> GetLocationById(int locationId)
        {
            var result = await _locationRepository.GetById(locationId);
            return result;
        }

        public IEnumerable<LocationReadRM> GetAllEmployeeLocations(DateTime requestedDate)
        {
            var result = _locationRepository.GetAllEmployeeLocations(requestedDate);
            return result;
        }

        public LocationReadRM GetLocationByUserId(string userId, DateTime requestedDate)
        {
            var result = _locationRepository.GetLocationByUserId(userId, requestedDate);
            return result;
        }

        public LocationReadRM GetLocationHistory(string userId, DateTime requestedDate)
        {
            var result = _locationRepository.GetLocationHisotry(userId, requestedDate);
            return result;
        }

        public void EditLocation(LocationEditVM editVM)
        {
            _locationRepository.Edit(editVM);
        }

        public void UpdateUserLocation(string userId, LocationUpdateVM updateDto)
        {
            if (updateDto.EffectiveDate == DateTime.MinValue)
            {
                updateDto.EffectiveDate = DateTime.Now;
            }
            updateDto.IsApproved = false;

            _locationRepository.Update(userId, updateDto);
        }
    }
}