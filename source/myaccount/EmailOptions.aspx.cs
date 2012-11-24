using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Collections.Generic;

public partial class user_EmailOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = "";
        if (!Page.IsPostBack)
        {

            if (!Page.User.Identity.IsAuthenticated)
            {
                string redirect_url = Config.GetUrl() + "myaccount/EmailOptions.aspx";
                Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                return;
            }

            // Myaccount top navigation setting
            macc_menu1.ActiveIndex = 6; // Account Settings

            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "emlchg":
                        Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_01, "Success!", 1); // "Confirmation mail has been sent to your new email address."
                        break;
                    case "updated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_02, "Success!", 1); // Email Options have been updated successfully.
                        break;
                    case "notvalidate":
                        Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_03, "Error!", 0); // Email &amp; Password not validated.
                        break;
                    case "activated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_04, "Success!", 1); // Email Address has been changed successfully.
                        break;
                    
                }
            }

            username = Page.User.Identity.Name;

            left_nav1.ActiveIndex = 3;

            emailo1.UserName = username;
        }
    }

  
}
