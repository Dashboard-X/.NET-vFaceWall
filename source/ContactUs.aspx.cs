using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ContactUs : System.Web.UI.Page
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
        string meta_title = Resources.vsk.contactus;
        string meta_description = Resources.vsk.meta_contactus_description;

        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("contactus.aspx"));
      
    }


}
