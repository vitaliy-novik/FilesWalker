using System.Web.Mvc;

namespace WebSite.Helpers
{
    /// <summary>
    /// HtmlHelper extensions class for html elements and attributes
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Adds disable attribute to the element
        /// </summary>
        /// <param name="html">Initial element</param>
        /// <param name="isDisabled">Determs if element should be disabled</param>
        /// <returns>Disabled element</returns>
        public static MvcHtmlString Disable(this MvcHtmlString html, bool isDisabled)
        {
            if (isDisabled)
            {
                return html.AddAttribute("input", "disabled");
            }

            return html;
        }

        /// <summary>
        /// Adds attribute for element
        /// </summary>
        /// <param name="html">Initial element</param>
        /// <param name="tag">Target tag</param>
        /// <param name="attribute">Attribute name</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Initial element with added attribute</returns>
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