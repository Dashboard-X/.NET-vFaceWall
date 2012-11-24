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

public partial class adm_sc_members_Default : System.Web.UI.Page
{
    private int PageSize = 40;

    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
                return (int)ViewState["PageNumber"];
            else
                return 1;
        }
        set
        {
            ViewState["PageNumber"] = value;
        }
    }

    public int TotalRecords
    {
        get
        {
            if (ViewState["TotalRecords"] != null)
                return (int)ViewState["TotalRecords"];
            else
                return 0;
        }
        set
        {
            ViewState["TotalRecords"] = value;
        }
    }

    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "all";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }
    
    public string Indicator
    {
        get
        {
            if (ViewState["Indicator"] != null)
                return ViewState["Indicator"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["Indicator"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        rptPages.ItemCommand += this.rptPages_ItemCommand;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Check whether admin section is in readonly mode.
            if (!Config.isAdminActionAllowed())
            {
                Response.Redirect( Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
                return;
            }
            Set_Sorting_Options(0);
            // Bind Member Channels

            // Bind Countries
            Load_Countries();
            // Set filter options
            if (Request.Params["f"] != null)
                drp_filter.SelectedValue = Request.Params["f"].ToString();
            if (Request.Params["t"] != null)
                drp_isenabled.SelectedValue = Request.Params["t"].ToString();
            if (Request.Params["type"] != null)
                drp_type.SelectedValue = Request.Params["type"].ToString();
            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "created":
                        Config.ShowMessageV2(msg, "New Account Created Successfully", "Success!", 1);
                        break;
                    case "deleted":
                        Config.ShowMessageV2(msg, "User Account Has Been Deleted Successfully", "Success!", 1);
                        break;
                }
            }
            // Bind List            
            BindList();
        }
    }

    // search
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        BindList();
    }


    private void BindList()
    {
        string search = "all";
        if (txt_search.Text != "")
            search = txt_search.Text;
        this.TotalRecords = members.Count_Members(search, drp_gender.SelectedValue, "0", drp_countries.SelectedValue, drp_isenabled.SelectedValue, drp_type.SelectedValue, Convert.ToInt32(drp_filter.SelectedValue)); 
        // no record found
        if (this.TotalRecords == 0)
        {
            pnl_main.Visible = false;
            pnl_norecord.Visible = true;
            lbl_records.Text = "0 records found";
            return;
        }

        List<Member_Struct> _lst = members.Load_Members(search, drp_gender.SelectedValue,"0", drp_countries.SelectedValue, drp_isenabled.SelectedValue, drp_type.SelectedValue, Convert.ToInt32(drp_filter.SelectedValue), ViewState["order"].ToString(), ViewState["direction"].ToString(),this.PageNumber,this.PageSize);
        if (_lst.Count>0)
        {
            if(this.TotalRecords>0)
                lbl_records.Text = _lst.Count + " records found";
            else
                lbl_records.Text = _lst.Count + " record found";

            Pagination_Process.Process_Pagination(MyList, rptPages, lnk_Prev, lnk_Next,this.PageSize, this.PageNumber, _lst);
            pnl_main.Visible = true;
            pnl_norecord.Visible = false;
        }
        else
        {
            pnl_main.Visible = false;
            pnl_norecord.Visible = true;
            lbl_records.Text = "0 records found";

        }
    }

    protected void MyList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            modules_item_chnl_itm itm = (modules_item_chnl_itm)e.Item.FindControl("chnl1");
            itm.PH = (Member_Struct)e.Item.DataItem;
            itm.ContentLayout = 1; // left / right side listing type
            itm.LeftWidth = 8;
            itm.RightWidth = 91;
            itm.NormalLinkCssClass = "xxmedium-text bold";
            itm.AltLinkCssClass = "normal-text light";
            itm.BoxCssClass = "bx_br_bt item_pad_4";
            itm.isHoverEffect = true;
            itm.ShowLocationInfo = true;
            itm.Height = 64;
            itm.Width = 64;
            itm.ShowViews = true;
            itm.ShowDate = true;
            itm.ShowDisabled = true;
        }
    }
    
    protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
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
    protected void lnk_Next_Click(object sender, EventArgs e)
    {
        PageNumber = PageNumber + 1;
        BindList();
    }
    protected void lnk_Prev_Click(object sender, EventArgs e)
    {
        PageNumber = PageNumber - 1;
        BindList();
    }
    protected void lnk_recent_Click(object sender, EventArgs e)
    {
        Set_Sorting_Options(0);
        PageNumber = 1;
        BindList();
    }
    protected void lnk_mostviewed_Click(object sender, EventArgs e)
    {
        Set_Sorting_Options(1);
        PageNumber = 1;
        BindList();
    }
    private void Set_Sorting_Options(int index)
    {
        switch (index)
        {
            case 0:
                ViewState["order"] = "register_date";
                ViewState["direction"] = "DESC";
                lnk_recent.CssClass = "active";

                lnk_mostviewed.CssClass = "";

                break;
            case 1:
                ViewState["order"] = "views";
                ViewState["direction"] = "DESC";
                lnk_recent.CssClass = "";

                lnk_mostviewed.CssClass = "active";

                break;

        }
    }
    

    private void Load_Countries()
    {
        drp_countries.DataSource = UtilityBLL.GetCountries(true);
        drp_countries.DataTextField = "Value";
        drp_countries.DataValueField = "Key";
        drp_countries.DataBind();
    }

    
}
