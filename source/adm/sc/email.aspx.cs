using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_email : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }


    protected void btn_add_Click(object sender, System.EventArgs e)
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }

        string value = txt_content.Value;
        if (txt_subject.Text == "")
        {
            Config.ShowMessageV2(msg, "Please enter subject", "Error!", 0);
            return;
        }

        List<Member_Struct> _lst = members.Fetch_User_UserNames();
        int counter = 0;
        if (_lst.Count > 0)
        {

            int i = 0;
            for (i = 0; i <= _lst.Count - 1; i++)
            {
                if (_lst[i].isAutoMail == 1)
                {
                    if (_lst[i].Email.Contains("@"))
                    {
                        MailTemplateProcess(_lst[i].Email, _lst[i].UserName);
                        counter++;
                    }
                }
            }
        }
       

        Config.ShowMessageV2(msg, "Mail has been sent to " + counter + " subscribers", "Success!", 1);
    }

    private void MailTemplateProcess(string emailaddress, string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string content = txt_content.Value;
            string subject = MailProcess.Process2(txt_subject.Text, "\\[username\\]", username);

            string contents = MailProcess.Process2(content, "\\[username\\]", username);
            contents = MailProcess.Process2(contents, "\\[email\\]", emailaddress);

            MailProcess.Send_Mail(emailaddress, subject, contents);
        }
    }
}