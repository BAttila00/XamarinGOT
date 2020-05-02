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
using XamarinGOT.Models;

namespace DatabaseBuilderWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        static GotDatabase gotDatabase;
        public MainWindow() {
            InitializeComponent();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                string fileName = openFileDialog.FileName;
                Debug.WriteLine(fileName);
                gotDatabase = new GotDatabase(fileName);
            }

            GetData();
        }

        private async void GetData() {
            List<Character> characters = await GetSelectedList<Character>("Characters");
            List<CharacterBase> charactersBase = new List<CharacterBase>();
            foreach (var item in characters) {
                charactersBase.Add(new CharacterBase(item.url, item.name));
            }
            Debug.WriteLine("Service Done");
            foreach (var item in charactersBase) {
                gotDatabase.SaveCharacterBase(item);
            }
            Debug.WriteLine("Data saving Done");
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

        public static async Task<T> GetAsync<T>(Uri uri) {
            using (var client = new HttpClient()) {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
