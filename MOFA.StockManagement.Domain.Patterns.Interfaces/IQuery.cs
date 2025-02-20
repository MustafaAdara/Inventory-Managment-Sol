using System.Linq.Expressions;

namespace MOFA.StockManagement.Domain.Patterns.Interfaces
{
    public interface IQuery<TEntity> where TEntity : class
    {
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IQuery<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty);
        IQuery<TEntity> Include(string navigationPropertyPath);

        IQuery<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector);
        IQuery<TEntity> OrderBy(string propertyName);
        IQuery<TEntity> OrderByDescending(Expression<Func<TEntity, object>> keySelector);
        IQuery<TEntity> OrderByDescending(string propertyName);
        IQuery<TEntity> ThenBy(Expression<Func<TEntity, object>> thenBy);
        IQuery<TEntity> ThenByDescending(Expression<Func<TEntity, object>> thenByDescending);

        Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken = default);

        IQuery<TEntity> Skip(int skip);
        IQuery<TEntity> Take(int take);


        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity?> SingleOrDefaultAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
