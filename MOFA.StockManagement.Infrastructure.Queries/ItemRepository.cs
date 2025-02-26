using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;

namespace MOFA.StockManagement.Infrastructure.Queries
{
    public static class ItemRepository
    {
        public static Task<IEnumerable<Item>> SelectSearchAsync<TDContext>(
         this ITrackableRepository<Item, TDContext> repository, string searchBy,
         int page,
         string sortBy,
         int pageSize = 10,
         bool sortAsc = true
        )
        {
            if (sortAsc)
                return repository
                    .Query()
                    .Where(q => string.IsNullOrEmpty(searchBy)|| q.SKU.Contains(searchBy) || q.BarCode.Contains(searchBy) || q.Name.Contains(searchBy))
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

        public static Task<int> CountSearchAsync<TDbContext>(this ITrackableRepository<Item, TDbContext> repository, string searchBy)
        {
            return repository
                .Query()
                .Where(q => string.IsNullOrEmpty(searchBy)  || q.BarCode.Contains(searchBy) || q.Name.Contains(searchBy))
                .CountAsync();
        }

        public async static Task<Item?> ItemSKUExists<TDbContext>(this ITrackableRepository<Item, TDbContext> repository, Item item)
        {
            return await repository.Query()
                    .FirstOrDefaultAsync(i => i.SKU == item.SKU || i.BarCode == item.BarCode);
        }
    }
}
