namespace MOFA.StockManagement.Domain.Entities
{
    public class ItemUnitOfMeasure : EntityBase<Guid>
    {
        public Guid ItemId { get; set; } // to know that this item in this UOF(box) have 5 items 
        public Item? Item { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public UnitOfMeasure? UnitOfMeasure { get; set; }

        public decimal Price { get; set; }
        public bool IsBase { get; set; }
        public decimal ConversionFactor { get; set; }
    }
}