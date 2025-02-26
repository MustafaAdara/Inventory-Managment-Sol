using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class UnitOfMeasureViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.UnitOfMeasureViewModelResource), Name = "UnitName")]
        public string UnitName { get; set; } = default!;
    }
}
