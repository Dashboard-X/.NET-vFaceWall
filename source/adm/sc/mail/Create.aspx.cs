using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_mail_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
       

    protected void btn_add_Click(object sender, System.EventArgs e)
    {

        string value = txt_content.Value;
        if(txt_key.Text=="")
        {
            Config.ShowMessageV2(msg, "Please enter valid template key", "Error!", 0);
            return;
        }

        if(MailTemplateBLL.Validate_TemplateKey(txt_key.Text))
        {
            Config.ShowMessageV2(msg, "Template key already exist", "Error!", 0);
            return;
        }

        string description = txt_description.Text;
        if(description.Length>100)
            description = description.Substring(0,95);
        MailTemplateBLL.Add(txt_key.Text, description, txt_subjecttags.Text, txt_contenttags.Text, txt_subject.Text, value, drp_sorttype.SelectedValue);

        Response.Redirect(Config.GetUrl("adm/sc/mail/Default.aspx?status=added"));
    }

}