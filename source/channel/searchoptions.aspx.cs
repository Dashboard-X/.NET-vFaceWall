using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class channel_searchoptions : System.Web.UI.Page
{
    protected string NavigationClass = "";
    protected string BodyClass = "";
    protected string BodyRight = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // process website layout
        Process_Layout();

        if (!Page.IsPostBack)
        {
            set_bread_links();
        }

    }
    
    // Core script to handle website layout
    private void Process_Layout()
    {
        UGeneral ug = new UGeneral();
        int selected_layout_option = Site_Settings.NavigationSection;
        if (selected_layout_option == 2)
        {
            // three column layout
            navigation1.Visible = false; // advance navigation disable
            navigation_sm1.Visible = true; // first column simple navigation
            third_nav1.Visible = true; // third column navigation
        }
        else
        {
            // two column layout
            navigation1.Visible = true;
            navigation_sm1.Visible = false;
            third_nav1.Visible = false;
        }

        NavigationClass = ug.Return_Navigation_Class();
        BodyClass = ug.Return_Body_Class();
        BodyRight = ug.Return_RightNav_Class();

        // set meta information
        HtmlHead head = (HtmlHead)Page.Header;
        string meta_title = "Advance Channel Search";
        string meta_description = "You can search user profiles and channels through list of advance search options available on this page";

        MetaTagsBLL.META_StaticPage(this.Page, head, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.GetUrl("channel/searchoptions.aspx"));

    }

    private void set_bread_links()
    {
        string _rooturl = Config.GetUrl();
        StringBuilder str = new StringBuilder();
        str.Append("<li><a href=\"" + _rooturl + "\">" + Resources.vsk.home + "</a><span class=\"divider\">/</span> </li>\n");
        str.Append("<li><a href=\"" + _rooturl + "channel/\">" + Resources.vsk.channels + "</a><span class=\"divider\">/</span> </li>\n");
        str.Append("<li class=\"active\">Advance Search</li>\n");
        bread.InnerHtml = str.ToString();
    }
}