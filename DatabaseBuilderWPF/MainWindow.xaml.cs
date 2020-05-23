using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XamarinGOT.DataBase;

namespace DatabaseBuilderWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        static GotDatabase gotDatabase;
        public MainWindow() {
            InitializeComponent();

        }

        private async void GetData() {
            //List<Character> characters = await GetSelectedList<Character>("Characters");
            //List<CharacterBase> charactersBase = new List<CharacterBase>();

            //saving characters
            //foreach (var item in characters) {
            //    charactersBase.Add(new CharacterBase(item.url, item.name));
            //}
            //Debug.WriteLine("Service Done");
            //foreach (var item in charactersBase) {
            //    gotDatabase.SaveCharacterBase(item);
            //}

            //saving characters by books
            List<Book> books = await GetSelectedList<Book>("books");
            List<CharactersInBook> charactersInBook = new List<CharactersInBook>();
            foreach (var item in books) {
                Debug.WriteLine("NEXT BOOK");
                var bookCharacters = await GetCharactersInBook(item.characters);
                foreach (var character in bookCharacters) {
                    charactersInBook.Add(new CharactersInBook(item.url, item.name, character.url, character.name));
                }
            }
            Debug.WriteLine("---------------------Getting Data: Done---------------------");
            foreach (var item in charactersInBook) {
                gotDatabase.SaveCharacterInBook(item);
            }
            Debug.WriteLine("---------------------Data saving: Done---------------------");
        }

        private static readonly Uri serverUrl = new Uri("https://www.anapioficeandfire.com/");
        public static async Task<List<T>> GetSelectedList<T>(string content) {
            List<T> list = new List<T>();
            List<T> listToReturn = new List<T>();
            int i = 1;
            do {
                list = await GetAsync<List<T>>(new Uri(serverUrl, $"api/{content}?page={i++}&pageSize=50"));
                listToReturn.AddRange(list);
                Debug.WriteLine(i + "  " + list.Count);

            } while (list.Any());

            return listToReturn;
        }

        public async Task<List<Character>> GetCharactersInBook(string[] characterUris) {
            List<Character> charactersList = new List<Character>();
            foreach (var characterUri in characterUris) {
                charactersList.Add(await GetAsync<Character>(new Uri(characterUri)));       //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat.
            }
            return charactersList;
        }

        public static async Task<T> GetAsync<T>(Uri uri) {
            using (var client = new HttpClient()) {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                string fileName = openFileDialog.FileName;
                Debug.WriteLine(fileName);
                gotDatabase = new GotDatabase(fileName);
            }

            GetData();
        }
    }
}
