namespace MOFA.StockManagement.Domain.Entities
{
    public class Consumer : EntityBase<Guid>
    {
        public string Name { get; set; } = default!;
        public int Phone { get; set; }
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public byte Type { get; set; } // Retail, Wholesale, Company

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
