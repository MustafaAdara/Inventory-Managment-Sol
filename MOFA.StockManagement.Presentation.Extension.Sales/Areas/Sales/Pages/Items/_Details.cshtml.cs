using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Sales.Areas.Sales.Pages.Items
{
    public class _DetailsModel : DetailsPartialModelBase<IItemAppService, ItemViewModel, Guid>
    {
        public _DetailsModel(IItemAppService appService) :base(appService)
        {
            
        }
    }
}
