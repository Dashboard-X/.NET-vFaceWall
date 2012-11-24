using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class updatepass : System.Web.UI.Page
{
    protected string NavigationClass = "chnl_left";
    protected string BodyClass = "chnl_right";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Site_Settings.NavigationSection == 1)
            {
                // right side navigation
                NavigationClass = "chnl_right_nav";
                BodyClass = "chnl_left_mn";
            }    

            btn_login.Text = Resources.vsk.update;

            if (Request.Params["u"] != null && Request.Params["k"] != null)
            {
                // validate user info
                members _members= new members();
                string username = Server.UrlDecode(Request.Params["u"].ToString());
                string key =Server.UrlDecode(Request.Params["k"].ToString());

                if (!_members.Check_Key(username, key))
                {
                    Response.Redirect(Config.GetUrl("forgotpassword.aspx?status=invalid"));
                    return;
                }

                string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (BlockIPBLL.Validate_IP(ipaddress))
                {
                    Response.Redirect(Config.GetUrl("IPBlocked.aspx"));
                    return;
                }

                lbl_username.Text = username;
            }
            else
            {
                Response.Redirect(Config.GetUrl("forgotpassword.aspx"));
            }
        }
    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (lbl_username.Text.Length > 3)
        {
            // change password
            int BCRYPT_WORK_FACTOR = 10;
            string encrypted_password = BCrypt.Net.BCrypt.HashPassword(Password.Text, BCRYPT_WORK_FACTOR);
            members.Update_Value(lbl_username.Text, "password", encrypted_password);
            Response.Redirect(Config.GetUrl("login.aspx?status=pchanged"));
        }
        else
        {
            Response.Redirect(Config.GetUrl("login.aspx?status=perror"));
        }



    }
}