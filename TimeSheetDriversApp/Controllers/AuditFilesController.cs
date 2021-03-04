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
    public class AuditFilesController : ControllerBase
    {
        private readonly IAuditFilesService _AuditFilesService;

        public AuditFilesController(IAuditFilesService AuditFilesService)
        {
            this._AuditFilesService = AuditFilesService;
        }

        // GET: api/AuditFiles
        [HttpGet]
        public ActionResult<List<Model.DTO.AuditFileDTO>> GetAuditFiles([FromQuery] Model.Requests.AuditFileSearchRequest request)
        {
            return _AuditFilesService.GetAuditFiles(request);

        }
        // GET: api/AuditFiles/5
        [HttpGet("{id}")]
        public ActionResult<Model.DTO.AuditFileDTO> GetAuditFile(int id)
        {
            if (!_AuditFilesService.AuditFileExists(id))
                return NotFound();

            return _AuditFilesService.GetAuditFile(id);
        }

        // PUT: api/AuditFiles/5
        [HttpPut("{id}")]
        public ActionResult<Model.DTO.AuditFileDTO> PutAuditFile(int id, AuditFileInsertRequest AuditFile)
        {
            if (id != AuditFile.AuditFileId)
                return BadRequest();

            if (!_AuditFilesService.AuditFileExists(id))
                return NotFound();

            return _AuditFilesService.PutAuditFile(id, AuditFile);
        }

        // POST: api/AuditFiles
        [HttpPost]
        public ActionResult<Model.DTO.AuditFileDTO> PostAuditFile(AuditFileInsertRequest AuditFile)
        {
            var newAuditFile = _AuditFilesService.PostAuditFile(AuditFile);

            return CreatedAtAction(nameof(GetAuditFile), new { id = newAuditFile.AuditFileId }, newAuditFile);
        }

        // DELETE: api/AuditFiles/5
        [HttpDelete("{id}")]
        public ActionResult<Model.DTO.AuditFileDTO> DeleteAuditFile(int id)
        {
            if (!_AuditFilesService.AuditFileExists(id))
                return NotFound();

            return _AuditFilesService.DeleteAuditFile(id);
        }
    }
}
