using Microsoft.AspNetCore.Mvc.RazorPages;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.Presentation.Extension.TagHelpers;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class PageModelBase<TIAppService, TViewModel, TKey> : PageModel where TIAppService : IAppServiceBase<TViewModel, TKey> where TViewModel : ViewModelBase<TKey>
    {
        public readonly TIAppService _appService;
        public bool Rtl { get; set; }
        public IList<BreadcrumbTagHelper.Item>? BreadcrumbItems { get; set; }

        public PageModelBase(TIAppService appService)
        {
            _appService = appService;
        }

        public bool GetRtl()
            => Rtl = Request.HttpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>()?.RequestCulture.Culture.Name != "en";
        protected virtual void BreadcrumbGenerator()
        {
            BreadcrumbItems ??= new List<BreadcrumbTagHelper.Item>();
        }
    }
}
