using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGOT.DataBase {
    public class CharacterBase {
        public CharacterBase() {
        }
        public CharacterBase(string url, string name) {
            this.url = url;
            this.name = name;
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }
}

