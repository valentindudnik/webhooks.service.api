using MongoDB.Driver;

namespace Webhooks.Service.DataAccess.Interfaces
{
    public interface IWebhooksServiceDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
    }
}
