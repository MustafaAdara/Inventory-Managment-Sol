using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.Warehouses
{
    public class _CreateModel : CreatePartialModelBase<IWarehouseAppService, WarehouseViewModel, Guid>
    {
        public _CreateModel(IWarehouseAppService appService) : base(appService, @"/Warehouses/Index", "Config")
        {
        }

        protected override async Task InitPageAsync()
        {
            Input = new WarehouseViewModel { Id = Guid.NewGuid()};
            await base.InitPageAsync();
        }

        protected override async Task<WarehouseViewModel?> GetViewModel()
        {
            return await Task.FromResult<WarehouseViewModel?>(new WarehouseViewModel() { Id = Guid.NewGuid()});
        }

    }
}
