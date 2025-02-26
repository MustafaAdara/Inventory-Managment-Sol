namespace MOFA.StockManagement.Infrastructure.Models
{
    public class OrderModel : ModelEntityBase<Guid>
    {
        public byte Type { get; set; } // "Supplier", "WarehouseTransfer", "Customer"
        public byte Status { get; set; } // "Pending", "Approved", "Completed", "Cancelled"
        public string Number { get; set; } = default!; // serial
        public DateTime OrderDate { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public Guid? ConsumerId { get; set; }
        public Guid? SupplierId { get; set; }

        public SupplierModel? Supplier { get; set; }
        public WarehouseModel? Warehouse { get; set; }
        public WarehouseModel? DestinationWarehouse { get; set; }
        public ConsumerModel? Consumer { get; set; }
        public ICollection<OrderDetailModel>? OrderDetails { get; set; }
        public TransactionModel? Transaction { get; set; }

        //public int CreatedByUserID { get; set; } // Who placed the order
        //public User CreatedByUser { get; set; }
    }
}
