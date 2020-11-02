using System;
using System.Collections.Generic;
using System.IO;
using MapNotepad.Models;
using SQLite;

namespace MapNotepad.Services.RepositoryService
{
    class RepositoryService : IRepositoryService
    {
        private readonly SQLiteConnection database;

        public RepositoryService()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db"));
        }

        public int CreateTable<T>() where T : IModelBase, new()
        {
            return (int)database.CreateTable<T>();
        }

        public IEnumerable<T> GetItems<T>() where T : IModelBase, new()
        {
            return database.Table<T>();
        }

        public T GetItem<T>(int id) where T : IModelBase, new()
        {
            return database.Get<T>(id);
        }

        public int DeleteItem<T>(int id) where T : IModelBase, new()
        {
            return database.Delete<T>(id);
        }

        public int InsertItem<T>(T item) where T : IModelBase, new()
        {
            return database.Insert(item);
        }

        public int UpdateItem<T>(T item) where T : IModelBase, new()
        {
            return database.Update(item);
        }
    }
}
