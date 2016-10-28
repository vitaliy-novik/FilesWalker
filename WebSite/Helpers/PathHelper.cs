using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace WebSite.Helpers
{
    public static class PathHelper
    {
        public static MvcHtmlString PathLink(this HtmlHelper html, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new MvcHtmlString(string.Empty);
            }

            TagBuilder ol = new TagBuilder("ol");
            ol.AddCssClass("breadcrumb");
            string[] folders = path.Split('/').Where(f => f != string.Empty).ToArray();
            StringBuilder href = new StringBuilder("/Folders");
            foreach (string folder in folders)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
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