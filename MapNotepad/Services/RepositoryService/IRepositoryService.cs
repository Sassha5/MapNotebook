using System;
using System.Collections.Generic;
using MapNotepad.Models;

namespace MapNotepad.Services.RepositoryService
{
    public interface IRepositoryService
    {
        int CreateTable<T>() where T : BaseModel, new();
        IEnumerable<T> GetItems<T>() where T : BaseModel, new();
        T GetItem<T>(int id) where T : BaseModel, new();
        int DeleteItem<T>(int id) where T : BaseModel, new();
        int InsertItem<T>(T item) where T : BaseModel, new();
        int UpdateItem<T>(T item) where T : BaseModel, new();
    }
}
