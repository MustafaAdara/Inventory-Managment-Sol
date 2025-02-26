using Microsoft.AspNetCore.Mvc.Rendering;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Sales.Areas.Sales.Pages.Items
{
    public class _EditModel : EditPartialModelBase<IItemAppService, ItemViewModel, Guid>
    {
        private IItemTypeAppService _itemTypeAppService;

        public SelectList itemTypes;
        public _EditModel(IItemAppService appService, IItemTypeAppService itemTypeAppService)
            : base(appService, listId: "listItems")
        {
            _itemTypeAppService = itemTypeAppService;
        }
        protected override async Task InitPageAsync(Guid id)
        {
            itemTypes = new SelectList(await _itemTypeAppService.GetAsync(), "Id", "Name");
        }
    }
}
