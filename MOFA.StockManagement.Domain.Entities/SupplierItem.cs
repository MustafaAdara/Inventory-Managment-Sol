namespace MOFA.StockManagement.Domain.Entities
{
    public class SupplierItem : EntityBase<Guid>
    {
        public Guid ItemId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime DateSupplied { get; set; }
        public float Quantity { get; set; }
        public byte Unit { get; set; } // if its quantity or number

        public Item? Item { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
