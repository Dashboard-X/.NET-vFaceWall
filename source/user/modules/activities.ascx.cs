using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class user_modules_activities : System.Web.UI.UserControl
{
    // Properties
    public string Term = "";
    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }
    public bool isCache = false; // cache list of records for specified time interval
    public string Order = "added_date desc";
    public string NoRecordFoundText = "No user activity found!";
    public int AddedFilter = 0; // 0 All Time, 1: Today Added, 2: This Week Added, 3: This Month Added
    // Archive Related
    public int Month = 0; // month id to filter record based on month
    public int Year = 0; // year to filter records based on year
    public bool isAdmin = false;
    public int PageSize = 20;
    public int TotalRecords = 0;

    public string ActivityLikeHandler = "";
    public string ActivityLoadHandler = "";
    public string LoadActivityParams = "";

    // Post Comment Handlers
    public int MaxComments = 20; // Total comments to show with single answer
    public int isComment = 1; // 1: on, 0: off
    public int Type = 14; // User Channel Activities
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // set handler paths for posting comment on each dynamic answer
            Set_Handler_Paths();
            // load posts
            BindList();
        }
    }

    public void BindList()
    {
        StringBuilder str = new StringBuilder();
        this.TotalRecords = ActivityBLL.Count_Activities(this.Term, this.UserName, this.Month, this.Year, this.Order, this.PageSize, this.AddedFilter);
        if (this.TotalRecords == 0)
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>" + NoRecordFoundText + "</h4>");
            str.Append("</div>");
            lst.InnerHtml = str.ToString();
            return;
        }

        List<UserActivity_Struct> _list = ActivityBLL.Load_Activities(this.Term, this.UserName, this.Month, this.Year, this.Order, this.PageSize, this.AddedFilter,this.isCache, 1);
        if (_list.Count > 0)
        {
            int i = 0;
            ActivityItem act = new ActivityItem();
            act.LeftWidth = this._leftwidth; // profile layout style
            act.RightWidth = this._rightwidth;
            act.Width = 50; // profile thumb height
            act.Height = 50;
            act.isHoverEffect = true;
            act.ShowProfileThumb = true; // show user profile thumb on right side.
            if (this.Page.User.Identity.IsAuthenticated)
            {
                act.LoggedInUserName = this.Page.User.Identity.Name;
            }
            for (i = 0; i <= _list.Count - 1; i++)
            {
                string _puser = _list[i].PosterUserName;
                if(_puser == "")
                    _puser = _list[i].UserName;

                PostUrl = UrlConfig.Prepare_User_Profile_Url(_puser, "activity", this.isAdmin) + "?id=" + _list[i].ActivityID;
                // generate list
                str.Append(act.Process(_list[i]));
                str.Append("<div class=\"clear\"></div>"); // clear floating items
            }
            str.Append("<div class=\"clear\"></div>"); // clear floating items
            // store comment flags
            int TotalPages = 1;
            if (this.TotalRecords >= this.PageSize)
                TotalPages = (int)Math.Ceiling((double)TotalRecords / this.PageSize);
            str.Append("<span style=\"display:none;\" id=\"cmt_tcmts\">" + this.TotalRecords + "</span>\n"); // store total pages info in <span>
            str.Append("<span style=\"display:none;\" id=\"cmt_tpages\">" + TotalPages + "</span>\n"); // store total pages info in <span>
            str.Append("<span style=\"display:none;\" id=\"cmt_psize\">" + PageSize + "</span>\n"); // store page size infor in <span>
            str.Append("<span style=\"display:none;\" id=\"cmt_pnum\">1</span>\n"); // current page number
            str.Append("<div id=\"cmt_load_cnt\"></div>\n"); // load more activity container
            str.Append("<div id=\"cmt_loading\"></div>\n"); // show loading progres
            if (TotalPages > 1)
            {
                str.Append("<div id=\"actldmore\" class=\"item_pad_4 bx_br_both actmore\" style=\"padding:5px 0px;\">\n"); 
                str.Append("<a href=\"#\" class=\"bold actldmore\" title=\"load more activities\"><i class=\"icon-chevron-down icon-white\"></i> Load more...</a>\n");
                str.Append("</div>\n");
            }
        }
        else
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>" + this.NoRecordFoundText + "</h4>");
            str.Append("</div>");
        }
        lst.InnerHtml = str.ToString();
    }

    private void Set_Handler_Paths()
    {
        string _root = Config.GetUrl();
        ActivityLikeHandler = _root + "user/modules/like.ashx";
        ActivityLoadHandler = _root + "user/modules/lactivity.ashx";
        LoadActivityParams = "term=" + this.Term + "&usr=" + this.UserName + "mn=" + this.Month + "&yr=" + this.Year + "&wd=50&ht=50&sphoto=true&isadm=" + this.isAdmin + "&ps=" + this.PageSize + "&o=" + this.Order + "&ishover=" + this.HoverEffect + "&clwidth=" + this._leftwidth + "&crwidth=" + this._rightwidth + "&afilter=" + this.AddedFilter;
        
        Vote_Handler = _root + "modules/comments/hdr/vote.ashx";
        Post_Handler = _root + "modules/comments/hdr/pcmt.ashx"; // Config.GetUrl("modules/comments/hdr/pcmt.ashx");
        Remove_Handler = _root + "modules/comments/hdr/rcmt.ashx";
        Flag_Handler = _root + "modules/comments/hdr/spam.ashx";
        Load_Handler = _root + "modules/comments/hdr/load.ashx";
        //if (this.ShowAuthorPhoto && Page.User.Identity.IsAuthenticated)
        //UserProfilePictureName = members.Get_Picture(Page.User.Identity.Name);
        Post_Params = "tp=" + this.Type + "&curl=" + PostUrl + "&wd=50&ht=50&sphoto=" + this.ShowAuthor + "&isadm=" + this.isAdmin + "&photo=&irep=" + this.ShowReplyLink + "&ivt=" + ShowVoteLink + "&tmlid=" + this.Comment_Template_ID + "&ishover=" + this.HoverEffect;
        Remove_Params = "tp=" + this.Type + "";
        Flag_Params = "prf=";
        Load_Params = "tp=" + this.Type + "&wd=50&ht=50&sphoto=" + this.ShowAuthor + "&isadm=" + this.isAdmin + "&photo=&ps=" + this.MaxComments + "&o=" + this.CommentOrder + "&irep=" + this.ShowReplyLink + "&ivt=" + ShowVoteLink + "&tmlid=" + this.Comment_Template_ID + "&ishover=" + this.HoverEffect + "&clwidth=76&crwidth=118";
    }

   
}