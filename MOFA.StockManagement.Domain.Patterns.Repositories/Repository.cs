using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace MOFA.StockManagement.Domain.Patterns.Repositories
{
    public class Repository<TEntity> : System.IDisposable, IRepository<TEntity> where TEntity : class
    {
        public Repository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected DbContext Context { get; }
        protected DbSet<TEntity> Set { get; }
        protected IDbContextTransaction? SetTransaction { get; set; }


        public virtual async Task<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return await Set.FindAsync(keyValues, cancellationToken);
        }

        public virtual async Task<TEntity?> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            if (keyValue == null)
                return null;
            else
                return await FindAsync(new object[] { keyValue }, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            return item != null;
        }

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            if (keyValue == null)
                return false;
            else
                return await ExistsAsync(new object[] { keyValue }, cancellationToken);
        }

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object?>> property,
            CancellationToken cancellationToken = default)
        {
            await Context.Entry(item).Reference(property).LoadAsync(cancellationToken);
        }

        public virtual void Attach(TEntity item)
        {
            Set.Attach(item);
        }
        public virtual async Task ModifyAsync<TKey>(TKey keyValue, Action<TEntity> func) where TKey : notnull
        {
            var oitem = await FindAsync(new object[] { keyValue });
            if (oitem == null)
                return;
            var entry = Set.Attach(oitem);
            func(oitem);
            entry.Properties.ToList().Select(property =>
            {
                var original = property.OriginalValue;
                var current = property.CurrentValue;

                if (ReferenceEquals(original, current))
                    return property;

                if (original == null)
                    property.IsModified = true;
                else
                    property.IsModified = !original.Equals(current);

                return property;
            });
        }

        public virtual void Detach(TEntity item)
        {
            Context.Entry(item).State = EntityState.Detached;
        }

        public virtual void Insert(TEntity item)
        {
            Context.Entry(item).State = EntityState.Added;
        }

        public virtual void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity item)
        {
            Context.Entry(item).State = EntityState.Deleted;
        }

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            if (item == null) return false;
            Context.Entry(item).State = EntityState.Deleted;
            return true;
        }

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            if (keyValue == null)
                return false;
            else
                return await DeleteAsync(new object[] { keyValue }, cancellationToken);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return Set;
        }

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters)
        {
            return Set.FromSqlRaw(sql, parameters);
        }
        public virtual void BeginTransaction()
        {
            SetTransaction = Context.Database.BeginTransaction();
        }
        public virtual Task? CommitTransactionAsync()
        {
            return SetTransaction?.CommitAsync();
        }
        public virtual async Task<T> ExecScallerSqlAsync<T>(string sql, T defaultvaue)
        {
            DbConnection connection = Context.Database.GetDbConnection();

            T value;
            using (DbCommand cmd = connection.CreateCommand())
            {

                cmd.CommandText = sql;


                if (connection.State.Equals(ConnectionState.Closed)) { connection.Open(); }

                object? res = await cmd.ExecuteScalarAsync();
                if (res == null || res.GetType() == typeof(DBNull))
                    value = defaultvaue;
                else
                    value = (T)res;
            }

            if (connection.State.Equals(ConnectionState.Open)) { connection.Close(); }

            return value;
        }

        public virtual IQuery<TEntity> Query()
        {
            return new Query<TEntity>(this);
        }

        public void Dispose()
        {
            Context.Dispose();
            System.GC.Collect();
            System.GC.SuppressFinalize(this);
        }
    }

    public class Repository<TEntity, TDbContext> : Repository<TEntity>, IRepository<TEntity, TDbContext>
        where TDbContext : DbContext
        where TEntity : class
    {
        public Repository(TDbContext context) : base(context)
        {
        }
    }
}
