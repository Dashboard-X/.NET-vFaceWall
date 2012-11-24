using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class user_modules_post : System.Web.UI.UserControl
{
    // User control responsible for posting content on wall

    protected string Params = "";

    // Check whether current user is group member or have right to post contents
    public bool isUserAllowed = true;
    protected string Handler_Path = "";
    protected string Preview_Handler_Path = "";
    protected string Delete_Post_Handler_Path = "";
    protected string Delete_Post_Params = "";


    public string UserName = "";
    public string AuthorUserName = "";
    protected string UserProfilePictureName = "none";

    // Post Comment Handlers
    public int MaxComments = 20; // Total comments to show with single answer
    public int isComment = 1; // 1: on, 0: off
    public int Type = 13; // Group Post Comments
    public int Comment_Template_ID = 3; // light version
    public bool ShowReplyLink = true;
    public bool ShowVoteLink = true;
    public bool HoverEffect = true;
    protected string Post_Handler = "";
    protected string Post_Params = "";
    protected string Vote_Handler = "";
    protected string Remove_Handler = "";
    protected string Flag_Handler = "";
    protected string Remove_Params = "";
    protected string Flag_Params = "";
    protected string Load_Handler = "";
    protected string Load_Params = "";
    protected int cmtwidthdiff = 0;
    private string PostUrl = "";
    private bool ShowAuthor = true;
    public string CommentOrder = "c.level desc"; // c.clevel order must be used in case reply is on. "points desc, added_date desc";

    private int _leftwidth = 10; // %; for post author photo preview
    private int _rightwidth = 89; // %; for original post section

    public string Term = ""; // filter group posts by search term
    public int Month = 0; // filter group posts by month
    public int Year = 0; // filter group posts by year

    public bool isAdmin = false;
    public int PageSize = 10; // Total number of posts to be shown
    public string Order = "added_date desc";
    public int AddedFilter = 0; // 0 All Time, 1: Today Added, 2: This Week Added, 3: This Month Added

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!isUserAllowed)
            {
                widget.Visible = false;
                return;
            }

            if (Page.User.Identity.IsAuthenticated)
            {
                this.UserName = Page.User.Identity.Name;
            }
            else
            {
                widget.Visible = false;
            }

            if (this.UserName != "")
                UserProfilePictureName = members.Get_Picture(this.UserName);

            // set handler paths for posting comment on each dynamic answer
            Set_Handler_Paths();

            Generate_Group_Posting_Options();
        }
    }

    private void Set_Handler_Paths()
    {
        string _root = Config.GetUrl();

        Handler_Path = _root + "user/modules/post.ashx";
        Preview_Handler_Path = _root + "user/modules/postpreview.ashx";
        Delete_Post_Handler_Path = _root + "user/modules/dpost.ashx";
        Delete_Post_Params = "";

        Params = "usr=" + this.AuthorUserName + "&pic=" + this.UserProfilePictureName;
    }


    private void Generate_Group_Posting_Options()
    {
        string _root = Config.GetUrl();
        StringBuilder str = new StringBuilder();
        str.Append("<div class=\"item_pad_4 bx_br_bt hor_lst\">\n");
        str.Append("<ul>\n");
        //str.Append("<li><a class=\"normal-text reverse bold\">Write Post</a></li>\n");
        str.Append("<li><span class=\"normal-text light bold\">Post on wall</span></li>");
        str.Append("</ul>\n");
        str.Append("</div>\n");
        glinks.InnerHtml = str.ToString();
    }
}