using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Validate : System.Web.UI.Page
{
    private members _members = new members();

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
            if (Request.Params["key"] != null && Request.Params["user"] != null)
            {
                // validate key
                string username = Request.Params["user"].ToString();
                if (_members.Check_Key(username, Request.Params["key"].ToString()))
                {
                    // activate account
                    _members.Update_IsEnabled(username, 1);
                    // authorize user
                    FormsAuthentication.SetAuthCookie(username, false);
                    // store IP Address Log 
                    User_IPLogBLL.Process_Ipaddress_Log(username, Request.ServerVariables["REMOTE_ADDR"].ToString());
                    if (Config.GetMembershipAccountUpgradeRedirect() == 1)
                        Response.Redirect("myaccount/Packages.aspx?status=activated");
                    else
                        Response.Redirect(Config.GetUrl("myaccount/Default.aspx?status=activated"));
                    return;
                }
                else
                {
                    // key validation failed
                    Response.Redirect( Config.GetUrl("Validate.aspx?user=" + Request.Params["user"].ToString() + "&status=failed"));
                    return;
                 }

            }
            if (Request.Params["user"] != null)
            {
                lbl_user.Text = Request.Params["user"].ToString();
            }
            if (Request.Params["status"] != null)
            {
                if (Request.Params["status"] == "failed")
                {
                    Config.ShowMessageV2(msg, Resources.vsk.registrationkeyvalidationfailed, "Error!", 0);
                }

            }

        }
    }
}
