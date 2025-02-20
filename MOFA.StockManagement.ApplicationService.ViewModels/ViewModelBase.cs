using System.ComponentModel.DataAnnotations;

namespace MOFA.StockManagement.ApplicationService.ViewModels
{
    public class ViewModelBase<TKey>
    {
        public virtual TKey? Id { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Last Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy  HH:mm:ss}")]
        public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
