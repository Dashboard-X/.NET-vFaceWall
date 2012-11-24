using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    private members _memberprocess = new members();
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
        // globalization text
        btn_login.Text = Resources.vsk.submit;

        if(!Page.IsPostBack)
        {
            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "invalid":
                        Config.ShowMessageV2(msg, "Error validation key and username", "Error!", 0);
                        break;
                }
            }
            pnl_result.Visible = false;
        }

        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = Resources.vsk.meta_forgotpassword_title;
        string meta_description = Resources.vsk.meta_forgotpassword_description;
        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("forgotpassword.aspx"));

    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        // validate username
        if(!_memberprocess.Check_UserName(UserName.Text))
        {
            Config.ShowMessageV2(msg, Resources.vsk.message_07, "Error!", 0); // User Name not validated, please make sure you enter valid User Name
            return;
        }

        // get email address
        string emailaddress = members.Return_Value(UserName.Text, "email"); 
        string val_key = Guid.NewGuid().ToString().Substring(0, 10);
        // update val_key
        members.Update_Value(UserName.Text, "val_key", val_key);
        // send validation url in email
        string url = Config.GetUrl("updatepass.aspx?u=" + UserName.Text + "&k=" + val_key);
        MailTemplateProcess(emailaddress, UserName.Text, url);
        pnl_result.Visible = true;
        pnl_fpass.Visible = false;

        //updatepass.aspx?u=tommy01&k=6ea19763-c
      
    }

    private void MailTemplateProcess(string emailaddress, string username, string url)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("FORPASS");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[url\\]", url);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }


}
