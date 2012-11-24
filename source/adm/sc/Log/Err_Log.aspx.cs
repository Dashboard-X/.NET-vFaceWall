using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class adm_sc_Log_Err_Log : System.Web.UI.Page
{
    private DictionaryBLL _val = new DictionaryBLL();
    private Pagination_Process _process_pagination = new Pagination_Process();

    //// Set Property for Pagination
    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
            {
                return (int)ViewState["PageNumber"];
            }
            else
            {
                return 1;
            }
        }
        set { ViewState["PageNumber"] = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        rptPages.ItemCommand += this.rptPages_ItemCommand;
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        HideMessage();
        if (!Page.IsPostBack)
        {
            if(Request.Params["status"] !=null)
            {
                 if(Request.Params["status"] == "delete")
                    ShowMessage("Error Log has been deleted");
            }
            BindList();
        }
    }

    //// Load Administrators
    private void BindList()
    {
        DataSet ds = ErrorLgBLL.Load_Log();
        if (ds.Tables[0].Rows.Count > 0)
        {
            Pagination_Process.Process_Pagination(MyList, rptPages, lnk_Prev, lnk_Next, 20, this.PageNumber, ds.Tables[0].DefaultView);
            //Process_Pagination(ds);
            pnl_main.Visible = true;
            pnl_norecord.Visible = false;
        }
        else
        {
            pnl_main.Visible = false;
            pnl_norecord.Visible = true;
        }
    }


    protected void lnk_Next_Click(object sender, System.EventArgs e)
    {
        PageNumber = PageNumber + 1;
        BindList();
    }

    protected void lnk_Prev_Click(object sender, System.EventArgs e)
    {
        PageNumber = PageNumber - 1;
        BindList();
    }

    protected void rptPages_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        LinkButton btnPage = (LinkButton)e.Item.FindControl("btnPage");
        PageNumber = int.Parse(btnPage.Text);
        BindList();
    }

    protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnPage = (LinkButton)e.Item.FindControl("btnPage");
            if (int.Parse(btnPage.Text) == PageNumber)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl li = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("nav");
                li.Attributes.Add("class", "active");
            }
        }
    }

    protected void btn_delete_Click(object sender, System.EventArgs e)
    {

        ErrorLgBLL.Delete_Log();
        BindList();
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


    protected void MyList_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        int id = int.Parse(((Label)e.Item.FindControl("lbl_id")).Text);
        ErrorLgBLL.Delete_Log(id);
        ShowMessage("Record has been deleted");
        BindList();
    }

    protected void MyList_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int id = int.Parse(((Label)e.Item.FindControl("lbl_id")).Text);
            HyperLink lnk_detail = (HyperLink)e.Item.FindControl("lnk_detail");
            lnk_detail.NavigateUrl =  Config.GetUrl("adm/sc/Log/ViewErrorDetail.aspx?id=" + id);
        }
    }
}
