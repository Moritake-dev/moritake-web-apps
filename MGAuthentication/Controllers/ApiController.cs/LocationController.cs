using MGAuthentication.Services;
using MGAuthentication.Services.LocationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static IdentityServer4.IdentityServerConstants;

namespace MGAuthentication.Controllers.ApiControllercs
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    [Route("api/user/location")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;

        public LocationController(ILocationService locationService, IUserService userService)
        {
            _locationService = locationService;
            _userService = userService;
        }

        // URL : api/user/location?requestedDate=2020-12-30
        [HttpGet]
        public IActionResult GetAllEmployeeLocations(string requestedDate = null)
        {
            bool parsedDateSuccess = DateTime.TryParse(requestedDate, out DateTime parsedDate);
            if (!parsedDateSuccess)
            {
                parsedDate = DateTime.Today;
            }
            else if (parsedDate == DateTime.MinValue)
            {
                parsedDate = DateTime.Today;
            }

            return Ok(_locationService.GetAllEmployeeLocations(parsedDate));
        }

        // URL : api/user/location/{userId}?reuqestedDate=2020-12-31
        [HttpGet("{userId}")]
        public IActionResult GetLocationByUserId(string userId, string requestedDate = null)
        {
            if (!_userService.DoesExist(userId))
                return BadRequest();

            bool parsedDateSuccess = DateTime.TryParse(requestedDate, out DateTime parsedDate);

            if (!parsedDateSuccess)
            {
                parsedDate = DateTime.Today;
            }
            else if (parsedDate == DateTime.MinValue)
            {
                parsedDate = DateTime.Today;
            }

            return Ok(_locationService.GetLocationByUserId(userId, parsedDate));
        }

        // URL : /api/location/locationhistory/{userId}
        [Route("locationHistory/{userId}")]
        [HttpGet]
        public IActionResult GetLocationHistory(string userId, string requestedDate = null)
        {
            bool parsedDateSuccess = DateTime.TryParse(requestedDate, out DateTime parsedDate);

            if (!parsedDateSuccess)
            {
                parsedDate = DateTime.Today;
            }
            else if (parsedDate == DateTime.MinValue)
            {
                parsedDate = DateTime.Today;
            }

            return Ok(_locationService.GetLocationHistory(userId, parsedDate));
        }
    }
}