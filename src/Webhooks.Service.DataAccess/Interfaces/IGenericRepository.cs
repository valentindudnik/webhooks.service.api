using Webhooks.Service.DataAccess.Models.Entities;

namespace Webhooks.Service.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity, new()
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);
        void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression);
        bool CollectionExists();
        bool CollectionExists(string collectionName);
        void Add(TEntity item);
        void Add(TEntity item, string collectionName);
        void Add(IEnumerable<TEntity> items);
        Task AddAsync(TEntity entity);
        Task AddAsync(TEntity entity, string collectionName);
        Task AddAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task UpdateAsync(TEntity entity);
        Task<TEntity?> GetAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> SaveChangesAsync();
    }
}
