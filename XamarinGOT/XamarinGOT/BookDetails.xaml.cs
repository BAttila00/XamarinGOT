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
    public partial class BookDetails : ContentPage {
        private Book _book;
        public ObservableCollection<Character> CharactersInBook { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<string> Authors { get; set; } = new ObservableCollection<string>();
        public Book Book {
            get { return _book; }
            set { _book = value; }
        }
        public BookDetails() {
            InitializeComponent();
        }

        public BookDetails(string url) {
            InitializeComponent();
            FillViewParameters(url);
        }

        public async void FillViewParameters(string url) {
            var service = new GOTService();
            Book = await service.GetBookAsync(url);

            BookTitle.Text = Book.name;
            BookIsbn.Text = Book.isbn;
            BookPublisher.Text = Book.publisher;
            BookCountry.Text = Book.country;
            BookReleased.Text = Book.released.ToString();

            foreach (var author in Book.authors) {
                Authors.Add(author);
            }
            BookAuthors.ItemsSource = Authors;

            var charactersInBook = await service.GetCharactersInBook(Book.characters);
            foreach (var character in charactersInBook) {
                CharactersInBook.Add(character);
            }

            BookCharacters.ItemsSource = CharactersInBook;
        }
    }
}