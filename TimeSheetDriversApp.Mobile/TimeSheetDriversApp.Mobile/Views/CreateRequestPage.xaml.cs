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
    public partial class CreateRequestPage : ContentPage
    {
        private readonly CreateRequestVM VM;

        public CreateRequestPage()
        {
            InitializeComponent();
            BindingContext = VM = new CreateRequestVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ExecuteLoadPickersCommand();
        }
    }
}