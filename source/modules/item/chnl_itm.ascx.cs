using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text;

public partial class modules_item_chnl_itm : System.Web.UI.UserControl
{
    protected string Item = "";

    #region MyRegion
    private Member_Struct _ph = null;
    public Member_Struct PH
    {
        set { _ph = value; }
        get { return _ph; }
    }
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


    protected void Page_PreRender(object sender, EventArgs e)
    {
        ChannelItem itm = new ChannelItem();
        itm.BoxWidth = this.BoxWidth;
        itm.TitleLength =this.TitleLength;
        itm.Width=this.Width;
        itm.LeftWidth =this.LeftWidth;
        itm.RightWidth = this.RightWidth;
        itm.Height=this.Height;
        itm.BoxSpace =this.BoxSpace;
        itm.isHoverEffect=this.isHoverEffect;
        itm.BreakLine=this.BreakLine;
        itm.isAdmin=this.isAdmin;
        itm.ThumbPreviewOption = this.ThumbPreviewOption;
        itm.ThumbOnly=this.ThumbOnly;
        itm.ShowDate =this.ShowDate;
        itm.DateFormat=this.DateFormat;
        itm.ShowViews=this.ShowViews;
        itm.ShowDisabled=this.ShowDisabled;
        itm.PreviewUrl=this.PreviewUrl;
        itm.ContentLayout=this.ContentLayout;
        itm.BoxCssClass=this.BoxCssClass;
        itm.HoverCssClass=this.HoverCssClass;
        itm.AltCssClass=this.AltCssClass;
        itm.ThumbCssClass=this.ThumbCssClass;
        itm.BoldLinkCssClass=this.BoldLinkCssClass;
        itm.NormalLinkCssClass=this.NormalLinkCssClass;
        itm.AltLinkCssClass=this.AltLinkCssClass;
        itm.GoodCssClass=this.GoodCssClass;
        itm.NormalTextCssClass=this.NormalTextCssClass;
        itm.BadCssClass=this.BadCssClass;
        itm.ShowLocationInfo=this.ShowLocationInfo;
        itm.ShowMessageLink=this.ShowMessageLink;
        itm.ShowAddFriendLink=this.ShowAddFriendLink;
        itm.ShowUserName=this.ShowUserName;
        itm.DateCaption=this.DateCaption;
        itm.UserCaption=this.UserCaption;

        Item = itm.Process(this.PH);
    }
     
}
