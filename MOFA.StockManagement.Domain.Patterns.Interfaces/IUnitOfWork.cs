namespace MOFA.StockManagement.Domain.Patterns.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters,
            CancellationToken cancellationToken = default);
    }

    public interface IUnitOfWork<TDbContext>
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters,
            CancellationToken cancellationToken = default);
    }
}
