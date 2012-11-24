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

public partial class adm_Default : System.Web.UI.Page
{
    private Administration _adminprocess = new Administration();
    private members _memberprocess = new members();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_validate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (UserName.Text == "" || Password.Text == "")
            {
                Config.ShowMessageV2(msg, "Please enter username and password", "Error!", 0);
                return;
            }
            List<Member_Struct> _lst = members.Get_Hash_Password(UserName.Text);
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
                if (!_memberprocess.Validate_Member(UserName.Text, Password.Text, false))
                {
                    Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                    return;
                }
            }
            else
            {
                // check encrypted password with user typed password
                bool matched = BCrypt.Net.BCrypt.Verify(Password.Text, _lst[0].Password);
                if (!matched)
                {
                    Config.ShowMessageV2(msg, Resources.vsk.message_06, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                    return;
                }
            }

            // IP Address tracking and processing
            string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
       
            // Read Access
            if (_adminprocess.isReadOnly(UserName.Text))
               Session["AccessLevel"] = "ReadOnly";
            else
               Session["AccessLevel"] = "Full";
            
            // Authorize the member, inorder to make it separate from front end authorization we use custom authorization for Administration.
            Session["AdminAuth"] = UserName.Text;

            // Store IP Address Log 
            User_IPLogBLL.Process_Ipaddress_Log(UserName.Text, ipaddress);
            // Update Last Login Activity of User
            members.Update_Value(UserName.Text, "last_login", DateTime.Now);
            Response.Redirect("sc/Default.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("Start with the help of God.");
    }
}
