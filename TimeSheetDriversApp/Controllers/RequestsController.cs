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
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsService _RequestsService;

        public RequestsController(IRequestsService RequestsService)
        {
            this._RequestsService = RequestsService;
        }

        // GET: api/Requests
        [HttpGet]
        public ActionResult<List<Model.DTO.RequestDTO>> GetRequests([FromQuery]RequestSearchRequest Request)
        {
            return _RequestsService.GetRequests(Request);
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public ActionResult<Model.DTO.RequestDTO> GetRequest(int id)
        {
            if (!_RequestsService.RequestExists(id))
                return NotFound();

            return _RequestsService.GetRequest(id);
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public ActionResult<Model.DTO.RequestDTO> PutRequest(int id, RequestInsertRequest Request)
        {
            if (id != Request.RequestId)
                return BadRequest();

            if (!_RequestsService.RequestExists(id))
                return NotFound();

            return _RequestsService.PutRequest(id, Request);
        }

        // POST: api/Requests
        [HttpPost]
        public ActionResult<Model.DTO.RequestDTO> PostRequest(RequestInsertRequest Request)
        {
            var newRequest = _RequestsService.PostRequest(Request);

            return CreatedAtAction(nameof(GetRequest), new { id = newRequest.RequestId }, newRequest);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public ActionResult<Model.DTO.RequestDTO> DeleteRequest(int id)
        {
            if (!_RequestsService.RequestExists(id))
                return NotFound();

            return _RequestsService.DeleteRequest(id);
        }
    }
}
