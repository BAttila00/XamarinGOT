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
    public partial class CharacterDetails : ContentPage
    {
        private Character _character;

        public Character Character {
            get { return _character; }
            set { _character = value; }
        }

        private Character _father;

        public Character Father {
            get { return _father; }
            set { _father = value; }
        }

        private Character _mother;

        public Character Mother {
            get { return _mother; }
            set { _mother = value; }
        }

        private Character _spouse;

        public Character Spouse {
            get { return _spouse; }
            set { _spouse = value; }
        }


        public CharacterDetails()
        {
            InitializeComponent();
        }

        public CharacterDetails(string url) {
            InitializeComponent();
            FillViewFields(url);
        }

        private async void FillViewFields(string url) {
            var service = new GOTService();
            Character = await service.GetCharacterAsync(url);
            //igy is lehetne:
            //Character = await service.GetAsync<Character>(new Uri(url));

            HideEmptyFieldsOnView();

            NameLabel.Text = Character.name;
            GenderLabel.Text = Character.gender;
            CultureLabel.Text = Character.culture;
            BornLabel.Text = Character.born;
            DiedLabel.Text = Character.died;

            if (Character.father != "") {
                Father = await service.GetAsync<Character>(new Uri(Character.father));
                FatherBtn.Text = Father.name;
            }
            if (Character.mother != "") {
                Mother = await service.GetAsync<Character>(new Uri(Character.mother));
                MotherBtn.Text = Mother.name;
            }
            if (Character.spouse != "") {
                Spouse = await service.GetAsync<Character>(new Uri(Character.spouse));
                SpouseBtn.Text = Spouse.name;
            }
        }

        private void HideEmptyFieldsOnView() {
            if (Character.gender == "") {
                GenderLabel0.IsVisible = false;     
                GenderLabel.IsVisible = false;
            }
            if (Character.culture == "") {
                CultureLabel0.IsVisible = false;
                CultureLabel.IsVisible = false;
            }
            if (Character.born == "") {
                BornLabel0.IsVisible = false;
                BornLabel.IsVisible = false;
            }
            if (Character.died == "") {
                DiedLabel0.IsVisible = false;
                DiedLabel.IsVisible = false;
            }

            //-----------------------
            if (Character.father == "") {
                FatherBtn0.IsVisible = false;
                FatherBtn.IsVisible = false;
            }
            if (Character.mother == "") {
                MotherBtn0.IsVisible = false;
                MotherBtn.IsVisible = false;
            }
            if (Character.spouse == "") {
                SpouseBtn0.IsVisible = false;
                SpouseBtn.IsVisible = false;
            }

            //----------------------
            if (Character.titles.Length == 0) {
                TitlesBtn0.IsVisible = false;
                TitlesBtn.IsVisible = false;
            }
            if (Character.aliases.Length == 0) {
                AliasesBtn0.IsVisible = false;
                AliasesBtn.IsVisible = false;
            }
            if (Character.allegiances.Length == 0) {
                AllegiancesBtn0.IsVisible = false;
                AllegiancesBtn.IsVisible = false;
            }
            if (Character.books.Length == 0) {
                BooksBtn0.IsVisible = false;
                BooksBtn.IsVisible = false;
            }
            if (Character.povBooks.Length == 0) {
                BooksPovBtn0.IsVisible = false;
                BooksPovBtn.IsVisible = false;
            }
            if (Character.tvSeries.Length == 0) {
                TvSeriesBtn0.IsVisible = false;
                TvSeriesBtn.IsVisible = false;
            }
            if (Character.playedBy.Length == 0) {
                PlayedByBtn0.IsVisible = false;
                PlayedByBtn.IsVisible = false;
            }
        }

        private void FatherBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new CharacterDetails(Character.father));
        }

        private void MotherBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new CharacterDetails(Character.mother));
        }

        private void SpouseBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new CharacterDetails(Character.spouse));
        }

        private void TitlesBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.titles));
        }

        private void AliasesBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.aliases));
        }

        private void AllegiancesBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.allegiances, "Houses"));
        }

        private void BooksBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.books, "Books"));
        }

        private void BooksPovBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.povBooks, "Books"));
        }

        private void TvSeriesBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.tvSeries));
        }

        private void PlayedByBtnClicked(object sender, EventArgs e) {
            Navigation.PushAsync(new DisplayListItems(Character.playedBy));
        }
    }
}