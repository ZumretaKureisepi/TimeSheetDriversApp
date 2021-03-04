using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.ViewModels;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Controllers
{
    [Authorization(superuser: true, admin: true)]
    public class RequestsController : Controller
    {
        private readonly Services.IRequestsService requestsService;

        public RequestsController(Services.IRequestsService requestsService)
        {
            this.requestsService = requestsService;
        }

        public IActionResult Index()
        {
            return View(new RequestsUpdateVM
            {
                requests = requestsService.GetRequests()
            });
        }

        [HttpPost]
        public IActionResult Update(RequestsUpdateVM VM)
        {
            if (VM.selectedRequests == null || VM.selectedRequests.Count == 0)
                return RedirectToAction(nameof(Index));

            requestsService.ProcessUpdate(VM);

            return RedirectToAction(nameof(Index));
        }
    }
}
