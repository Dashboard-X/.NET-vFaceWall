using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class channel_Search : System.Web.UI.Page
{
    protected string NavigationClass = "";
    protected string BodyClass = "";
    protected string BodyRight = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        set_bread_links();
        // process website layout
        Process_Layout();

        if (!Page.IsPostBack)
        {
            string term = "";
            if (Request.Params["query"] != null)
            {
                term = UtilityBLL.CleanSearchTerm(Server.UrlDecode(Request.Params["query"].ToString()).Trim());
            }
            if (Request.Params["atype"] != null)
                channel_list1.AccountType = Request.Params["atype"].ToString();

            if (Request.Params["cnt"] != null)
                channel_list1.Country = Request.Params["cnt"].ToString();

            if (Request.Params["gender"] != null)
                channel_list1.Gender = Request.Params["gender"].ToString();

            if (Request.Params["ponly"] != null)
            {
                if (Request.Params["ponly"].ToString() == "true")
                    channel_list1.HavePhoto = true;
            }
           
            if (Request.Params["o"] != null)
                channel_list1.Order = Server.UrlDecode(Request.Params["o"].ToString());
            else
                channel_list1.Order = "u.register_date desc";

            channel_list1.isNavigation = false;

            channel_list1.HeadingTitle = "Search Results";

            channel_list1.AdvList = true; // advance listing

            channel_list1.isCache = false;

            channel_list1.PageSize = 10;
            // generate pagination link if advance search selected
            if (Request.Params["o"] != null)
            {
                StringBuilder str = new StringBuilder();
                str.Append("?o=" + channel_list1.Order);
                if (Request.Params["query"] != null)
                    str.Append("&query=" + Request.Params["query"].ToString());
                if (Request.Params["atype"] != "")
                    str.Append("&atype=" + Request.Params["atype"]);
                if (Request.Params["cnt"] != "")
                    str.Append("&cnt=" + Request.Params["cnt"]);
                if (Request.Params["gender"] != "")
                    str.Append("&gender=" + Request.Params["gender"]);
                if (Request.Params["ponly"] != "")
                    str.Append("&ponly=" + Request.Params["ponly"]);
                channel_list1.Default_Url = "channel/search.aspx" + str.ToString();
                channel_list1.Pagination_Url = "channel/search.aspx" + str.ToString() + "&p=[p]";
            }
            else
            {
                // normal search
                channel_list1.Default_Url = "channel/search.aspx?query=" + Request.Params["query"].ToString();
                channel_list1.Pagination_Url = "channel/search.aspx?query=" + Request.Params["query"].ToString() + "&p=[p]";
            }
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

    }

    private void set_bread_links()
    {
        string _rooturl = Config.GetUrl();
        StringBuilder str = new StringBuilder();
        str.Append("<li><a href=\"" + _rooturl + "\">" + Resources.vsk.home + "</a><span class=\"divider\">/</span> </li>\n");
        str.Append("<li><a href=\"" + _rooturl + "channel/\">" + Resources.vsk.channels + "</a><span class=\"divider\">/</span> </li>\n");
        str.Append("<li><a href=\"" + _rooturl + "channel/searchoptions.aspx\">Advance Search</a><span class=\"divider\">/</span> </li>\n");
        str.Append("<li class=\"active\">Search Result</li>\n");
        bread.InnerHtml = str.ToString();
    }
}