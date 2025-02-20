using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.ItemTypes
{
    public class _ListModel : ListPartialModelBase<IItemTypeAppService, ItemTypeViewModel, Guid>
    {
        public _ListModel(IItemTypeAppService appService) : base(appService, "Name")
        {
            
        }
    }
}
