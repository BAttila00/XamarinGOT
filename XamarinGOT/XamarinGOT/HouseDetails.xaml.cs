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

            HideEmptyFieldsOnView();

            NameLabel.Text = House.name;
            RegionLabel.Text = House.region;
            WordsLabel.Text = House.words;
            CoatLabel.Text = House.coatOfArms;
            FoundedLabel.Text = House.founded;
            DiedOutLabel.Text = House.diedOut;

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

            //foreach (var title in House.titles) {
            //    Titles.Add(title);
            //}
            //TitlesList.ItemsSource = Titles;

            //foreach (var seat in House.seats) {
            //    Seats.Add(seat);
            //}
            //SeatsList.ItemsSource = Seats;

            //foreach (var ancestralWeapon in House.ancestralWeapons) {
            //    AncestralWeapons.Add(ancestralWeapon);
            //}
            //AncestralWeaponsList.ItemsSource = AncestralWeapons;

            //var swornMembers = await service.GetItemsList<Character>(House.swornMembers);
            //foreach (var character in swornMembers) {
            //    SwornMembers.Add(character);
            //}
            //SwornMembersList.ItemsSource = SwornMembers;

            //var cadetBranches = await service.GetItemsList<House>(House.cadetBranches);
            //foreach (var house in cadetBranches) {
            //    CadetBranches.Add(house);
            //}
            //CadetBranchesList.ItemsSource = CadetBranches;
        }

        //TODO
        private void HideEmptyFieldsOnView() {
            if(House.region == "") {
                RegionLabel0.IsVisible = false;     //csak azért kell RegionLabel0-t elnevezni h el tudjam tuntetni
                RegionLabel.IsVisible = false;
            }
            if (House.words == "") {
                WordsLabel0.IsVisible = false;
                WordsLabel.IsVisible = false;
            }
            if (House.coatOfArms == "") {
                CoatLabel0.IsVisible = false;
                CoatLabel.IsVisible = false;
            }
            if (House.founded == "") {
                FoundedLabel0.IsVisible = false;
                FoundedLabel.IsVisible = false;
            }
            if (House.diedOut == "") {
                DiedOutLabel0.IsVisible = false;
                DiedOutLabel.IsVisible = false;
            }

            //------------
            if (House.currentLord == "") {
                CurrentLordBtn0.IsVisible = false;
                CurrentLordBtn.IsVisible = false;
            }
            if (House.heir == "") {
                HeirBtn0.IsVisible = false;
                HeirBtn.IsVisible = false;
            }
            if (House.overlord == "") {
                OverlordBtn0.IsVisible = false;
                OverlordBtn.IsVisible = false;
            }
            if (House.founder == "") {
                FounderBtn0.IsVisible = false;
                FounderBtn.IsVisible = false;
            }

            //-----------
            if(House.swornMembers.Length == 0) {
                SwornMembersBtn0.IsVisible = false;
                SwornMembersBtn.IsVisible = false;
            }
            if (House.cadetBranches.Length == 0) {
                CadetBranchesBtn0.IsVisible = false;
                CadetBranchesBtn.IsVisible = false;
            }
            if (House.ancestralWeapons.Length == 0) {
                AncestralWeaponsBtn0.IsVisible = false;
                AncestralWeaponsBtn.IsVisible = false;
            }
            if (House.seats.Length == 0) {
                SeatsBtn0.IsVisible = false;
                SeatsBtn.IsVisible = false;
            }
            if (House.titles.Length == 0) {
                TitlesBtn0.IsVisible = false;
                TitlesBtn.IsVisible = false;
            }
        }

        private void SwornMembersBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(House.swornMembers, "Characters"));
        }

        private void AncestralWeaponsBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(House.ancestralWeapons));
        }

        private void SeatsBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(House.seats));
        }
    }
}