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
    
    public partial class TimeSheetEditPage : ContentPage
    {
        private readonly TimeSheetEditVM VM;

        public TimeSheetEditPage()
        {
            InitializeComponent();

            BindingContext = VM = new TimeSheetEditVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ExecuteLoadPickersCommand();
        }
    }
}