using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

public partial class modules_item_list_nav_ch : System.Web.UI.UserControl
{
    private int _activelink = 0;
    public int ActiveLink
    {
        set
        {
            _activelink = value;
        }
        get
        {
            return _activelink;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {  
            string recent_css="";
            string mv_css="";
            switch(this.ActiveLink)
            {
                case 0:
                    recent_css = " class=\"active\"";
                    break;
                case 1:
                    mv_css = " class=\"active\"";
                    break;
            }
            StringBuilder str =new StringBuilder();
            str.Append("<li" + recent_css + "><a href=\"" + Config.GetUrl("channel/") + "\">" + Resources.vsk.recentlyadded + "</a></li>\n"); 
            str.Append("<li" + mv_css + "><a href=\"" + Config.GetUrl("channel/mostviewed.aspx") + "\">" + Resources.vsk.mostviewed + "</a></li>\n");
            nav.InnerHtml = str.ToString();
        }
    }

}
