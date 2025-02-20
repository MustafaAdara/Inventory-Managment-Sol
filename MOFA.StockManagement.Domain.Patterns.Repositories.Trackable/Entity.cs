using System.ComponentModel.DataAnnotations.Schema;
using TrackableEntities.Common.Core;

namespace MOFA.StockManagement.Domain.Patterns.Repositories.Trackable
{
    public abstract class Entity : ITrackable, IMergeable
    {
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        [NotMapped]
        public ICollection<string>? ModifiedProperties { get; set; }

        [NotMapped]
        public Guid EntityIdentifier { get; set; }
    }
}
