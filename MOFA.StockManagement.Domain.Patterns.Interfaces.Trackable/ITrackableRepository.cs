using TrackableEntities.Common.Core;

namespace MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable
{
    public interface ITrackableRepository<TEntity> : IRepository<TEntity> where TEntity : class, ITrackable
    {
        void ApplyChanges(params TEntity[] entities);
        void AcceptChanges(params TEntity[] entities);
        void DetachEntities(params TEntity[] entities);
        Task LoadRelatedEntities(params TEntity[] entities);
    }

    public interface ITrackableRepository<TEntity, TDbContext> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
    }
}
