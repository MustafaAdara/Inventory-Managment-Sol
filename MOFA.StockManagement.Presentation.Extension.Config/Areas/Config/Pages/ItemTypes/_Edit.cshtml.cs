using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.ItemTypes
{
    public class _EditModel : EditPartialModelBase<IItemTypeAppService, ItemTypeViewModel, Guid>
    {

        public _EditModel(IItemTypeAppService appService ) 
            : base(appService, listId: "listItemTypes")
        {
        }

    }
}
