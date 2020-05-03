using GameofThrones.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinGOT.DataBase;
using XamarinGOT.Models;


namespace XamarinGOT {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        private static GotDatabase _map;
        //public ObservableCollection<string> Characters { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Book> ThroneBooks { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> SearchResultThroneBooks { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> SearchResultCharacters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<House> SearchResultHouses { get; set; } = new ObservableCollection<House>();
        public MainPage() {
            InitializeComponent();
            _map = App.Database;
            Categories.Add("Books");
            Categories.Add("Characters");
            Categories.Add("Houses");

            //Characters.Add("Jon Snow");
            //Characters.Add("Jaime Lannister");

            CategoryPicker.ItemsSource = Categories;
            MainList.ItemsSource = Characters;
        }

        private void SearchTapped(object sender, EventArgs e) {
            string selectedCategory = CategoryPicker.SelectedItem.ToString();
            switch (selectedCategory) {
                case "Books":
                    SearchResultThroneBooks.Clear();
                    foreach (var item in ThroneBooks) {
                        if (item.name.ToLower().Contains(Search.Text))
                            SearchResultThroneBooks.Add(item);
                    }
                    MainList.ItemsSource = SearchResultThroneBooks;
                    break;
                case "Characters":
                    SearchResultCharacters.Clear();
                    foreach (var item in Characters) {
                        if (item.name.ToLower().Contains(Search.Text))
                            SearchResultCharacters.Add(item);
                    }
                    MainList.ItemsSource = SearchResultCharacters;
                    break;
                case "Houses":
                    SearchResultHouses.Clear();
                    foreach (var item in Houses) {
                        if (item.name.ToLower().Contains(Search.Text))
                            SearchResultHouses.Add(item);
                    }
                    MainList.ItemsSource = SearchResultHouses;
                    break;
                default:
                    break;
            }
        }

        private void MainListItemTapped(object sender, ItemTappedEventArgs e) {
            string selectedCategory = CategoryPicker.SelectedItem.ToString();
            object selectedItem = MainList.SelectedItem;
            switch (selectedCategory) {
                case "Books":
                    var selectedBook = (Book)selectedItem;
                    Navigation.PushAsync(new BookDetails(selectedBook.url));
                    break;
                case "Characters":
                    var selectedCharacter = (Character)selectedItem;
                    Navigation.PushAsync(new CharacterDetails(selectedCharacter.url));
                    break;
                case "Houses":
                    var selectedHouse = (House)selectedItem;
                    Navigation.PushAsync(new HouseDetails(selectedHouse.url));
                    break;
                default:
                    break;
            }
        }

        private async void CategoryChanged(object sender, EventArgs e) {
            string selectedCategory = CategoryPicker.Items[CategoryPicker.SelectedIndex];
            var service = new GOTService();

            switch (selectedCategory) {
                case "Books":
                    if (!ThroneBooks.Any()) {
                        var books = await service.GetSelectedList<Book>(selectedCategory.ToLower());
                        foreach (var item in books) {
                            ThroneBooks.Add(item);
                        }
                    }
                    MainList.ItemsSource = ThroneBooks;
                    break;
                case "Characters":
                    if (!Characters.Any()) {
                        var characters = await service.GetSelectedList<Character>(selectedCategory.ToLower());
                        foreach (var item in characters) {
                            Characters.Add(item);
                        }
                    }
                    MainList.ItemsSource = Characters;
                    break;
                case "Houses":
                    if (!Houses.Any()) {
                        var houses = await service.GetSelectedList<House>(selectedCategory.ToLower());
                        foreach (var item in houses) {
                            Houses.Add(item);
                        }
                    }
                    MainList.ItemsSource = Houses;
                    break;
                default:
                    break;
            }
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e) {
            string selectedCategory = CategoryPicker.Items[CategoryPicker.SelectedIndex];

            if (Search.Text == "") {
                switch (selectedCategory) {
                    case "Books":
                        MainList.ItemsSource = ThroneBooks;
                        break;
                    case "Characters":
                        MainList.ItemsSource = Characters;
                        break;
                    case "Houses":
                        MainList.ItemsSource = Houses;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
