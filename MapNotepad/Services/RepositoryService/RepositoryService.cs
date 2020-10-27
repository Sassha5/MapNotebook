﻿using System;
using System.Collections.Generic;
using System.IO;
using MapNotepad.Models;
using SQLite;

namespace MapNotepad.Services.RepositoryService
{
    class RepositoryService : IRepositoryService// where T : BaseModel, new()
    {
        private readonly SQLiteConnection database;

        public RepositoryService()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db"));
        }

        public int CreateTable<T>() where T : BaseModel, new()
        {
            return (int)database.CreateTable<T>();
        }

        public IEnumerable<T> GetItems<T>() where T : BaseModel, new()
        {
            return database.Table<T>();
        }

        public T GetItem<T>(int id) where T : BaseModel, new()
        {
            return database.Get<T>(id);
        }

        public int DeleteItem<T>(int id) where T : BaseModel, new()
        {
            return database.Delete<T>(id);
        }

        public int InsertItem<T>(T item) where T : BaseModel, new()
        {
            return database.Insert(item);
        }

        public int UpdateItem<T>(T item) where T : BaseModel, new()
        {
            return database.Update(item);
        }
    }
}