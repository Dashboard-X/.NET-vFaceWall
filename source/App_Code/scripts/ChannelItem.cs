using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for ChannelItem
/// </summary>
public class ChannelItem
{
	public ChannelItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region MyRegion

    private int _boxwidth = 0; // complete width of box (0:100%)
    private int _width = 50; // width of thumb inside box
    private int _height = 50; // 0: default height of thumb inside box
    private int _leftwidth = 40; // in % in case of showing thumb left and content on right
    private int _rightwidth = 59; // in % in case of showing thumb left and content on right

    private int _space = 15; // space between two items

    private bool _ishovereffect = false; // show hover effect on mouse move
    private bool _breakline = false; // break current line

    private bool _isadmin = false; // admin mode
    private int _thumbpreviewoption = 0; // 0: thumb, 1: mid thumb, 2: original photo

    // Show Options
    private bool _thumbonly = false; // display photo thumb only without displaying any further data
    private bool _showlocationinfo = false;
    private bool _showmessagelink = false;
    private bool _showaddfriendlink = false;
    private bool _showdate = true;
    private int _dateformat = 3; //  // 0:  21 May, 2011, 1: May 30th, 2011, 2: May 11 2011, 3: 2 days ago, 4: Today 10:54 PM
    private bool _showviews = true;
    private bool _showdisabled = false;
    private bool _showusername = true;

    // urls
    private string _previewurl = ""; // url of title or thumb preview, if empty default url will be used
    private int _contentlayout = 0; // 0: content on bottom of thumb, 1: content on left side of thumb
    private int _titlelength = 0;

    // css styles
    private string _boxcssclass = "";
    private string _hovercssclass = "simplehover";
    private string _altcsslass = "simplehoveralt";
    private string _boldlinkcssclass = "normal-text bold";
    private string _normallinkcssclass = "normal-text";
    private string _altlinkcssclass = "normal-text light";
    private string _goodcssclass = "mini-text green bold";
    private string _normaltextcssclass = "normal-text";
    private string _badcssclass = "mini-text red bold";
    private string _thumbcssclass = "thumbnail";

    // captions
    private string _datecpation = "";
    private string _usercapption = "";

    public int BoxWidth
    {
        set { _boxwidth = value; }
        get { return _boxwidth; }
    }

    public int TitleLength
    {
        set { _titlelength = value; }
        get { return _titlelength; }
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

    public int BoxSpace
    {
        set { _space = value; }
        get { return _space; }
    }

    public bool isHoverEffect
    {
        set { _ishovereffect = value; }
        get { return _ishovereffect; }
    }

    public bool BreakLine
    {
        set { _breakline = value; }
        get { return _breakline; }
    }

    public bool isAdmin
    {
        set { _isadmin = value; }
        get { return _isadmin; }
    }

    public int ThumbPreviewOption
    {
        set { _thumbpreviewoption = value; }
        get { return _thumbpreviewoption; }
    }

    public bool ThumbOnly
    {
        set { _thumbonly = value; }
        get { return _thumbonly; }
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
    public bool ShowViews
    {
        set { _showviews = value; }
        get { return _showviews; }
    }

    public bool ShowDisabled
    {
        set { _showdisabled = value; }
        get { return _showdisabled; }
    }
    
    public string PreviewUrl
    {
        set { _previewurl = value; }
        get { return _previewurl; }
    }

    public int ContentLayout
    {
        set { _contentlayout = value; }
        get { return _contentlayout; }
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
    public string AltLinkCssClass
    {
        set { _altlinkcssclass = value; }
        get { return _altlinkcssclass; }
    }
    public string GoodCssClass
    {
        set { _goodcssclass = value; }
        get { return _goodcssclass; }
    }
    public string NormalTextCssClass
    {
        set { _normaltextcssclass = value; }
        get { return _normaltextcssclass; }
    }
    public string BadCssClass
    {
        set { _badcssclass = value; }
        get { return _badcssclass; }
    }

    public bool ShowLocationInfo
    {
        set { _showlocationinfo = value; }
        get { return _showlocationinfo; }
    }

    public bool ShowMessageLink
    {
        set { _showmessagelink = value; }
        get { return _showmessagelink; }
    }

    public bool ShowAddFriendLink
    {
        set { _showaddfriendlink = value; }
        get { return _showaddfriendlink; }
    }

    public bool ShowUserName
    {
        set { _showusername = value; }
        get { return _showusername; }
    }
    public string DateCaption
    {
        set { _datecpation = value; }
        get { return _datecpation; }
    }

    public string UserCaption
    {
        set { _usercapption = value; }
        get { return _usercapption; }
    }
    #endregion

    public string Process(Member_Struct ph)
    {
        if (this.PreviewUrl == "")
            this.PreviewUrl = UrlConfig.Prepare_User_Profile_Url(ph.UserName, this.isAdmin);
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
            if (this.BoxSpace > 0)
                containerwidth = this.BoxWidth - this.BoxSpace;
            _cntwidth = containerwidth + "px";
        }

        str.Append("<div class=\"" + this.BoxCssClass + "\" style=\"float:left; width:" + _bxwidth + "\">\n");
        string container = "class=\"item\"";
        if (this.isHoverEffect)
            container = "class=\"" + this.HoverCssClass + "\"";
        str.Append("<div " + container + " style=\"width:" + _cntwidth + "\">\n");
        if (this.ThumbOnly)
        {
            str.Append(ProcessThumb(ph));
        }
        else
        {
            if (this.ContentLayout == 0)
            {
                // Content on bottom of thumb
                str.Append("<div class=\"item\">\n");
                str.Append(ProcessThumb(ph));
                str.Append("</div>\n");
                str.Append("<div class=\"item\">\n");
                str.Append(ProcessContent(ph));
                str.Append("</div>\n");
            }
            else
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
        }
        str.Append("</div>\n"); // close sub box
        str.Append("</div>\n"); // close main box
        if (this.BreakLine)
        {
            str.Append("<div class=\"clear\"></div>\n");
            this.BreakLine = false;
        }

        // reset urls for next item
        this.PreviewUrl = "";
        return str.ToString();
    }

    private string ProcessThumb(Member_Struct ph)
    {
        StringBuilder str = new StringBuilder();
        if (this.ThumbCssClass == "")
            this.ThumbCssClass = "thumbnail";
        // photo thumb
        string image_src = UrlConfig.Return_User_Profile_Photo(ph.UserName, ph.PictureName, this.ThumbPreviewOption, 0);
        if (ph.PictureName == "none" || ph.PictureName == "")
            image_src = Config.GetUrl("images/dmember.png");
        else if (ph.PictureName.Contains("http"))
            image_src = ph.PictureName;

        str.Append("<a class=\"" + ThumbCssClass + "\" style=\"width:" + this.Width + "px;\" href=\"" + this.PreviewUrl + "\" title=\"" + ph.UserName + "\">"); // thumb link setup

        str.Append("<img  src=\"" + image_src + "\" height=\"" + this.Height + "\" width=\"" + this.Width + "\">");
        str.Append("</a>");
        return str.ToString();
    }

    private string ProcessContent(Member_Struct ph)
    {
        StringBuilder str = new StringBuilder();
        // username 
        str.Append("<div class=\"item_pad_2\">\n");
        if (Config.isFeature_UserName() && this.ShowUserName)
        {
            // if username enabled
            string _usr = ph.UserName;
            if (_usr.Length > this.TitleLength && this.TitleLength!=0)
                _usr = _usr.Substring(0, this.TitleLength) + "..";
            string _usr_link = "<a class=\"" + this.NormalLinkCssClass + "\" href=\"" + this.PreviewUrl + "\"  title=\"" + ph.UserName + "\">" + _usr + "</a>";

            if (this.UserCaption != "")
                str.Append("<span class=\"" + this.AltLinkCssClass + "\">" + this.UserCaption + "</span> \n");
            str.Append(_usr_link);
        }
        // show location information
        if (this.ShowLocationInfo)
        {
            str.Append("<br />");
            str.Append(ph.Gender);
            if (ph.RelationshipStatus != "")
                str.Append(", " + ph.RelationshipStatus);
            if (ph.HometTown != "" || ph.CurrentCity != "")
            {
                if (ph.CurrentCity != "")
                    str.Append(", " + ph.HometTown);
                else
                    str.Append(", " + ph.CurrentCity);
            }
            if (ph.Zipcode != "")
                str.Append(", " + ph.Zipcode);
            str.Append(", " + ph.CountryName);
        }
        // show views
        if (Config.isFeature_Views() && this.ShowViews)
        {
            string _views = "0";
            if (ph.Views > 0)
                _views = string.Format("{0:#,###}", ph.Views);

            str.Append("<br /><span class=\"" + this.AltLinkCssClass + "\">" + _views + " " + Resources.vsk.views + "</span>");
        }
        // Added
        if (Config.isFeature_Date() && this.ShowDate)
        {
            str.Append("<br />");
            if (this.DateCaption != "")
                str.Append("<span class=\"" + this.AltLinkCssClass + "\">" + this.DateCaption + " </span>");
            str.Append("<span class=\"" + this.AltLinkCssClass + "\">Joined " + UtilityBLL.Generate_Date(ph.RegisterDate, this.DateFormat) + "</span>");
        }
        if (this.ShowDisabled)
        {
            string _disabled = "<span class=\"" + this.GoodCssClass + "\">" + Resources.vsk.enabled + "</span>\n";
            if (ph.isEnabled == 0)
                _disabled = "<span class=\"" + this.BadCssClass + "\">Disabled</span>\n";
            str.Append("<br />" + _disabled);
        }
        str.Append("</div>\n");
        return str.ToString();
    }
}