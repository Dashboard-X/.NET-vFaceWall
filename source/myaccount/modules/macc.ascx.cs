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

public partial class myaccount_modules_macc : System.Web.UI.UserControl
{
    private members _memberprocess = new members();
     
    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }

    public bool isAdmin
    {
        get
        {
            if (ViewState["isAdmin"] != null)
                return (bool)ViewState["isAdmin"];
            else
                return false;
        }
        set
        {
            ViewState["isAdmin"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Globalization Text
        btn_change.Text = Resources.vsk.changepassword; // Change Password

        if (!Page.IsPostBack && this.UserName != "")
        {
            usr.InnerHtml = this.UserName;
        }
    }
   
    protected void btn_change_Click(object sender, EventArgs e)
    {
        // validate old password
        // validate member
        // Update Password Validation Script
        List<Member_Struct> _lst = members.Get_Hash_Password(this.UserName);
        if (_lst.Count == 0)
        {
            // No user account found based on username search
            Config.ShowMessageV2(msg, Resources.vsk.message_pass_01, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
            return;
        }
        // check encrypted password
        if (_lst[0].Password.Length < 20)
        {
            // backward compatibility
            // check existing user passwords with old system
            if (!_memberprocess.Validate_Member(this.UserName, txt_opassword.Text, false))
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_pass_01, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                return;
            }
        }
        else
        {
            // check encrypted password with user typed password
            bool matched = BCrypt.Net.BCrypt.Verify(txt_opassword.Text, _lst[0].Password);
            if (!matched)
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_pass_01, "Error!", 0);  // "Login failed, please enter correct user name &amp; password."
                return;
            }
        }
        // change password
        int BCRYPT_WORK_FACTOR = 10;
        string encrypted_password = BCrypt.Net.BCrypt.HashPassword(txt_npassword.Text, BCRYPT_WORK_FACTOR);
        members.Update_Value(this.UserName, "password", encrypted_password);

        MailTemplateProcess(this.UserName);
        if(this.isAdmin)
            Response.Redirect(Config.GetUrl("adm/sc/changepassword.aspx?status=updated"));
        else
            Response.Redirect(Config.GetUrl("myaccount/ManageAccount.aspx?status=updated"));
    }

    private void MailTemplateProcess(string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string emailaddress = members.Return_Value(username, "email");
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRPASSCHN");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
}
