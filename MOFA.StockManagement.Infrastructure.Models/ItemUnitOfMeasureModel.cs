namespace MOFA.StockManagement.Infrastructure.Models
{
    public class ItemUnitOfMeasureModel : ModelEntityBase<Guid>
    {
        public Guid ItemId { get; set; } // to know that this item in this UOF(box) have 5 items 
        public ItemModel? Item { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public UnitOfMeasureModel? UnitOfMeasure { get; set; }

        public decimal Price { get; set; }
        public bool IsBase { get; set; }
        public decimal ConversionFactor { get; set; }
    }
}