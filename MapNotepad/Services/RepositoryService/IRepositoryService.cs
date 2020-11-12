using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapNotepad.Models;

namespace MapNotepad.Services.RepositoryService
{
    public interface IRepositoryService
    {
        void CreateTableAsync<T>() where T : IModelBase, new();
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : IModelBase, new();
        Task<T> GetItemAsync<T>(int id) where T : IModelBase, new();
        Task<int> DeleteItemAsync<T>(int id) where T : IModelBase, new();
        Task<int> InsertItemAsync<T>(T item) where T : IModelBase, new();
        Task<int> UpdateItemAsync<T>(T item) where T : IModelBase, new();
    }
}
