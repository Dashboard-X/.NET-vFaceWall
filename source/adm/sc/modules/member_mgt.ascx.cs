using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

public partial class admin_secure_modules_member_mgt : System.Web.UI.UserControl
{

    private members _memberdalc = new members();
   
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
     

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["status"] != null)
            {
                string status = Request.Params["status"].ToString();
                switch (status)
                {
                    case "doblock":
                        Disable_User();
                        break;
                }
            }

            Set_Options(this.UserName);

          
        }
    }
    
    private void Set_Options(string username)
    {
        List<Member_Struct> _lst = members.Fetch_User_Status_Info(this.UserName);
        if (_lst.Count > 0)
        {
            if (_lst[0].isEnabled == 1)
            {
                lnk_enable.Visible = false;
                lnk_disable.Visible = true;
                lbl_status.Text = "Enabled";
                lbl_status.CssClass = "mini-text green bold";
            }
            else
            {
                 lnk_enable.Visible = true;
                 lnk_disable.Visible = false;
                 lbl_status.Text = "Blocked";
                 lbl_status.CssClass = "mini-text red bold";
            }

            drp_type.SelectedValue = _lst[0].Type.ToString();
 
        }
    }
 
  
    protected void lnk_deletevideo_Click(object sender, EventArgs e)
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect( Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }

        // Script here required to delete all member contents including videos, subscriptions, friends and more.

        // delete member records
        members.Delete(this.UserName);

        Config.ShowMessageV2(msg, "User account has been deleted", "Success!",1);

    }
    protected void lnk_enable_Click(object sender, EventArgs e)
    {
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect( Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }
        _memberdalc.Update_IsEnabled(this.UserName, 1);
        Set_Options(this.UserName);
        Config.ShowMessageV2(msg, "User account has been enabled", "Success!", 1);
    }
    protected void lnk_disable_Click(object sender, EventArgs e)
    {
        Disable_User();
    }

    protected void btn_upd_Click(object sender, EventArgs e)
    {
        members.Update_Value(UserName, "type", drp_type.SelectedValue);
        Config.ShowMessageV2(msg, "User Type has been updated", "Success!", 1);
    }

    private void Disable_User()
    {
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }
        _memberdalc.Update_IsEnabled(this.UserName, 0);
        Set_Options(this.UserName);
        Config.ShowMessageV2(msg, "User account has been disabled", "Success!", 1);
    }

    protected void btn_rupd_Click(object sender, EventArgs e)
    {
        members.Update_User_Role(this.UserName, 0);
        Config.ShowMessageV2(msg, "User Role has been updated", "Success!", 1);
    }
}