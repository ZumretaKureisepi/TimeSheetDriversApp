using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.WebAPI.Services
{
	public interface IRequestsService
	{
		public List<Model.DTO.RequestDTO> GetRequests(RequestSearchRequest req);
		public Model.DTO.RequestDTO GetRequest(int id);
		public Model.DTO.RequestDTO PostRequest(RequestInsertRequest Request);
		public Model.DTO.RequestDTO PutRequest(int id, RequestInsertRequest Request);
		public Model.DTO.RequestDTO DeleteRequest(int id);
		public bool RequestExists(int id);
	}
}
