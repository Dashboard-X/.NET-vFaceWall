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

public partial class myaccount_modules_privacyo : System.Web.UI.UserControl
{
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Globalization Text
        btn_bsave.Text = Resources.vsk.savechanges; // Save Changes
        btn_save.Text = Resources.vsk.savechanges; // Save Changes

        if (!Page.IsPostBack && this.UserName != "")
        {
            // Load user privacy options
            Load_User_Privacy_Options(this.UserName);
        }

    }

    private void Load_User_Privacy_Options(string username)
    {
        List<Member_Struct> _list = User_SettingsBLL.Fetch_User_Privacy_Options(username);
        if (_list.Count > 0)
        {
            if (_list[0].Privacy_FMessages == 1)
                chk_allow.Checked = true;
            else
                chk_allow.Checked = false;
        }
    }

    private void Update_User_Private_Options(string username)
    {
        int privacy_fmessages = 1;
        if (chk_allow.Checked == false)
            privacy_fmessages = 0;
        User_SettingsBLL.Update_User_Privacy_Settings(username, privacy_fmessages);

        // send mail
        MailTemplateProcess(username);

        Response.Redirect( Config.GetUrl("myaccount/Privacy.aspx?status=updated"));

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if(this.UserName!="")
        Update_User_Private_Options(this.UserName);
    }
    protected void btn_bsave_Click(object sender, EventArgs e)
    {
        if(this.UserName!="")
        Update_User_Private_Options(this.UserName);
    }


    private void MailTemplateProcess(string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string emailaddress = members.Return_Value(username, "email");
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRPRIVCHN");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
    
}
