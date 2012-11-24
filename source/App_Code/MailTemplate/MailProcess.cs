using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;

/// <summary>
/// Summary description for MailProcess
/// </summary>
public class MailProcess
{
	public MailProcess()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public static string Process2(string text, string keyword, string value)
    {
        return Regex.Replace(text, keyword, value);
    }

    public static void Send_Mail(string emailaddress, string subject, string content)
    {
        //// Sender Address
        string fromEmail = Config.Return_Email();
        string fromEmailDisplayName = Config.Return_EmailDisplayName();
        if (fromEmail == "")
            return;
        
        try
        {
            // if email option is enabled by administrator from control panel / configuration section
            if (Config.isEmailEnabled())
            {
                //// Send email
                EmailBLLC.SendMailMessage(fromEmail,fromEmailDisplayName, emailaddress, null, null, subject, content);
            }
            
        }
        catch (Exception ex)
        {
        }
    }

    // ******************************
    // Mail Templates
    // ******************************

    // send mail to user when admin approved user posted content
    public static void User_Mail_Content_Action(string username, string contenttype, string url, string status)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRCNTAPP");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                subject = MailProcess.Process2(subject, "\\[status\\]", status);
                subject = MailProcess.Process2(subject, "\\[contenttype\\]", contenttype);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[contenttype\\]", contenttype);
                contents = MailProcess.Process2(contents, "\\[url\\]", url);
                contents = MailProcess.Process2(contents, "\\[status\\]", status);
                string emailaddress = members.Return_Value(username, "email");
                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }

    // send mail to admin when new content added by user
    public static void Admin_New_Content_Added(string username, string contenttype, string url)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRAPRCNT");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                subject = MailProcess.Process2(subject, "\\[contenttype\\]", contenttype);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                subject = MailProcess.Process2(subject, "\\[contenttype\\]", contenttype);
                contents = MailProcess.Process2(contents, "\\[url\\]", url);

                string emailaddress = Site_Settings.Admin_Mail;
                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }

}
