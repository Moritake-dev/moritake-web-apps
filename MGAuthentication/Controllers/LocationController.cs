using AutoMapper;
using IdentityServer4.Extensions;
using MGAuthentication.Data.ViewModels.Location;
using MGAuthentication.Services;
using MGAuthentication.Services.LocationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;

        public LocationController(ILocationService locationService, IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _locationService = locationService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _locationService.GetAllEmployeeLocations(DateTime.Today);
            return View(result);
        }

        // Editing the existing location
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> EditGet(int locationId)
        {
            var locations = await _locationService.GetLocationById(locationId);
            var result = _mapper.Map<LocationEditVM>(locations);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(LocationEditVM editVM)
        {
            // assigning the userid when pressed the edit button by the user
            try
            {
                _locationService.EditLocation(editVM);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return View("EditConfirmation");
        }

        // Adding new location to the database
        [HttpGet]
        public IActionResult LocationUpdate(string userId)
        {
            return View(new LocationUpdateVM { UserId = userId, FullName = _userService.GetUserInfoById(userId).FullName });
        }

        [HttpPost]
        public IActionResult LocationUpdate(string userId, LocationUpdateVM editVM)
        {
            if (userId == null)
            {
                userId = User.GetSubjectId();
                editVM.UserId = userId;
            }
            try
            {
                _locationService.UpdateUserLocation(userId, editVM);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MonthlyLocationHistory()
        {
            var userId = User.GetSubjectId();
            var result = _locationService.GetLocationHistory(userId, DateTime.UtcNow);
            return View(result);
        }
    }
}