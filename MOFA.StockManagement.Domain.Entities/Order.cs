namespace MOFA.StockManagement.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public byte Type { get; set; } // "Supplier", "WarehouseTransfer", "Customer"
        public byte Status { get; set; } // "Pending", "Approved", "Completed", "Cancelled"
        public string Number { get; set; } = default!; // serial
        public DateTime OrderDate { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public Guid? ConsumerId { get; set; }
        public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Warehouse? DestinationWarehouse { get; set; }
        public Consumer? Consumer { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public Transaction? Transaction { get; set; }

        //public int CreatedByUserID { get; set; } // Who placed the order
        //public User CreatedByUser { get; set; }
    }
}
