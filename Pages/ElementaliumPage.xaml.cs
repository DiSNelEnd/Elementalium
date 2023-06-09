using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elementalium.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElementaliumPage : ContentPage
    {
        public ElementaliumPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.DatabaseWoElement.GetElementAsync();
        }

        async void OnElementAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ElementEntryPage
            {
                BindingContext = new WoElement()
            });
        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new ElementEntryPage()
                {
                    BindingContext = e.Item as WoElement
                });
            }
        }

        async void OnSortListClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Сортировать?", "Отмена", null, "По сложности", "По типу", "По владению");
            if(action== "По сложности") listView.ItemsSource = App.DatabaseWoElement.GetElementAsync().Result.OrderBy(x=>x.Complexity);
            if (action == "По типу") listView.ItemsSource = App.DatabaseWoElement.GetElementAsync().Result.OrderBy(x => x.Type);
            if (action == "По владению") listView.ItemsSource = App.DatabaseWoElement.GetElementAsync().Result.OrderByDescending(x => x.Can);
        }
    }
}