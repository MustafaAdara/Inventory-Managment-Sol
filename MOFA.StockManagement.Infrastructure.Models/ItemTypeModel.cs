namespace MOFA.StockManagement.Infrastructure.Models
{
    public class ItemTypeModel : ModelEntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid? ParentItemTypeID { get; set; }

        //public ICollection<ItemModel>? Items { get; set; }
    }
}
