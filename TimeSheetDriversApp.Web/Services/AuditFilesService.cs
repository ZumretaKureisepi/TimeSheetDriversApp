using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Model.Requests;
using TimeSheetDriversApp.Web.Helpers;
using TimeSheetDriversApp.Web.ViewModels;

namespace TimeSheetDriversApp.Web.Services
{
    public class AuditFilesService : IAuditFilesService
    {
        private readonly WebAPI.Services.IAuditFilesService auditFilesAPIService;
        private readonly WebAPI.Services.IUsersService usersAPIService;

        public AuditFilesService(WebAPI.Services.IAuditFilesService auditFilesAPIService, WebAPI.Services.IUsersService usersAPIService)
        {
            this.auditFilesAPIService = auditFilesAPIService;
            this.usersAPIService = usersAPIService;
        }

        public List<Model.DTO.AuditFileDTO> GetAuditFiles()
        {
            return auditFilesAPIService.GetAuditFiles(null);
        }

        public void LoadSelectListData(AuditFileExportVM VM)
        {
            VM.UsersList = usersAPIService.GetUsers().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.UserId.ToString(),
            }).ToList();

            VM.YearsList = new List<SelectListItem>();
            for (int year = 2020; year <= DateTime.Today.Year; year++)
            {
                VM.YearsList.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString(),
                    Selected = year == VM.SearchRequest.Year
                });
            }

            VM.MonthsList = new List<SelectListItem>();
            for (int month = 1; month <= 12; month++)
            {
                var date = new DateTime(DateTime.Today.Year, month, 1);

                VM.MonthsList.Add(new SelectListItem
                {
                    Text = date.ToString("MMMM"),
                    Value = date.Month.ToString()
                });
            }
        }

        public byte[] ProcessExport(AuditFileExportVM request, string SelectedFormat)
        {
            var VM = new AuditFileExportVM
            {
                SearchRequest = new Model.Requests.AuditFileSearchRequest()
                {
                    Year = request?.SearchRequest?.Year ?? DateTime.Today.Year,
                    Months = request?.SearchRequest?.Months ?? new List<int> { DateTime.Today.Month }
                }
            };

            if (request.SearchRequest?.UserIDs != null && request.SearchRequest.UserIDs.Count > 0)
                VM.SearchRequest.UserIDs = request.SearchRequest.UserIDs;

            LoadSelectListData(VM);

            VM.AuditFiles = auditFilesAPIService.GetAuditFiles(VM.SearchRequest);

            var book = new ExcelFile();
            var sheet = book.Worksheets.Add("Sheet1");

            CellStyle style = sheet.Rows[0].Style;
            style.Font.Weight = ExcelFont.BoldWeight;
            style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Columns[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

            sheet.Columns[0].SetWidth(120, LengthUnit.Pixel);
            sheet.Columns[2].SetWidth(80, LengthUnit.Pixel);
            sheet.Columns[3].SetWidth(70, LengthUnit.Pixel);
            sheet.Columns[4].SetWidth(70, LengthUnit.Pixel);
            sheet.Columns[5].SetWidth(150, LengthUnit.Pixel);
            sheet.Columns[6].SetWidth(110, LengthUnit.Pixel);

            sheet.Cells["A1"].Value = "TimeSheet KEY";
            sheet.Cells["B1"].Value = "DayNo";
            sheet.Cells["C1"].Value = "Field";
            sheet.Cells["D1"].Value = "Old Value";
            sheet.Cells["E1"].Value = "New Value";
            sheet.Cells["F1"].Value = "Date&Time of Change";
            sheet.Cells["G1"].Value = "Changed by User";

            for (int r = 1; r <= VM.AuditFiles.Count; r++)
            {
                var item = VM.AuditFiles[r - 1];

                sheet.Cells[r, 0].Value = item.TimeSheet.TimeSheetKey;
                sheet.Cells[r, 1].Value = item.TimeSheet.DayNo;

                sheet.Cells[r, 2].Value = item.FieldStr;
                sheet.Cells[r, 3].Value = item.OldValueStr;
                sheet.Cells[r, 4].Value = item.NewValueStr;
                sheet.Cells[r, 5].Value = item.DateChangedStr;
                sheet.Cells[r, 6].Value = item.ChangedByUser.Username;
            }

            SaveOptions options = FileExportHelper.GetSaveOptions(SelectedFormat);

            using (var stream = new MemoryStream())
            {
                book.Save(stream, options);
                return stream.ToArray();
            }
        }

        public AuditFileExportVM SetupExportAuditFilesForm(AuditFileExportVM VM)
        {
            if (VM == null)
            {
                VM = new AuditFileExportVM();
            }
            if (VM.SearchRequest == null)
                VM.SearchRequest = new AuditFileSearchRequest
                {
                    Months = new List<int> { DateTime.Now.Month },
                    Year = DateTime.Now.Year
                };

            LoadSelectListData(VM);

            return VM;
        }
    }
}
