using MOFA.StockManagement.Domain.Patterns.Repositories.Trackable;
using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.Domain.Entities
{
    public abstract class EntityBase<T> : Entity where T : notnull
    {
        public virtual T Id { get; set; } = default!;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}
