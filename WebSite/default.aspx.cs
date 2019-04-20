using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sqlSelect = @"select top(1) * from T_ARTICLE where ArtType=0 and ArtDate=(select max(ArtDate) from T_ARTICLE where ArtType=0)
union all
select top(1) * from T_ARTICLE where ArtType=1 and ArtDate=(select max(ArtDate) from T_ARTICLE where ArtType=1)
union all
select top(1) * from T_ARTICLE where ArtType=2 and ArtDate=(select max(ArtDate) from T_ARTICLE where ArtType=2)
union all
select top(1) * from T_ARTICLE where ArtType=3 and ArtDate=(select max(ArtDate) from T_ARTICLE where ArtType=3)
union all
select top(1) * from T_ARTICLE where ArtType=4 and ArtDate=(select max(ArtDate) from T_ARTICLE where ArtType=4)";
        DataTable dt = SqlServerHooker.GetDataTable(sqlSelect);
        lvLastedArticle.DataSource = dt;
        lvLastedArticle.DataBind();
    }
}