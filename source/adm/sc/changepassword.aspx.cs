using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class adm_sc_changepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["status"] != null)
            {
                if (Request.Params["status"] == "updated")
                    Config.ShowMessageV2(msg, "Password has been updated successfully", "Success!", 1);
            }
            //// Check whether admin section is in readonly mode.
            //if (!Config.isAdminActionAllowed())
            //{
            //    Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            //    return;
            //}
            macc1.UserName = Session["AdminAuth"].ToString();
            macc1.isAdmin = true;

        }
    }
}
