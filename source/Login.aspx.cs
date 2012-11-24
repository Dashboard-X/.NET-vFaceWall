using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page
{
    private members _memberprocess = new members();
    protected string Redirect_Url = "";

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
        // globalization settings
        btn_login.Text = Resources.vsk.login;
        if (!Page.IsPostBack)
        {
            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "pchanged":
                        Config.ShowMessageV2(msg, "Password has been updated successfully", "Success!", 1);
                        break;
                    case "perror":
                        Config.ShowMessageV2(msg, "Failed to update password", "Error!", 0);
                        break;
                }
            }
            if (Request.Params["ReturnUrl"] != null)
            {
                this.Redirect_Url = Request.Params["ReturnUrl"].ToString();
            }
            if (Request.Params["error"] == "failed")
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0); // "Login failed, please enter correct user name &amp; password."
            }
        }

        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = Resources.vsk.meta_login_title;
        string meta_description = Resources.vsk.meta_login_description;
        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("login.aspx"));
    }
  
    protected void btn_login_Click1(object sender, EventArgs e)
    {

        // validate member
        // Update Password Validation Script
        if (lUserName.Text == "" || lPassword.Text == "")
        {
            Config.ShowMessageV2(msg, "Please enter username and password", "Error!", 0);
            return;
        }
        List<Member_Struct> _lst = members.Get_Hash_Password(lUserName.Text);
        if (_lst.Count == 0)
        {
            // No user account found based on username search
            Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
            return;
        }
        // check encrypted password
        if (_lst[0].Password.Length < 20)
        {
            // backward compatibility
            // check existing user passwords with old system
            if (!_memberprocess.Validate_Member(lUserName.Text, lPassword.Text, false))
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                return;
            }
        }
        else
        {
            // check encrypted password with user typed password
            bool matched = BCrypt.Net.BCrypt.Verify(lPassword.Text, _lst[0].Password);
            if (!matched)
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                return;
            }
        }
        // IP Address tracking and processing
        string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (BlockIPBLL.Validate_IP(ipaddress))
        {
            Response.Redirect(Config.GetUrl("IPBlocked.aspx"));
            return;
        }

        if (Site_Settings.Store_IPAddress)
        {
            // Store IP Address Log 
            User_IPLogBLL.Process_Ipaddress_Log(lUserName.Text, ipaddress);
        }

        // Update Last Login Activity of User
        members.Update_Value(lUserName.Text, "last_login", DateTime.Now);
        // member is validated
        FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(1, lUserName.Text, DateTime.Now, DateTime.Now.AddMonths(1), chk_remember.Checked, lUserName.Text.ToString(), FormsAuthentication.FormsCookiePath);
        string encTicket = FormsAuthentication.Encrypt(_ticket);
        HttpCookie _cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        if (chk_remember.Checked)
            _cookie.Expires = DateTime.Now.AddMonths(1);
        Response.Cookies.Add(_cookie);

       
        if (Request.Params["ReturnUrl"] != null && Request.Params["ReturnUrl"] != "")
            Response.Redirect(Request.Params["ReturnUrl"].ToString());
        else
            Response.Redirect(Config.GetUrl("myaccount/Default.aspx"));
    }
}
