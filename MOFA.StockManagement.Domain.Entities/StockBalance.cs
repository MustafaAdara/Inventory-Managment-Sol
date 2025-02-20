namespace MOFA.StockManagement.Domain.Entities
{
    public class StockBalance : EntityBase<Guid>
    {
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public float Quantity { get; set; }

        public float MinimumStockLevel { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
