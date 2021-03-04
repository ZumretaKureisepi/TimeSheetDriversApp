using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
    public interface IUsersService
    {
        List<Model.DTO.UserDTO> GetUsers();
        UsersCreateVM SetupCreateUserForm();
        Model.DTO.UserDTO UpdateUser(int id, UserUpdateRequest request);
        UsersUpdateVM SetupUpdateUserForm(int id);
        void CreateUser(UserInsertRequest request);
    }
}
