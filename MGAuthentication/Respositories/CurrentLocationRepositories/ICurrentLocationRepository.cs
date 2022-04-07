using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.CurrentLocationRepositories
{
    public interface ICurrentLocationRepository
    {
        IEnumerable<CurrentLocation> GetAll();

        IEnumerable<CurrentLocation> GetAllDeleted();

        Task<CurrentLocation> GetById(int currentLocationId);

        Task Create(CurrentLocation currentLocation);

        void Update(string userId, LocationUpdateVM updateVM);

        void Delete(int currentLocationId);

        Task<CurrentLocation> RestoreAsync(int currentLocationId);

        Task SaveChangesAsync();

        void Edit(LocationEditVM editVM);

        IEnumerable<LocationReadRM> GetAllEmployeeLocations(DateTime requestedDate);

        LocationReadRM GetLocationByUserId(string userId, DateTime requestedDate);

        LocationReadRM GetLocationHisotry(string userId, DateTime requestedDate);
    }
}