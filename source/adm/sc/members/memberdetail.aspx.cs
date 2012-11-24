using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class adm_sc_memberdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Params["user"] != null)
            {
                if (Session["AdminAuth"] == null)
                {
                    Response.Redirect(Config.GetUrl("adm/"),true);
                    return;
                }
                string _username = Request.Params["user"].ToString();
                if (_username == Session["AdminAuth"].ToString())
                   Member_mgt1.Visible = false;
                else
                   Member_mgt1.UserName = _username;
                
                profile1.UserName = _username;
                
            }
            else
            {
                // no record exist
                main.Visible = false;
            }
        }
    }

}
