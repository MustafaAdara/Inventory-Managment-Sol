using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IItemTypeAppService : IAppServiceBase<ItemTypeViewModel, Guid>
    {
    }

    public class ItemTypeAppService : AppServiceBase<ItemTypeViewModel, Guid>, IItemTypeAppService
    {
        private readonly IHttpClientHelper<ItemTypeViewModel> _helper;
        public ItemTypeAppService(IHttpClientHelper<ItemTypeViewModel> helper)
            : base(helper, "/api/ItemTypes")
        {
            _helper = helper;
        }
    }
}
