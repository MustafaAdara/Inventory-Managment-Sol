using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Sales.Areas.Sales.Pages.Items
{
    public class _ListModel : ListPartialModelBase<IItemAppService, ItemViewModel, Guid>
    {
        public _ListModel(IItemAppService appService) : base(appService, "Name")
        {
            
        }
    }
}
