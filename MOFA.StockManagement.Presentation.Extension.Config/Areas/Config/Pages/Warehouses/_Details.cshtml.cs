using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.Warehouses
{
    public class _DetailsModel : DetailsPartialModelBase<IWarehouseAppService, WarehouseViewModel, Guid>
    {
        public _DetailsModel(IWarehouseAppService appService) :base(appService)
        {
            
        }
    }
}
