using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class adm_sc_Log_ViewErrorDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideMessage();
        if (!Page.IsPostBack)
        {
            load_log();
        }
    }

    private void load_log()
    {
        DataSet ds =ErrorLgBLL.Get_Log(int.Parse(Request.Params["id"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_id.Text = ds.Tables[0].Rows[0]["id"].ToString();
            lbl_message.Text = ds.Tables[0].Rows[0]["description"].ToString();
            lbl_url.Text = ds.Tables[0].Rows[0]["url"].ToString();
            lbl_stack.Text = ds.Tables[0].Rows[0]["stack_trace"].ToString();
            lbl_addeddate.Text = ds.Tables[0].Rows[0]["added_date"].ToString();
        }
    }
    protected void btn_add_Click(object sender, System.EventArgs e)
    {
        if (lbl_id.Text !="")
        {
            ErrorLgBLL.Delete_Log(int.Parse(lbl_id.Text));
        }
        Response.Redirect( Config.GetUrl("adm/sc/log/Default.aspx?status=delete"));
    }



    protected void btn_delete_Click(object sender, System.EventArgs e)
    {

        ErrorLgBLL.Delete_Log();
        ShowMessage("Error Log has been cleared out");

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
