using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoesService
    {
        private readonly IMongoCollection<Todo> _todoItemsCollection;

        public TodoesService(
            IOptions<TodoDatabaseSettings> todoDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                todoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                todoDatabaseSettings.Value.DatabaseName);

            _todoItemsCollection = mongoDatabase.GetCollection<Todo>(
                todoDatabaseSettings.Value.TodoCollectionName);
        }

        public async Task<List<Todo>> GetAsync() =>
            await _todoItemsCollection.Find(_ => true).ToListAsync();

        public async Task<Todo?> GetAsync(string id) =>
            await _todoItemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Todo newTodo) =>
            await _todoItemsCollection.InsertOneAsync(newTodo);

        public async Task UpdateAsync(string id, Todo updatedTodo) =>
            await _todoItemsCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

        public async Task RemoveAsync(string id) =>
            await _todoItemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
