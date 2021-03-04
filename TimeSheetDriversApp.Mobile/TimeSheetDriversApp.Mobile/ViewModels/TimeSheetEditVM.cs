using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeSheetDriversApp.Mobile.Helper;
using TimeSheetDriversApp.Mobile.Models;
using TimeSheetDriversApp.Mobile.Views;
using TimeSheetDriversApp.Model.DTO;
using TimeSheetDriversApp.Model.Helper;
using Xamarin.Forms;

namespace TimeSheetDriversApp.Mobile.ViewModels
{
    [QueryProperty(nameof(TimeSheetId), nameof(TimeSheetId))]
    public class TimeSheetEditVM : BaseViewModel
    {
        private readonly APIService _serviceTimeSheets = new APIService("TimeSheets");

        public ICommand LoadItemCommand;


        string timeSheetId = "";
        public string TimeSheetId
        {
            get => timeSheetId;
            set
            {
                timeSheetId = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                LoadItemCommand.Execute(null);
            }
        }


        public Command LoadPickersCommand { get; set; }
        public Command SaveCommand { get; set; }

        public ObservableCollection<PickerItem> DayTypeList { get; set; } = new ObservableCollection<PickerItem>();


        public TimeSheetEditVM()
        {
            Title = "Edit Timesheet";
            LoadItemCommand = new Command(async () => await ExecuteLoadItemCommand());
            SaveCommand = new Command(async () => await ExecuteSaveItemCommand());
        }

        private async Task ExecuteSaveItemCommand()
        {
            if (StartTime > EndTime)
            {
                await Shell.Current.DisplayAlert("Error", "Start time has to be before End time.", "OK");
                return;
            }

            var TSBreakTime = TimeSpan.MinValue;
            if (TimeSpan.TryParse(BreakTime, out TimeSpan BreakTimeInput))
                TSBreakTime = BreakTimeInput;
            else
            {
                await Shell.Current.DisplayAlert("Error", "Break time is not entered correctly.", "OK");
                return;
            }

            var DayType = TimeSheet.DayType;
            if (SelectedDayType != null)
                DayType = (DayType)SelectedDayType.Id;

            var request = new Model.Requests.TimeSheetInsertRequest
            {
                TimeSheetId = TimeSheet.TimeSheetId,
                StartTime = DateTime.Today.Add(StartTime),
                EndTime = DateTime.Today.Add(EndTime),
                BreakTime = DateTime.Today.Add(TSBreakTime),
                EntryDate = TimeSheet.EntryDate,
                Status = TimeSheet.Status,
                DayType = DayType,
                AdBlue = TimeSheet.AdBlue,
                Fuel = TimeSheet.Fuel,
                PrivatTanken = TimeSheet.PrivatTanken,
                KmStand = TimeSheet.KmStand,
                MetersSquared = TimeSheet.MetersSquared,
                Notes = TimeSheet.Notes
            };
            var result = await _serviceTimeSheets.Update<Model.DTO.TimeSheetDTO>(TimeSheet.TimeSheetId, request);
            if (result != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        public void ExecuteLoadPickersCommand()
        {
            DayTypeList.Clear();
            foreach (DayType item in Enum.GetValues(typeof(DayType)))
            {
                if (item == DayType.TimeConsumption || item == DayType.Vacation)
                    continue;

                DayTypeList.Add(new PickerItem
                {
                    Id = (int)item,
                    Text = item.GetDescription()
                });
            }

        }

        public async Task ExecuteLoadItemCommand()
        {
            try
            {
                TimeSheet = await _serviceTimeSheets.GetById<Model.DTO.TimeSheetDTO>(timeSheetId);

                for (int i = 0; i < DayTypeList.Count; i++)
                {
                    if (DayTypeList[i].Id == (int)TimeSheet.DayType)
                    {
                        SelectedDayType = DayTypeList[i];
                        break;
                    }
                }

                if (TimeSheet.StartTime != null)
                    StartTime = TimeSheet.StartTime.Value.TimeOfDay;

                if (TimeSheet.EndTime != null)
                    EndTime = TimeSheet.EndTime.Value.TimeOfDay;

                BreakTime = TimeSheet.BreakTimeStr;

                if (TimeSheet.DayType == DayType.TimeConsumption || TimeSheet.DayType == DayType.Vacation)
                    DayTypeLocked = true;
                else
                    DayTypeEditable = true;

                if (TimeSheet.Status == Status.Closed || TimeSheet.Status == Status.ApprovedByEmployee)
                {
                    DayTypeEnabled = false;
                    TimeEntryLocked = true;
                }
            }
            catch (FlurlHttpException ex)
            {
                var responseBody = ex.GetResponseStringAsync().GetAwaiter().GetResult();
                Debug.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private PickerItem _selectedDayType;

        public PickerItem SelectedDayType
        {
            get { return _selectedDayType; }
            set { SetProperty(ref _selectedDayType, value); }
        }


        private TimeSheetDTO _timeSheet;

        public TimeSheetDTO TimeSheet
        {
            get { return _timeSheet; }
            set { SetProperty(ref _timeSheet, value); }
        }

        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }
        private TimeSpan _endTime;

        public TimeSpan EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }
        private string _breakTime;

        public string BreakTime
        {
            get { return _breakTime; }
            set { SetProperty(ref _breakTime, value); }
        }

        private bool _dayTypeEditable = false;

        public bool DayTypeEditable
        {
            get { return _dayTypeEditable; }
            set { SetProperty(ref _dayTypeEditable, value); }
        }


        private bool _dayTypeLocked = false;

        public bool DayTypeLocked
        {
            get { return _dayTypeLocked; }
            set { SetProperty(ref _dayTypeLocked, value); }
        }

        private bool _dayTypeEnabled = true;

        public bool DayTypeEnabled
        {
            get { return _dayTypeEnabled; }
            set { SetProperty(ref _dayTypeEnabled, value); }
        }


        private bool _timeEntryLocked = false;

        public bool TimeEntryLocked
        {
            get { return _timeEntryLocked; }
            set { SetProperty(ref _timeEntryLocked, value); }
        }



    }
}