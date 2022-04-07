using MGAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MGAuthentication.Controllers.ApiControllercs
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUserInfo()
        {
            return Ok(_userService.GetAllUserInfo());
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserInfoById(string userId)
        {
            return Ok(_userService.GetUserInfoById(userId));
        }
    }
}