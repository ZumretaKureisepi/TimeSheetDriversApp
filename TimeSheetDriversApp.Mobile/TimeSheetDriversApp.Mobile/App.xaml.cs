using System;
using TimeSheetDriversApp.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeSheetDriversApp.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
