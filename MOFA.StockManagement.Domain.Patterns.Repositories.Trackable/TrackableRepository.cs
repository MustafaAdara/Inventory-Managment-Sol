using Microsoft.EntityFrameworkCore;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using TrackableEntities.Common.Core;
using TrackableEntities.EF.Core;

namespace MOFA.StockManagement.Domain.Patterns.Repositories.Trackable
{
    public class TrackableRepository<TEntity> : Repository<TEntity>, ITrackableRepository<TEntity>
        where TEntity : class, ITrackable
    {
        public TrackableRepository(DbContext context) : base(context)
        {
        }

        public override void Insert(TEntity item)
        {
            item.TrackingState = TrackingState.Added;
            base.Insert(item);
        }

        public override void Update(TEntity item)
        {
            item.TrackingState = TrackingState.Modified;
            base.Update(item);
        }

        public override void Delete(TEntity item)
        {
            item.TrackingState = TrackingState.Deleted;
            base.Delete(item);
        }

        public override async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            if (item == null) return false;
            item.TrackingState = TrackingState.Deleted;
            Context.Entry(item).State = EntityState.Deleted;
            return true;
        }

        public virtual void ApplyChanges(params TEntity[] entities)
            => Context.ApplyChanges(entities);

        public virtual void AcceptChanges(params TEntity[] entities)
            => Context.AcceptChanges(entities);

        public virtual void DetachEntities(params TEntity[] entities)
            => Context.DetachEntities(entities);

        public virtual async Task LoadRelatedEntities(params TEntity[] entities)
            => await Context.LoadRelatedEntitiesAsync(entities);
    }

    public class TrackableRepository<TEntity, TDbContext> : TrackableRepository<TEntity>, ITrackableRepository<TEntity, TDbContext>
        where TDbContext : DbContext
        where TEntity : class, ITrackable
    {
        public TrackableRepository(TDbContext context) : base(context)
        {
        }
    }
}
