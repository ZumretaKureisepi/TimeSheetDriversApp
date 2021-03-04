using AutoMapper;
using GemBox.Spreadsheet;
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

namespace TimeSheetDriversApp.Web.Services
{
    public class TimeSheetsService : ITimeSheetsService
    {
        private readonly WebAPI.Services.ITimeSheetsService timeSheetsAPIService;
        private readonly WebAPI.Services.IUsersService usersAPIService;
        private readonly IMapper mapper;

        public TimeSheetsService(WebAPI.Services.ITimeSheetsService timeSheetsAPIService, WebAPI.Services.IUsersService usersAPIService, IMapper mapper)
        {
            this.timeSheetsAPIService = timeSheetsAPIService;
            this.usersAPIService = usersAPIService;
            this.mapper = mapper;
        }

        public TimeSheetIndexVM DisplayTimeSheet(TimeSheetIndexVM request)
        {
            var VM = new TimeSheetIndexVM
            {
                SearchRequest = new Model.Requests.TimeSheetSearchRequest()
                {
                    Year = request?.SearchRequest?.Year ?? DateTime.Today.Year,
                    Month = request?.SearchRequest?.Month ?? DateTime.Today.Month
                }
            };

            if (request.SearchRequest?.UserId != null)
                VM.SearchRequest.UserId = request.SearchRequest.UserId;

            LoadSelectListData(VM);

            if (VM.SearchRequest.UserId == null && VM.UsersList.Count > 0)
                VM.SearchRequest.UserId = int.Parse(VM.UsersList[0].Value);

            VM.TimeSheet = timeSheetsAPIService.GetTimeSheets(VM.SearchRequest);

            return VM;
        }

        public byte[] ExportTimeSheet(TimeSheetIndexVM request, string SelectedFormat)
        {
            var VM = new TimeSheetIndexVM
            {
                SearchRequest = new Model.Requests.TimeSheetSearchRequest()
                {
                    Year = request?.SearchRequest?.Year ?? DateTime.Today.Year,
                    Months = request?.SearchRequest?.Months ?? new List<int> { DateTime.Today.Month }
                }
            };

            if (request.SearchRequest?.UserIDs != null && request.SearchRequest.UserIDs.Count > 0)
                VM.SearchRequest.UserIDs = request.SearchRequest.UserIDs;

            LoadSelectListData(VM);

            VM.TimeSheet = timeSheetsAPIService.GetTimeSheets(VM.SearchRequest);

            var book = new ExcelFile();
            var sheet = book.Worksheets.Add("Sheet1");

            CellStyle style = sheet.Rows[0].Style;
            style.Font.Weight = ExcelFont.BoldWeight;
            style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            sheet.Columns[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

            sheet.Columns[0].SetWidth(50, LengthUnit.Pixel);
            sheet.Columns[1].SetWidth(120, LengthUnit.Pixel);

            sheet.Cells["A1"].Value = "Day";
            sheet.Cells["B1"].Value = "Type";
            sheet.Cells["C1"].Value = "Start Time";
            sheet.Cells["D1"].Value = "End Time";
            sheet.Cells["E1"].Value = "Break";
            sheet.Cells["F1"].Value = "Work Time";
            sheet.Cells["G1"].Value = "M³";
            sheet.Cells["H1"].Value = "Km - stand";
            sheet.Cells["I1"].Value = "Privat";
            sheet.Cells["J1"].Value = "Fuel";
            sheet.Cells["K1"].Value = "AdBlue";
            sheet.Cells["L1"].Value = "Notes";

            for (int r = 1; r <= VM.TimeSheet.Count; r++)
            {
                var item = VM.TimeSheet[r - 1];

                sheet.Cells[r, 0].Value = item.DayNo;
                sheet.Cells[r, 1].Value = item.DayType.GetDescription();

                if (item.DayType == DayType.WorkDay || item.DayType == DayType.TimeConsumption)
                {
                    sheet.Cells[r, 2].Value = item.StartTimeStr;
                    sheet.Cells[r, 3].Value = item.EndTimeStr;
                    sheet.Cells[r, 4].Value = item.BreakTimeStr;
                }

                sheet.Cells[r, 5].Value = item.WorkTime;
                sheet.Cells[r, 6].Value = item.MetersSquared;
                sheet.Cells[r, 7].Value = item.KmStand;
                sheet.Cells[r, 8].Value = item.PrivatTanken;
                sheet.Cells[r, 9].Value = item.Fuel;
                sheet.Cells[r, 10].Value = item.AdBlue;
                sheet.Cells[r, 11].Value = item.AdBlue;
            }

            SaveOptions options = FileExportHelper.GetSaveOptions(SelectedFormat);

            using (var stream = new MemoryStream())
            {
                book.Save(stream, options);
                return stream.ToArray();
            }
        }

        public TimeSheetIndexVM SetupExportFormData(TimeSheetIndexVM VM)
        {
            if (VM == null)
                VM = new TimeSheetIndexVM();

            if (VM.SearchRequest == null)
                VM.SearchRequest = new TimeSheetSearchRequest
                {
                    Months = new List<int> { DateTime.Now.Month },
                    Year = DateTime.Now.Year
                };
            else
            {
                if (VM.SearchRequest.Month != null)
                    VM.SearchRequest.Months = new List<int> { VM.SearchRequest.Month.Value };
                if (VM.SearchRequest.UserId != null)
                    VM.SearchRequest.UserIDs = new List<int> { VM.SearchRequest.UserId.Value };
            }

            LoadSelectListData(VM);

            return VM;
        }

        public void UpdateTimeSheet(TimeSheetUpdateVM request)
        {
            if (request.TimeSheet != null)
            {
                foreach (var item in request.TimeSheet)
                {
                    var updatedEntry = timeSheetsAPIService.GetTimeSheet(item.TimeSheetId);
                    if (updatedEntry != null)
                    {
                        updatedEntry.BreakTime = item.BreakTime;
                        updatedEntry.StartTime = item.StartTime;
                        updatedEntry.EndTime = item.EndTime;
                        updatedEntry.DayType = item.DayType;

                        var mappedEntry = mapper.Map<TimeSheetInsertRequest>(updatedEntry);
                        timeSheetsAPIService.PutTimeSheet(item.TimeSheetId, mappedEntry);
                    }
                }
            }
        }

        private void LoadSelectListData(TimeSheetIndexVM VM)
        {
            VM.UsersList = usersAPIService.GetUsers().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.UserId.ToString(),
                Selected = (VM.SearchRequest.UserId != null && x.UserId == VM.SearchRequest.UserId)
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
                    Value = date.Month.ToString(),
                    Selected = month == VM.SearchRequest.Month
                });
            }

            VM.DayTypeList = Enum.GetValues(typeof(DayType)).Cast<DayType>().Select(x => new SelectListItem
            {
                Text = x.GetDescription(),
                Value = ((int)x).ToString()
            }).ToList();
        }
    }
}
