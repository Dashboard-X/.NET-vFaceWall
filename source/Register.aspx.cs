using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Register : System.Web.UI.Page
{
    protected string Redirect_Url = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Site_Settings.Feature_MemberRegistration == 0)
        {
            mn.Visible = false;
            Config.ShowMessageV2(msg, Resources.vsk.registeration_message, "Error!", 0); // "User Registration Services Has Been Blocked By Site Administrator"
            
        }
        if (!Page.IsPostBack)
        {

            if (Request.Params["ReturnUrl"] != null)
            {
                this.Redirect_Url = Request.Params["ReturnUrl"].ToString();
            }
           
            // set meta information
            HtmlHead head = (HtmlHead)Page.Header;
            string meta_title = Resources.vsk.meta_register_title;
            string meta_description = Resources.vsk.meta_register_description;
            MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("register.aspx"));

        }
    }
}
