using MGAuthentication.Data;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Role;
using MGAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Roles.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM roleVM)
        {
            var isExist = await _roleManager.RoleExistsAsync(roleVM.Name);
            if (!isExist)
            {
                var role = new IdentityRole { Name = roleVM.Name, NormalizedName = roleVM.Name.ToUpper() };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AssignRoleToUser()
        {
            var users = _userService.GetAllUserInfo();
            var userList = new SelectList(users, "UserId", "FullName");
            ViewBag.Employees = userList;

            var roles = _context.Roles.ToList();
            var roleList = new SelectList(roles, "Id", "Name");
            ViewBag.Roles = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByIdAsync(vm.UserId);

            var role = await _roleManager.FindByIdAsync(vm.RoleId);

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                return View(result.Errors.FirstOrDefault());
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AssignClaimToUser()
        {
            // populating users for the dropdown
            var users = _userService.GetAllUserInfo();
            var userList = new SelectList(users, "UserId", "FullName");
            ViewBag.Employees = userList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignClaimToUser(AssignClaimToUserVM vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            var result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(vm.ClaimType, vm.ClaimValue));
            if (!result.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteUserFromRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteUserFromRole(DeleteUserFromRoleVM vm)
        {
            return View();
        }
    }
}