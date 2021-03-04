using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.WebAPI.Services
{
	public interface IAuditFilesService
	{
		public List<Model.DTO.AuditFileDTO> GetAuditFiles(AuditFileSearchRequest request);
		public Model.DTO.AuditFileDTO GetAuditFile(int id);
		public Model.DTO.AuditFileDTO PostAuditFile(AuditFileInsertRequest AuditFile);
		public Model.DTO.AuditFileDTO PutAuditFile(int id, AuditFileInsertRequest AuditFile);
		public Model.DTO.AuditFileDTO DeleteAuditFile(int id);
		public bool AuditFileExists(int id);
	}
}
