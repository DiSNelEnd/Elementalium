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
    public partial class TrainingListPage : ContentPage
    {
        public TrainingListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = App.DatabaseTraining.GetElementAsync().Result.OrderByDescending(t=>t.TrainingDateTime);
        }

        private async void OnTrainingItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new DetailTrainingPage
                {
                    BindingContext = e.Item as Training
                });
            }
        }
    }
}