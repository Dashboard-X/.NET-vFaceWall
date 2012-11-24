using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text;

public partial class myaccount_modules_macc_menu : System.Web.UI.UserControl
{
    private int _activeindex = 6; // settings
    public int ActiveIndex
    {
        set { _activeindex = value; }
        get { return _activeindex; }
    }
   
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            macc.HRef = Config.GetUrl("myaccount/");
            Generate_MyAccount_Navigation();
        }
    }

    // Generate MyAccount Feature List
    private void Generate_MyAccount_Navigation()
    {
      
    }
}
