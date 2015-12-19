using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;


namespace Kunst.Helpers
{
    public static class ImageActionLinkHelper
    {
        public static IHtmlString ImageActionLink(this AjaxHelper helper,string linkText,string imageUrl,string altText,string height,string width, string actionName,string controller, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("width", width);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            var link = helper.ActionLink(linkText, actionName,controller, routeValues, ajaxOptions).ToHtmlString();
            var imageBuilder = builder.ToString(TagRenderMode.SelfClosing);
            var linkWithImage = link.Replace(linkText,imageBuilder);
            return MvcHtmlString.Create(linkWithImage);
           
        }
    }
}