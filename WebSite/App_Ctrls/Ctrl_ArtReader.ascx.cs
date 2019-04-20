using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Ctrl_Ctrl_ArtReader : System.Web.UI.UserControl
{
    private int _artType;
    /// <summary>
    /// 文章类型
    /// </summary>
    public int ArtType
    {
        get { return _artType; }
        set { _artType = value; }
    }
    private int _artOrder;
    /// <summary>
    /// 文章编号
    /// </summary>
    public int ArtOrder
    {
        get { return _artOrder; }
        set { _artOrder = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string sqlQuery = string.Format("select * from T_ARTICLE where ArtOrder={0} and ArtType={1}",_artOrder,_artType);
        DataTable dt = SqlServerHooker.GetDataTable(sqlQuery);

        artTitle.InnerText = dt.Rows[0]["ArtTitle"].ToString();
        artDateTime.InnerText ="发表时间："+((DateTime)dt.Rows[0]["ArtDate"]).ToShortDateString();
        artContent.InnerHtml = dt.Rows[0]["ArtContent"].ToString();
    }
}