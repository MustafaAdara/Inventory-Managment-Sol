namespace MOFA.StockManagement.Infrastructure.Models
{
    public class OrderDetailModel : ModelEntityBase<Guid>
    {

        public Guid OrderId { get; set; }
        public OrderModel? Order { get; set; }

        public Guid ItemId { get; set; }
        public ItemModel? Item { get; set; }

        public float Quantity { get; set; } // Ordered quantity
        public decimal UnitPrice { get; set; } // Price per unit
        public byte OrderItemStatus { get; set; } // "Pending", "Fulfilled"
    }
}