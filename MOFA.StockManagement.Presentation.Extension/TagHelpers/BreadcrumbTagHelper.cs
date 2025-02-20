using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace MOFA.StockManagement.Presentation.Extension.TagHelpers
{
    [HtmlTargetElement("breadcrumb", Attributes = "items")]
    public class BreadcrumbTagHelper : TagHelper
    {
        [HtmlAttributeName("items")]
        public IList<Item>? Items { get; set; }
        private readonly LinkGenerator _linkGenerator;
        public BreadcrumbTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = (TagMode)0;
            output.TagName = "nav";
            output.Attributes.Add("aria-label", (object)"breadcrumb");
            output.PreContent.AppendHtml("<ol class=\"breadcrumb\">");
            if (this.Items != null)
            {
                foreach (Item item in (IEnumerable<Item>)this.Items)
                {
                    TagHelperContent content = output.Content;
                    string str;
                    if (!string.IsNullOrWhiteSpace(item.Page))
                        str = "<li class=\"breadcrumb-item\"><a href=\"" + this._linkGenerator.GetPathByPage(item.Page, null, item.RouteValues) + "\">" + item.Name + "</a></li>";
                    else
                        str = "<li class=\"breadcrumb-item active\" aria-current=\"page\">" + item.Name + "</li>";
                    content.AppendHtml(str);
                }
            }
            output.PostContent.AppendHtml("</ol>");
            output.Attributes.Clear();
        }
        public class Item
        {
            public string Name { get; set; }
            public string? Page { get; set; }
            public object? RouteValues { get; set; }

            public Item(string name, string page, object? routeValues)
            {
                Name = name;
                Page = page;
                RouteValues = routeValues;
            }
            public Item(string name)
            {
                Name = name;
                Page = null;
                RouteValues = null;
            }
        }
    }
}
