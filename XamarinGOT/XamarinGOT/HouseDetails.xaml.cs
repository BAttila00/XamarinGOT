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

namespace XamarinGOT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseDetails : ContentPage
    {

        private House _house;
        public ObservableCollection<string> Titles { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Seats { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> AncestralWeapons { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<House> CadetBranches { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Character> SwornMembers { get; set; } = new ObservableCollection<Character>();

        public House House {
            get { return _house; }
            set { _house = value; }
        }

        private Character _currentLord;
        public Character CurrentLord {
            get { return _currentLord; }
            set { _currentLord = value; }
        }

        private Character _heir;
        public Character Heir {
            get { return _heir; }
            set { _heir = value; }
        }

        private House _overlord;
        public House Overlord {
            get { return _overlord; }
            set { _overlord = value; }
        }

        private Character _founder;
        public Character Founder {
            get { return _founder; }
            set { _founder = value; }
        }

        public HouseDetails()
        {
            InitializeComponent();
        }

        public HouseDetails(string url) {
            InitializeComponent();
            FillViewFields(url);
        }

        private async void FillViewFields(string url) {
            var service = new GOTService();
            House = await service.GetHouseAsync(url);

            NameLabel.Text = House.name;
            RegionLabel.Text = House.region;
            WordsLabel.Text = House.words;
            CoatLabel.Text = House.coatOfArms;
            FoundedLabel.Text = House.founded.ToString();
            DiedOutLabel.Text = House.diedOut.ToString();

            if (House.currentLord != "") {
                CurrentLord = await service.GetAsync<Character>(new Uri(House.currentLord));
                CurrentLordBtn.Text = CurrentLord.name;
            }
            if (House.heir != "") {
                Heir = await service.GetAsync<Character>(new Uri(House.heir));
                HeirBtn.Text = Heir.name;
            }
            if (House.overlord != "") {
                Overlord = await service.GetAsync<House>(new Uri(House.overlord));
                OverlordBtn.Text = Overlord.name;
            }
            if (House.founder != "") {
                Founder = await service.GetAsync<Character>(new Uri(House.founder));
                FounderBtn.Text = Founder.name;
            }

            foreach (var title in House.titles) {
                Titles.Add(title);
            }
            TitlesList.ItemsSource = Titles;

            foreach (var seat in House.seats) {
                Seats.Add(seat);
            }
            SeatsList.ItemsSource = Seats;

            foreach (var ancestralWeapon in House.ancestralWeapons) {
                AncestralWeapons.Add(ancestralWeapon);
            }
            AncestralWeaponsList.ItemsSource = AncestralWeapons;

            var swornMembers = await service.GetItemsList<Character>(House.swornMembers);
            foreach (var character in swornMembers) {
                SwornMembers.Add(character);
            }
            SwornMembersList.ItemsSource = SwornMembers;

            var cadetBranches = await service.GetItemsList<House>(House.cadetBranches);
            foreach (var house in cadetBranches) {
                CadetBranches.Add(house);
            }
            CadetBranchesList.ItemsSource = CadetBranches;
        }
    }
}