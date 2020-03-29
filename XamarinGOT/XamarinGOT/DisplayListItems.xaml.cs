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

        public DisplayListItems() {
            InitializeComponent();
        }
        public DisplayListItems(string[] listItems, string type) {
            InitializeComponent();
            FillViewFields(listItems, type);

        }

        private async void FillViewFields(string[] listItems, string type) {
            var service = new GOTService();
            if(type == "Characters") {
                var swornMembers = await service.GetItemsList<Character>(listItems);
                foreach (var character in swornMembers) {
                    Characters.Add(character);
                }
                DisplayList.ItemsSource = Characters;
            }
        }
    }
}
