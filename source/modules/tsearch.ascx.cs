using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_tsearch : System.Web.UI.UserControl
{

    public string search_path = "tags.aspx";
    
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (search.Text != "")
        {

            Response.Redirect(search_path + "?query=" + Server.UrlEncode(search.Text));
        }
    }
}