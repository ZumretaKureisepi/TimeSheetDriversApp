using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.Models;

namespace TimeSheetDriversApp.Web.Controllers
{
    [Authorization(superuser: true, admin: true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
