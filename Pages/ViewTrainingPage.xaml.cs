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
    public partial class ViewTrainingPage : ContentPage
    {
        private Training Training=>BindingContext as Training;
        private HashSet<string> RepetitionsHash { get; set; }
        private HashSet<string> LearnedHash { get; set; }
        private HashSet<Bunch> BunchHash { get; set; }
        public ViewTrainingPage()
        {
            InitializeComponent();
            RepetitionsHash = new HashSet<string>();
            LearnedHash = new HashSet<string>();
            BunchHash = new HashSet<Bunch>();
            picker.ItemsSource = App
                .DatabaseWoElement
                .GetElementAsync()
                .Result
                .Where(e => e.Can)
                .ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnLearnElement(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            checkBox.IsEnabled = false;
            var element = checkBox.BindingContext as WoElement;
            LearnedHash.Add(element.Name);            
            element.Can = true;
            await App.DatabaseWoElement.SaveElementAsync(element);
        }

        private async void OnCancelTrainingButtonClicked(object sender, EventArgs e)
        {
            Training.FinishWorkout(RepetitionsHash, LearnedHash, BunchHash);
            await App.DatabaseTraining.SaveElementAsync(Training);
            await Navigation.PushAsync(new TrainingListPage
            {
                BindingContext = new Training()
            });
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void OnPerformBundleClicked(object sender, EventArgs e)
        {

            if (Athlete.Level > 5)
            {
                var result = await DisplayAlert("Подтверждение",
                    $"Создать связку?",
                    "Да", "Нет");
                if (result)
                {
                    var (item1, item2) = Athlete.GetCountAndComplexity();
                    var bunch = Ligamentous.GenerateBunch(item1, item2);
                    bunchLabel.Text = bunch.ToString();
                    BunchHash.Add(bunch);
                }
            }
            else 
            {
                await DisplayAlert("Сообщение",
                "Чтобы выполнять связки изучите больше элементов!",
                "Ок");
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var element = picker.SelectedItem as WoElement;
            if (element != null) 
            {
                RepetitionsHash.Add(element.Name);
                typeLabel.Text = element.RussianNameType;
                featureLabel.Text = element.Features != null ? featureLabel.Text = element.Features.ToString() : "";
            }
        }
    }
}