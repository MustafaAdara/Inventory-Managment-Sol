using Microsoft.AspNetCore.Mvc.Rendering;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Sales.Areas.Sales.Pages.Items
{
    public class _CreateModel : CreatePartialModelBase<IItemAppService, ItemViewModel, Guid>
    {
        private IItemTypeAppService _itemTypeAppService;

        public SelectList itemTypes;
        public _CreateModel(IItemAppService appService, IItemTypeAppService itemTypeAppService) : base(appService, @"/Items/Index", "Sales")
        {
            _itemTypeAppService = itemTypeAppService;
        }

        protected override async Task InitPageAsync()
        {
            Input = new ItemViewModel { Id = Guid.NewGuid()};
            itemTypes = new SelectList(await _itemTypeAppService.GetAsync(), "Id", "Name");
            await base.InitPageAsync();
        }

        protected override async Task<ItemViewModel?> GetViewModel()
        {
            return await Task.FromResult<ItemViewModel?>(new ItemViewModel() { Id = Guid.NewGuid()});
        }

    }
}
