using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.WebAPI.Services
{
	public interface ITimeSheetsService
	{
		public List<Model.DTO.TimeSheetDTO> GetTimeSheets(TimeSheetSearchRequest searchRequest);
		public Model.DTO.TimeSheetDTO GetTimeSheet(int id);
		public Model.DTO.TimeSheetDTO PostTimeSheet(TimeSheetInsertRequest TimeSheet);
		public Model.DTO.TimeSheetDTO PutTimeSheet(int id, TimeSheetInsertRequest TimeSheet);
		public Model.DTO.TimeSheetDTO DeleteTimeSheet(int id);
		public bool TimeSheetExists(int id);
	}
}
