using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;

namespace TimeSheetDriversApp.WebAPI.Services
{
	public interface IUsersService
	{

		public List<Model.DTO.UserDTO> GetUsers();
		public Model.DTO.UserDTO GetUser(int id);
		public Model.DTO.UserDTO MyProfile();
		public Model.DTO.UserDTO PostUser(UserInsertRequest user);
		public Model.DTO.UserDTO PutUser(int id, UserUpdateRequest user);
		public Model.DTO.UserDTO DeleteUser(int id);
		public bool UserExists(int id);

		public void SetCurrentUser(Model.DTO.UserDTO user);
		public Model.DTO.UserDTO GetCurrentUser();
        public Model.DTO.UserDTO Authenticate(string username, string password);
    }
}
