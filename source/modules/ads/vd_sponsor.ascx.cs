using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class modules_ads_vd_sponsor : System.Web.UI.UserControl
{
    // CSS Properties
    public int BoxTemplate = 0; // Default,
    public string BoxCssClass = ""; // main box css style
    public string HeadingCssClass = ""; // main box heading css style

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.BoxCssClass == "")
            PrepareBoxTemplates();

        if (!Page.IsPostBack)
        {
            if (!Config.isAdsEnabled())
            {
                widget.Visible = false;
            }
        }
    }

    private void PrepareBoxTemplates()
    {
        this.BoxCssClass = UGeneral.Return_Box_Css(this.BoxTemplate);
        this.HeadingCssClass = UGeneral.Return_Heading_Css(this.BoxTemplate);
    }
}
