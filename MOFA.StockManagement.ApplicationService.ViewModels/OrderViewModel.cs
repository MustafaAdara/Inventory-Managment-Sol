using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class OrderViewModel : ViewModelBase<Guid>
    {
        [Required]
        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "Type")]
        public byte Type { get; set; } // "Supplier", "WarehouseTransfer", "Customer"

        [Required]
        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "Status")]
        public byte Status { get; set; } // "Pending", "Approved", "Completed", "Cancelled"

        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "Number")]
        public string Number { get; set; } = default!; // serial

        public DateTime OrderDate { get; set; }

        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "WarehouseId")]
        public Guid? WarehouseId { get; set; }

        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "DestinationWarehouseId")]
        public Guid? DestinationWarehouseId { get; set; }

        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "ConsumerId")]
        public Guid? ConsumerId { get; set; }

        [Display(ResourceType = typeof(Resources.OrderViewModelResource), Name = "SupplierId")]
        public Guid? SupplierId { get; set; }

        public SupplierViewModel? Supplier { get; set; }
        public WarehouseViewModel? Warehouse { get; set; }
        public WarehouseViewModel? DestinationWarehouse { get; set; }
        public ConsumerViewModel? Consumer { get; set; }

        //public int CreatedByUserID { get; set; } // Who placed the order
        //public User CreatedByUser { get; set; }
    }
}
