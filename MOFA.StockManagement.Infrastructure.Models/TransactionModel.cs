namespace MOFA.StockManagement.Infrastructure.Models
{
    public class TransactionModel : ModelEntityBase<Guid>
    {
        public Guid? OrderId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? SourceWarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public byte TransactionType { get; set; } // "Purchase", "WarehouseTransfer", "Sale", etc.
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public OrderModel? Order { get; set; }
        public SupplierModel? Supplier { get; set; }
        public WarehouseModel? SourceWarehouse { get; set; }
        public WarehouseModel? DestinationWarehouse { get; set; }
        public ICollection<TransactionDetailModel>? TransactionDetails { get; set; }
    }

}
