using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.ViewModels;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersService usersAPIService;

        public LoginController(IUsersService usersAPIService)
        {
            this.usersAPIService = usersAPIService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SetCurrentUser(null);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Login(LoginVM VM)
        {
            var user = usersAPIService.Authenticate(VM.Username, VM.Password);
            if (user != null)
            {
                HttpContext.SetCurrentUser(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["login_error"] = "Username or password is not correct";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
