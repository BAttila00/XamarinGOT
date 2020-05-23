using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGOT.DataBase {
    public class GotDatabase {
        readonly SQLiteConnection database;
        public GotDatabase(string dbPath) {

            database = new SQLiteConnection(dbPath);

            database.CreateTable<CharacterBase>();
            database.CreateTable<CharactersInBook>();

        }

        #region CharacterBase

        public List<CharacterBase> GetCharacterBases() {
            return database.Table<CharacterBase>().ToList();
        }

        public CharacterBase GetCharacterBase(int id) {
            return database.Table<CharacterBase>()
                            .Where(i => i.ID == id)
                            .FirstOrDefault();
        }

        public int SaveCharacterBase(CharacterBase CharacterBase) {
            if (CharacterBase.ID != 0) {
                return database.Update(CharacterBase);
            } else {
                return database.Insert(CharacterBase);
            }
        }

        public int DeleteCharacterBase(CharacterBase CharacterBase) {
            return database.Delete(CharacterBase);
        }

        #endregion

        #region CharactersInBook

        public List<CharactersInBook> GetCharactersInBook() {
            return database.Table<CharactersInBook>().ToList();
        }

        public CharactersInBook GetCharacterInBook(int id) {
            return database.Table<CharactersInBook>()
                            .Where(i => i.ID == id)
                            .FirstOrDefault();
        }

        public int SaveCharacterInBook(CharactersInBook CharacterInBook) {
            if (CharacterInBook.ID != 0) {
                return database.Update(CharacterInBook);
            } else {
                return database.Insert(CharacterInBook);
            }
        }

        public int DeleteCharacterInBook(CharactersInBook CharacterInBook) {
            return database.Delete(CharacterInBook);
        }

        public List<CharactersInBook> GetCharactersInBookByBookUrl(string url) {
            return database.Table<CharactersInBook>().Where(w => w.BookUrl == url).ToList();
        }

        #endregion

    }
}
