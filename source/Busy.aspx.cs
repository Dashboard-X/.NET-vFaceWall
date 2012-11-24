using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
public partial class Busy : System.Web.UI.Page
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
        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = "Error Occured";
        string meta_description = "Sorry for inconvinience you face, error occured while processing your request, please try later or consult with site administrator.";
        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("error.aspx"));
    }
}