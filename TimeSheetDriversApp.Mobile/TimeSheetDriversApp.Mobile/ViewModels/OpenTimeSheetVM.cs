using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.Mobile.Helper;
using TimeSheetDriversApp.Mobile.Models;
using TimeSheetDriversApp.Mobile.Views;
using TimeSheetDriversApp.Model.DTO;
using Xamarin.Forms;

namespace TimeSheetDriversApp.Mobile.ViewModels
{
    public class OpenTimeSheetVM : BaseViewModel
    {
        private readonly APIService _serviceTimeSheets = new APIService("TimeSheets");
        private readonly APIService _serviceRequests = new APIService("Requests");

        public ObservableCollection<Model.DTO.TimeSheetDTO> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command LoadPickersCommand { get; set; }
        public Command ApproveCloseCommand { get; set; }
        public Command<TimeSheetDTO> ItemTapped { get; }

        public ObservableCollection<PickerItem> YearsList { get; set; } = new ObservableCollection<PickerItem>();
        public ObservableCollection<PickerItem> MonthsList { get; set; } = new ObservableCollection<PickerItem>();


        public OpenTimeSheetVM()
        {
            Title = "Timesheet";
            Items = new ObservableCollection<Model.DTO.TimeSheetDTO>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ApproveCloseCommand = new Command(async () => await ExecuteApproveCloseCommand());

            ItemTapped = new Command<TimeSheetDTO>(OnItemSelected);
        }

        private async Task ExecuteApproveCloseCommand()
        {
            var today = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(SelectedYear.Id, SelectedMonth.Id);
            DateTime firstDayOfMonth = new DateTime(SelectedYear.Id, SelectedMonth.Id, 1);
            DateTime lastDayOfMonth = new DateTime(SelectedYear.Id, SelectedMonth.Id, daysInMonth);

            if (today.Date < lastDayOfMonth.Date)
            {
                await Shell.Current.DisplayAlert("Error", "Timesheet can not be approved prior to the last day of the selected month.", "OK");
                return;
            }

            var request = new Model.Requests.TimeSheetSearchRequest
            {
                Month = SelectedMonth.Id,
                Year = SelectedYear.Id
            };

            var timesheets = await _serviceTimeSheets.Get<List<Model.DTO.TimeSheetDTO>>(request);
            if (timesheets.Where(x => x.Status == Status.WorkInProgress).Count() == 0)
            {
                await Shell.Current.DisplayAlert("Error", "There are no timesheet entries to approve for the current month.", "OK");
                return;
            }

            List<DateTime> incompleteDates = new List<DateTime>();
            foreach (var item in timesheets)
            {
                if (item.DayType != DayType.Vacation && item.DayType != DayType.Sick)
                {
                    if (item.StartTime is null || item.EndTime is null || item.BreakTime is null || item.MetersSquared is null || item.KmStand is null)
                        incompleteDates.Add(item.EntryDate);
                }
            }

            if (incompleteDates.Count > 0)
            {
                string allDates = "";
                foreach (var date in incompleteDates)
                {
                    if (allDates != "")
                        allDates += ", ";

                    allDates += date.ToShortDateString();
                }
                await Shell.Current.DisplayAlert("Error", "Timesheet is incomplete for the following dates: " + allDates, "OK");
                return;
            }

            var asyncTaskList = new List<Task>();

            foreach (var item in timesheets)
            {
                item.Status = Status.ApprovedByEmployee;

                var asyncUpdateTask = _serviceTimeSheets.Update<Model.DTO.TimeSheetDTO>(item.TimeSheetId, item);

                asyncTaskList.Add(asyncUpdateTask);
            }

            await Task.WhenAll(asyncTaskList);

            var requestInsert = new Model.Requests.RequestInsertRequest
            {
                StartDateTime = firstDayOfMonth,
                EndDateTime = lastDayOfMonth,
                RequestType = RequestType.TimeSheet,
                Status = RequestStatus.Pending
            };
            var createdRequest = await _serviceRequests.Insert<Model.DTO.RequestDTO>(requestInsert);
            if (createdRequest != null)
                await Shell.Current.DisplayAlert("Success", "Timesheet for the past month has been approved.", "OK");
        }

        public void ExecuteLoadPickersCommand()
        {
            for (int year = 2020; year <= DateTime.Today.Year; year++)
            {
                var yearItem = new PickerItem
                {
                    Id = year,
                    Text = year.ToString(),
                };

                YearsList.Add(yearItem);
            }

            for (int month = 1; month <= 12; month++)
            {
                var date = new DateTime(DateTime.Today.Year, month, 1);
                var monthItem = new PickerItem
                {
                    Id = date.Month,
                    Text = date.ToString("MMMM"),
                };

                MonthsList.Add(monthItem);
            }

        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var request = new Model.Requests.TimeSheetSearchRequest
                {
                    Month = SelectedMonth?.Id ?? DateTime.Today.Month,
                    Year = SelectedYear?.Id ?? DateTime.Today.Year
                };
                var items = await _serviceTimeSheets.Get<List<Model.DTO.TimeSheetDTO>>(request);
                Items.Clear();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (FlurlHttpException ex)
            {
                var responseBody = await ex.GetResponseStringAsync();
                Debug.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnItemSelected(TimeSheetDTO item)
        {
            if (item == null)
                return;

            if (item.DayType == DayType.Vacation)
            {
                await Shell.Current.DisplayAlert("Error", "Vacation timesheet entries cannot be edited.", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(TimeSheetEditPage)}?{nameof(TimeSheetDTO.TimeSheetId)}={item.TimeSheetId}");
        }

        private PickerItem _selectedMonth;

        public PickerItem SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }


        private PickerItem _selectedYear;
        public PickerItem SelectedYear
        {
            get { return _selectedYear; }
            set { SetProperty(ref _selectedYear, value); }
        }


    }
}