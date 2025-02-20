namespace MOFA.StockManagement.Domain.Entities
{
    public class Supplier : EntityBase<Guid>
    {
        public string Name { get; set; }    = default!;
        public int Phone { get; set; }   = default!;
        public string Email { get; set; }   = default!;
        public string Address { get; set; } = default!;

        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<SupplierItem>? SupplierItems { get; set; }
    }
}
