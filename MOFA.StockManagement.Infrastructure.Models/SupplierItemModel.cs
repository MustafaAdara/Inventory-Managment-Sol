namespace MOFA.StockManagement.Infrastructure.Models
{
    public class SupplierItemModel : ModelEntityBase<Guid>
    {
        public Guid ItemId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime DateSupplied { get; set; }
        public float Quantity { get; set; }
        public byte Unit { get; set; } // if its quantity or number

        public ItemModel? Item { get; set; }
        public SupplierModel? Supplier { get; set; }
    }
}
