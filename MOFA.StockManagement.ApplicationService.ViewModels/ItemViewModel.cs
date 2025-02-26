using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class ItemViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "Name")]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "Description")]
        public string Description { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "BarCode")]
        public string BarCode { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "SKU")]
        public string SKU { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "ItemTypeId")]
        public Guid ItemTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.ItemViewModelResource), Name = "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        public ItemTypeViewModel? ItemType { get; set; }
        //public ICollection<StockBalanceViewModel>? StockBalances { get; set; }
        //public ICollection<ItemUnitOfMeasureViewModel>? ItemUnitOfMeasures { get; set; }
        //public ICollection<SupplierItemViewModel>? SupplierItems { get; set; }
    }
}
