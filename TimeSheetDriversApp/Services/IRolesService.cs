using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.WebAPI.Services
{
	public interface IRolesService
	{
		public List<Model.DTO.RoleDTO> GetRoles();
		public Model.DTO.RoleDTO GetRole(int id);
	}
}
