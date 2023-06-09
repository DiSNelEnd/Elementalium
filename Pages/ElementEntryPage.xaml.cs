using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElementEntryPage : ContentPage
    {
        private List<Entry> Entries=> new List<Entry> 
        {
            complexityEntry,
            typeEntry,
            startPositionEntry,
            endPositionEntry,
            featuresEntry
        };

        public ElementEntryPage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (AllCorrectEntries())
            {
                var element = (WoElement)BindingContext;
                await App.DatabaseWoElement.SaveElementAsync(element);
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Сообщение",
                "Чтобы сохранить изменения исправьте неправельно заполненные поля!",
                "Ок");
        }

        private bool AllCorrectEntries()
        {
            return Entries.All(entry => entry.BackgroundColor != Color.Red);
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var element = (WoElement)BindingContext;
            var result= await DisplayAlert("Подтверждение",
                $"Вы уверены что хотите удалить элемент {element.Name}?",
                "Да","Нет");
            if (result)
            {
                await App.DatabaseWoElement.DeleteNoteAsync(element);
                await Navigation.PopAsync();
            }
        }

        private async void CompletedPosition(Entry entry)
        {
            if (entry.Text!=null && !Regex.IsMatch(entry.Text, Position.GetRegexFormat()))
            {
                await DisplayAlert("Сообщение",
                    "Позиция должна быть цифрами (номер позиции:номер хвата,номер хвата...).Либо номер позиции если она без хвата!",
                    "Ок");
                entry.BackgroundColor = Color.Red;
            }
            else entry.BackgroundColor = default;
        }

        private async void OnEntryComplexityCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text != null && !Regex.IsMatch(entry.Text, @"[1-9]$"))
            {
                await DisplayAlert("Сообщение", "Сложность должна быть цифрой от 1 до 9!", "Ок");
                entry.BackgroundColor=Color.Red;
            }
            else entry.BackgroundColor = default;
        }

        private async void OnEntryTypeCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text != null && !Regex.IsMatch(entry.Text, GetFamiliesRegexFormat()))
            {
                await DisplayAlert("Сообщение", "Тип должен быть цифрой от 0 до 7", "Ок");
                entry.BackgroundColor = Color.Red;
            }
            else
            {
                entry.BackgroundColor = default;
                var element = BindingContext as WoElement;
                element.Type = (Families)int.Parse(entry.Text);
            }

        }

        private void OnStartPositionCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            CompletedPosition(entry);
        }

        private void OnEndPositionCompleted(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            CompletedPosition(entry);
        }

        private async void OnFeaturesEntry(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text != null && entry.Text!="" && !Regex.IsMatch(entry.Text, Feature.GetRegexFormat()))
            {
                await DisplayAlert("Сообщение", "Особености должны быть цифрами от 0 до 6 через (,), если они есть", "Ок");
                entry.BackgroundColor = Color.Red;
            }
            else entry.BackgroundColor = default;
        }
    }
}