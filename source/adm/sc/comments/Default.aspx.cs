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
using System.Text;

public partial class adm_sc_comments_Default : System.Web.UI.Page
{

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

    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
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

    protected string EHandler = "";
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        rptPages.ItemCommand += this.rptPages_ItemCommand;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            EHandler = Config.GetUrl("adm/sc/comments/edit.ashx");
            // Check whether admin section is in readonly mode.
            if (!Config.isAdminActionAllowed())
            {
                Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
                return;
            }

            // set actions
            if (Request.Params["user"] != null)
            {
                UserName = Request.Params["user"].ToString();
                src.Visible = false;
            }

            if (Request.Params["f"] != null)
                drp_filter.SelectedValue = Request.Params["f"].ToString();
            if (Request.Params["t"] != null)
                drp_isenable.SelectedValue = Request.Params["t"].ToString();
            if (Request.Params["anp"] != null)
                drp_approve.SelectedValue = Request.Params["anp"].ToString();
            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {                 
                    case "deleted":
                        Config.ShowMessageV2(msg, "Comments has been deleted successfully", "Success!", 1);
                        break;
                }
            }
            BindList();
        }
    }


    private void BindList()
    {
        int pagesize = Convert.ToInt32(drp_pagesize.SelectedValue);
        string search = "";
        if (txt_search.Text != "")
            search = txt_search.Text;
      
        //if (this.TotalRecords == 0)
        this.TotalRecords =CommentsBLL.Count_Comments(search,this.UserName,drp_type.SelectedValue, Convert.ToInt32(drp_isenable.SelectedValue), Convert.ToInt32(drp_approve.SelectedValue),Convert.ToInt32(drp_filter.SelectedValue));

        if (this.TotalRecords == 0)
        {
            // no record exist
            pnl_main.Visible = false;
            pnl_norecord.Visible = true;
            lbl_records.InnerHtml = "<h4>0 records found</h4>";
            return;
        }

        List<Comment_Struct> list = CommentsBLL.Load_Comments(search,this.UserName,drp_type.SelectedValue, Convert.ToInt32(drp_isenable.SelectedValue), Convert.ToInt32(drp_approve.SelectedValue), Convert.ToInt32(drp_filter.SelectedValue), drp_order.SelectedValue, this.PageNumber, pagesize);
        if (list.Count > 0)
        {
            ViewState["script_value"] = "";
            //int count = int.Parse(list.Count.ToString());
            if (this.TotalRecords > 1)
                lbl_records.InnerHtml = "<h4>" + this.TotalRecords + " records found</h4>";
            else
                lbl_records.InnerHtml = this.TotalRecords + " record found";
            // setup pagination
            Pagination_Process.Process_Pagination(MyList, rptPages, lnk_Prev, lnk_Next, pagesize, this.PageNumber, list, this.TotalRecords);

            pnl_main.Visible = true;
            pnl_norecord.Visible = false;
        }
        else
        {
            pnl_main.Visible = false;
            pnl_norecord.Visible = true;
            lbl_records.InnerHtml = "<h4>0 records found</h4>";

        }

    }

    protected void MyList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // Action section
            modules_comments_cmtitem itm = (modules_comments_cmtitem)e.Item.FindControl("cmtitem1");
            itm.Cmt = (Comment_Struct)e.Item.DataItem;
            itm.ShowAuthorImage = false;
            itm.ShowDisabled = true;
            itm.ShowApproved = true;
            itm.TemplateID = 0;
            Label gid = (Label)e.Item.FindControl("lbl_id");
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
                HtmlGenericControl li = (HtmlGenericControl)e.Item.FindControl("nav");
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

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        BindList();
    }

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        UpdateApproved(1);
    }
    protected void btn_disable_Click(object sender, EventArgs e)
    {
        UpdateDisabled(0);
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Delete();
    }
    

    protected void btn_enabled_Click(object sender, EventArgs e)
    {
        UpdateDisabled(1);
    }
    
    
    protected void Delete()
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }
        int index;
        bool flg = false;
        for (index = 0; index < MyList.Items.Count; index++)
        {
            CheckBox select_chk;
            select_chk = (CheckBox)MyList.Items[index].FindControl("chk_inbox");
            if (select_chk.Checked)
            {
                long id = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_id")).Text);
                long contentid = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_contentid")).Text);
                int type = Convert.ToInt32(((Label)MyList.Items[index].FindControl("lbl_type")).Text);
                string profileid = ((Label)MyList.Items[index].FindControl("lbl_profileid")).Text;
                int TotalComments = CommentsBLL.Fetch_Total_Comments(contentid,profileid,type);
                CommentsBLL.Delete(id, type, contentid, profileid, TotalComments);
                if (flg == false)
                    flg = true;
            }

        }
        if (flg == false)
            Config.ShowMessageV2(msg, "please select record", "Info!", 2);
        else
            Config.ShowMessageV2(msg, "selected records have been deleted successfully", "Success!", 1);

        BindList();
    }

    protected void UpdateDisabled(int status)
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }
        int index;
        bool flg = false;
        for (index = 0; index < MyList.Items.Count; index++)
        {
            CheckBox select_chk;
            select_chk = (CheckBox)MyList.Items[index].FindControl("chk_inbox");
            if (select_chk.Checked)
            {
                long id = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_id")).Text);
                long contentid = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_contentid")).Text);
                int type = Convert.ToInt32(((Label)MyList.Items[index].FindControl("lbl_type")).Text);
                string profileid = ((Label)MyList.Items[index].FindControl("lbl_profileid")).Text;
                int OldValue = Convert.ToInt32(((Label)MyList.Items[index].FindControl("lbl_isenabled")).Text);
                CommentsBLL.Update_Action(id, OldValue, status, contentid, profileid, type, "isenabled", true);
                if (flg == false)
                    flg = true;
            }

        }
        if (flg == false)
            Config.ShowMessageV2(msg, "please select record", "Info!", 2);
        else
        {
            if (status == 1)
                Config.ShowMessageV2(msg, "selected records have been enabled successfully", "Success!", 1);
            else
                Config.ShowMessageV2(msg, "selected records have been disabled successfully", "Success!", 1);
        }

        BindList();
    }

    protected void UpdateApproved(int status)
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }
        int index;
        bool flg = false;
        for (index = 0; index < MyList.Items.Count; index++)
        {
            CheckBox select_chk;
            select_chk = (CheckBox)MyList.Items[index].FindControl("chk_inbox");
            if (select_chk.Checked)
            {
                long id = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_id")).Text);
                long contentid = Convert.ToInt64(((Label)MyList.Items[index].FindControl("lbl_contentid")).Text);
                int type = Convert.ToInt32(((Label)MyList.Items[index].FindControl("lbl_type")).Text);
                string profileid = ((Label)MyList.Items[index].FindControl("lbl_profileid")).Text;
                int OldValue = Convert.ToInt32(((Label)MyList.Items[index].FindControl("lbl_isapproved")).Text);
                CommentsBLL.Update_Action(id, OldValue, status, contentid, profileid, type, "isapproved", true);

                if (flg == false)
                    flg = true;
            }

        }
        if (flg == false)
            Config.ShowMessageV2(msg, "please select record", "Info!", 2);
        else
        {
            if (status == 1)
                Config.ShowMessageV2(msg, "selected records have been approved successfully", "Success!", 1);
            else
                Config.ShowMessageV2(msg, "selected records have been dis approved successfully", "Success!", 1);
        }

        BindList();
    }

    protected void drp_pagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();
    }
}