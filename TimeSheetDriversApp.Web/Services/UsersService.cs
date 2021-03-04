using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
    public class UsersService: IUsersService
    {
        private readonly WebAPI.Services.IUsersService usersAPIService;
        private readonly WebAPI.Services.IRolesService rolesAPIService;
        private readonly WebAPI.Services.ITimeSheetsService timeSheetsAPIService;
        private readonly IMapper mapper;

        public UsersService(WebAPI.Services.IUsersService usersAPIService, WebAPI.Services.IRolesService rolesAPIService, WebAPI.Services.ITimeSheetsService timeSheetsAPIService, IMapper mapper)
        {
            this.usersAPIService = usersAPIService;
            this.rolesAPIService = rolesAPIService;
            this.timeSheetsAPIService = timeSheetsAPIService;
            this.mapper = mapper;
        }

        public void CreateUser(UserInsertRequest request)
        {
            var user = usersAPIService.PostUser(request);
            var role = usersAPIService.GetUser(user.UserId).Role.RoleName;

            if (role == "User")
            {
                var today = DateTime.Today;
                var entryDate = today;
                var DaysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                for (int i = today.Day; i <= DaysInMonth; i++)
                {
                    timeSheetsAPIService.PostTimeSheet(new TimeSheetInsertRequest
                    {
                        EntryDate = entryDate,
                        UserId = user.UserId
                    });

                    entryDate = entryDate.AddDays(1);
                }
            }
        }

        public List<UserDTO> GetUsers()
        {
            return usersAPIService.GetUsers();
        }

        public UsersCreateVM SetupCreateUserForm()
        {
            var VM = new UsersCreateVM
            {
                RoleList = rolesAPIService.GetRoles().Select(x => new SelectListItem
                {
                    Text = x.RoleName,
                    Value = x.RoleId.ToString()
                }).ToList()
            };

            return VM;
        }

        public UsersUpdateVM SetupUpdateUserForm(int Id)
        {
            var user = usersAPIService.GetUser(Id);
            if (user == null)
                return null;

            var VM = new UsersUpdateVM
            {
                RoleList = rolesAPIService.GetRoles().Select(x => new SelectListItem
                {
                    Text = x.RoleName,
                    Value = x.RoleId.ToString()
                }).ToList(),
                Request = mapper.Map<UserUpdateRequest>(user)
            };

            return VM;
        }

        public UserDTO UpdateUser(int Id, UserUpdateRequest request)
        {
            var user = usersAPIService.GetUser(Id);
            if (user == null)
                return null;

            return usersAPIService.PutUser(Id, request);
        }
    }
}
