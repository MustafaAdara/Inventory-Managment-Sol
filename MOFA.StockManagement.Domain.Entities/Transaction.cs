namespace MOFA.StockManagement.Domain.Entities
{
    public class Transaction : EntityBase<Guid>
    {
        public Guid? OrderId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? SourceWarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public byte TransactionType { get; set; } // "Purchase", "WarehouseTransfer", "Sale", etc.
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public Order? Order { get; set; }
        public Supplier? Supplier { get; set; }
        public Warehouse? SourceWarehouse { get; set; }
        public Warehouse? DestinationWarehouse { get; set; }
        public ICollection<TransactionDetail>? TransactionDetails { get; set; }
    }

}
