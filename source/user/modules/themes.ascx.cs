using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_modules_themes : System.Web.UI.UserControl
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

    public string Channel_Theme
    {
        get
        {
            if (ViewState["Channel_Theme"] != null)
                return ViewState["Channel_Theme"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["Channel_Theme"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            
        }
    }
    protected void btn_thm_save_Click(object sender, EventArgs e)
    {
        
    }
}