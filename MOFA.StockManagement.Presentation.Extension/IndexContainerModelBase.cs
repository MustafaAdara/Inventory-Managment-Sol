using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MOFA.StockManagement.Presentation.Extension.TagHelpers;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class IndexContainerModelBase : PageModel
    {
        public IList<BreadcrumbTagHelper.Item>? BreadcrumbItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }
        public bool Rtl { get; set; }

        public IndexContainerModelBase()
        {
        }

        public virtual async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            Rtl = Request.HttpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>()?.RequestCulture.Culture.Name != "en";

            BreadcrumbGenerator();

            return Page();
        }

        protected virtual void BreadcrumbGenerator()
        {
            BreadcrumbItems ??= new List<BreadcrumbTagHelper.Item>();
        }
    }
}
