using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class adm_sc_members_modules_recent_login : System.Web.UI.UserControl
{

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
        if (!Page.IsPostBack && this.UserName != "")
        {
            BindList();
        }

    }

    private void BindList()
    {
        DataSet ds = User_IPLogBLL.load_ipaddress(this.UserName);
        if (ds.Tables[0].Rows.Count > 0)
        {
            MyList.DataSource = ds.Tables[0];
            MyList.DataBind();
        }
        else
        {
            widget.Visible = false;
        }
    }
}
