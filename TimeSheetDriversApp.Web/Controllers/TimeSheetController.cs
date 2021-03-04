using AutoMapper;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Helper;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.ViewModels;
using TimeSheetDriversApp.WebAPI.Services;

namespace TimeSheetDriversApp.Web.Controllers
{
    [Authorization(superuser: true, admin: true)]
    public class TimeSheetController : Controller
    {
        private readonly Services.ITimeSheetsService timeSheetsService;

        public TimeSheetController(Services.ITimeSheetsService timeSheetsService)
        {
            this.timeSheetsService = timeSheetsService;
        }

        [HttpGet]
        public IActionResult Index(TimeSheetIndexVM request)
        {
            TimeSheetIndexVM VM = timeSheetsService.DisplayTimeSheet(request);
            return View(VM);
        }

        [HttpPost]
        public IActionResult Update(TimeSheetUpdateVM request)
        {
            timeSheetsService.UpdateTimeSheet(request);

            return Redirect("/TimeSheet?" +
                    "SearchRequest.UserId=" + request.SearchRequest.UserId.Value + "&" +
                    "SearchRequest.Month=" + request.SearchRequest.Month.Value + "&" +
                    "SearchRequest.Year=" + request.SearchRequest.Year.Value);
        }
        [HttpGet]
        public IActionResult Export(TimeSheetIndexVM VM)
        {
            VM = timeSheetsService.SetupExportFormData(VM);

            return View(VM);
        }

        [HttpPost]
        public IActionResult Export(TimeSheetIndexVM request, string SelectedFormat)
        {
            SaveOptions options = FileExportHelper.GetSaveOptions(SelectedFormat);
            byte[] content = timeSheetsService.ExportTimeSheet(request, SelectedFormat);

            return File(content, options.ContentType, "Export-TimeSheet." + SelectedFormat.ToLower());
        }

    }
}
