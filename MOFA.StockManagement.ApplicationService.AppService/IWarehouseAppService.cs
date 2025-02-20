using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IWarehouseAppService : IAppServiceBase<WarehouseViewModel, Guid>
    {
    }

    public class WarehouseAppService : AppServiceBase<WarehouseViewModel, Guid>, IWarehouseAppService
    {
        private readonly IHttpClientHelper<WarehouseViewModel> _helper;
        public WarehouseAppService(IHttpClientHelper<WarehouseViewModel> helper)
            : base(helper, "/api/Warehouses")
        {
            _helper = helper;
        }
    }
}
