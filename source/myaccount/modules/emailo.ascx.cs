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

public partial class myaccount_modules_emailo : System.Web.UI.UserControl
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
        btn_bsave.Text = Resources.vsk.savechanges; // Save Changes
        btn_save.Text = Resources.vsk.savechanges; // Save Changes

        if (!Page.IsPostBack && this.UserName != "")
        {
            // load email options
            Load_Email_Options(this.UserName);
        }

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if(this.UserName !="")
         Update_Email_Options(this.UserName);
    }
    protected void btn_bsave_Click(object sender, EventArgs e)
    {
        if(this.UserName !="")
         Update_Email_Options(this.UserName);
    }

    private void Load_Email_Options(string username)
    {
        List<Member_Struct> _list = User_SettingsBLL.Fetch_User_Mail_Options(username);
        if (_list.Count > 0)
        {
            cemail.InnerHtml = _list[0].Email;
            if (_list[0].isAutoMail == 0)
            {
                chk_lst.Attributes.Add("class", "ui-state-disabled");
                chk_vcomment.Enabled = false;
                chk_ccomment.Enabled = false;
                chk_pmsg.Enabled = false;
                chk_finvite.Enabled = false;
                chk_subscribe.Enabled = false;

                r_nosend.Checked = true;
            }
            else
            {
                chk_lst.Attributes.Add("class", "item");
                r_send.Checked = true;
            }
            if (_list[0].Mail_VComment == 1)
                chk_vcomment.Checked = true;
            else
                chk_vcomment.Checked = false;

            if (_list[0].Mail_CComment == 1)
                chk_ccomment.Checked = true;
            else
                chk_ccomment.Checked = false;

            if (_list[0].Mail_PMessage == 1)
                chk_pmsg.Checked = true;
            else
                chk_pmsg.Checked = false;

            if (_list[0].Mail_FInvite == 1)
                chk_finvite.Checked = true;
            else
                chk_finvite.Checked = false;

            if (_list[0].Mail_Subscribe == 1)
                chk_subscribe.Checked = true;
            else
                chk_subscribe.Checked = false;


        }
    }

    private void Update_Email_Options(string username)
    {
        int isautomail = 1;
        int mail_vcomment = 1;
        int mail_ccomment = 1;
        int mail_pmessage = 1;
        int mail_finvite = 1;
        int mail_subscribe = 1;

        if (r_nosend.Checked)
            isautomail = 0;
        if (chk_vcomment.Checked == false)
            mail_vcomment = 0;
        if (chk_ccomment.Checked == false)
            mail_ccomment = 0;
        if (chk_pmsg.Checked == false)
            mail_pmessage = 0;
        if (chk_finvite.Checked == false)
            mail_finvite = 0;
        if (chk_subscribe.Checked == false)
            mail_subscribe = 0;
        User_SettingsBLL.Update_User_Mail_Settings(username, isautomail, mail_vcomment, mail_ccomment, mail_pmessage, mail_finvite, mail_subscribe);

        // send mail
        MailTemplateProcess_EmlOptions(username);

        Response.Redirect( Config.GetUrl("myaccount/EmailOptions.aspx?status=updated"));
    }
  
    protected void btn_send_Click(object sender, EventArgs e)
    {
        // validate email address and password.
        List<Member_Struct> _lst = members.Get_Hash_Password(this.UserName);
        if (_lst.Count == 0)
        {
            // No user account found based on username search
            Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_03, "Error!", 0);
            //Response.Redirect(Config.GetUrl("myaccount/EmailOptions.aspx?status=notvalidate"));
            return;
        }
        // check encrypted password
        if (_lst[0].Password.Length < 20)
        {
            // backward compatibility
            if (!members.Validate_Member_Email(cemail.InnerHtml, txt_pass.Text))
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_03, "Error!", 0);
                return;
            }
        }
        else
        {
            // check encrypted password with user typed password
            bool matched = BCrypt.Net.BCrypt.Verify(txt_pass.Text, _lst[0].Password);
            if (!matched)
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_emailoptions_03, "Error!", 0);
                return;
            }
        }
                
        string val_key = Guid.NewGuid().ToString().Substring(0, 10);
        // update user validation key
        members.Update_Value(this.UserName, "val_key", val_key);

        // send mail validation request on new email address
        MailTemplateProcess(txt_nemail.Text, this.UserName, val_key);

        Response.Redirect( Config.GetUrl("myaccount/EmailOptions.aspx?status=emlchg"));

    }
    
    // send email validation
    private void MailTemplateProcess(string emailaddress,string username,string key)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USREMLCREQ");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[key_url\\]", key);
                contents = MailProcess.Process2(contents, "\\[emailaddress\\]", emailaddress);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }

    // notification for changing email options
    private void MailTemplateProcess_EmlOptions(string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string emailaddress = members.Return_Value(username, "email");
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USREMLOPT");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
}
