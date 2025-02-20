using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class WarehouseViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.WarehouseViewModelResource), Name = "Code")]
        public string Code { get; set; } = default!;

        [Required]
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.WarehouseViewModelResource), Name = "Name")]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        [Display(ResourceType = typeof(Resources.WarehouseViewModelResource), Name = "Location")]
        public string Location { get; set; } = default!;

        [Required]
        [Display(ResourceType = typeof(Resources.WarehouseViewModelResource), Name = "Capacity")]
        public float Capacity { get; set; } = default!;

        [Required]
        [Display(ResourceType = typeof(Resources.WarehouseViewModelResource), Name = "IsFull")]
        public byte IsFull { get; set; }


        //public ICollection<StockBalanceViewModel>? StockBalances { get; set; }
        //public ICollection<OrderViewModel>? Orders { get; set; }
        //public ICollection<TransactionViewModel>? SourceTransactions { get; set; }
        //public ICollection<TransactionViewModel>? DestinationTransactions { get; set; }
        //public ICollection<OrderSerialViewModel>? OrderSerials { get; set; }
    }
}
