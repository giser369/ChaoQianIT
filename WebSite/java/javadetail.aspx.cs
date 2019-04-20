using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class coding_codingdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取文章
        string order = (Request["order"] == null) ? "1" : Request["order"];
        newsReader_1.ArtOrder = int.Parse(order);

        //获取文章类型
        string type = (Request["type"] == null) ? "1" : Request["type"];
        newsReader_1.ArtType = int.Parse(type);
    }
}