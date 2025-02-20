using Microsoft.AspNetCore.Mvc.RazorPages;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class PagePartialModelBase<TIAppService, TViewModel, TKey> : PageModel where TIAppService : IAppServiceBase<TViewModel, TKey> where TViewModel : ViewModelBase<TKey>
    {
        public readonly TIAppService _appService;

        public bool Rtl { get; set; }

        public PagePartialModelBase(TIAppService appService)
        {
            _appService = appService;
        }
        public bool GetRtl()
            => Rtl = Request.HttpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>()?.RequestCulture.Culture.Name != "en";
    }
}
