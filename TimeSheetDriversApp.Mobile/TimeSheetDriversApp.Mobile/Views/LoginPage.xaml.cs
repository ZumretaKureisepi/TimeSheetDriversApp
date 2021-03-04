using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetDriversApp.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeSheetDriversApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel VM;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = VM = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.Username = "";
            VM.Password = "";
        }
    }
}