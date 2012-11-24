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

public partial class user_Profile : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string username = "";
            if (!Page.User.Identity.IsAuthenticated)
            {
                string redirect_url = Config.GetUrl() + "myaccount/Profile.aspx";
                Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                return;
            }
            username = Page.User.Identity.Name;

            // Myaccount top navigation setting
            macc_menu1.ActiveIndex = 6; 

            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "invalidformat":
                        Config.ShowMessageV2(msg,Resources.vsk.message_profile_01, "Error!", 0); // "Invalid image format, please upload proper image"
                        break;
                    case "pupdated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_profile_02, "Success!", 1); // Photo has been changed successfully
                        break;
                    case "updated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_profile_03, "Success!", 1); //"Profile has been updated successfully"
                        break;
                    case "perror":
                        Config.ShowMessageV2(msg, Resources.vsk.message_profile_04 , "Error!", 0); //"Failed to replace user photo"
                        break;
                }
            }

            left_nav1.ActiveIndex = 1;
           
            // profile user control setup
            profile1.UserName = username;
        }
    }
   
}
