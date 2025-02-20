using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using TrackableEntities.Common.Core;

namespace MOFA.StockManagement.Domain.Patterns.Interfaces.Services
{
    public interface IService<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
    }

    public interface IService<TEntity, TDbContext> : IService<TEntity> where TEntity : class, ITrackable
    {
    }
}
