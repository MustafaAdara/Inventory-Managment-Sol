using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IOrderAppService : IAppServiceBase<OrderViewModel, Guid>
    {
    }

    public class OrderAppService : AppServiceBase<OrderViewModel, Guid>, IOrderAppService
    {
        private readonly IHttpClientHelper<OrderViewModel> _helper;
        public OrderAppService(IHttpClientHelper<OrderViewModel> helper)
            : base(helper, "/api/Orders")
        {
            _helper = helper;
        }
    }
}
