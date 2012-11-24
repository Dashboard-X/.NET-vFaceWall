using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class adm_sc_advertisement_Default : System.Web.UI.Page
{
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
        if (!Page.IsPostBack)
        {
            if(Request.Params["type"]!=null)
                drp_sorttype.SelectedValue = Request.Params["type"].ToString();
            // process advertisement for first time
            ProcessAdsBLL();
            // bind ads listing
            BindList();
        }
    }

    private void BindList()
    {

        DataSet ds = AdsBLL.Load_Ads(int.Parse(drp_sorttype.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            Pagination_Process.Process_Pagination(MyList, rptPages, lnk_Prev, lnk_Next, 40, this.PageNumber, ds.Tables[0].DefaultView);
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

    protected void rptPages_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnPage = (LinkButton)e.Item.FindControl("btnPage");
            if (int.Parse(btnPage.Text) == PageNumber)
            {
                btnPage.CssClass = "btn btn-danger btn-mini";
            }
            else
            {
                btnPage.CssClass = "btn btn-primary btn-mini";
            }
        }
    }

    protected void drp_filter_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindList();
    }

    protected void MyList_CancelCommand(object source, DataListCommandEventArgs e)
    {
        MyList.EditItemIndex = -1;
        BindList();
    }
    protected void MyList_EditCommand(object source, DataListCommandEventArgs e)
    {
        string cmd = ((LinkButton)e.CommandSource).CommandName;
        if(cmd=="Edit")
        {
            DataList dl = (DataList)source;
            dl.EditItemIndex = e.Item.ItemIndex;
        }
        BindList();
    }
    protected void MyList_ItemDataBound1(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl_type = (Label)e.Item.FindControl("lbl_type");
            int type =int.Parse(lbl_type.Text);
            if(type==0)
                lbl_type.Text = "Non Adult";
            else
                lbl_type.Text = "Adult";
          
        }
    }
    protected void MyList_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        int id = int.Parse(((Label)e.Item.FindControl("lbl_id")).Text);
        string value = ((TextBox)e.Item.FindControl("txt_name")).Text;
        string script = ((TextBox)e.Item.FindControl("txt_adscript")).Text;
        AdsBLL.Update_Script(id, value, script);
        // refresh ad update script
        HttpContext.Current.Application["a" + id] = AdsBLL.Fetch_Ad_Script(id);
        Config.ShowMessageV2(msg, "Record updated successfully", "Success!", 1);
        MyList.EditItemIndex = -1;
        BindList();
    }

  

    private void ProcessAdsBLL()
    {
        if(!AdsBLL.Count_Script())
        {
            int adult =1;
            int nonadult = 0;
            // add ads entry for first time
            AdsBLL.Add_Script("Horizontal - 728x90", "no script", nonadult);
            AdsBLL.Add_Script("Horizontal - 728x90", "no script", adult);
            AdsBLL.Add_Script("Horizontal - 468x60", "no script", nonadult);
            AdsBLL.Add_Script("Horizontal - 468x60", "no script", adult);
            AdsBLL.Add_Script("Horizontal - 234x60", "no script", nonadult);
            AdsBLL.Add_Script("Horizontal - 234x60", "no script", adult);
            AdsBLL.Add_Script("Vertical - 120x600", "no script", nonadult);
            AdsBLL.Add_Script("Vertical - 120x600", "no script", adult);
            AdsBLL.Add_Script("Vertical - 160x600", "no script", nonadult);
            AdsBLL.Add_Script("Vertical - 160x600", "no script", adult);
            AdsBLL.Add_Script("Vertical - 120x240", "no script", nonadult);
            AdsBLL.Add_Script("Vertical - 120x240", "no script", adult);
            AdsBLL.Add_Script("Square - 336x280", "no script", nonadult);
            AdsBLL.Add_Script("Square - 336x280", "no script", adult);
            AdsBLL.Add_Script("Square - 300x250", "no script", nonadult);
            AdsBLL.Add_Script("Square - 300x250", "no script", adult);
            AdsBLL.Add_Script("Square - 250x250", "no script", nonadult);
            AdsBLL.Add_Script("Square - 250x250", "no script", adult);
            AdsBLL.Add_Script("Square - 200x200", "no script", nonadult);
            AdsBLL.Add_Script("Square - 200x200", "no script", adult);
            AdsBLL.Add_Script("Square - 180x150", "no script", nonadult);
            AdsBLL.Add_Script("Square - 180x150", "no script", adult);
            AdsBLL.Add_Script("Square - 125x125", "no script", nonadult);
            AdsBLL.Add_Script("Square - 125x125", "no script", adult);
         }
    }
 
}
