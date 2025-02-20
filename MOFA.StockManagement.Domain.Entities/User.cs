namespace MOFA.StockManagement.Domain.Entities
{
    public class User : EntityBase<Guid>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }

        public ICollection<Order>? CreatedOrders { get; set; }
        public ICollection<Transaction>? CreatedTransactions { get; set; }
    }

}
