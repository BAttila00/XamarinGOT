using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBuilderWPF {
    public class GotDatabase {
        readonly SQLiteConnection database;
        public GotDatabase(string dbPath) {

            database = new SQLiteConnection(dbPath);

            database.CreateTable<CharacterBase>();

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

    }
}
