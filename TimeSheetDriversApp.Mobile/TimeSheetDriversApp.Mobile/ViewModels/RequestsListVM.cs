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
    public class RequestsListVM : BaseViewModel
    {
        private readonly APIService _serviceRequests = new APIService("Requests");

        public ObservableCollection<Model.DTO.RequestDTO> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command GoToCreateRequestCommand { get; }

        public RequestsListVM()
        {
            Title = "Requests";
            Items = new ObservableCollection<Model.DTO.RequestDTO>();
            
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            GoToCreateRequestCommand = new Command(async () => await ExecuteGoToCreateRequestCommand());
        }

        private async Task ExecuteGoToCreateRequestCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateRequestPage)}");
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await _serviceRequests.Get<List<Model.DTO.RequestDTO>>(null);
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

    }
}