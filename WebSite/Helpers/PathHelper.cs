using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;

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

            string[] folders = path.Split('/');
            StringBuilder linkString = new StringBuilder();
            StringBuilder href = new StringBuilder("/Folders");
            foreach (string folder in folders)
            {
                TagBuilder a = new TagBuilder("a");
                a.SetInnerText(folder);
                href.Append("/");
                href.Append(folder);
                a.Attributes.Add(new KeyValuePair<string, string>("href", href.ToString()));
                linkString.Append(a);
                linkString.Append("/");
            }

            return new MvcHtmlString(linkString.ToString());
        }
    }
}