using GameofThrones.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGOT.DataBase;
using XamarinGOT.Models;

namespace XamarinGOT {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetails : ContentPage {
        private static GotDatabase _gotDatabase;
        private Book _book;
        public ObservableCollection<Character> CharactersInBook { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<CharactersInBook> CharactersInBookFromDB { get; set; } = new ObservableCollection<CharactersInBook>();
        public ObservableCollection<string> Authors { get; set; } = new ObservableCollection<string>();
        public Book Book {
            get { return _book; }
            set { _book = value; }
        }
        public BookDetails() {
            InitializeComponent();
            _gotDatabase = App.Database;
        }

        public BookDetails(string url) {
            InitializeComponent();
            FillViewFields(url);
            _gotDatabase = App.Database;
        }

        public async void FillViewFields(string url) {
            var service = new GOTService();
            Book = await service.GetBookAsync(url);
            //lehetne igy is:
            Book = await service.GetAsync<Book>(new Uri(url));

            BookTitle.Text = Book.name;
            BookIsbn.Text = Book.isbn;
            BookPublisher.Text = Book.publisher;
            BookCountry.Text = Book.country;
            BookReleased.Text = Book.released.ToString();

            foreach (var author in Book.authors) {
                Authors.Add(author);
            }
            BookAuthors.ItemsSource = Authors;

            //var charactersInBook = await service.GetCharactersInBook(Book.characters);
            //foreach (var character in charactersInBook) {
            //    CharactersInBook.Add(character);
            //}

            //BookCharacters.ItemsSource = CharactersInBook;

            var charactersInBook = _gotDatabase.GetCharactersInBookByBookUrl(Book.url);
            Debug.WriteLine("ennyi karaktert találtam a könyvhöz(" + Book.url + "): " + charactersInBook.Count);
            foreach (var character in charactersInBook) {
                //Debug.WriteLine(character.name);
                CharactersInBookFromDB.Add(character);
            }

            BookCharacters.ItemsSource = CharactersInBookFromDB;
        }

        private void BookCharactersItemTapped(object sender, ItemTappedEventArgs e) {
            //Navigation.PushAsync(new CharacterDetails(((Character)e.Item).url));
            Navigation.PushAsync(new CharacterDetails(((CharactersInBook)e.Item).CharacterUrl));
        }
    }
}