namespace MOFA.StockManagement.Infrastructure.Models
{
    public class WarehouseModel : ModelEntityBase<Guid>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
        public float Capacity { get; set; } = default!;
        public byte IsFull { get; set; }


        //public ICollection<StockBalanceModel>? StockBalances { get; set; }
        //public ICollection<OrderModel>? Orders { get; set; }
        //public ICollection<TransactionModel>? SourceTransactions { get; set; }
        //public ICollection<TransactionModel>? DestinationTransactions { get; set; }
        //public ICollection<OrderSerialModel>? OrderSerials { get; set; }
    }
}
