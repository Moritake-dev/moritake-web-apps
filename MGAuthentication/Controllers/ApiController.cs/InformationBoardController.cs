using AutoMapper;
using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using MGAuthentication.Services.CommonServices.InformationBoardServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MGAuthentication.Controllers.ApiControllercs
{
    [ApiController]
    [Route("api/info")]
    public class InformationBoardController : Controller
    {
        private readonly IInformationBoardService _infoService;

        public InformationBoardController(IInformationBoardService infoService)
        {
            _infoService = infoService;
        }

        // GET ALL
        // URL : api/info/messageboard?requestedDate=2020-12-30
        [HttpGet]
        public IActionResult GetAllMessages(string requestedDate = null)
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

            return Ok(_infoService.GetAll());
        }

        // GET BY ID
        // URL : api/info/{id}
        [HttpGet("{infoId}", Name = nameof(GetInfoById))]
        public async Task<IActionResult> GetInfoById(int infoId)
        {
            var model = await _infoService.GetInfoById(infoId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // CREATE
        // URL : api/info
        [HttpPost]
        [Route("create")]
        public IActionResult CreateMessage(InformationBoardCreateDto infoCreateDto)
        {
            if (infoCreateDto.UserId == null)
            {
                return BadRequest();
            }
            var messageModel = _infoService.Create(infoCreateDto).GetAwaiter().GetResult();
            return CreatedAtRoute(nameof(GetInfoById), new { infoId = messageModel.Id }, messageModel);
            //return Ok(messageModel.Result);
        }

        // PATCH
        // URL : api/info/update/{id}
        [HttpPost("update/{infoId}")]
        public IActionResult Edit(int infoId, [FromBody] InformationBoardUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            _infoService.UpdateAsync(updateDto).GetAwaiter().GetResult();

            return NoContent();
        }

        // DELETE
        // URL : api/info/delete/{id}
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _infoService.Delete(id);
            return NoContent();
        }

        // RESTORE
        // URL : api/info/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _infoService.RestoreAsync(id);
            return Ok(result);
        }
    }
}