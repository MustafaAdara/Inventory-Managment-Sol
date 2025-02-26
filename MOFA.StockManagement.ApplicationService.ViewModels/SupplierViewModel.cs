using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class SupplierViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.SupplierViewModelResource), Name = "Name")]
        public string Name { get; set; } = default!;

        [Required]
        [Display(ResourceType = typeof(Resources.SupplierViewModelResource), Name = "Phone")]
        public int Phone { get; set; } = default!;

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.SupplierViewModelResource), Name = "Email")]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.SupplierViewModelResource), Name = "Address")]
        public string Address { get; set; } = default!;

        public ICollection<OrderViewModel>? Orders { get; set; }
    }
}
