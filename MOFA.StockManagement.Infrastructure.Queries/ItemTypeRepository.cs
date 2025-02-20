using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;

namespace MOFA.StockManagement.Infrastructure.Queries
{
    public static class ItemTypeRepository
    {
        public static Task<IEnumerable<ItemType>> SelectSearchAsync<TDContext>(
            this ITrackableRepository<ItemType, TDContext> repository, string searchBy,
             int page,
             string sortBy,
             int pageSize = 10,
             bool sortAsc = true
            )
        {
            if (sortAsc)
                return repository
                    .Query()
                    .Where(q => string.IsNullOrEmpty(searchBy) || q.Name.Contains(searchBy) )
                    .OrderBy(sortBy)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .SelectAsync();
            else
                return repository
                    .Query()
                    .Where(q => string.IsNullOrEmpty(searchBy))
                    .OrderByDescending(sortBy)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .SelectAsync();
        }

        public static Task<int> CountSearchAsync<TDbContext>(this ITrackableRepository<ItemType, TDbContext> repository, string searchBy)
        {
            return repository
                .Query()
                .Where(q => string.IsNullOrEmpty(searchBy) || q.Name.Contains(searchBy))
                .CountAsync();
        }
    }
}
