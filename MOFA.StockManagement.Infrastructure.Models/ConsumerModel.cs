namespace MOFA.StockManagement.Infrastructure.Models
{
    public class ConsumerModel : ModelEntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public int Phone { get; set; }
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public byte Type { get; set; } // Retail, Wholesale, Company

        public ICollection<OrderModel>? Orders { get; set; }
        public ICollection<TransactionModel>? Transactions { get; set; }

    }
}
