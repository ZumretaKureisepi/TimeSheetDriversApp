using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheetDriversApp.Web.ViewModels
{
    public class UsersUpdateVM
    {
        public Model.Requests.UserUpdateRequest Request { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
}
