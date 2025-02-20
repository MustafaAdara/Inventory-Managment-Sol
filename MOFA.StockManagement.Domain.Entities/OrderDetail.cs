namespace MOFA.StockManagement.Domain.Entities
{
    public class OrderDetail : EntityBase<Guid>
    {

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        public float Quantity { get; set; } // Ordered quantity
        public decimal UnitPrice { get; set; } // Price per unit
        public byte OrderItemStatus { get; set; } // "Pending", "Fulfilled"
    }
}