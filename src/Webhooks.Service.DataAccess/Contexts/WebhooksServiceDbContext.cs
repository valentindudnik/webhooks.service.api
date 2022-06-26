using MongoDB.Driver;
using Webhooks.Service.DataAccess.Interfaces;

namespace Webhooks.Service.DataAccess.Contexts
{
    public class WebhooksServiceDbContext : IWebhooksServiceDbContext
    {
        private readonly List<Func<Task>> _commands;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public WebhooksServiceDbContext(IMongoClient client, IMongoDatabase database)
        {
            _commands = new List<Func<Task>>();

            _client = client;
            _database = database;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public async Task<int> SaveChangesAsync()
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();

                await Task.WhenAll(_commands.Select(x => x()));

                await session.CommitTransactionAsync();
            }

            return _commands.Count;
        }
    }
}
