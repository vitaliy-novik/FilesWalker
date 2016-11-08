using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    /// <summary>
    /// HtmlHelper extensions class for working with file system paths
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Returns breadcrumb element with directories links
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="path">File system path string</param>
        /// <returns>Breadcrumb html element</returns>
        public static MvcHtmlString PathLink(this HtmlHelper html, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new MvcHtmlString(string.Empty);
            }

            TagBuilder ol = new TagBuilder("ol");
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");

            a.SetInnerText(Environment.MachineName);
            a.Attributes.Add(new KeyValuePair<string, string>("href", "/Folders/Index"));
            li.InnerHtml = a.ToString();
            ol.AddCssClass("breadcrumb");
            ol.InnerHtml += li.ToString();

            string[] folders = path.Split('/').Where(f => f != string.Empty).ToArray();
            StringBuilder href = new StringBuilder("/Folders");
            foreach (string folder in folders)
            {
                li = new TagBuilder("li");
                a = new TagBuilder("a");
                a.SetInnerText(folder);
                href.Append("/");
                href.Append(folder);
                href.Append("/");
                a.Attributes.Add(new KeyValuePair<string, string>("href", href.ToString().Replace(":", "")));
                li.InnerHtml = a.ToString();
                ol.InnerHtml += li.ToString();
            }

            return new MvcHtmlString(ol.ToString());
        }
    }
}