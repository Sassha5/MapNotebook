using System;
using System.Collections.Generic;
using MapNotepad.Models;

namespace MapNotepad.Services.RepositoryService
{
    public interface IRepositoryService
    {
        int CreateTable<T>() where T : IModelBase, new();
        IEnumerable<T> GetItems<T>() where T : IModelBase, new();
        T GetItem<T>(int id) where T : IModelBase, new();
        int DeleteItem<T>(int id) where T : IModelBase, new();
        int InsertItem<T>(T item) where T : IModelBase, new();
        int UpdateItem<T>(T item) where T : IModelBase, new();
    }
}
