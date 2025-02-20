using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Domain.Entities;

using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;

namespace MOFA.StockManagement.Infrastructure.Queries
{
    public static class RepositoryBase
    {
        public static Task<TEntity?> SelectByIdAsync<TEntity, TKey, TDbContext>(this ITrackableRepository<TEntity, TDbContext> repository, TKey id) where TEntity : EntityBase<TKey> where TKey : notnull
        {
            return repository
                .Queryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id.Equals(id));
        }
        public static Task<TEntity?> FirstOrDefaultAsync<TEntity, TKey, TDbContext>(this ITrackableRepository<TEntity, TDbContext> repository) where TEntity : EntityBase<TKey> where TKey : notnull
        {
            return repository
                .Queryable()
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public static Task<IEnumerable<TEntity>> SelectAsync<TEntity, TKey, TDbContext>(this ITrackableRepository<TEntity, TDbContext> repository) where TEntity : EntityBase<TKey> where TKey : notnull
        {
            return repository
                .Query()
                .SelectAsync();
        }

        public static Task<IEnumerable<TEntity>> SelectAsync<TEntity, TKey, TDbContext>(this ITrackableRepository<TEntity, TDbContext> repository, int page, string sortBy, int pageSize = 10, bool sortAsc = true) where TEntity : EntityBase<TKey> where TKey : notnull
        {
            if (sortAsc)
                return repository
                    .Query()
                    .OrderBy(sortBy)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .SelectAsync();
            else
                return repository
                    .Query()
                    .OrderByDescending(sortBy)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .SelectAsync();
        }

        public static Task<int> CountAsync<TEntity, TKey, TDbContext>(this ITrackableRepository<TEntity, TDbContext> repository) where TEntity : EntityBase<TKey> where TKey : notnull
        {
            return repository
                .Query()
                .CountAsync();
        }
    }
}
