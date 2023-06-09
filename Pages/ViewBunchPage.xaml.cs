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
    public partial class ViewBunchPage : ContentPage
    {
        public ViewBunchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var bunch = BindingContext as Bunch;
            bunchLabel.Text = bunch.ToString();
        }

        async void OnDeleteBunchClicked(object sender, EventArgs e)
        {
            var bunch = BindingContext as Bunch;
            App.BunchRepository.Delete(bunch); 
            await Navigation.PopAsync();
        }
    }
}