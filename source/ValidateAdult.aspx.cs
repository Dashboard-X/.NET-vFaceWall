using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class ValidateAdult : System.Web.UI.Page
{
    public string RedirectUrl
    {
        get
        {
            if (ViewState["RedirectUrl"] != null)
                return ViewState["RedirectUrl"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["RedirectUrl"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {       

        // globalization text settings
        SetGlobalizationText();

        if (!Page.IsPostBack)
        {
            if (Request.Params["surl"] != null)
            {
                this.RedirectUrl = Request.Params["surl"].ToString();
                lnk_cancel.NavigateUrl = Config.GetUrl("Default.aspx");
            }
            else
            {
                lnk_cancel.NavigateUrl =  Config.GetUrl("Default.aspx");
          
            }
        }
        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = "Warning: Adult Age Verification Required!";
        string meta_description = "Adult content access warning. In order to access adult content you must verify your age and agree that your age is above 18. Click cancel if you'r age is below 18";
        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl());
    }

    protected void lnk_enter_Click(object sender, EventArgs e)
    {
        // start adult session for this member
        Session["adultmember"] = "adult";

        // redirect him to same page from where he is directed here.
        if(this.RedirectUrl=="")
            Response.Redirect(Config.GetUrl());
        else
            Response.Redirect(this.RedirectUrl);
      
    }

    // globalization text settings
    private void SetGlobalizationText()
    {        
        lnk_enter.Text = Resources.vsk.enter;
        lnk_cancel.Text = Resources.vsk.cancel;
    }
}
