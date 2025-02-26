using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class ItemTypeViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.ItemTypeViewModelResource), Name = "Name")]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(64)]
        [Display(ResourceType = typeof(Resources.ItemTypeViewModelResource), Name = "Description")]
        public string Description { get; set; } = default!;

        [MaxLength(32)]
        [Display(ResourceType = typeof(Resources.ItemTypeViewModelResource), Name = "ParentItemTypeID")]
        public Guid? ParentItemTypeID { get; set; }

        public ICollection<ItemViewModel>? Items { get; set; }
    }
}
