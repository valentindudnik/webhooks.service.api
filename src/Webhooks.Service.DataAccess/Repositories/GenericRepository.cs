using MongoDB.Bson;
using MongoDB.Driver;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Models.Entities;

namespace Webhooks.Service.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly IWebhooksServiceDbContext _context;

        public GenericRepository(IWebhooksServiceDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> All()
        {
            return _context.GetCollection<TEntity>(typeof(TEntity).Name).AsQueryable();
        }

        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return All().Where(expression);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            _context.GetCollection<TEntity>(typeof(TEntity).Name).DeleteMany(predicate);
        }

        public TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return All().Where(expression).Single();
        }

        public bool CollectionExists()
        {
            var collection = _context.GetCollection<TEntity>(typeof(TEntity).Name);

            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);

            return totalCount > 0;
        }

        public bool CollectionExists(string collectionName)
        {
            var collection = _context.GetCollection<TEntity>(collectionName);

            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);

            return totalCount > 0;
        }

        public void Add(TEntity entity)
        {
            _context.GetCollection<TEntity>(typeof(TEntity).Name).InsertOne(entity);
        }

        public void Add(TEntity entity, string collectionName)
        {
            _context.GetCollection<TEntity>(collectionName).InsertOne(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            _context.GetCollection<TEntity>(typeof(TEntity).Name).InsertMany(entities);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.GetCollection<TEntity>(typeof(TEntity).Name).InsertOneAsync(entity);
        }

        public async Task AddAsync(TEntity entity, string collectionName)
        {
            await _context.GetCollection<TEntity>(collectionName).InsertOneAsync(entity);
        }

        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _context.GetCollection<TEntity>(typeof(TEntity).Name).InsertManyAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _context.GetCollection<TEntity>(typeof(TEntity).Name).ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            await _context.GetCollection<TEntity>(typeof(TEntity).Name).DeleteManyAsync(predicate);
        }

        public async Task<TEntity?> GetAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Where(predicate).FirstOrDefault());
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(Where(x => x.IsActive).ToArray());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
