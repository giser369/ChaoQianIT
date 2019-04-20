using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using MVCExercise.App_Code.EntityClass;
using MVCExercise.App_Code.DBTool;

namespace MVCExercise.WebService
{
    /// <summary>
    /// StatictiService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class StatictiService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<string> GetVisitorInfo()
        {
            string sql = "select * from T_ARTICLE where ArtType=0";
            DataTable dt = SqlServerHooker.GetDataTable(sql);
            string msg = string.Format("当前发表了{0}篇GIS类文章", dt.Rows.Count);

            List<string> listInfo = new List<string>();
            listInfo.Add(Application["count"].ToString());
            if (Application["browsertype"] != null)
                listInfo.Add(Application["browsertype"].ToString());
            if (Application["ismobile"] != null)
                listInfo.Add(Application["ismobile"].ToString());
            if (Application["ipaddress"] != null)
                listInfo.Add(Application["ipaddress"].ToString());
            if (Application["visitpage"] != null)
                listInfo.Add(Application["visitpage"].ToString());

            return listInfo;
        }
        [WebMethod]
        public List<SiteVisitor> GetStatictisInfo()
        {
            string sqlSelect = "select * from T_VISITOR_INFO order by Date desc";
            DataTable dt = SqlServerHooker.GetDataTable(sqlSelect);
            List<SiteVisitor> listVisitor = new List<SiteVisitor>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SiteVisitor visitor = new SiteVisitor();
                visitor.ID = i + 1;
                visitor.Date = dt.Rows[i]["DATE"].ToString();
                visitor.BrowserType = dt.Rows[i]["BROWSERTYPE"].ToString();
                visitor.DeviceType = dt.Rows[i]["DEVICETYPE"].ToString();
                visitor.Location = dt.Rows[i]["LOCATION"].ToString();
                visitor.VisitPage = dt.Rows[i]["VISITPAGE"].ToString();
                listVisitor.Add(visitor);
            }
            return listVisitor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [WebMethod]
        public int Add(int i, int j)
        {
            return i + j;
        }
    }
}
