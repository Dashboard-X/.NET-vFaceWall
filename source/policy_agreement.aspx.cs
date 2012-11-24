using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class modules_policies_policy_agreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = Resources.vsk.meta_privacy_title;
        string meta_description = Resources.vsk.meta_privacy_description;
        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.Return_Current_Page());
    }
}
