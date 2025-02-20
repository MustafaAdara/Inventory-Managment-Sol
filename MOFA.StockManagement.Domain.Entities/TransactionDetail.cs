namespace MOFA.StockManagement.Domain.Entities
{
    public class TransactionDetail : EntityBase<Guid>
    {
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
        public Guid OrderDetailId { get; set; }

        public float Quantity { get; set; } // Number of units moved
        public decimal UnitCost { get; set; } // Cost of item during movement

        public Transaction? Transaction { get; set; }
        public Item? Item { get; set; }
        public OrderDetail? OrderDetail { get; set; }
    }

}