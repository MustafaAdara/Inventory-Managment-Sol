using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Domain.Patterns.Interfaces;

namespace MOFA.StockManagement.Domain.Patterns.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters,
            CancellationToken cancellationToken = default)
        {
            return await Context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }
    }

    public class UnitOfWork<TDbContext> : UnitOfWork, IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        public UnitOfWork(TDbContext context)
            : base(context)
        {
        }
    }
}
