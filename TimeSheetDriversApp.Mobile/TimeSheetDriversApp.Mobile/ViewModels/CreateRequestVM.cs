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
    public class CreateRequestVM : BaseViewModel
    {
        private readonly APIService _serviceRequests = new APIService("Requests");

        public Command LoadPickersCommand { get; set; }
        public Command SubmitRequestCommand { get; set; }

        public ObservableCollection<PickerItem> RequestTypeList { get; set; } = new ObservableCollection<PickerItem>();


        public CreateRequestVM()
        {
            Title = "Create Request";
            SubmitRequestCommand = new Command(async () => await ExecuteSubmitRequestCommand());
        }

        private async Task ExecuteSubmitRequestCommand()
        {
            if(SelectedRequestType == null)
            {
                await Shell.Current.DisplayAlert("Error", "Request type is a required field.", "OK");
                return;
            }

            var StartDateTime = StartDate.Date;
            var EndDateTime = EndDate.Date;
            if (TimeEditable)
            {
                StartDateTime = StartDateTime.Add(StartTime);
                EndDateTime = EndDateTime.Add(EndTime);

                if (StartTime > EndTime) {
                    await Shell.Current.DisplayAlert("Error", "Start time has to be before End time.", "OK");
                    return;
                }
            }

            if (StartDateTime > EndDateTime)
            {
                await Shell.Current.DisplayAlert("Error", "Start date has to be before End date.", "OK");
                return;
            }

            var request = new Model.Requests.RequestInsertRequest
            {
                StartDateTime = StartDateTime,
                EndDateTime = EndDateTime,
                RequestType = (RequestType)SelectedRequestType.Id,
                Status = RequestStatus.Pending,
            };

            var result = await _serviceRequests.Insert<Model.DTO.RequestDTO>(request);
            if (result != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        public void ExecuteLoadPickersCommand()
        {
            RequestTypeList.Clear();
            foreach (RequestType item in Enum.GetValues(typeof(RequestType)))
            {
                if (item == RequestType.TimeSheet)
                    continue;

                RequestTypeList.Add(new PickerItem
                {
                    Id = (int)item,
                    Text = item.GetDescription()
                });
            }

        }

        private PickerItem _selectedRequestType;

        public PickerItem SelectedRequestType
        {
            get { return _selectedRequestType; }
            set {
                SetProperty(ref _selectedRequestType, value);
                if (value.Id == (int)RequestType.Vacation)
                    TimeEditable = false;
                else
                    TimeEditable = true;
            }
        }


        private DateTime _startDate = DateTime.Today;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate = DateTime.Today;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
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
    
        private bool _timeEditable = false;

        public bool TimeEditable
        {
            get { return _timeEditable; }
            set { SetProperty(ref _timeEditable, value); }
        }

    }
}