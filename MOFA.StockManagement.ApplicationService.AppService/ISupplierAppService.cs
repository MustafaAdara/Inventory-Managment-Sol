using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface ISupplierAppService : IAppServiceBase<SupplierViewModel, Guid>
    {
    }

    public class SupplierAppService : AppServiceBase<SupplierViewModel, Guid>, ISupplierAppService
    {
        private readonly IHttpClientHelper<SupplierViewModel> _helper;
        public SupplierAppService(IHttpClientHelper<SupplierViewModel> helper)
            : base(helper, "/api/Suppliers")
        {
            _helper = helper;
        }
    }
}
