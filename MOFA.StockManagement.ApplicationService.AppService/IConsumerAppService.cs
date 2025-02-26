using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IConsumerAppService : IAppServiceBase<ConsumerViewModel, Guid>
    {
    }

    public class ConsumerAppService : AppServiceBase<ConsumerViewModel, Guid>, IConsumerAppService
    {
        private readonly IHttpClientHelper<ConsumerViewModel> _helper;
        public ConsumerAppService(IHttpClientHelper<ConsumerViewModel> helper)
            : base(helper, "/api/Consumers")
        {
            _helper = helper;
        }
    }
}
