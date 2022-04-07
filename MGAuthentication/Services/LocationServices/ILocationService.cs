using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Services.LocationServices
{
    public interface ILocationService
    {
        Task<CurrentLocation> GetLocationById(int locationId);

        void EditLocation(LocationEditVM editVM);

        IEnumerable<LocationReadRM> GetAllEmployeeLocations(DateTime requestedDate);

        LocationReadRM GetLocationByUserId(string id, DateTime requestedDate);

        void UpdateUserLocation(string userId, LocationUpdateVM updateDto);

        LocationReadRM GetLocationHistory(string userId, DateTime requestedDate);
    }
}