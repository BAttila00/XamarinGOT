using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinGOT.Models;

namespace GameofThrones.Services
{
    class GOTService
    {

        private readonly Uri serverUrl = new Uri("https://www.anapioficeandfire.com/");

        //ezt ne használd, helyette -> await service.GetSelectedList<Book>("Books");
        public async Task<List<Book>> GetBooks()
        {
            return await GetAsync<List<Book>>(new Uri(serverUrl, "api/books"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<List<T>> GetSelectedList<T>(string content)
        {
            List<T> list = new List<T>();
            List<T> listToReturn = new List<T>();
            int i = 1;
            do
            {
                list = await GetAsync<List<T>>(new Uri(serverUrl, $"api/{content}?page={i++}&pageSize=50"));
                listToReturn.AddRange(list);
                Debug.WriteLine(i + "  " + list.Count);

            } while (list.Any());

            return listToReturn;
        }


        /// <summary>
        /// Returns characters based on array of uri
        /// </summary>
        /// <param name="characterUris">The array of uri</param>
        /// <returns>
        /// cdgfffffffffff
        /// </returns>
        public async Task<List<Character>> GetCharactersInBook(string[] characterUris)
        {
            List<Character> charactersList = new List<Character>();
            foreach (var characterUri in characterUris)
            {
                charactersList.Add(await GetAsync<Character>(new Uri(characterUri)));       //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat. 04 Xaml 2 -> 21.old
            }
            return charactersList;
        }

        public List<List<string>> splitList(string[] characterUris, int howManyList)
        {
            List<string> characterUrisList = new List<string>(characterUris);
            var list = new List<List<string>>();

            int nSize = Convert.ToInt32(Math.Ceiling((decimal)characterUris.Length / (decimal)howManyList));
            for (int i = 0; i < characterUrisList.Count; i += nSize)
            {
                list.Add(characterUrisList.GetRange(i, Math.Min(nSize, characterUrisList.Count - i)));
            }
            return list;
        }

        public async Task<List<T>> GetItemsList<T>(string[] uris)
        {
            List<T> itemsList = new List<T>();
            foreach (var uri in uris)
            {
                itemsList.Add(await GetAsync<T>(new Uri(uri)));
            }
            return itemsList;
        }
        /*
		public async Task<List<Character>> GetCharacters(string[] characterUris) {							//igy is le lehet kerni a karaktereket, de nem tunik gyorsabbnak
			return await GetAsyncMine(characterUris);
		}*/

        public async Task<Book> GetBookAsync(string bookUrl)
        {
            return await GetAsync<Book>(new Uri(bookUrl));      //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat. 04 Xaml 2 -> 21.old
        }

        public async Task<House> GetHouseAsync(string url)
        {
            return await GetAsync<House>(new Uri(url));      //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat. 04 Xaml 2 -> 21.old
        }

        public async Task<Character> GetCharacterAsync(string url)
        {
            return await GetAsync<Character>(new Uri(url));      //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat. 04 Xaml 2 -> 21.old
        }


        /*
		private async Task<List<Character>> GetAsyncMine(string[] characterUris) {
			List<Character> list = new List<Character>();
			using (var client = new HttpClient()) {
				foreach (var characterUri in characterUris) {
					var response = await client.GetAsync(characterUri);
					var json = await response.Content.ReadAsStringAsync();
					Character result = JsonConvert.DeserializeObject<Character>(json);
					list.Add(result);
				}
				//var response = await client.GetAsync(uri);

				return list;
			}
		}*/

        public async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
