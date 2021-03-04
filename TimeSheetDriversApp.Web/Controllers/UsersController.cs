using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.ViewModels;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Controllers
{
    [Authorization(admin: true)]
    public class UsersController : Controller
    {
        private readonly Services.IUsersService usersService;

        public UsersController(Services.IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = usersService.GetUsers();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            UsersCreateVM VM = usersService.SetupCreateUserForm();
            return View(VM);
        }

        [HttpPost]
        public IActionResult Create(Model.Requests.UserInsertRequest request)
        {
            usersService.CreateUser(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            UsersUpdateVM VM = usersService.SetupUpdateUserForm(Id);
            if (VM == null)
                return NotFound();

            return View(VM);
        }

        [HttpPost]
        public IActionResult Edit(int Id, Model.Requests.UserUpdateRequest request)
        {
            var updatedUser = usersService.UpdateUser(Id, request);
            if (updatedUser == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
