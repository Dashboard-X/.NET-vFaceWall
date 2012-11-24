using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class channel_Default : System.Web.UI.Page
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
            // Channel return params
            // status :-> accountnotexist (user account not found or disabled)

            channel_list1.Filter = Set_Filter();
            channel_list1.Order = "u.register_date desc";
            if (Request.Params["filter"] != null)
            {
                channel_list1.Filter_DefaultUrl = "channel/default/" + Request.Params["filter"].ToString() + ".aspx";
                channel_list1.Filter_Pagination_Url = "channel/default/" + Request.Params["filter"].ToString() + "_[p].aspx";
            }

            channel_list1.Default_Url = "channel/";
            channel_list1.Pagination_Url = "channel/default_[p].aspx";

        }

    }

    private int Set_Filter()
    {
        int filter = 0;
        if (Request.Params["filter"] != null)
        {
            switch (Request.Params["filter"].ToString())
            {
                case "all":
                    filter = 0;
                    break;
                case "ThisMonth":
                    filter = 3;
                    break;
                case "ThisWeek":
                    filter = 2;
                    break;
                case "Today":
                    filter = 1;
                    break;
            }
        }
        return filter;
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
}
