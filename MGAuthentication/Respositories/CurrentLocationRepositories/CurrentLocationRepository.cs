using MGAuthentication.Data;
using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.CurrentLocationRepositories
{
    public class CurrentLocationRepository : ICurrentLocationRepository
    {
        private readonly ApplicationDbContext _context;

        public CurrentLocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region CRUD

        public async Task Create(CurrentLocation currentLocation)
        {
            await _context.AddAsync(currentLocation);
        }

        public void Delete(int currentLocationId)
        {
            var result = _context.CurrentLocations.Find(currentLocationId);
            _context.Remove(result);
        }

        public IEnumerable<CurrentLocation> GetAll()
        {
            return _context.CurrentLocations.Where(x => x.IsDeleted == false).AsEnumerable();
        }

        public IEnumerable<CurrentLocation> GetAllDeleted()
        {
            return _context.CurrentLocations.Where(x => x.IsDeleted == true).AsEnumerable();
        }

        public async Task<CurrentLocation> GetById(int currentLocationId)
        {
            var result = await _context.CurrentLocations.FindAsync(currentLocationId);
            return result;
        }

        public async Task<CurrentLocation> RestoreAsync(int currentLocationId)
        {
            var result = await _context.CurrentLocations.FindAsync(currentLocationId);
            result.IsDeleted = false;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Edit(LocationEditVM editVM)
        {
            var locationFromDB = _context.CurrentLocations.Where(x => x.Id == editVM.Id).FirstOrDefault();

            locationFromDB.CurrentLocationPlan = editVM.CurrentLocationPlan;
            locationFromDB.CurrentLocationName = editVM.CurrentLocationName;

            _context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void Update(string userId, LocationUpdateVM updateVM)
        {
            var location = new CurrentLocation
            {
                CurrentLocationName = updateVM.CurrentLocationName,
                CurrentLocationPlan = updateVM.CurrentLocationPlan,
                EffectiveDate = updateVM.EffectiveDate,
                IsDeleted = false
            };
            _context.CurrentLocations.Add(location);
            _context.SaveChangesAsync().GetAwaiter().GetResult();

            var locationId = location.Id;
            var userLocation = new UserCurrentLocation
            {
                UserId = userId,
                CurrentLocationId = locationId,
                IsApproved = false
            };
            _context.UserCurrentLocation.Add(userLocation);
            _context.SaveChanges();
        }

        #endregion CRUD

        public IEnumerable<LocationReadRM> GetAllEmployeeLocations(DateTime requestedDate)
        {
            var result = _context.Users.Include(x => x.UserCurrentLocations).ThenInclude(x => x.CurrentLocation).AsNoTracking()
                .Where(x => x.IsEmployeeProfile == true && x.IsActiveEmployee == true)
                .Select(x => new LocationReadRM
                {
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    Gender = x.Gender,
                    EmployeeOrder = x.EmployeeOrder,
                    JobPositionName = x.JobPosition.Name,
                    CurrentLocations = x.UserCurrentLocations.Where(t => t.UserId == x.Id && t.CurrentLocation.EffectiveDate.Day == requestedDate.Day
                        && t.CurrentLocation.EffectiveDate.Month == requestedDate.Month)
                        .Select(t => new CurrentLocationRM
                        {
                            Id = t.CurrentLocation.Id,
                            LocationName = t.CurrentLocation.CurrentLocationName,
                            LocationPlan = t.CurrentLocation.CurrentLocationPlan,
                            EffectiveDate = t.CurrentLocation.EffectiveDate,
                            IsApproved = t.IsApproved,
                            ApprovedBy = t.ApprovedBy
                        }).OrderByDescending(t => t.EffectiveDate).OrderByDescending(t => t.EffectiveDate.TimeOfDay).ToList()
                }).OrderBy(x => x.EmployeeOrder).ToList();

            return result;
        }

        public LocationReadRM GetLocationByUserId(string userId, DateTime requestedDate)
        {
            var result = _context.Users.Where(x => x.Id == userId).Include(x => x.UserCurrentLocations).ThenInclude(x => x.CurrentLocation).AsNoTracking()
                .Select(x => new LocationReadRM
                {
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    Gender = x.Gender,
                    EmployeeOrder = x.EmployeeOrder,
                    JobPositionName = x.JobPosition.Name,
                    CurrentLocations = x.UserCurrentLocations.Where(t => t.UserId == x.Id && t.CurrentLocation.EffectiveDate.Day == requestedDate.Day)
                        .Select(t => new CurrentLocationRM
                        {
                            Id = t.CurrentLocation.Id,
                            LocationName = t.CurrentLocation.CurrentLocationName,
                            LocationPlan = t.CurrentLocation.CurrentLocationPlan,
                            EffectiveDate = t.CurrentLocation.EffectiveDate,
                            IsApproved = t.IsApproved,
                            ApprovedBy = t.ApprovedBy
                        }).OrderByDescending(t => t.EffectiveDate).ToList()
                }).FirstOrDefault();

            return result;
        }

        public LocationReadRM GetLocationHisotry(string userId, DateTime requestedDate)
        {
            var requiredDateMax = requestedDate.AddDays(15);
            var requiredDateMin = requestedDate.AddDays(-15);
            var result = _context.Users.Where(x => x.Id == userId).Include(x => x.UserCurrentLocations).ThenInclude(x => x.CurrentLocation).AsNoTracking()
                .Select(x => new LocationReadRM
                {
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    Gender = x.Gender,
                    EmployeeOrder = x.EmployeeOrder,
                    JobPositionName = x.JobPosition.Name,
                    CurrentLocations = x.UserCurrentLocations.Where(t => t.UserId == x.Id && t.CurrentLocation.EffectiveDate.Date >= requiredDateMin && t.CurrentLocation.EffectiveDate.Date <= requiredDateMax)
                        .Select(t => new CurrentLocationRM
                        {
                            Id = t.CurrentLocation.Id,
                            LocationName = t.CurrentLocation.CurrentLocationName,
                            LocationPlan = t.CurrentLocation.CurrentLocationPlan,
                            EffectiveDate = t.CurrentLocation.EffectiveDate,
                            IsApproved = t.IsApproved,
                            ApprovedBy = t.ApprovedBy
                        }).OrderByDescending(t => t.EffectiveDate).ToList()
                }).FirstOrDefault();

            return result;
        }
    }
}