namespace MOFA.StockManagement.Domain.Entities
{
    public class Item : EntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string BarCode { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public decimal? UnitPrice { get; set; }
        public Guid ItemTypeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ItemType? ItemType { get; set; }
        public ICollection<StockBalance>? StockBalances { get; set; }
        public ICollection<ItemUnitOfMeasure>? ItemUnitOfMeasures { get; set; }
        public ICollection<SupplierItem>? SupplierItems { get; set; }

    }
}
