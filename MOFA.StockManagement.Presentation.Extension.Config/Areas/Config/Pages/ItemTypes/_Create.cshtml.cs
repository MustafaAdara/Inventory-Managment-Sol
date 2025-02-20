using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.ItemTypes
{
    public class _CreateModel : CreatePartialModelBase<IItemTypeAppService, ItemTypeViewModel, Guid>
    {
        public _CreateModel(IItemTypeAppService appService) : base(appService, @"/ItemTypes/Index", "Config")
        {
        }

        protected override async Task InitPageAsync()
        {
            Input = new ItemTypeViewModel { Id = Guid.NewGuid() };
            await base.InitPageAsync();
        }

        protected override async Task<ItemTypeViewModel?> GetViewModel()
        {
            return await Task.FromResult<ItemTypeViewModel?>(new ItemTypeViewModel() { Id = Guid.NewGuid()});
        }

    }
}
