using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class adm_sc_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            if (Request.Params["action_error"] != null)
            {
                Config.ShowMessageV2(msg, "All actions including add, edit, delete are disabled by Administrator. Admin section is on Read Only mode", "Security!", 0);
            }
        }
    }

}
