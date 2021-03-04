using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeSheetDriversApp.Mobile.Helper;
using TimeSheetDriversApp.Mobile.Views;
using Xamarin.Forms;

namespace TimeSheetDriversApp.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService _serviceUsers = new APIService("Users");
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            Title = "Login";

        }

        private async void OnLoginClicked()
        {
            APIService.Username = Username;
            APIService.Password = Password;
            try
            {
                APIService.CurrentUser = await _serviceUsers.Get<Model.DTO.UserDTO>(null, "MyProfile");
                if (APIService.CurrentUser.Role.RoleName == "User")
                    await Shell.Current.GoToAsync($"//{nameof(OpenTimeSheetPage)}");
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Access to mobile app is allowed only to end-users", "OK");
            }
            catch (Exception) { }
        }

        private string _email = string.Empty;
        public string Username
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand LoginCommand { get; set; }
    }

}
