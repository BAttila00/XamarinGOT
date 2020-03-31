using GameofThrones.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGOT.Models;

namespace XamarinGOT {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayListItems : ContentPage {

        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<string> OtherItems { get; set; } = new ObservableCollection<string>();

        string displayedItemTypes = "";

        public DisplayListItems() {
            InitializeComponent();
        }
        public DisplayListItems(string[] listItems, string type) {
            InitializeComponent();
            displayedItemTypes = type;
            FillViewFields(listItems);
        }

        public DisplayListItems(string[] listItems) {
            InitializeComponent();
            FillViewFields(listItems);
        }

        private async void FillViewFields(string[] listItems) {
            var service = new GOTService();
            if(displayedItemTypes == "Characters") {
                var swornMembers = await service.GetItemsList<Character>(listItems);
                foreach (var character in swornMembers) {
                    Characters.Add(character);
                }
                DisplayList.ItemsSource = Characters;
                MainStackLayout.Children.Remove(ActInd);
            }
            else if (displayedItemTypes == "Houses") {
                var houses = await service.GetItemsList<House>(listItems);
                foreach (var house in houses) {
                    Houses.Add(house);
                }
                DisplayList.ItemsSource = Houses;
                MainStackLayout.Children.Remove(ActInd);
            }
            else if (displayedItemTypes == "Books") {
                var books = await service.GetItemsList<Book>(listItems);
                foreach (var book in books) {
                    Books.Add(book);
                }
                DisplayList.ItemsSource = Books;
                MainStackLayout.Children.Remove(ActInd);
            } else {
                foreach (var item in listItems) {
                    OtherItems.Add(item);
                }
                DisplayList.ItemTemplate = null;
                DisplayList.ItemsSource = OtherItems;
                //DisplayList2.ItemsSource = OtherItems;
                //MainStackLayout.Children.Remove(DisplayList);
                MainStackLayout.Children.Remove(ActInd);
            }
        }

        private void DisplayListTapped(object sender, ItemTappedEventArgs e) {
            if (displayedItemTypes == "Characters") {
                string url = ((Character)e.Item).url;
                Navigation.PushAsync(new CharacterDetails(url));
            }
            if (displayedItemTypes == "Houses") {
                string url = ((Character)e.Item).url;
                Navigation.PushAsync(new HouseDetails(url));
            }
            if (displayedItemTypes == "Books") {
                string url = ((Book)e.Item).url;
                Navigation.PushAsync(new BookDetails(url));
            }
        }
    }
}
