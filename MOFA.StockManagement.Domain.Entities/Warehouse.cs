namespace MOFA.StockManagement.Domain.Entities
{
    public class Warehouse : EntityBase<Guid>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
        public float Capacity { get; set; } = default!;
        public byte IsFull { get; set; }


        public ICollection<StockBalance>? StockBalances { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Transaction>? SourceTransactions { get; set; }
        public ICollection<Transaction>? DestinationTransactions { get; set; }
        public ICollection<OrderSerial>? OrderSerials { get; set; }
    }
}
