using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modules_comments_cmtitem : System.Web.UI.UserControl
{

    #region MyRegion
    private Comment_Struct _cmt = null;
    public Comment_Struct Cmt
    {
        set { _cmt = value; }
        get { return _cmt; }
    }

    protected string Item = "";
    private int _templateid = 1; // 0: Youtube Style, 1: Facebook Style
    private long _postid = 0; // Post ID Of Record // On which comment is posted
    private string _profileid = ""; // Profile ID On which comment posted e.g user profile.
    private string _authorusername = ""; // Auther of post
    private string _posterusername = ""; // Currently logged in user

    private int _width = 140; // width of thumb inside box
    private int _height = 108; // 0: default height of thumb inside box

    private int _leftwidth = 40; // in % in case of showing thumb left and content on right
    private int _rightwidth = 59; // in % in case of showing thumb left and content on right

    private bool _isroundcorners = true; // enable, disable round corners on thumb item
    private bool _ishovereffect = false; // show hover effect on mouse move
    private bool _isaltcolor = false; // show background in alternative color
    private bool _isadmin = false; // admin mode

    // Show Options
    private int _commentlength = 0; // show complete comment
    private bool _showdate = true;
    private int _dateformat = 3; //  // 0:  21 May, 2011, 1: May 30th, 2011, 2: May 11 2011, 3: 2 days ago, 4: Today 10:54 PM
    private bool _showapproved = false;
    private bool _showdisabled = false;
    private bool _showauthor = true;
    private bool _showauthorthumb = true;

    private bool _showreply = true;
    private bool _showvotes = true;

    // urls
    private string _authorurl = ""; // url of author of photo, if empty default url will be used.
   
    public int TemplateID
    {
        set { _templateid = value; }
        get { return _templateid; }
    }

    public long PostID
    {
        set { _postid = value; }
        get { return _postid; }
    }

    public string ProfileID
    {
        set { _profileid = value; }
        get { return _profileid; }
    }

    public string AuthorUserName
    {
        set { _authorusername = value; }
        get { return _authorusername; }
    }

    public string PosterUserName
    {
        set { _posterusername = value; }
        get { return _posterusername; }
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

    public bool isRoundCorners
    {
        set { _isroundcorners = value; }
        get { return _isroundcorners; }
    }

    public bool isHoverEffect
    {
        set { _ishovereffect = value; }
        get { return _ishovereffect; }
    }

    public bool isAltColor
    {
        set { _isaltcolor = value; }
        get { return _isaltcolor; }
    }


    public bool isAdmin
    {
        set { _isadmin = value; }
        get { return _isadmin; }
    }

    public int CommentLength
    {
        set { _commentlength = value; }
        get { return _commentlength; }
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

    public bool ShowApproved
    {
        set { _showapproved = value; }
        get { return _showapproved; }
    }

    public bool ShowDisabled
    {
        set { _showdisabled = value; }
        get { return _showdisabled; }
    }

    public bool ShowAuthorImage
    {
        set { _showauthorthumb = value; }
        get { return _showauthorthumb; }
    }

    public bool ShowAuthor
    {
        set { _showauthor = value; }
        get { return _showauthor; }
    }

    public string AuthorUrl
    {
        set { _authorurl = value; }
        get { return _authorurl; }
    }
    public bool ShowReplyLink
    {
        set { _showreply = value; }
        get { return _showreply; }
    }

    public bool ShowVotes
    {
        set { _showvotes = value; }
        get { return _showvotes; }
    }

  
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // set post layout
        CmtItem postitem = new CmtItem();
        postitem.Height = 50;
        postitem.Width = 50;

        postitem.ShowDate = true;
        postitem.ShowAuthor = true;
        postitem.ShowAuthorImage = this.ShowAuthorImage;
        postitem.Height = this.Height;
        postitem.Width = this.Width;
        postitem.isAdmin = this.isAdmin;
        postitem.isHoverEffect = true;
        postitem.isRoundCorners = false;
        postitem.LeftWidth = 8; // %
        postitem.RightWidth = 91; // %
        postitem.ShowReplyLink = true;
        postitem.ShowVotes = true;
        postitem.AuthorUserName = this.AuthorUserName; // Author Of Post
        string PosterUserName = "";
        if (Page.User.Identity.IsAuthenticated)
            PosterUserName = Page.User.Identity.Name;
        postitem.PosterUserName = PosterUserName; // Currently logged in User
        postitem.ShowReplyLink = this.ShowReplyLink;
        postitem.ShowVotes = this.ShowVotes;

        postitem.isHoverEffect = this.isHoverEffect;
        postitem.TemplateID = this.TemplateID;
        postitem.isAdmin = this.isAdmin;

        postitem.ShowApproved = this.ShowApproved;
        postitem.ShowDisabled = this.ShowDisabled;
        this.Item = postitem.Process(Cmt);

    }
}