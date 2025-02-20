namespace MOFA.StockManagement.Domain.Entities
{
    public class ItemType : EntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid? ParentItemTypeID { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}
