using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;

namespace MOFA.StockManagement.Infrastructure.Queries
{
    public static class OrderRepository
    {
        public static Task<IEnumerable<Order>> SelectSearchAsync<TDContext>(
            this ITrackableRepository<Order, TDContext> repository,
            string searchBy,
            int page,
            string sortBy,
            int pageSize = 10,
            bool sortAsc = true)
        {
            var query = repository.Query()
                .Where(q => string.IsNullOrEmpty(searchBy) || q.Number.Contains(searchBy) );

            query = sortAsc ? query.OrderBy(sortBy) : query.OrderByDescending(sortBy);

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .SelectAsync();
        }


        public static Task<int> CountSearchAsync<TDbContext>(this ITrackableRepository<Order, TDbContext> repository, string searchBy)
        {
            return repository
                .Query()
                .Where(q => string.IsNullOrEmpty(searchBy) || q.Number.Contains(searchBy))
                .CountAsync();
        }
    }
}
