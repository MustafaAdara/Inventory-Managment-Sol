using System.Linq.Expressions;
namespace MOFA.StockManagement.Domain.Patterns.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);

        Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object?>> property,
            CancellationToken cancellationToken = default);

        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        Task ModifyAsync<TKey>(TKey keyValue, Action<TEntity> func) where TKey : notnull;
        void Update(TEntity item);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableSql(string sql, params object[] parameters);
        void BeginTransaction();
        Task? CommitTransactionAsync();

        Task<T> ExecScallerSqlAsync<T>(string sql, T defaultvaue);
        IQuery<TEntity> Query();
    }

    public interface IRepository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class
    {
    }
}
