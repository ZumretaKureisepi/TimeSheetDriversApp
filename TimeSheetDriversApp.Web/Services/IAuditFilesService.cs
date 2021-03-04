using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
    public interface IAuditFilesService
    {
        void LoadSelectListData(AuditFileExportVM VM);
        byte[] ProcessExport(AuditFileExportVM request, string selectedFormat);
        List<AuditFileDTO> GetAuditFiles();
        AuditFileExportVM SetupExportAuditFilesForm(AuditFileExportVM vM);
    }
}
