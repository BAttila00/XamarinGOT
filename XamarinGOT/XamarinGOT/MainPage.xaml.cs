using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGOT
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Characters { get; set; } = new ObservableCollection<string>();
        public MainPage()
        {
            InitializeComponent();
            Categories.Add("Books");
            Categories.Add("Characters");
            Categories.Add("Houses");

            Characters.Add("Jon Snow");
            Characters.Add("Jaime Lannister");

            CategoryPicker.ItemsSource = Categories;
            ResultList.ItemsSource = Characters;
        }

        private void SearchTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HouseDetails());
        }
    }
}
