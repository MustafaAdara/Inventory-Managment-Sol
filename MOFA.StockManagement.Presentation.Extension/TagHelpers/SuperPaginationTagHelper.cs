using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;
using System.Reflection;

namespace MOFA.StockManagement.Presentation.Extension.TagHelpers
{
    [HtmlTargetElement("pagination")]
    public class SuperPaginationTagHelper : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IAntiforgery _antiForgery;

        public SuperPaginationTagHelper(LinkGenerator linkGenerator, IAntiforgery antiForgery)
        {
            this._linkGenerator = linkGenerator;
            this._antiForgery = antiForgery;
        }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("total-pages")]
        public int TotalPages { get; set; }

        [HtmlAttributeName("max-displayed-pages")]
        public int MaxDisplayedPages { get; set; } = 10;

        [HtmlAttributeName("show-gap")]
        public bool ShowGap { get; set; } = false;

        [HtmlAttributeName("rtl")]
        public bool Rtl { get; set; } = false;

        [HtmlAttributeName("is-post-method")]
        public bool IsPostMethod { get; set; } = false;

        [HtmlAttributeName("query-json")]
        public string QueryJson { get; set; }

        [HtmlAttributeName("query-element-id")]
        public string QueryElementId { get; set; }

        [HtmlAttributeName("page-name")]
        public string PageName { get; set; }

        [HtmlAttributeName("route-values")]
        public object RouteValues { get; set; }

        [HtmlAttributeName("page-index-element-id")]
        public string PageIndexElementId { get; set; }

        [HtmlAttributeName("http-context")]
        public HttpContext Context { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.IsPostMethod && string.IsNullOrEmpty(this.QueryJson))
                throw new Exception("Query JSON cannot be null or empty.");
            if (this.IsPostMethod && string.IsNullOrEmpty(this.QueryElementId))
                throw new Exception("Element Id cannot be null or empty.");
            TagBuilder tagBuilder1 = new TagBuilder("ul");
            tagBuilder1.AddCssClass("pagination");
            Boundaries boundaries = this.CalculateBoundaries();
            string str1 = "<li class=\"page-item disabled\"><a class=\"page-link\">&nbsp;...&nbsp;</a></li>";
            if (this.CurrentPage > 1)
            {
                string str2 = $"<li class=\"page-item\"><a class=\"page-link\" href=\"{this.CreatePagingUrl(this.CurrentPage - 1)}\" aria-label=\"First\"><i style=\"pointer-events: none\" class=\"fa {"fa-chevron-left"}\"></i></a></li>";
                tagBuilder1.InnerHtml.AppendHtml(str2);
            }
            if (this.ShowGap && boundaries.End > this.MaxDisplayedPages)
            {
                TagBuilder pagingLink = this.CreatePagingLink(1);
                tagBuilder1.InnerHtml.AppendHtml((IHtmlContent)pagingLink);
                tagBuilder1.InnerHtml.AppendHtml(str1);
            }
            for (int start = boundaries.Start; start <= boundaries.End; ++start)
            {
                TagBuilder pagingLink = this.CreatePagingLink(start);
                tagBuilder1.InnerHtml.AppendHtml((IHtmlContent)pagingLink);
            }
            if (this.ShowGap && boundaries.End < this.TotalPages)
            {
                tagBuilder1.InnerHtml.AppendHtml(str1);
                TagBuilder pagingLink = this.CreatePagingLink(this.TotalPages);
                tagBuilder1.InnerHtml.AppendHtml((IHtmlContent)pagingLink);
            }
            if (this.CurrentPage < this.TotalPages)
            {
                string str3 = $"<li class=\"page-item\"><a class=\"page-link\" href=\"{this.CreatePagingUrl(this.CurrentPage + 1)}\" aria-label=\"First\"><i style=\"pointer-events: none\" class=\"fa {"fa-chevron-right"}\"></i></a></li>";
                tagBuilder1.InnerHtml.AppendHtml(str3);
            }
            if (this.IsPostMethod)
            {
                TagBuilder tagBuilder2 = new TagBuilder("input");
                tagBuilder2.Attributes.Add("id", this.QueryElementId);
                tagBuilder2.Attributes.Add("name", this.QueryElementId);
                tagBuilder2.Attributes.Add("value", this.QueryJson);
                tagBuilder2.Attributes.Add("type", "hidden");
                TagBuilder tagBuilder3 = new TagBuilder("nav");
                tagBuilder3.Attributes.Add("aria-label", "...");
                tagBuilder3.InnerHtml.AppendHtml((IHtmlContent)tagBuilder1);
                TagBuilder tagBuilder4 = new TagBuilder("input");
                tagBuilder4.Attributes.Add("name", "__RequestVerificationToken");
                tagBuilder4.Attributes.Add("type", "hidden");
                tagBuilder4.Attributes.Add("value", this._antiForgery.GetAndStoreTokens(this.Context).RequestToken);
                output.TagName = "form";
                output.Attributes.SetAttribute("method", (object)"post");
                output.Content.AppendHtml((IHtmlContent)tagBuilder2);
                output.Content.AppendHtml((IHtmlContent)tagBuilder3);
                output.Content.AppendHtml((IHtmlContent)tagBuilder4);
            }
            else
            {
                output.TagName = "nav";
                output.Attributes.SetAttribute("aria-label", (object)"...");
                output.Content.AppendHtml((IHtmlContent)tagBuilder1);
            }
        }

        private TagBuilder CreatePagingLink(int targetPage)
        {
            TagBuilder pagingLink = new TagBuilder("li");
            pagingLink.AddCssClass("page-item");
            string pagingUrl = this.CreatePagingUrl(targetPage);
            if (this.IsPostMethod)
            {
                TagBuilder tagBuilder = new TagBuilder("button");
                tagBuilder.AddCssClass("page-link");
                tagBuilder.Attributes.Add("type", "submit");
                tagBuilder.Attributes.Add("formaction", pagingUrl);
                tagBuilder.InnerHtml.Append(string.Format("{0}", (object)targetPage));
                if (this.CurrentPage == targetPage)
                {
                    pagingLink.AddCssClass("active");
                    tagBuilder.Attributes.Add("tabindex", "-1");
                }
                pagingLink.InnerHtml.AppendHtml((IHtmlContent)tagBuilder);
            }
            else
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.AddCssClass("page-link");
                tagBuilder.Attributes.Add("href", pagingUrl);
                tagBuilder.InnerHtml.Append(string.Format("{0}", (object)targetPage));
                if (this.CurrentPage == targetPage)
                {
                    pagingLink.AddCssClass("active");
                    tagBuilder.Attributes.Add("tabindex", "-1");
                    tagBuilder.Attributes.Remove("href");
                }
                pagingLink.InnerHtml.AppendHtml((IHtmlContent)tagBuilder);
            }
            return pagingLink;
        }

        private Boundaries CalculateBoundaries()
        {
            int num1 = (int)Math.Ceiling((double)this.MaxDisplayedPages / 2.0);
            if (this.MaxDisplayedPages > this.TotalPages)
                this.MaxDisplayedPages = this.TotalPages;
            int num2;
            int num3;
            if (this.TotalPages == 1)
                num3 = num2 = 1;
            else if (this.CurrentPage < this.MaxDisplayedPages)
            {
                num3 = 1;
                num2 = this.MaxDisplayedPages;
            }
            else if (this.CurrentPage + this.MaxDisplayedPages == this.TotalPages)
            {
                num3 = this.TotalPages - this.MaxDisplayedPages > 0 ? this.TotalPages - this.MaxDisplayedPages - 1 : 1;
                num2 = this.TotalPages - 2;
            }
            else if (this.CurrentPage + this.MaxDisplayedPages == this.TotalPages + 1)
            {
                num3 = this.TotalPages - this.MaxDisplayedPages > 0 ? this.TotalPages - this.MaxDisplayedPages : 1;
                num2 = this.TotalPages - 1;
            }
            else if (this.CurrentPage + this.MaxDisplayedPages > this.TotalPages + 1)
            {
                num3 = this.TotalPages - this.MaxDisplayedPages > 0 ? this.TotalPages - this.MaxDisplayedPages - 1 : 1;
                num2 = this.TotalPages;
            }
            else
            {
                num3 = this.CurrentPage - num1 > 0 ? this.CurrentPage - num1 + 1 : 1;
                num2 = num3 + this.MaxDisplayedPages - 1;
            }
            return new Boundaries() { Start = num3, End = num2 };
        }

        private string CreatePagingUrl(int page)
        {
            IDictionary<string, object> dictionary = (IDictionary<string, object>)new ExpandoObject();
            dictionary[this.PageIndexElementId] = (object)page;
            if (this.RouteValues == null)
                return PageLinkGeneratorExtensions.GetPathByPage(this._linkGenerator, this.PageName, (string)null, (object)dictionary, new PathString(), new FragmentString(), (LinkOptions)null);
            foreach (PropertyInfo property in this.RouteValues.GetType().GetProperties())
                dictionary[((MemberInfo)property).Name] = property.GetValue(this.RouteValues);
            return PageLinkGeneratorExtensions.GetPathByPage(this._linkGenerator, this.PageName, (string)null, (object)dictionary, new PathString(), new FragmentString(), (LinkOptions)null);
        }
    }

    public class Boundaries
    {
        public int Start { get; set; }

        public int End { get; set; }
    }
}
