using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.WebAPI.Models;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSheetsController : ControllerBase
    {
        private readonly ITimeSheetsService _TimeSheetsService;

        public TimeSheetsController(ITimeSheetsService TimeSheetsService)
        {
            this._TimeSheetsService = TimeSheetsService;
        }

        // GET: api/TimeSheets
        [HttpGet]
        public ActionResult<List<Model.DTO.TimeSheetDTO>> GetTimeSheets([FromQuery] Model.Requests.TimeSheetSearchRequest request)
        {
            return _TimeSheetsService.GetTimeSheets(request);
        }

        // GET: api/TimeSheets/5
        [HttpGet("{id}")]
        public ActionResult<Model.DTO.TimeSheetDTO> GetTimeSheet(int id)
        {
            if (!_TimeSheetsService.TimeSheetExists(id))
                return NotFound();

            return _TimeSheetsService.GetTimeSheet(id);
        }

        // PUT: api/TimeSheets/5
        [HttpPut("{id}")]
        public ActionResult<Model.DTO.TimeSheetDTO> PutTimeSheet(int id, TimeSheetInsertRequest TimeSheet)
        {
            if (id != TimeSheet.TimeSheetId)
                return BadRequest();

            if (!_TimeSheetsService.TimeSheetExists(id))
                return NotFound();

            return _TimeSheetsService.PutTimeSheet(id, TimeSheet);
        }

        // POST: api/TimeSheets
        [HttpPost]
        public ActionResult<Model.DTO.TimeSheetDTO> PostTimeSheet(TimeSheetInsertRequest TimeSheet)
        {
            var newTimeSheet = _TimeSheetsService.PostTimeSheet(TimeSheet);

            return CreatedAtAction(nameof(GetTimeSheet), new { id = newTimeSheet.TimeSheetId }, newTimeSheet);
        }

        // DELETE: api/TimeSheets/5
        [HttpDelete("{id}")]
        public ActionResult<Model.DTO.TimeSheetDTO> DeleteTimeSheet(int id)
        {
            if (!_TimeSheetsService.TimeSheetExists(id))
                return NotFound();

            return _TimeSheetsService.DeleteTimeSheet(id);
        }
    }
}
