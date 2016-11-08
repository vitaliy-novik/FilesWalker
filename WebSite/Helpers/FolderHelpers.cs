using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    /// <summary>
    /// HtmlHelper extensions class for Directories
    /// </summary>
    public static class FolderHelpers
    {
        /// <summary>
        /// Returns link for directory
        /// </summary>
        /// <param name="html">HtmlHelper instance</param>
        /// <param name="directoryInfo">Contains info about directory</param>
        /// <param name="path">Path to the directory</param>
        /// <returns>Directory link</returns>
        public static MvcHtmlString FolderLink(this HtmlHelper html, DirectoryInfo directoryInfo, string path)
        {
            if (directoryInfo == null)
            {
                return new MvcHtmlString(string.Empty);
            }

            TagBuilder a = new TagBuilder("a");
            TagBuilder span = new TagBuilder("span");
            if (directoryInfo.Parent == null)
            {
                span.AddCssClass("glyphicon glyphicon-cd");
            }
            else
            {
                span.AddCssClass("glyphicon glyphicon-folder-open");
            }

            a.InnerHtml = span.ToString() + " " + directoryInfo.Name;
            StringBuilder href = new StringBuilder("/Folders/");
            if (!string.IsNullOrEmpty(path))
            {
                href.Append(path);
                if (!href.ToString().EndsWith("/"))
                {
                    href.Append("/");
                }
            }

            href.Append(directoryInfo.Name);
            a.Attributes.Add(new KeyValuePair<string, string>("href", href.ToString().Replace(":", "")));

            return new MvcHtmlString(a.ToString());
        }
    }
}