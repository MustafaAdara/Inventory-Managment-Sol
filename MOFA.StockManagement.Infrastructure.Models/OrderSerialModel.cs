namespace MOFA.StockManagement.Infrastructure.Models
{
    public class OrderSerialModel : ModelEntityBase<Guid>
    {
        public string Type { get; set; } = default!;
        public Guid WarehouseId { get; set; }
        public DateTime Year { get; set; }

        public WarehouseModel? Warehouse { get; set; }
    }
}
