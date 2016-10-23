using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    public static class FolderHelpers
    {
        public static MvcHtmlString FolderLink(this HtmlHelper html, DirectoryInfo directoryInfo, string path)
        {
            if (directoryInfo == null)
            {
                return new MvcHtmlString(string.Empty);
            }

            TagBuilder a = new TagBuilder("a");
            a.SetInnerText(directoryInfo.Name);
            a.Attributes.Add(new KeyValuePair<string, string>("href", string.Format("/Folders/{0}/{1}", path, directoryInfo.Name)));

            return new MvcHtmlString(a.ToString());
        }
    }
}