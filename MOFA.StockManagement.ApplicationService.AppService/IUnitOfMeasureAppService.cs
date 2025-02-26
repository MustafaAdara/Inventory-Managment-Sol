using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.ApplicationService.AppService
{
    public interface IUnitOfMeasureAppService : IAppServiceBase<UnitOfMeasureViewModel, Guid>
    {
    }

    public class UnitOfMeasureAppService : AppServiceBase<UnitOfMeasureViewModel, Guid>, IUnitOfMeasureAppService
    {
        private readonly IHttpClientHelper<UnitOfMeasureViewModel> _helper;
        public UnitOfMeasureAppService(IHttpClientHelper<UnitOfMeasureViewModel> helper)
            : base(helper, "/api/UnitOfMeasures")
        {
            _helper = helper;
        }
    }
}
