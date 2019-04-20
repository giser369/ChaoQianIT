using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_MasterPage_MstPage_L0 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void menuPreRender(object sender, EventArgs e)
    {
        try
        {
            MenuItem mi = menu_mainNav.FindItem(SiteMap.CurrentNode.ResourceKey);
            mi.Selected = true;
        }
        catch
        {
        }
    }
}
