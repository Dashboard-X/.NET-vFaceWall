using System;
using System.Collections.Generic;
using System.Web;
using System.Text;


/// <summary>
/// Handler is responsible for layouting comment item
/// </summary>
public class CmtItem
{
    #region MyRegion

    public int _content_left_width = 78; // %
    public int _content_right_width = 118; // px;

    private int  _templateid = 1; // 0: Youtube Style, 1: Facebook Style, 2: blog style, 3: light
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
    // css styles
    private string _boxcssclass = "citem";
    private string _hovercssclass = "simplehover ui-corner-all";
    private string _altcsslass = "simplehoveralt ui-corner-all";
    private string _thumbcssclass = "thumbnail";
    private string _thumbroundcssclass = "thumbnail";
    private string _boldlinkcssclass = "normal-text bold";
    private string _normallinkcssclass = "normal-text";
    private string _goodcssclass = "mini-text green bold";
    private string _badcssclass = "mini-text red bold";

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

    public string AltCssClass
    {
        set { _altcsslass = value; }
        get { return _altcsslass; }
    }

    public string ThumbCssClass
    {
        set { _thumbcssclass = value; }
        get { return _thumbcssclass; }
    }

    public string ThumbRoundCssClass
    {
        set { _thumbroundcssclass = value; }
        get { return _thumbroundcssclass; }
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

    public string GoodCssClass
    {
        set { _goodcssclass = value; }
        get { return _goodcssclass; }
    }

    public string BadCssClass
    {
        set { _badcssclass = value; }
        get { return _badcssclass; }
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

    public string Process(Comment_Struct grp)
    {
        this.AuthorUrl = UrlConfig.Prepare_User_Profile_Url(grp.UserName, this.isAdmin);

        StringBuilder str = new StringBuilder();
        string reply_pad = "";
        string container_size = "100%";
        if (grp.ReplyID > 0)
        {
            reply_pad = "padding-left:3%;";
            container_size = "97%";
        }
        // don't remove class name "vskcmtcnt" its internal use only
        str.Append("<div id=\"citem_" + grp.CommentID + "\" class=\"vskcmtcnt\" style=\"width:" + container_size + ";" + reply_pad + ";\">\n");
        str.Append("<div id=\"cmsg_" + grp.CommentID + "\"></div>\n"); // to display runtime messages
        str.Append("<div class=\"" + this.BoxCssClass + "\">\n");
        string container = "class=\"item_pad_2\"";
        if (this.isHoverEffect)
            container = "class=\"" + this.HoverCssClass + "\"";
        if (this.isAltColor)
            container = "class=\"" + this.AltCssClass + "\"";
        str.Append("<div " + container + ">\n");
        switch (this.TemplateID)
        {
            case 0:
                // youtube style comment style
                str.Append(YoutubeStyleTemplate(grp));
                break;
            case 1:
                // Facebook style comment
                str.Append(FBStyleTemplate(grp));
                break;
            case 2:
                // Blog style comment
                str.Append(BlogStyleTemplate(grp));
                break;
            case 3:
                // Simple Style
                str.Append(ProcessContent_Simple(grp));
                break;
        }
        str.Append("</div>\n"); //close hover box
        str.Append("</div>\n"); // close box css class
        str.Append("</div>\n"); // close main box

        // reset urls for next itemgroup
        this.AuthorUrl = "";

        return str.ToString();
    }
    #region Youtube Style Template

    private string YoutubeStyleTemplate(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        if (this.ShowAuthorImage)
        {
            // show author thumb on right side of comment
            str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
            str.Append(ProcessThumb(grp));
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
            str.Append(ProcessContent(grp));
            str.Append("</div>\n");
            str.Append("<div class=\"clear\"></div>\n");
        }
        else
        {
            str.Append("<div class=\"item\">\n");
            str.Append(ProcessContent(grp));
            str.Append("</div>\n");
        }
        return str.ToString();
    }
   
    private string ProcessContent(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        int _content_left_width = 78; // %
        int _content_right_width = 118; // px;
        if (grp.ReplyID > 0 || !this.ShowReplyLink)
        {
            _content_left_width = _content_left_width + 4;
            _content_right_width = _content_right_width - 28;
        }

        if (!this.ShowVotes)
        {
            _content_left_width = _content_left_width + 8;
            _content_right_width = _content_right_width - 58;
        }
        str.Append("<span id=\"cmtpts_" + grp.CommentID + "\" style=\"display:none;\">" + grp.Points + "</span>\n");
        // Comment Section
        str.Append("<div style=\"float:left; width:" + _content_left_width + "%;\">\n");
        // Show Comment
        str.Append("<div class=\"item\">\n");
        string cmt_edit_css = "";
        if (this.isAdmin)
            cmt_edit_css = " id=\"cmt_edit_" + grp.CommentID + "\" class=\"cmtedit\"";

        str.Append("<p" + cmt_edit_css + ">" + grp.Comment + "</p>");
        str.Append("</div>\n");
        // show author
        str.Append("<div class=\"item\">\n");
        if (Config.isFeature_UserName() && this.ShowAuthor)
        {
            // if username enabled
            string _usr = grp.UserName;
            if (_usr.Length > 18)
                _usr = _usr.Substring(0, 18) + "..";
            string _usr_link = "<a id=\"cmtusr_" + grp.CommentID + "\" class=\"" + this.NormalLinkCssClass + "\" href=\"" + this.AuthorUrl + "\"  title=\"" + grp.UserName + "\">" + _usr + "</a>&nbsp;";
            str.Append(_usr_link);
        }
        // Added
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("&nbsp;<span class=\"light\">" + UtilityBLL.Generate_Date(grp.Added_Date, this.DateFormat) + "</span>");
        }

        //// Add Like Action
        //// comment like action to be store in span// 0: Like, 1: Unlike
        //str.Append("<span id=\"cmtlkact_" + grp.CommentID + "\" style=\"display:none;\">0</span>");
        //str.Append("&nbsp;<a id=\"cmt_lk_" + grp.CommentID + "\" class=\"cmt_lk\" href=\"#\">Like</a>\n");
        if (grp.Points < 0)
            str.Append("&nbsp;<span id=\"cmtpl_" + grp.Points + "\" class=\"red_bx ui-corner-all\">" + grp.Points + "</span>");
        else if (grp.Points > 0)
            str.Append("&nbsp;<span id=\"cmtpl_" + grp.Points + "\" class=\"green_bx ui-corner-all\">+" + grp.Points + "</span>");

        str.Append("</div>\n");
        // approved, disabled
        if (this.ShowApproved && this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";

            string _disabled = "<span  class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";

            str.Append(_approve + "&nbsp;|&nbsp;" + _disabled);
            str.Append("</div>\n");
        }
        else if (this.ShowApproved)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";
            str.Append(_approve);
            str.Append("</div>\n");
        }
        else if (this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";
            str.Append(_disabled);
            str.Append("</div>\n");
        }
        str.Append("</div>\n"); // close float left div
        if (!this.isAdmin)
        {
            str.Append("<div id=\"cmt_action_" + grp.CommentID + "\" class=\"itm_cross btn-group\" style=\"float:right; width:" + _content_right_width + "px; display:none;\">\n");
            if (this.ShowVotes)
            {
                // vote up link
                str.Append("<a href=\"#\" class=\"btn btn-mini cmt_vu\" rel=\"tooltip\" title=\"Vote Up\"><i class=\"icon-thumbs-up\"></i></a>");
                // vote down link
                str.Append("<a href=\"#\" class=\"btn btn-mini cmt_vd\" title=\"Vote Down\"><i class=\"icon-thumbs-down\"></i></a>");
            }
            if (grp.ReplyID == 0 && this.ShowReplyLink)
            {
                // reply link
                str.Append("<a href=\"#\" class=\"btn btn-mini cmt_rep\" title=\"Reply to this comment\"><i class=\"icon-repeat\"></i></a>");
            }
            if (this.PosterUserName != "")
            {
                // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
                if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
                {
                    // remove link
                    str.Append("<a href=\"#\" class=\"btn btn-mini cmt_remove\" title=\"Remove Comment\"><i class=\"icon-trash\"></i></a>");
                }
                else
                {
                    // spam link
                    str.Append("<a href=\"#\" class=\"btn btn-mini cmt_flag\" title=\"Flag for Spam\"><i class=\"icon-flag\"></i></a>");
                }
            }
            else
            {
                // spam link
                str.Append("<a href=\"#\" class=\"btn btn-mini cmt_flag\" title=\"Flag for Spam\"><i class=\"icon-flag\"></i></a>");
            }
            str.Append("</div>\n"); // close float right div
        }
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("<div id=\"cmtrepmsg_" + grp.CommentID + "\"></div>\n"); // reply comment box
        str.Append("<div id=\"cmtrep_" + grp.CommentID + "\"></div>\n"); // for showing comment box
        // Action Secction

        return str.ToString();
    } 
    #endregion

    #region FB Style Template
    private string FBStyleTemplate(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        if (this.ShowAuthorImage)
        {
            // show author thumb on right side of comment
            str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
            str.Append(ProcessThumb(grp));
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
            str.Append(ProcessContent_Fb(grp));
            str.Append("</div>\n");
            str.Append("<div class=\"clear\"></div>\n");
        }
        else
        {
            str.Append("<div class=\"item\">\n");
            str.Append(ProcessContent_Fb(grp));
            str.Append("</div>\n");
        }
        return str.ToString();
    }

    private string ProcessContent_Fb(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        int _content_left_width = 97; // %
        int _content_right_width = 12; // px;
        str.Append("<span id=\"cmtpts_" + grp.CommentID + "\" style=\"display:none;\">" + grp.Points + "</span>\n");
        // Comment Section
        str.Append("<div style=\"float:left; width:" + _content_left_width + "%;\">\n");
        // show author
        if (Config.isFeature_UserName() && this.ShowAuthor)
        {
            // if username enabled
            string _usr = grp.UserName;
            if (_usr.Length > 18)
                _usr = _usr.Substring(0, 18) + "..";
            string _usr_link = "<a id=\"cmtusr_" + grp.CommentID + "\" class=\"" + this.BoldLinkCssClass + "\" href=\"" + this.AuthorUrl + "\"  title=\"" + grp.UserName + "\">" + _usr + "</a>&nbsp;";
            str.Append(_usr_link);
        }
        // Show Comment
        str.Append("<div class=\"item\">\n");
        str.Append(grp.Comment);
        str.Append("</div>\n");
        str.Append("<div id=\"cmt_action_fb_" + grp.CommentID + "\" class=\"action\">\n");
        if (grp.ReplyID == 0 && this.ShowReplyLink)
        {
            // reply link
            str.Append("<a href=\"#\" class=\"cmt_rep\" title=\"Reply to this comment\">Reply</a>&nbsp;.&nbsp;\n");
        }
        if (this.ShowVotes)
        {
            //// comment like action to be store in span// 0: Like, 1: Unlike cmt_lk
            int likes = grp.Points;
            if (likes < 0)
                likes = 0;
            if(likes>0)
                str.Append("<span id=\"cmtpl_" + grp.Points + "\" class=\"normal-text reverse\">" + likes + "</span>&nbsp;.&nbsp;");
            str.Append("<a id=\"cmt_lk_" + grp.CommentID + "\" class=\"cmt_vu\" href=\"#\">Like</a>&nbsp;.&nbsp;\n");
        }
        if (this.PosterUserName != "")
        {
            // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
            if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
            {
                // remove link
            }
            else
            {
                // spam link
                str.Append("<a href=\"#\" class=\"cmt_flag\" title=\"Flag for Spam\">" + Resources.vsk.flag + "</a>&nbsp;.&nbsp;");
            }
        }
        else
        {
            // spam link
            str.Append("<a href=\"#\" class=\"cmt_flag\" title=\"Flag for Spam\">" + Resources.vsk.flag + "</a>&nbsp;.&nbsp;");
        }
        // Added
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("<span class=\"light\">" + UtilityBLL.Generate_Date(grp.Added_Date, this.DateFormat) + "</span>");
        }
        str.Append("</div>\n");
        // approved, disabled
        if (this.ShowApproved && this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";

            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";

            str.Append(_approve + "&nbsp;|&nbsp;" + _disabled);
            str.Append("</div>\n");
        }
        else if (this.ShowApproved)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";
            str.Append(_approve);
            str.Append("</div>\n");
        }
        else if (this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";
            str.Append(_disabled);
            str.Append("</div>\n");
        }
        str.Append("</div>\n"); // close float left div
        str.Append("<div id=\"cmt_action_" + grp.CommentID + "\" class=\"itm_cross\" style=\"float:right; width:" + _content_right_width + "px; display:none;\">\n");
        if (this.PosterUserName != "")
        {
            // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
            if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
            {
                // remove link
                str.Append("<a href=\"#\" class=\"cmt_remove\" title=\"Remove Comment\">X</a>");
            }
        }
        str.Append("</div>\n"); // close float right div
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("<div id=\"cmtrepmsg_" + grp.CommentID + "\"></div>\n"); // reply comment box
        str.Append("<div id=\"cmtrep_" + grp.CommentID + "\"></div>\n"); // for showing comment box
        // Action Secction

        return str.ToString();
    }  
    #endregion

    #region Blog Style Template
    private string BlogStyleTemplate(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        if (this.ShowAuthorImage)
        {
            this.LeftWidth = 12;
            this.RightWidth = 87;
            // show author thumb on right side of comment
            str.Append("<div class=\"item_pad_4\">\n");
            str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
            str.Append(ProcessThumb(grp));
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
            // show author
            if (Config.isFeature_UserName() && this.ShowAuthor)
            {
                // if username enabled
                string _usr = grp.UserName;
                if (_usr.Length > 18)
                    _usr = _usr.Substring(0, 18) + "..";
                string _usr_link = "<a id=\"cmtusr_" + grp.CommentID + "\" class=\"" + this.BoldLinkCssClass + "\" href=\"" + this.AuthorUrl + "\"  title=\"" + grp.UserName + "\">" + _usr + "</a>";
                str.Append(_usr_link);
            }
            str.Append("</div>\n");
            str.Append("<div class=\"clear\"></div>\n");
            str.Append("</div>\n");
        }
        else
        {
            str.Append("<div class=\"item_pad_4\">\n");
            // show author
            if (Config.isFeature_UserName() && this.ShowAuthor)
            {
                // if username enabled
                string _usr = grp.UserName;
                if (_usr.Length > 18)
                    _usr = _usr.Substring(0, 18) + "..";
                string _usr_link = "<a id=\"cmtusr_" + grp.CommentID + "\" class=\"" + this.BoldLinkCssClass + "\" href=\"" + this.AuthorUrl + "\"  title=\"" + grp.UserName + "\">" + _usr + "</a>";
                str.Append(_usr_link);
            }
            str.Append("</div>\n");
        }
        str.Append("<div class=\"item_pad_4\">\n");
        str.Append(ProcessContent_Blog(grp));
        str.Append("</div>\n");
        return str.ToString();
    }

    private string ProcessContent_Blog(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        int _content_left_width = 97; // %
        int _content_right_width = 12; // px;
        str.Append("<span id=\"cmtpts_" + grp.CommentID + "\" style=\"display:none;\">" + grp.Points + "</span>\n");
        // Comment Section
        str.Append("<div style=\"float:left; width:" + _content_left_width + "%;\">\n");
        
        // Show Comment
        str.Append("<div class=\"item\"><p>\n");
        str.Append(grp.Comment);
        str.Append("</p></div>\n");
        str.Append("<div class=\"action\">\n");
        str.Append("<div style=\"float:left;width:50%;\">\n");
        // Added Date
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("<span class=\"light\">" + UtilityBLL.Generate_Date(grp.Added_Date, this.DateFormat) + "</span>");
        }
        str.Append("</div>\n");
        str.Append("<div id=\"cmt_action_fb_" + grp.CommentID + "\"  class=\"item_r\" style=\"float:right;width:49%;\">\n");

        if (grp.ReplyID == 0 && this.ShowReplyLink)
        {
            // reply link
            str.Append("<a href=\"#\" class=\"cmt_rep\" title=\"Reply to this comment\">Reply</a>&nbsp;.&nbsp;\n");
        }
        if (this.ShowVotes)
        {
            //// comment like action to be store in span// 0: Like, 1: Unlike cmt_lk
            int likes = grp.Points;
            if (likes < 0)
                likes = 0;
            if (likes > 0)
                str.Append("<span id=\"cmtpl_" + grp.Points + "\" class=\"normal-text reverse\">" + likes + "</span>&nbsp;.&nbsp;");
            str.Append("<a id=\"cmt_lk_" + grp.CommentID + "\" class=\"cmt_vu\" href=\"#\">Like</a>&nbsp;.&nbsp;\n");
        }
        if (this.PosterUserName != "")
        {
            // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
            if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
            {
                // remove link
            }
            else
            {
                // spam link
                str.Append("<a href=\"#\" class=\"cmt_flag\" title=\"Flag for Spam\">" + Resources.vsk.flag + "</a>");
            }
        }
        else
        {
            // spam link
            str.Append("<a href=\"#\" class=\"cmt_flag\" title=\"Flag for Spam\">" + Resources.vsk.flag + "</a>");
        }

      
        str.Append("</div>\n");
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("</div>\n");
        // approved, disabled
        if (this.ShowApproved && this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";

            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";

            str.Append(_approve + "&nbsp;|&nbsp;" + _disabled);
            str.Append("</div>\n");
        }
        else if (this.ShowApproved)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";
            str.Append(_approve);
            str.Append("</div>\n");
        }
        else if (this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";
            str.Append(_disabled);
            str.Append("</div>\n");
        }
        str.Append("</div>\n"); // close float left div
        str.Append("<div id=\"cmt_action_" + grp.CommentID + "\" class=\"itm_cross\" style=\"float:right; width:" + _content_right_width + "px; display:none;\">\n");
        if (this.PosterUserName != "")
        {
            // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
            if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
            {
                // remove link
                str.Append("<a href=\"#\" class=\"cmt_remove\" title=\"Remove Comment\">X</a>");
            }
        }
        str.Append("</div>\n"); // close float right div
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("<div id=\"cmtrepmsg_" + grp.CommentID + "\"></div>\n"); // reply comment box
        str.Append("<div id=\"cmtrep_" + grp.CommentID + "\"></div>\n"); // for showing comment box
        // Action Secction

        return str.ToString();
    }
    #endregion
    private string ProcessThumb(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        // photo thumb
        string image_src =  UrlConfig.Return_Photo_Url(grp.UserName, grp.PictureName, 0, 0);
        str.Append("<a href=\"" + this.AuthorUrl + "\" title=\"" + grp.UserName + "\">"); // thumb link setup
        string border_class = this.ThumbRoundCssClass;
        if (!this.isRoundCorners)
            border_class = this.ThumbCssClass;
        str.Append("<img class=\"" + border_class + "\" src=\"" + image_src + "\" height=\"" + this.Height + "\" width=\"" + this.Width + "\">");
        str.Append("</a>");
        return str.ToString();
    }

    #region Simple Style Template
   
    private string ProcessContent_Simple(Comment_Struct grp)
    {
        StringBuilder str = new StringBuilder();
        int _content_left_width = 78; // %
        int _content_right_width = 118; // px;
        if (grp.ReplyID > 0 || !this.ShowReplyLink)
        {
            _content_left_width = _content_left_width + 4;
            _content_right_width = _content_right_width - 28;
        }

        if (!this.ShowVotes)
        {
            _content_left_width = _content_left_width + 8;
            _content_right_width = _content_right_width - 58;
        }
        str.Append("<span id=\"cmtpts_" + grp.CommentID + "\" style=\"display:none;\">" + grp.Points + "</span>\n");
        // Comment Section
        str.Append("<div style=\"float:left; width:" + _content_left_width + "%;\">\n");
        // show votes
        if (this.ShowVotes)
        {
            //// comment like action to be store in span// 0: Like, 1: Unlike cmt_lk
            int likes = grp.Points;
            if (likes < 0)
                str.Append("<span id=\"cmtpl_" + grp.CommentID + "\" class=\"badge badge-important mini-text\">" + likes + "</span>&nbsp;");
            else if (likes > 0)
                str.Append("<span id=\"cmtpl_" + grp.CommentID + "\" class=\"badge badge-warning mini-text\">" + likes + "</span>&nbsp;");
            else
                str.Append("<span id=\"cmtpl_" + grp.CommentID + "\"></span>&nbsp;"); // keep it empty in case of no votes
        }
        // Show Comment
        //str.Append("<div class=\"item\"><p>\n");
        str.Append("<span class=\"normal-text\">" + grp.Comment + "</span>");
        //str.Append("</p></div>\n");
        // show author
        if (Config.isFeature_UserName() && this.ShowAuthor)
        {
            // if username enabled
            string _usr = grp.UserName;
            if (_usr.Length > 18)
                _usr = _usr.Substring(0, 18) + "..";
            string _usr_link = "&nbsp;<a id=\"cmtusr_" + grp.CommentID + "\" class=\"" + this.BoldLinkCssClass + "\" href=\"" + this.AuthorUrl + "\"  title=\"" + grp.UserName + "\">" + _usr + "</a>&nbsp;";
            str.Append(_usr_link);
        }
        // Added Date
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("&nbsp;<span class=\"mini-text light\">" + UtilityBLL.Generate_Date(grp.Added_Date, this.DateFormat) + "</span>");
        }
       
        // approved, disabled
        if (this.ShowApproved && this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";

            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";

            str.Append(_approve + "&nbsp;|&nbsp;" + _disabled);
            str.Append("</div>\n");
        }
        else if (this.ShowApproved)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _approve = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.approved + "</span>\n";
            if (grp.isApproved == 0)
                _approve = "<span class=\"" + this.BadCssClass + "\">Not approved</span>\n";
            str.Append(_approve);
            str.Append("</div>\n");
        }
        else if (this.ShowDisabled)
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (grp.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";
            str.Append(_disabled);
            str.Append("</div>\n");
        }
        str.Append("</div>\n"); // close float left div
        //str.Append("<div id=\"cmt_action_" + grp.CommentID + "\" class=\"itm_cross\" style=\"float:right; width:" + _content_right_width + "px; display:none;\">\n");
        //if (this.PosterUserName != "")
        //{
        //    // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
        //    if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
        //    {
        //        // remove link
        //        str.Append("<a href=\"#\" class=\"cmt_remove\" title=\"Remove Comment\">X</a>");
        //    }
        //}
        //str.Append("</div>\n"); // close float right div
        if (!this.isAdmin)
        {
            str.Append("<div id=\"cmt_action_" + grp.CommentID + "\" class=\"itm_cross btn-group\" style=\"float:right; width:" + _content_right_width + "px; display:none;\">\n");
            if (this.ShowVotes)
            {
                // vote up link
                str.Append("<a href=\"#\" class=\"btn btn-warning btn-mini cmt_vu\" rel=\"tooltip\" title=\"Vote Up\"><i class=\"icon-thumbs-up icon-white\"></i></a>");
                // vote down link
                str.Append("<a href=\"#\" class=\"btn btn-warning btn-mini cmt_vd\" title=\"Vote Down\"><i class=\"icon-thumbs-down icon-white\"></i></a>");
            }
            if (grp.ReplyID == 0 && this.ShowReplyLink)
            {
                // reply link
                str.Append("<a href=\"#\" class=\"btn btn-warning btn-mini cmt_rep\" title=\"Reply to this comment\"><i class=\"icon-repeat icon-white\"></i></a>");
            }
            if (this.PosterUserName != "")
            {
                // if currently logged in user is author of post or author of comment, then shown remove comment link instead of flag link
                if (this.PosterUserName == grp.UserName || this.PosterUserName == this.AuthorUserName)
                {
                    // remove link
                    str.Append("<a href=\"#\" class=\"btn btn-warning btn-mini cmt_remove\" title=\"Remove Comment\"><i class=\"icon-trash icon-white\"></i></a>");
                }
                else
                {
                    // spam link
                    str.Append("<a href=\"#\" class=\"btn btn-warning btn-mini cmt_flag\" title=\"Flag for Spam\"><i class=\"icon-flag icon-white\"></i></a>");
                }
            }
            else
            {
                // spam link
                str.Append("<a href=\"#\" class=\"btn  btn-warning btn-mini cmt_flag\" title=\"Flag for Spam\"><i class=\"icon-flag icon-white\"></i></a>");
            }
            str.Append("</div>\n"); // close float right div
        }
        //*********************
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("<div id=\"cmtrepmsg_" + grp.CommentID + "\"></div>\n"); // reply comment box
        str.Append("<div id=\"cmtrep_" + grp.CommentID + "\"></div>\n"); // for showing comment box
        // Action Secction

        return str.ToString();
    }
    #endregion
}