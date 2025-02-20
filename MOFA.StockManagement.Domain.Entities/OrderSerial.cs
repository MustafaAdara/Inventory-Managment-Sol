namespace MOFA.StockManagement.Domain.Entities
{
    public class OrderSerial : EntityBase<Guid>
    {
        public string Type { get; set; } = default!;
        public Guid WarehouseId { get; set; }
        public DateTime Year { get; set; }

        public Warehouse? Warehouse { get; set; }
    }
}
