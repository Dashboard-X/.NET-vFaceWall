using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class modules_tabs_simple : System.Web.UI.UserControl
{

    private string _rooturl = "";
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Generate_Left_Navigation();
    }
   
    // Left Navigation
    private void Generate_Left_Navigation()
    {
        string currenurl = Config.Return_Current_Page();
        StringBuilder str = new StringBuilder();
        _rooturl = Config.GetUrl();
        str.Append("<ul class=\"nav nav-pills\">\n");
           string channelactive = "";
        if (currenurl.Contains("/channel/") || currenurl.Contains(@"\channel\"))
            channelactive = "class=\"active\"";
        str.Append("<li><a href=\"" + _rooturl + "\">Home</a></li>\n");
        str.Append("<li " + channelactive + "><a href=\"" + _rooturl + "channel/\">Channels</a></li>\n");
        str.Append("</ul>\n");

        lsec.InnerHtml = str.ToString();
    }

}