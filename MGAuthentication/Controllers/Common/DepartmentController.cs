using AutoMapper;
using MGAuthentication.Data.ViewModels.Common.Department;
using MGAuthentication.Services.CommonServices.DepartmentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers.Common
{
    [Authorize("RequireAdmin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _departmentService.GetAllDepartments();
            return View(result);
        }

        [HttpGet]
        public IActionResult DeletedData()
        {
            var result = _departmentService.GetAllDeletedDepartments();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateVM createDto)
        {
            var result = await _departmentService.Create(createDto);
            if (result != null)
            {
                return RedirectToAction("Index", result);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int departmentId)
        {
            var model = await _departmentService.GetDepartmentById(departmentId);

            var result = _mapper.Map<DepartmentUpdateVM>(model);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentUpdateVM updateVM)
        {
            try
            {
                await _departmentService.UpdateAsync(updateVM.Id, updateVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int departmentId)
        {
            return View(new DepartmentReadVM { Id = departmentId });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(DepartmentReadVM vm)
        {
            _departmentService.Delete(vm.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Restore(int departmentId)
        {
            try
            {
                var result = await _departmentService.RestoreAsync(departmentId);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DeletedData", ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}