using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
    public interface ITimeSheetsService
    {
        TimeSheetIndexVM DisplayTimeSheet(TimeSheetIndexVM request);
        void UpdateTimeSheet(TimeSheetUpdateVM request);
        TimeSheetIndexVM SetupExportFormData(TimeSheetIndexVM VM);
        byte[] ExportTimeSheet(TimeSheetIndexVM request, string SselectedFormat);
    }
}
