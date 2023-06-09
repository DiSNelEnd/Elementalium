using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elementalium.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTrainingPage : ContentPage
    {
        public DetailTrainingPage()
        {
            InitializeComponent();
        }

        private async void OnDeleteTrainingButtonClicked(object sender, EventArgs e)
        {
            var training = (Training)BindingContext;
            await App.DatabaseTraining.DeleteNoteAsync(training);
            await Navigation.PopAsync();
        }
    }
}