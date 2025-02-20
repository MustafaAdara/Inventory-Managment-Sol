namespace MOFA.StockManagement.Domain.Entities
{
    public class UnitOfMeasure : EntityBase<Guid>
    {
        public string UnitName { get; set; } = default!;
        public ICollection<ItemUnitOfMeasure>? ItemUnitOfMeasures { get; set; }
    }
}

