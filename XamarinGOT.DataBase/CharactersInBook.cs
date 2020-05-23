using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGOT.DataBase {
    public class CharactersInBook {
        public CharactersInBook() {

        }

        public CharactersInBook(string bookUrl, string bookName, string characterUrl, string characterName) {
            this.BookUrl = bookUrl;
            this.BookName = bookName;
            this.CharacterName = characterName;
            this.CharacterUrl = characterUrl;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string BookName { get; set; }
        public string BookUrl { get; set; }
        public string CharacterUrl { get; set; }
        public string CharacterName { get; set; }


    }
}
