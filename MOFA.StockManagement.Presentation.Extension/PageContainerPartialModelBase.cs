using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MOFA.StockManagement.Presentation.Extension
{
    public class PageContainerPartialModelBase : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }
        public bool Rtl { get; set; }

        public PageContainerPartialModelBase()
        {
        }

        public IActionResult OnGetAsync(int? pageIndex)
        {
            Rtl = Request.HttpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>()?.RequestCulture.Culture.Name != "en";


            return Page();
        }

    }
}
