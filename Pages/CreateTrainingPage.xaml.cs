using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elementalium.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTrainingPage : ContentPage
    {
        public CreateTrainingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var source= App.DatabaseWoElement
                .GetElementAsync().Result
                .Where(e => !e.Can)
                .OrderBy(e => e.Complexity);
            collectionView.ItemsSource = new ObservableCollection<WoElement>(source);
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            var training=new Training();
            training.StartWorkout(
                DateTime.Now, 
                collectionView
                    .SelectedItems
                    .Select(element => element as WoElement)
                    .ToList());
            await Navigation.PushAsync(new ViewTrainingPage
            {
                BindingContext = training
            });
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void OnTrainingListButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrainingListPage
            {
                BindingContext = new Training()
            });
        }
    }
}