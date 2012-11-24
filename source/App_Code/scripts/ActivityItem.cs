using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for ActivityItem
/// </summary>
public class ActivityItem
{
	public ActivityItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region MyRegion

    private string _cuser = ""; // currently logged in username

    private int _boxwidth = 0; // complete width of box (0:100%)
    private int _width = 136; // width of thumb inside box
    private int _height = 0; // 0: default height of thumb inside box
    private int _leftwidth = 40; // in % in case of showing thumb left and content on right
    private int _rightwidth = 59; // in % in case of showing thumb left and content on right


    private bool _ishovereffect = false; // show hover effect on mouse move
    private bool _isadmin = false; // admin mode
   
    // Show Options
    private bool _showtitle = true; // show title of photo
    private bool _showdescription = false; // show description of photo
    private bool _showdate = true;
    private int _dateformat = 3; //  // 0:  21 May, 2011, 1: May 30th, 2011, 2: May 11 2011, 3: 2 days ago, 4: Today 10:54 PM
    private bool _showliked = false;
    private bool _showdisliked = false;
    private bool _showcomments = false;
    private bool _showauthor = false;
    private bool _showprofilethumb = false;

    // css styles
    private string _boxcssclass = "bx_br_bt item_pad_4";
    private string _hovercssclass = "simplehover";
    private string _boldlinkcssclass = "normal-text bold";
    private string _normallinkcssclass = "normal-text";
    private string _actionlinkcssclass = "mini-text bold underline";
    private string _lightcssclass = "mini-text light";

    public string LoggedInUserName
    {
        set { _cuser = value; }
        get { return _cuser; }
    }

    public int BoxWidth
    {
        set { _boxwidth = value; }
        get { return _boxwidth; }
    }

    public int Width
    {
        set { _width = value; }
        get { return _width; }
    }

    public int LeftWidth
    {
        set { _leftwidth = value; }
        get { return _leftwidth; }
    }

    public int RightWidth
    {
        set { _rightwidth = value; }
        get { return _rightwidth; }
    }

    public int Height
    {
        set { _height = value; }
        get { return _height; }
    }

    public bool isHoverEffect
    {
        set { _ishovereffect = value; }
        get { return _ishovereffect; }
    }

      
    public bool isAdmin
    {
        set { _isadmin = value; }
        get { return _isadmin; }
    }

    public bool ShowTitle
    {
        set { _showtitle = value; }
        get { return _showtitle; }
    }

    public bool ShowDescription
    {
        set { _showdescription = value; }
        get { return _showdescription; }
    }
  
    public bool ShowDate
    {
        set { _showdate = value; }
        get { return _showdate; }
    }
    public int DateFormat
    {
        set { _dateformat = value; }
        get { return _dateformat; }
    }
   

    public bool Showliked
    {
        set { _showliked = value; }
        get { return _showliked; }
    }

    public bool ShowDisliked
    {
        set { _showdisliked = value; }
        get { return _showdisliked; }
    }

    public bool ShowComments
    {
        set { _showcomments = value; }
        get { return _showcomments; }
    }

    public bool ShowAuthor
    {
        set { _showauthor = value; }
        get { return _showauthor; }
    }

    public string BoxCssClass
    {
        set { _boxcssclass = value; }
        get { return _boxcssclass; }
    }

    public string HoverCssClass
    {
        set { _hovercssclass = value; }
        get { return _hovercssclass; }
    }
    public string ActionLinkCssClass
    {
        set { _actionlinkcssclass = value; }
        get { return _actionlinkcssclass; }
    }
  
    public string BoldLinkCssClass
    {
        set { _boldlinkcssclass = value; }
        get { return _boldlinkcssclass; }
    }
   
    public string NormalLinkCssClass
    {
        set { _normallinkcssclass = value; }
        get { return _normallinkcssclass; }
    }

    public string LightCssClass
    {
        set { _lightcssclass = value; }
        get { return _lightcssclass; }
    }

    public bool ShowProfileThumb
    {
        set { _showprofilethumb = value; }
        get { return _showprofilethumb; }
    }

    #endregion

    #region CommentProperties
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
    private bool ShowChannelAuthor = true;
    public string CommentOrder = "c.level desc"; // c.clevel order must be used in case reply is on. "points desc, added_date desc"; 
    #endregion

    public string Process(UserActivity_Struct ph)
    {       
        StringBuilder str = new StringBuilder();
        string _bxwidth = "100%";
        if (this.BoxWidth > 0)
            _bxwidth = this.BoxWidth + "px;";
        string _cntwidth = "";
        if (this.BoxWidth == 0)
            _cntwidth = "99%";
        else
        {
            int containerwidth = this.BoxWidth;
            _cntwidth = containerwidth + "px";
        }
        str.Append("<div id=\"ansid_" + ph.ActivityID + "\" class=\"" + this.BoxCssClass + " anscmt\" style=\"float:left; width:" + _bxwidth + "\">\n");
        if (this.LoggedInUserName != "")
        {
           
            // if currently logged in user is author of post or author of group
            if (this.LoggedInUserName == ph.UserName || this.LoggedInUserName == ph.PosterUserName)
            {
                // remove link
                str.Append("<div id=\"del_post_" + ph.ActivityID + "\" class=\"itm_cross\"\" style=\"display:none;\"><a id=\"rem_post_" + ph.ActivityID + "\" class=\"rempost " + this.BoldLinkCssClass + "\" title=\"remove post\" href=\"#\">&times;</a></div>\n");
            }
        }
        string container = "class=\"item_pad_2\"";
        if (this.isHoverEffect)
            container = "class=\"" + this.HoverCssClass + "\"";
      
        str.Append("<div " + container + " style=\"width:" + _cntwidth + "\">\n");
        str.Append("<div id=\"pmsg_" + ph.ActivityID + "\"></div>\n"); // for displaying messages
        if (this.ShowProfileThumb)
        {
            // Content on left side of thumb
            str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
            str.Append(ProcessThumb(ph));
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
            str.Append(ProcessContent(ph));
            str.Append("</div>\n");
            str.Append("<div class=\"clear\"></div>\n");
        }
        else
        {
            str.Append(ProcessContent(ph));
        }
        str.Append("</div>\n"); // close sub box
        // load and list comments // below hover post section
        //str.Append("<div id=\"cmtitm_" + ph.ActivityID + "\"></div>\n"); // div where activity comments will be loading.
        str.Append(ProcessAnswerComments(ph.ActivityID, ph.Comments, ph.UserName));
        str.Append("</div>\n"); // close main box
       
        return str.ToString();
    }

    private string ProcessThumb(UserActivity_Struct ph)
    {
        StringBuilder str = new StringBuilder();
        string _user = ph.PosterUserName;
        if (_user == "")
            _user = ph.UserName;
        string image_src = UrlConfig.Return_User_Profile_Photo(_user, ph.PictureName, 0, 0);

        str.Append("<a class=\"thumbnail\" style=\"width:" + this.Width + "px;\" href=\"" + UrlConfig.Prepare_User_Profile_Url(_user, this.isAdmin) + "\" title=\"" + _user + "\">"); // thumb link setup
        str.Append("<img  src=\"" + image_src + "\" height=\"" + this.Height + "\" width=\"" + this.Width + "\">");
        str.Append("</a>");
        return str.ToString();
    }

    private string ProcessContent(UserActivity_Struct ph)
    {
        StringBuilder str = new StringBuilder();
        // title or caption
        str.Append("<span id=\"actusr_" + ph.ActivityID + "\" style=\"display:none;\">" + ph.UserName + "</span>");
        if (this.ShowTitle && ph.Title !="")
        {
            // user link
            str.Append("<div class=\"item\">\n");
            str.Append("<a class=\"" + this.NormalLinkCssClass + "\" href=\"" + UrlConfig.Prepare_User_Profile_Url(ph.UserName, this.isAdmin) + "\" title=\"" + ph.UserName + "\">" + ph.UserName + "</a>");
            str.Append(" " + ph.Title);
            str.Append("</div>\n");
        }
        // activity
        str.Append(ph.Activity);
        
        // action section
        str.Append("<div id=\"" + ph.ActivityID + "\" class=\"item\">\n");
        str.Append("<a id=\"ans_" + ph.ActivityID + "\" href=\"#\" title=\"Post Comment\" class=\"" + this.ActionLinkCssClass + " acmt\">Comment</a>&nbsp;.&nbsp;");
        str.Append("<a href=\"#\" title=\"Like this post\" class=\"" + this.ActionLinkCssClass + " actlike\">Like</a>&nbsp;.&nbsp;");
        str.Append("<a href=\"#\" title=\"Dislike this post\" class=\"" + this.ActionLinkCssClass + " actdislike\">Dislike</a>");
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("&nbsp;.&nbsp;");
            str.Append("<span class=\"" + this.LightCssClass + "\">" + UtilityBLL.Generate_Date(ph.Added_Date, this.DateFormat) + "</span>");
        }
        // statistics
        str.Append("&nbsp.&nbsp;<span class=\"simplehover2 ui-corner-all mini-text ldcmt\"><span><i class=\"icon-thumbs-up\"></i> <span id=\"actlk_" + ph.ActivityID + "\">" + ph.Liked + "</span></span>&nbsp;<span><i class=\"icon-thumbs-down\"></i> <span id=\"actdlk_" + ph.ActivityID + "\">" + ph.Disliked + "</span></span>&nbsp;<span><i class=\"icon-comment\"></i> <span id=\"actcmts_" + ph.ActivityID + "\">" + ph.Comments + "</span></span></span>\n");
        str.Append("</div>\n");
       
        // Added
        
      
        return str.ToString();
    }

    private string ProcessAnswerComments(long Aid, int TotalComments, string AUserName)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
        // Store Answer Values for Posting Comments
        str.Append("<span style=\"display:none;\" id=\"ansusr_" + Aid + "\">" + AUserName + "</span>\n"); // store answer author user name for posting comment use
        // main block
        str.Append("<div class=\"anscmt\" id=\"ansid_" + Aid + "\">\n");
        // comment post section
        str.Append("<div id=\"msg_" + Aid + "\"></div>\n"); // comment messages section
        //str.Append("<div class=\"bx_br_bt item_pad_4\"><a id=\"ans_" + Aid + "\" href=\"#\" class=\"normal-text bold acmt\">Add Comment</a>\n");
        //str.Append("</div>\n");
        str.Append("<div id=\"qcmt_" + Aid + "\"></div>\n"); // load comment textbox section
        str.Append("<div id=\"tlst_" + Aid + "\"></div>\n"); // attaching dynamically posted comments with list of existing comments
        // store comment flags
        int TotalPages = 1;
        if (TotalComments > this.MaxComments)
            TotalPages = (int)Math.Ceiling((double)TotalComments / this.MaxComments);
        str.Append("<span style=\"display:none;\" id=\"cmt_tcmts_" + Aid + "\">" + TotalComments + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_tpages_" + Aid + "\">" + TotalPages + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_psize_" + Aid + "\">" + MaxComments + "</span>\n"); // store page size infor in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_pnum_" + Aid + "\">0</span>\n"); // current page mark as 0, no page loaded in start
        // no comments loaded in start.
        //List<Comment_Struct> _list = CommentsBLL.Fetch_Comments_V2(Aid, "", this.Type, 1, this.MaxComments, "c.level desc", ShowChannelAuthor);
        //if (_list.Count > 0)
        //{
        //    // load comments
        //    int i = 0;

        //    str.Append("<div class=\"item\">");
        //    for (i = 0; i <= _list.Count - 1; i++)
        //    {
        //        // set post layout
        //        CmtItem postitem = new CmtItem();
        //        postitem.Height = 50;
        //        postitem.Width = 50;

        //        postitem.ShowDate = true;
        //        postitem.ShowAuthor = true;
        //        postitem.ShowAuthorImage = ShowChannelAuthor;
        //        postitem.Height = 50;
        //        postitem.Width = 50;
        //        postitem.isAdmin = this.isAdmin;
        //        postitem.isHoverEffect = true;
        //        postitem.isRoundCorners = false;
        //        postitem.LeftWidth = 8; // %
        //        postitem.RightWidth = 91; // %
        //        postitem.ShowReplyLink = true;
        //        postitem.ShowVotes = true;
        //        postitem.AuthorUserName = AUserName; // Author Of Post
        //        string PosterUserName = "";
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //            PosterUserName = HttpContext.Current.User.Identity.Name;
        //        postitem.PosterUserName = PosterUserName; // Currently logged in User
        //        postitem.ShowReplyLink = this.ShowReplyLink;
        //        postitem.ShowVotes = this.ShowVoteLink;

        //        postitem.isHoverEffect = this.HoverEffect;
        //        postitem.TemplateID = this.Comment_Template_ID;
        //        postitem.BoxCssClass = "citem";

        //        postitem._content_left_width = 76; // %
        //        postitem._content_right_width = 118; // px
        //        str.Append(postitem.Process(_list[i]));
        //    }
        // }
        str.Append("<div id=\"cmt_load_cnt_" + Aid + "\"></div>\n"); // load more comment container

        str.Append("<div id=\"cmt_loading_" + Aid + "\"></div>\n"); // show loading progres
        str.Append("<div class=\"clear\"></div>"); // clear floating items
        //str.Append("</div>");
        // ********************************************
        // PAGINATION SCRIPT
        // ********************************************
        // jquery load more pagination
        if (TotalPages > 1)
        {
            str.Append("<div id=\"pcmtldmore_" + Aid + "\" class=\"item_pad_4 bx_br_both cmtmore\" style=\"padding:5px 0px; display:none;\">\n"); // don't visit in start
            str.Append("<a id=\"cmtldmore_" + Aid + "\" href=\"#\" class=\"bold cmtldmore\" title=\"load more comments\"><i class=\"icon-chevron-down icon-white\"></i> Load more...</a>\n");
            str.Append("</div>\n");
        }

        str.Append("</div>\n"); // close main answer comment block
        str.Append("</div>\n"); // close main answer comment block
        str.Append("<div class=\"clear\"></div>\n");
        return str.ToString();
    }
}