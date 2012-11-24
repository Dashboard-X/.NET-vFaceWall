using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class user_EmailValidate : System.Web.UI.Page
{
    private members _members = new members();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["key"] != null && Request.Params["user"] != null && Request.Params["eml"] != null)
            {
                // validate key / username
                string key = Request.Params["key"].ToString();
                string user = Request.Params["user"].ToString();

                if (_members.Check_Key(user, key))
                {
                    // update email address
                    members.Update_Value(user, "Email",Request.Params["eml"].ToString());
                    // send mail
                    MailTemplateProcess(user);

                    Response.Redirect( Config.GetUrl("myaccount/EmailOptions.aspx?status=activated"));
                    return;
                }
                else
                {
                    // key validation failed
                    Response.Redirect(Config.GetUrl("myaccount/EmailOptions.aspx?status=notvalidate"));
                    return;
                }
            }
            else
            {
                // key validation failed
                Response.Redirect( Config.GetUrl("myaccount/EmailOptions.aspx?status=notvalidate"));
                return;
            }
        }
    }

    // notification for changing email options
    private void MailTemplateProcess(string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string emailaddress = members.Return_Value(username, "email");

            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USREMLCHNG");
            if (lst.Count > 0)
            {
             
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
}
