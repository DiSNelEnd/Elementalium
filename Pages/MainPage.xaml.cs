using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Elementalium.Pages;
using Xamarin.Forms;

namespace Elementalium
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            levelLabel.Text = Athlete.Level.ToString();
        }

        private async void OnElementaliumButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ElementaliumPage
            {
                BindingContext = new WoElement()
            });

        }

        private async void OnLigamentousButtonClicked(object sender, EventArgs e)
        {
            if (Athlete.Level < 5) await DisplayAlert("Сообщение",
                "Чтобы зайти в связочную повысти уровень изучая новые элементы!",
                "Ок");
            else await Navigation.PushAsync(new LigamentousPage());
        }

        private void WriteNicknameCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            Athlete.Niсkname = entry.Text;
        }

        private async void OnTrainingButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateTrainingPage
            {
                BindingContext =new WoElement()
            });
        }
    }
}
