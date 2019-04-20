using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class MyExtensions
    {
        public static MvcHtmlString GetLi(this HtmlHelper helper,string text,object htmlAttr)
        {
            TagBuilder liTag = new TagBuilder("li");

            liTag.SetInnerText(text);
            liTag.MergeAttributes(new RouteValueDictionary(htmlAttr));
            return new MvcHtmlString(liTag.ToString());
        }
    }
}