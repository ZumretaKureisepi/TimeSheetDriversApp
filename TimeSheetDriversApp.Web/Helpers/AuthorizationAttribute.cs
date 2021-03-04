using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.WebAPI.Models;

namespace TimeSheetDriversApp.Web.Helpers
{

    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute(bool superuser = false, bool admin = false, bool user = false)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { superuser, admin, user };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool superuser = false, bool admin = false, bool user = false)
        {
            _superuser = superuser;
            _admin = admin;
            _user = user;
        }
        private readonly bool _superuser;
        private readonly bool _admin;
        private readonly bool _user;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            UserDTO user = filterContext.HttpContext.GetCurrentUser();

            if (user == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["login_error"] = "You are not authenticated.";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
                return;
            }

            RoleDTO role = user.Role;
            if (role != null)
            {
                if ((_superuser && role.RoleName == "Super User") ||
                    (_admin && role.RoleName == "Admin") ||
                    (_user && role.RoleName == "User"))
                {
                    await next();
                    return;
                }

            }


            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["login_error"] = "You are not authorized.";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}