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
    public partial class RequestsListPage : ContentPage
    {
        private readonly RequestsListVM VM;
        public RequestsListPage()
        {
            InitializeComponent();
            BindingContext = VM = new RequestsListVM();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
       

            await VM.ExecuteLoadItemsCommand();

        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await VM.ExecuteLoadItemsCommand();
        }
    }
}