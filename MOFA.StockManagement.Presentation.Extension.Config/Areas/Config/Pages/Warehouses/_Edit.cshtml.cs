using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.Warehouses
{
    public class _EditModel : EditPartialModelBase<IWarehouseAppService, WarehouseViewModel, Guid>
    {

        public _EditModel(IWarehouseAppService appService ) 
            : base(appService, listId: "listWarehouses")
        {
        }

    }
}
