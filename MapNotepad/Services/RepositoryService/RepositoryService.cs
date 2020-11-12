using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MapNotepad.Models;
using SQLite;
using static MapNotepad.Constants;

namespace MapNotepad.Services.RepositoryService
{
    class RepositoryService : IRepositoryService
    {
        private readonly SQLiteAsyncConnection database;

        public RepositoryService()
        {
            database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName));
        }

        public void CreateTableAsync<T>() where T : IModelBase, new()
        {
            database.CreateTableAsync<T>();
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : IModelBase, new()
        {
            return await database.Table<T>().ToListAsync();
        }

        public Task<T> GetItemAsync<T>(int id) where T : IModelBase, new()
        {
            return database.GetAsync<T>(id);
        }

        public Task<int> DeleteItemAsync<T>(int id) where T : IModelBase, new()
        {
            return database.DeleteAsync<T>(id);
        }

        public Task<int> InsertItemAsync<T>(T item) where T : IModelBase, new()
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync<T>(T item) where T : IModelBase, new()
        {
            return database.UpdateAsync(item);
        }
    }
}
