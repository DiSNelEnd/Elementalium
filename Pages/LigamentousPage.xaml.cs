using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elementalium.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LigamentousPage : ContentPage
    {
        public LigamentousPage()
        {
            InitializeComponent();
        }

        protected async void OnBunchGenerateClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countEntry.Text) && !string.IsNullOrWhiteSpace(complexityEntry.Text))
            {
                var bunch = Ligamentous.GenerateBunch(int.Parse(countEntry.Text), int.Parse(complexityEntry.Text));
                if (bunch == null) await DisplayAlert("Сообщение",
                    "Неудалось сгенерировать связку с вашими параметрами!",
                    "Ок");
                else
                {
                    App.BunchRepository.Save(bunch);
                    bunchLabel.Text = bunch.ToString();
                }
            }

        }

        private async void OnBunchesListClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BunchesListPage());
        }
    }
}