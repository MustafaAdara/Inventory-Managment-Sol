namespace MOFA.StockManagement.Infrastructure.Models
{
    public class StockBalanceModel : ModelEntityBase<Guid>
    {
        public Guid ItemId { get; set; }
        public ItemModel? Item { get; set; }
        public Guid WarehouseId { get; set; }
        public WarehouseModel? Warehouse { get; set; }
        public float Quantity { get; set; }

        public float MinimumStockLevel { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
