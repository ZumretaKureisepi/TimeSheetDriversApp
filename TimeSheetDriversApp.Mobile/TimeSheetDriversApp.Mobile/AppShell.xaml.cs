using System;
using System.Collections.Generic;
using TimeSheetDriversApp.Mobile.ViewModels;
using TimeSheetDriversApp.Mobile.Views;
using Xamarin.Forms;

namespace TimeSheetDriversApp.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(OpenTimeSheetVM), typeof(OpenTimeSheetVM));
            Routing.RegisterRoute(nameof(TimeSheetEditPage), typeof(TimeSheetEditPage));
            Routing.RegisterRoute(nameof(RequestsListPage), typeof(RequestsListPage));
            Routing.RegisterRoute(nameof(CreateRequestPage), typeof(CreateRequestPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
            Shell.SetNavBarIsVisible(this, false);
        }
    }
}
