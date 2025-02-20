namespace MOFA.StockManagement.Presentation.Extension.Config.Areas.Config.Pages.ItemTypes
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
            BreadcrumbItems!.Add(new TagHelpers.BreadcrumbTagHelper.Item(Extension.Resources.NavResource.nav_Config, "/Index", new { area = "Config" }));
            BreadcrumbItems!.Add(new TagHelpers.BreadcrumbTagHelper.Item(Extension.Resources.NavResource.nav_Config_ItemTypes));
        }
    }
}
