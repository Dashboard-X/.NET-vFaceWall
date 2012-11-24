using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;

public partial class _Default : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            // set meta information
            HtmlHead head = (HtmlHead)Page.Header;
            string meta_title = ".NET vFaceWall Script"; // Resources.vsk.meta_home_title;
            string meta_description = "Complete script for generating next generation facebook style wall for sharing contents and post using asp.net applications";

            MetaTagsBLL.META_StaticPage(this.Page, head,meta_title,meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl());
        }
   
    }
}