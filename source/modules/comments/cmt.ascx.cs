using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class modules_comments_cmt : System.Web.UI.UserControl
{
    #region Properties
    private int _templateid = 0; //0: youtube style, 1: fb style
    private int _comments = 0;
    private long _contentid = 0;
    private string _profileid = ""; // in case of string value
    private int _type = 0;
    private string _AuthorUserName = "";
    private int _pagesize = 20;
    private bool _isadmin = false;
    private bool _showreplylink = true;
    private bool _showvotelink = true;
    private string _seealllink = "";
    private string _seealltext = "see all";
    private string _seealltooltip = "";
    private bool _showauthorphoto = true;
    
    private int _thumbwidth = 40;
    private int _thumbheight = 40;
    private string _url = "";
    private int _iscomment = 0;
    private string _cmt_text_caption = "Respond to this Video";
    private string _order = "c.level desc";
    private int _pageninationtype =0; // 0: ajax pagination, 1: load more, 2: normal pagination

    public string DefaultUrl = "";
    public string PaginationUrl = "";

    public bool HoverEffect = true;

    public string ContentType = "Video";
    private int PageNumber = 1;

    public int TemplateID
    {
        set { _templateid = value; }
        get { return _templateid; }
    }

    public long ContentID
    {
        set { _contentid = value; }
        get { return _contentid; }
    }

    public string ProfileID
    {
        set { _profileid = value; }
        get { return _profileid; }
    }

    public int isComment
    {
        set { _iscomment = value; }
        get { return _iscomment; }
    }

    public string AuthorUserName
    {
        set { _AuthorUserName = value; }
        get { return _AuthorUserName; }
    }

    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }

    public int TotalComments
    {
        set { _comments = value; }
        get { return _comments; }
    }

    public int PageSize
    {
        set { _pagesize = value; }
        get { return _pagesize; }
    }

    public bool ShowReplyLink
    {
        set { _showreplylink = value; }
        get { return _showreplylink; }
    }

    public bool ShowVoteLink
    {
        set { _showvotelink = value; }
        get { return _showvotelink; }
    }

    public bool isAdmin
    {
        set { _isadmin = value; }
        get { return _isadmin; }
    }

    public string See_All_Text
    {
        set { _seealltext = value; }
        get { return _seealltext; }
    }

    public string See_All_Url
    {
        set { _seealllink = value; }
        get { return _seealllink; }
    }

    public string See_All_Tooltip
    {
        set { _seealltooltip = value; }
        get { return _seealltooltip; }
    }

    public bool ShowAuthorPhoto
    {
        set { _showauthorphoto = value; }
        get { return _showauthorphoto; }
    }

    public string Order
    {
        set { _order = value; }
        get { return _order; }
    }

    public int Width
    {
        set { _thumbwidth = value; }
        get { return _thumbwidth; }
    }

    public int Height
    {
        set { _thumbheight = value; }
        get { return _thumbheight; }
    }

    public string Comment_URL
    {
        set { _url = value; }
        get { return _url; }
    }

    public string Text_Caption
    {
        set { _cmt_text_caption = value; }
        get { return _cmt_text_caption; }
    }

    public int PaginationType
    {
        set { _pageninationtype = value; }
        get { return _pageninationtype; }
    }
    
    protected string Post_Handler = "";
    protected string Vote_Handler = "";
    protected string Remove_Handler = "";
    protected string Flag_Handler = "";
    protected string Post_Params = "";
    protected string Remove_Params = "";
    protected string Flag_Params = "";
    protected string Load_Handler = "";
    protected string Load_Params = "";
    protected int cmtwidthdiff = 0;
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        cpost.Attributes.Add("placeholder", this.Text_Caption);
        if (!Page.IsPostBack)
        {
            
        }
    }

    public void BindList()
    {
        if (this.isComment == 0)
        {
            lst.InnerHtml ="<div class=\"bx_msg\"><h4>Comment has been disabled on this " + ContentType.ToLower() + ".</h4></div>";
            return;
        }
        if (this.ContentID > 0 || this.ProfileID != "")
        {
            if (this.PaginationType == 2)
            {
                // Normal Pagination
                if (Request.Params["p"] != null && Request.Params["p"] != "")
                {
                    this.PageNumber = Convert.ToInt32(Request.Params["p"]);
                }
            }

            if (this.ShowAuthorPhoto)
                cmtwidthdiff = 60; // cmt post textbox field width
            Set_Comment_Heading();

            Set_Handler_Paths();

            Load_Comments();
        }
    }
    
    private void Load_Comments()
    {
        StringBuilder str = new StringBuilder();
        // store comment flags
        int TotalPages = 1;
        if (this.TotalComments > this.PageSize)
            TotalPages = (int)Math.Ceiling((double)this.TotalComments / this.PageSize);
        str.Append("<span style=\"display:none;\" id=\"cmt_tcmts\">" + this.TotalComments + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_tpages\">" + TotalPages + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_psize\">" + PageSize + "</span>\n"); // store page size infor in <span>
        str.Append("<span style=\"display:none;\" id=\"cmt_pnum\">1</span>\n"); // current page index <span>
        if (this.TotalComments == 0)
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>Be the first to post comment on this " + ContentType.ToLower() + "!</h4>");
            str.Append("</div>");
            lst.InnerHtml = str.ToString();
            return;
        }
        // check for comments
        List<Comment_Struct> _list = CommentsBLL.Fetch_Comments_V2(this.ContentID, this.ProfileID, this.Type, this.PageNumber, this.PageSize, this.Order, this.ShowAuthorPhoto);
        if (_list.Count > 0)
        {
            // load comments
            int i = 0;
           
            str.Append("<div class=\"item\">");
            for (i = 0; i <= _list.Count - 1; i++)
            {
                // set post layout
                CmtItem postitem = new CmtItem();
                postitem.Height = 50;
                postitem.Width = 50;

                postitem.ShowDate = true;
                postitem.ShowAuthor = true;
                postitem.ShowAuthorImage = this.ShowAuthorPhoto;
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
                postitem.ShowVotes = this.ShowVoteLink;

                postitem.isHoverEffect = this.HoverEffect;
                postitem.TemplateID = this.TemplateID;
                str.Append(postitem.Process(_list[i]));
            }
            if(this.PaginationType==1)
               str.Append("<div id=\"cmt_load_cnt\"></div>\n"); // load more comment container
            str.Append("<div id=\"cmt_loading\"></div>\n"); // show loading progres
            str.Append("<div class=\"clear\"></div>"); // clear floating items
           
            
            str.Append("</div>");
            // ********************************************
            // PAGINATION SCRIPT
            // ********************************************
            pg.Visible = false;
            apg.Visible = false;
            if (TotalPages > 1)
            {
                switch (this.PaginationType)
                {
                    case 0:
                        // jquery ajax pagination
                        apg.Visible = true;
                        apagination1.TotalRecords = this.TotalComments;
                        apagination1.PageSize = this.PageSize;
                        apagination1.PageNumber = this.PageNumber;
                        apagination1.LoadHandler = Config.GetUrl("modules/comments/hdr/load.ashx");
                        apagination1.LoadParams = this.Load_Params;
                        apagination1.LoadContainer = lst.ClientID;
                        apagination1.LoadStatusContainer = "cmt_loading";
                        apagination1.BindPagination();
                        break;
                    case 1:
                        // jquery load more pagination
                        str.Append("<div id=\"cmt_more\" class=\"item_pad_4 bx_br_both\" style=\"padding:5px 0px;\">\n");
                        str.Append("<span class=\"ui-icon ui-icon-triangle-1-s\" style=\"float: left; margin-right: .3em;\"></span><a id=\"cmt_ld_more\"  href=\"#\" class=\"bold\" title=\"load more comments\">Load more...</a>\n");
                        str.Append("</div>\n");
                        break;
                    case 2:
                         // normal pagination
                         pg.Visible = true;
                         pagination1.TotalRecords = this.TotalComments;
                         pagination1.PageSize = this.PageSize;
                         pagination1.PageNumber = this.PageNumber;
                         pagination1.Default_Url = this.DefaultUrl;
                         pagination1.Pagination_Url = this.PaginationUrl;
                         pagination1.BindPagination();
                        break;
                }
            }
        }
        else
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>Be the first to post comment on this " + ContentType.ToLower() + "!</h4>");
            str.Append("</div>");
        }
        lst.InnerHtml = str.ToString();
    }

    private void Set_Handler_Paths()
    {
        Vote_Handler = Config.GetUrl() + "modules/comments/hdr/vote.ashx";
        Post_Handler = Config.GetUrl() + "modules/comments/hdr/pcmt.ashx"; // Config.GetUrl("modules/comments/hdr/pcmt.ashx");
        Remove_Handler = Config.GetUrl() + "modules/comments/hdr/rcmt.ashx";
        Flag_Handler = Config.GetUrl() + "modules/comments/hdr/spam.ashx";
        Load_Handler = Config.GetUrl() + "modules/comments/hdr/load.ashx";
        string UserProfilePictureName = "none";
        if (this.ShowAuthorPhoto && Page.User.Identity.IsAuthenticated)
            UserProfilePictureName = members.Get_Picture(Page.User.Identity.Name);
        Post_Params = "id=" + this.ContentID + "&tp=" + this.Type + "&ausr=" + this.AuthorUserName + "&prf=" + this.ProfileID + "&curl=" + Comment_URL + "&wd=" + this.Width + "&ht=" + this.Height + "&sphoto=" + this.ShowAuthorPhoto + "&isadm=" + this.isAdmin + "&photo=" + UserProfilePictureName + "&irep=" + this.ShowReplyLink + "&ivt=" + ShowVoteLink + "&tmlid=" + this.TemplateID + "&ishover=" + this.HoverEffect;
        Remove_Params = "id=" + this.ContentID + "&tp=" + this.Type + "&prf=" + this.ProfileID;
        Flag_Params = "id=" + this.ContentID + "&prf=" + this.ProfileID + "&ausr=" + this.AuthorUserName;
        Load_Params = "id=" + this.ContentID + "&tp=" + this.Type + "&ausr=" + this.AuthorUserName + "&prf=" + this.ProfileID + "&wd=" + this.Width + "&ht=" + this.Height + "&sphoto=" + this.ShowAuthorPhoto + "&isadm=" + this.isAdmin + "&photo=" + UserProfilePictureName + "&ps=" + this.PageSize + "&o=" + this.Order + "&irep=" + this.ShowReplyLink + "&ivt=" + ShowVoteLink + "&tmlid=" + this.TemplateID + "&ishover=" + this.HoverEffect;
    }

    private void Set_Comment_Heading()
    {
        StringBuilder str = new StringBuilder();
        if (this.TotalComments < this.PageSize || this.See_All_Url == "")
        {
            str.Append("<span class=\"xmedium-text light\">All Comments (" + this.TotalComments + ")</span>");
        }
        else
        {
            str.Append("<div style=\"float:left; width:60%;\">\n");
            str.Append("<span class=\"xmedium-text light\">All Comments (" + this.TotalComments + ")</span>");
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; text-align:right; width:28%;\">\n");
            str.Append("<a href=\"" + this.See_All_Url + "\" title=\"" + this.See_All_Tooltip + "\">" + this.See_All_Text + "</a>\n");
            str.Append("</div>\n");
            str.Append("<div class=\"clear\"></div>\n");
        }

        cmt.InnerHtml = str.ToString();
    }
}