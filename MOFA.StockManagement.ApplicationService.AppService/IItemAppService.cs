using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IItemAppService : IAppServiceBase<ItemViewModel, Guid>
    {
    }

    public class ItemAppService : AppServiceBase<ItemViewModel, Guid>, IItemAppService
    {
        private readonly IHttpClientHelper<ItemViewModel> _helper;
        public ItemAppService(IHttpClientHelper<ItemViewModel> helper)
            : base(helper, "/api/Items")
        {
            _helper = helper;
        }
    }
}
