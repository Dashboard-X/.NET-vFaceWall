using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class adm_sc_mail_Detail : System.Web.UI.Page
{
  
    public int TID
    {
        get
        {
            if (ViewState["TID"] != null)
            {
                return (int)ViewState["TID"];
            }
            else
            {
                return 1;
            }
        }
        set { ViewState["TID"] = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        HideMessage();
        if (!Page.IsPostBack)
        {
            if ((Request.Params["id"] != null))
            {
                this.TID = int.Parse(Request.Params["id"]);
                LoadData(this.TID);

            }
        }
    }

    private void LoadData(int id)
    {
        DataSet ds = MailTemplateBLL.Get_Value(id);
        if (ds.Tables[0].Rows.Count > 0)
        {
          
            lbl_key.Text = ds.Tables[0].Rows[0]["templatekey"].ToString();
            lbl_tags.Text = ds.Tables[0].Rows[0]["tags"].ToString();
            lbl_type.Text = ds.Tables[0].Rows[0]["type"].ToString();
            lbl_description.Text = ds.Tables[0].Rows[0]["description"].ToString();
            lbl_subjecttags.Text = ds.Tables[0].Rows[0]["subjecttags"].ToString();
            txt_subject.Text = ds.Tables[0].Rows[0]["subject"].ToString();
            txt_content.Value = ds.Tables[0].Rows[0]["contents"].ToString();
        }
    }

    protected void btn_add_Click(object sender, System.EventArgs e)
    {

        string value = txt_content.Value;
        if (string.IsNullOrEmpty(value))
        {
            ShowMessage("Insert mail template content");
            return;
        }
        if (string.IsNullOrEmpty(txt_subject.Text))
        {
            ShowMessage("Insert subject of mail template");
            return;
        }
        MailTemplateBLL.Update_Record(this.TID, txt_subject.Text, value);
       
        Response.Redirect( Config.GetUrl("adm/sc/mail/Default.aspx?status=updated"));
    }


    private void ShowMessage(string message)
    {
        pnl_error.Visible = true;
        lbl_error.Text = message;
    }

    private void HideMessage()
    {
        pnl_error.Visible = false;
        lbl_error.Text = "";
    }



}
