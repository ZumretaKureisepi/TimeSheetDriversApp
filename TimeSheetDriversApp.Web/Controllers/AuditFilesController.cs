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
    public class AuditFilesController : Controller
    {
        private readonly Services.IAuditFilesService auditFilesService;

        public AuditFilesController(Services.IAuditFilesService auditFilesService)
        {
            this.auditFilesService = auditFilesService;
        }

        public IActionResult Index()
        {
            var list = auditFilesService.GetAuditFiles();

            return View(list);
        }

        [HttpGet]
        public IActionResult Export(AuditFileExportVM VM)
        {
            VM = auditFilesService.SetupExportAuditFilesForm(VM);
            
            return View(VM);
        }

        [HttpPost]
        public IActionResult Export(AuditFileExportVM request, string SelectedFormat)
        {
            SaveOptions options = FileExportHelper.GetSaveOptions(SelectedFormat);

            byte[] content = auditFilesService.ProcessExport(request, SelectedFormat);

            return File(content, options.ContentType, "Export-AuditFiles." + SelectedFormat.ToLower());
        }


    }
}
