namespace MOFA.StockManagement.Infrastructure.Models
{
    public class SupplierModel : ModelEntityBase<Guid>
    {
        public string Name { get; set; }    = default!;
        public int Phone { get; set; }   = default!;
        public string Email { get; set; }   = default!;
        public string Address { get; set; } = default!;

        public ICollection<TransactionModel>? Transactions { get; set; }
        public ICollection<OrderModel>? Orders { get; set; }
        public ICollection<SupplierItemModel>? SupplierItems { get; set; }
    }
}
