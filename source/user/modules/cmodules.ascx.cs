using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_modules_cmodules : System.Web.UI.UserControl
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

    public int Channel_isFriends
    {
        get
        {
            if (ViewState["isFriends"] != null)
                return (int)ViewState["isFriends"];
            else
                return 1;
        }
        set
        {
            ViewState["isFriends"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
        }
    }
   
}