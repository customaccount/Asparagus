using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace AzureTraining.DeviceEmulators.Abstractions
{
    public interface IRepository<T>
    {
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<Document> CreateItemAsync(T item);
        Task<Document> UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
        void Initialize();
    }
}
