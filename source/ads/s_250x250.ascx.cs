using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class ads_s_250x250 : System.Web.UI.UserControl
{

    private string _script = "";
    private bool _isadult = false;
    public string Script
    {
        set { _script = value; }
        get { return _script; }
    }
    public bool isAdult
    {
        set { _isadult = value; }
        get { return _isadult; }
    }

    protected void Page_Load(object send, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Config.isAdsEnabled())
            {
                // Square Ad - 250x250
                // Adult ID : 18
                // Non Adult ID : 17
                if (this.isAdult)
                {
                    // adult section
                    this.Script = AdsBLL.Return_Ad_Script(18);
                }
                else
                {
                    // non adult section
                    this.Script = AdsBLL.Return_Ad_Script(17);
                }
            }
        }

    }

   
}
