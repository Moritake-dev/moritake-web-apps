using AutoMapper;
using MGAuthentication.Data.ViewModels.Common.JobPosition;
using MGAuthentication.Services.CommonServices.JobPositionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers.Common
{
    [Authorize("RequireAdmin")]
    public class JobPositionController : Controller
    {
        private readonly IJobPositionService _jobPositionService;
        private readonly IMapper _mapper;

        public JobPositionController(IJobPositionService jobPositionService, IMapper mapper)
        {
            _jobPositionService = jobPositionService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _jobPositionService.GetAllJobPositions();
            return View(result);
        }

        [HttpGet]
        public IActionResult DeletedData()
        {
            var result = _jobPositionService.GetAllDeletedJobPositions();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPositionCreateVM createDto)
        {
            var result = await _jobPositionService.Create(createDto);
            if (result != null)
            {
                return RedirectToAction("Index", result);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int jobPositionId)
        {
            var model = await _jobPositionService.GetJobPositionById(jobPositionId);

            var result = _mapper.Map<JobPositionUpdateVM>(model);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobPositionUpdateVM updateVM)
        {
            try
            {
                await _jobPositionService.UpdateAsync(updateVM.Id, updateVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int jobPositionId)
        {
            return View(new JobPositionReadVM { Id = jobPositionId });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(JobPositionReadVM vm)
        {
            _jobPositionService.Delete(vm.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Restore(int jobPositionId)
        {
            try
            {
                var result = await _jobPositionService.RestoreAsync(jobPositionId);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DeletedData", ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}