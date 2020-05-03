using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinGOT.DataBase;

namespace XamarinGOT
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Characters { get; set; } = new ObservableCollection<string>();
        private static GotDatabase _map;
        public MainPage()
        {
            InitializeComponent();
            _map = App.Database;
            Categories.Add("Books");
            Categories.Add("Characters");
            Categories.Add("Houses");

            Characters.Add("Jon Snow");
            Characters.Add("Jaime Lannister");

            CategoryPicker.ItemsSource = Categories;
            MainList.ItemsSource = Characters;
        }

        private void SearchTapped(object sender, EventArgs e)
        {
            switch ((string)CategoryPicker.SelectedItem)
            {
                case "Books":
                    Navigation.PushAsync(new BookDetails());
                    break;
                case "Characters":
                    Navigation.PushAsync(new CharacterDetails());
                    break;
                case "Houses":
                    Navigation.PushAsync(new HouseDetails());
                    break;
                default:
                    break;
            }
        }

        private void MainListItemTapped(object sender, ItemTappedEventArgs e)
        {
            switch ((string)CategoryPicker.SelectedItem)
            {
                case "Books":
                    Navigation.PushAsync(new BookDetails());
                    break;
                case "Characters":
                    Navigation.PushAsync(new CharacterDetails());
                    break;
                case "Houses":
                    Navigation.PushAsync(new HouseDetails());
                    break;
                default:
                    break;
            }
        }
    }
}
