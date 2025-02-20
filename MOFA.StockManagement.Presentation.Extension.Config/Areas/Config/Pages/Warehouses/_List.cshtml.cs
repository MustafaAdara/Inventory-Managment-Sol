using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.Warehouses
{
    public class _ListModel : ListPartialModelBase<IWarehouseAppService, WarehouseViewModel, Guid>
    {
        public _ListModel(IWarehouseAppService appService) : base(appService, "Name")
        {
            
        }
    }
}
