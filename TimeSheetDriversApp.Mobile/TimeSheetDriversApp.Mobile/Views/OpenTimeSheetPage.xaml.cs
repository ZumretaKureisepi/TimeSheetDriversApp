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
    public partial class OpenTimeSheetPage : ContentPage
    {
        private readonly OpenTimeSheetVM VM;
        public OpenTimeSheetPage()
        {
            InitializeComponent();
            BindingContext = VM = new OpenTimeSheetVM();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            VM.ExecuteLoadPickersCommand();
            for (int i = 0; i < VM.MonthsList.Count; i++)
            {
                if (VM.MonthsList[i].Id == DateTime.Today.Month)
                    monthsPicker.SelectedIndex = i;
            }
            for (int i = 0; i < VM.YearsList.Count; i++)
            {
                if (VM.YearsList[i].Id == DateTime.Today.Year)
                    yearsPicker.SelectedIndex = i;
            }

            await VM.ExecuteLoadItemsCommand();

        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await VM.ExecuteLoadItemsCommand();
        }
    }
}