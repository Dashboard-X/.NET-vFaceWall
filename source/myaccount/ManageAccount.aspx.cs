using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class user_ManageAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            string username = "";
            if (!Page.User.Identity.IsAuthenticated)
            {
                string redirect_url = Config.GetUrl() + "user/ManageAccount.aspx";
                Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                return;
            }

            // Myaccount top navigation setting
            macc_menu1.ActiveIndex = 6; // Account Settings

            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                   
                    case "updated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_managed_01, "Success!", 1); //Account password has been updated successfully.
                        break;
                   
                }
            }

            username = Page.User.Identity.Name;

            // setup left navigation links
            left_nav1.ActiveIndex = 5;

            macc1.UserName = username;  
        }
    }

   
}
