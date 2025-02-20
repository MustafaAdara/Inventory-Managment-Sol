using System.Text.Json.Serialization;

namespace MOFA.StockManagement.Infrastructure.Models
{
    public class ModelEntityBase<TKey>
    {
        public virtual TKey? Id { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public byte[]? RowVersion { get; set; }
    }
}
