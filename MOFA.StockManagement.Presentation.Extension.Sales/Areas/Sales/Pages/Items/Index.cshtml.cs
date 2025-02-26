namespace MOFA.StockManagement.Presentation.Extension.Sales.Areas.Sales.Pages.Items
{
    public class IndexModel : IndexContainerModelBase
    {
        public IndexModel() : base()
        {
        }

        protected override void BreadcrumbGenerator()
        {
            base.BreadcrumbGenerator();
            BreadcrumbItems!.Add(new TagHelpers.BreadcrumbTagHelper.Item(Extension.Resources.NavResource.nav_Home, "/Index", new { area = "" }));
            BreadcrumbItems!.Add(new TagHelpers.BreadcrumbTagHelper.Item(Extension.Resources.NavResource.nav_Sales, "/Index", new { area = "Sales" }));
            BreadcrumbItems!.Add(new TagHelpers.BreadcrumbTagHelper.Item(Extension.Resources.NavResource.nav_Sales_Items));
        }
    }
}
