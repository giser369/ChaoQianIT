using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCExercise.App_Code.EntityClass
{
    /// <summary>
    /// SiteVisitor 的摘要说明
    /// </summary>
    public class SiteVisitor
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string DeviceType { get; set; }
        public string BrowserType { get; set; }
        public string Location { get; set; }
        public string VisitPage { get; set; }
    }
}