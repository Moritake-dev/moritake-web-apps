using IdentityServer4.Extensions;
using IdentityServer4.Services;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Auth;
using MGAuthentication.Services;
using MGAuthentication.Services.CommonServices.DepartmentServices;
using MGAuthentication.Services.CommonServices.JobPositionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IJobPositionService _jobPositionService;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IIdentityServerInteractionService interactionService,
            IUserService userService,
            IDepartmentService departmentService,
            IJobPositionService jobPositionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _userService = userService;
            _departmentService = departmentService;
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout(LoginViewModel viewModel)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

        //    if (result.Succeeded)
        //    {
        //        if (viewModel.ReturnUrl != null)
        //        {
        //            return Redirect(viewModel.ReturnUrl);
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }

        //    //var user = User.Claims.ToList();

        //    return View();
        //}

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

            if (result.Succeeded)
            {
                if (viewModel.ReturnUrl != null)
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Error", "Username or password does not match");
            //var user = User.Claims.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var departments = _departmentService.GetAllDepartments();
            var departmentsList = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = departmentsList;

            var jobPositions = _jobPositionService.GetAllJobPositions();
            var jobPositionsList = new SelectList(jobPositions, "Id", "Name");
            ViewBag.JobPositions = jobPositionsList;

            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [Authorize("RequireAdmin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            // Creating the new user
            var user = new ApplicationUser
            {
                UserName = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DateOfBirth = viewModel.DateOfBirth,
                EmergencyContactNumber = viewModel.EmergencyContactNumber,
                Gender = viewModel.Gender,
                BloodType = viewModel.BloodType,
                RestType = viewModel.RestType,
                EmployeeOrder = (int)viewModel.EmployeeOrder,
                IsAdmin = viewModel.IsAdmin,
                IsActiveEmployee = viewModel.IsActiveEmployee,
                IsEmployeeProfile = viewModel.IsEmployeeProfile,
                DepartmentId = viewModel.DepartmentId,
                JobPositionId = viewModel.JobPositionId
            };
            user.Address = new Address(viewModel.PostNo, viewModel.Address);

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            // Assigining the claims for the location feature in this app
            // TODO : need to clear this hardcoding and make a handler for the claims
            _userManager.AddClaimAsync(user, new Claim("feature_access", "location")).GetAwaiter().GetResult();

            // if the user creating succeeds, login
            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, false);
                //if (viewModel.ReturnUrl != null)
                //{
                //    return Redirect(viewModel.ReturnUrl);
                //}
                return RedirectToAction("AdminPanel", "Home");
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM passwordDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("model", "Please enter correct values");
                return View(passwordDto);
            }
            var user = await _userManager.FindByIdAsync(User.GetSubjectId());
            var passwordCheck = await _userManager.CheckPasswordAsync(user, passwordDto.CurrentPassword);
            if (passwordCheck)
            {
                var result = await _userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "User");
                }
            }
            ModelState.AddModelError("error", "Something went wrong");
            return View(passwordDto);
        }

        [Authorize("RequireAdmin")]
        [HttpGet]
        public IActionResult PasswordReset()
        {
            var users = _userService.GetAllUserInfo();
            var userList = new SelectList(users, "UserId", "FullName");
            ViewBag.Users = userList;

            return View();
        }

        [Authorize("RequireAdmin")]
        [HttpPost]
        public async Task<IActionResult> PasswordReset(string userId, PasswordResetVM resetVM)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("model", "Please enter correct values");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordChanged = await _userManager.ResetPasswordAsync(user, token, resetVM.NewPassword);

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}