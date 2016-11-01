using System.Web.Mvc;

namespace WebSite.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Disable(this MvcHtmlString html, bool isDisabled)
        {
            if (isDisabled)
            {
                return html.AddAttribute("input", "disabled");
            }

            return html;
        }

        private static MvcHtmlString AddAttribute(this MvcHtmlString html, string tag, string attribute, string value = null)
        {
            var oldTag = string.Format("<{0}", tag);
            if (html.ToString().Contains(oldTag))
            {
                var replaceTag = string.Format("<{0} {1}=\"{2}\"", tag, attribute, value);

                return new MvcHtmlString(html.ToString().Replace(oldTag, replaceTag));
            }

            return html;
        }
    }


}