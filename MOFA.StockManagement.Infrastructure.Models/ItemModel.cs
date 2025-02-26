namespace MOFA.StockManagement.Infrastructure.Models
{
    public class ItemModel : ModelEntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string BarCode { get; set; } = default!;
        public string? BarCodeImg { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public decimal? UnitPrice { get; set; }
        public Guid ItemTypeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ItemTypeModel? ItemType { get; set; }
        public ICollection<StockBalanceModel>? StockBalances { get; set; }
        public ICollection<ItemUnitOfMeasureModel>? ItemUnitOfMeasures { get; set; }
        public ICollection<SupplierItemModel>? SupplierItems { get; set; }
    }
}
