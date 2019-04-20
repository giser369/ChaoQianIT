<%@ Application Language="C#" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        Application["count"] = 0;
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }
    public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.Add(new System.Web.Routing.Route
        (
             "articles/{id}",
             new System.Web.Routing.PageRouteHandler("~/AllArticles/Coding/CodingArticles.aspx")
        ));
    }
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码
        Application.Lock();

        Application["count"] = (int)Application["count"] + 1; //访问者个数+1
        System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
        string isMobile = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice ? "移动设备" : "电脑";
        Application["ismobile"] = isMobile;
        Application["browsertype"] = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).Type;
        Application["ipaddress"] = Request.UserHostAddress; //客户端ip地址
        Application["visitpage"] = Request.RawUrl; //访问的页面原始URL

        //根据IP地址获取位置信息
        string url = "http://api.ip138.com/query/?ip=" + Request.UserHostAddress + "&datatype=jsonp&callback=find&token=ad56856c0d06a1d1e3a12788b6948b88";
        string strJson = WebServiceJson.RequestWebService(url, "");
        strJson = strJson.Substring(5, strJson.Length - 6);
        var tempEntity = new { ret = string.Empty, ip = string.Empty, data = new List<string>() };
        tempEntity = JsonHelper.DeserializeAnonymousType(strJson, tempEntity);

        //将位置信息存储到数据库中
        if (tempEntity.data != null)
        {
            string location = tempEntity.data[0] + "," + tempEntity.data[1] + "," + tempEntity.data[2] + "," + tempEntity.data[3];
            string addSql = string.Format("insert into T_VISITOR_INFO(ID,DATE,DEVICETYPE,BROWSERTYPE,LOCATION,VISITPAGE) values('{0}','{1}','{2}','{3}','{4}','{5}')", Guid.NewGuid(),
                DateTime.Now.ToString(), isMobile, ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).Type, location, Request.RawUrl);
            bool isSuc = SqlServerHooker.InsertDataToTable(addSql);
        }
        
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        Application.Lock();
        
        Application["count"] = (int)Application["count"] -1;
        
        Application.UnLock();
    } 
</script>
