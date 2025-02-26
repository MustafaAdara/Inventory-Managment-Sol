namespace MOFA.StockManagement.Infrastructure.Models
{
    public class TransactionDetailModel : ModelEntityBase<Guid>
    {
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderDetailId { get; set; }

        public float Quantity { get; set; } // Number of units moved
        public decimal UnitCost { get; set; } // Cost of item during movement

        public TransactionModel? Transaction { get; set; }
        public ItemModel? Item { get; set; }
        public OrderDetailModel? OrderDetail { get; set; }
    }

}