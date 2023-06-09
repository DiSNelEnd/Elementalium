using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using SQLite;

namespace Elementalium.Data
{
    public class WoElementDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public WoElementDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<WoElement>().Wait();
        }

        public Task<List<WoElement>> GetElementAsync()
        {
            return _database.Table<WoElement>().ToListAsync();
        }

        public Task<WoElement> GetElementAsync(int id)
        {
            return _database.Table<WoElement>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveElementAsync(WoElement element)
        {
            return element.Id != 0 ? _database.UpdateAsync(element) : _database.InsertAsync(element);
        }

        public Task<int> DeleteNoteAsync(WoElement element)
        {
            return _database.DeleteAsync(element);
        }
    }
}
