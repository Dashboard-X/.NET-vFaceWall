using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_modules_nav_v2 : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["AdminAuth"] == null || Session["AdminAuth"] == "")
            {
                Response.Redirect(Config.GetUrl("adm/"));
                return;
            }

            myprofile.HRef = Config.GetUrl("adm/sc/members/MemberDetail.aspx?user=" + Session["AdminAuth"].ToString());

            Toggle_UI();


        }
    }

    // Toggle on | off features
    private void Toggle_UI()
    {
       

    }
}