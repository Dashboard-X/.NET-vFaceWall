using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class IPBlocked : System.Web.UI.Page
{
    protected string NavigationClass = "chnl_left";
    protected string BodyClass = "chnl_right";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Site_Settings.NavigationSection == 1)
        {
            // right side navigation
            NavigationClass = "chnl_right_nav";
            BodyClass = "chnl_left_mn";
        }
        if (!Page.IsPostBack)
        {
             msg.InnerHtml = Resources.vsk.ipaddressblckedmessage.ToString().Replace("[message]",Request.ServerVariables["REMOTE_ADDR"].ToString());
            // set meta information
            HtmlHead head = (HtmlHead)Page.Header;
            string meta_title = Resources.vsk.ipaddressblocked;
            string meta_description = Resources.vsk.meta_ipaddress_description;
            MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("ipblocked.aspx"));
        }
    }
}
