using AutoMapper;
using IdentityServer4.Extensions;
using MGAuthentication.Data.ViewModels.UserProfile;
using MGAuthentication.Services;
using MGAuthentication.Services.CommonServices.DepartmentServices;
using MGAuthentication.Services.CommonServices.JobPositionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace MGAuthentication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        private readonly IJobPositionService _jobPositionService;

        public UserController(IUserService userService, IMapper mapper,
            IDepartmentService departmentService,
            IJobPositionService jobPositionService)
        {
            _userService = userService;
            _mapper = mapper;
            _departmentService = departmentService;
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (User.IsAuthenticated())
            {
                var userId = User.GetSubjectId();
                var userInfo = _userService.GetUserInfoById(userId);

                var result = _mapper.Map<ProfileReadVM>(userInfo);

                return View(result);
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        [ActionName("Edit")]
        public IActionResult EditGet()
        {
            var departments = _departmentService.GetAllDepartments();
            var departmentsList = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = departmentsList;

            var jobPositions = _jobPositionService.GetAllJobPositions();
            var jobPositionsList = new SelectList(jobPositions, "Id", "Name");
            ViewBag.JobPositions = jobPositionsList;

            var userModel = _userService.GetUserInfoById(User.GetSubjectId());

            var result = _mapper.Map<ProfileUpdateVM>(userModel);

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProfileUpdateVM updateDto)
        {
            if (User.IsAuthenticated())
            {
                var userId = User.GetSubjectId();
                if (userId != null)
                {
                    try
                    {
                        _userService.ProfileUpdate(userId, updateDto);
                        return RedirectToAction("Profile");
                    }
                    catch (Exception ex)
                    {
                        return View(ex.Message);
                    }
                }
            }
            return View();
        }
    }
}