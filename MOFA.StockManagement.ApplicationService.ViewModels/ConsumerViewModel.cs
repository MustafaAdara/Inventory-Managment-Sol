using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class ConsumerViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ConsumerViewModelResource), Name = "Name")]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ConsumerViewModelResource), Name = "Phone")]
        public int Phone { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ConsumerViewModelResource), Name = "Email")]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ConsumerViewModelResource), Name = "Address")]
        public string Address { get; set; } = default!;

        [Required]
        [MaxLength(5)]
        [Display(ResourceType = typeof(Resources.ConsumerViewModelResource), Name = "Type")]
        public byte Type { get; set; } // Retail, Wholesale, Company

        public ICollection<OrderViewModel>? Orders { get; set; }
    }
}
