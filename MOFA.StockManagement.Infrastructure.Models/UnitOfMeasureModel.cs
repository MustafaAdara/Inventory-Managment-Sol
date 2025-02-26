namespace MOFA.StockManagement.Infrastructure.Models
{
    public class UnitOfMeasureModel : ModelEntityBase<Guid>
    {
        public string UnitName { get; set; } = default!;
        public ICollection<ItemUnitOfMeasureModel>? ItemUnitOfMeasures { get; set; }
    }
}

