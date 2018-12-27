using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AzureTraining.DeviceEmulators.Abstractions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace AzureTraining.DeviceEmulators.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private const string Endpoint = "https://localhost:8081";
        private const string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string _databaseId = "TestCosmosDb";
        private DocumentClient Client;
        private readonly string _collectionId;

        protected BaseRepository(string collectionId)
        {
            _collectionId = collectionId;
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));

                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = Client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                    new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            return await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), item);
        }

        public async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await Client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id), item);
        }

        public async Task DeleteItemAsync(string id)
        {
            await Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
        }

        public void Initialize()
        {
            Client = new DocumentClient(new Uri(Endpoint), Key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDatabaseAsync(new Database { Id = _databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection { Id = _collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
