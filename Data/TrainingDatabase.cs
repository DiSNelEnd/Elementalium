using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elementalium.Domain.Model;
using SQLite;

namespace Elementalium.Data
{
    public class TrainingDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TrainingDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Training>().Wait();
        }

        public Task<List<Training>> GetElementAsync()
        {
            return _database.Table<Training>().ToListAsync();
        }

        public Task<Training> GetElementAsync(int id)
        {
            return _database.Table<Training>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveElementAsync(Training training)
        {
            return training.Id != 0 ? _database.UpdateAsync(training) : _database.InsertAsync(training);
        }

        public Task<int> DeleteNoteAsync(Training training)
        {
            return _database.DeleteAsync(training);
        }
    }
}
