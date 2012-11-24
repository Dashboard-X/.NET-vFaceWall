using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class modules_list_channels_v2 : System.Web.UI.UserControl
{
    private members _members = new members();
    #region Properties

    // New Properties
    public string Term = ""; // filter channels based on term
    public string Character = ""; // filter channels based on first character
    // Old Properties

    private int _tabindex = 0; // recently added, 1: most viewed, 2: most favorited 3: top rated
    private int _pagenumber = 1;
    private int _totalrecords = 0;
    private int _pagesize = 20;
    private string _order = "";
    private int _filter = 0; // All Time
    private string _accounttype = "all";
    private bool _isnavigation = true;

    public int PageNumber
    {
        get { return _pagenumber; }
        set { _pagenumber = value; }
    }

    public int TotalRecords
    {
        get { return _totalrecords; }
        set { _totalrecords = value; }
    }

    public int PageSize
    {
        get { return _pagesize; }
        set { _pagesize = value; }
    }

    public int TabIndex
    {
        get
        {
            return _tabindex;
        }
        set
        {
            _tabindex = value;
        }
    }

    public string Order
    {
        get { return _order; }
        set { _order = value; }
    }

    public string AccountType
    {
        get { return _accounttype; }
        set { _accounttype = value; }
    }

    public bool isNavigation
    {
        get { return _isnavigation; }
        set { _isnavigation = value; }
    }
    
    public int Filter
    {
        get { return _filter; }
        set { _filter = value; }
    }

    // New Properties
    public string Gender = "";
    public string Country = "";
    public bool HavePhoto = false;
    public bool AdvList = false;

    public string HeadingTitle = "";
    public string Default_Url = "";
    public string Pagination_Url = "";
    public string Filter_DefaultUrl = "";
    public string Filter_Pagination_Url = "";

    public string NoRecordFound = Resources.vsk.nochannelfound;
    public bool isCache = true;
    public bool isListStatus = true;
    public string Thumb_Url = "";
    public string PageDescription = "";
    // Searh Option
    public bool EnableSearch = false;
    public string SearchPlaceHolder = "Search Members";
    public string SearchUrl // keep it in viewstate to avoid postpack issue
    {
        get
        {
            if (ViewState["SearchUrl"] != null)
                return ViewState["SearchUrl"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["SearchUrl"] = value;
        }
    }
    // group related
    public long GroupID = 0;
    public int GroupStatus = 0; // 0: allowed, 1: pending approval
    public int GroupType = 3; // 0: normal member, 1: owner of group, 2: group admin, 3: all
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (EnableSearch)
        {
            txt_search.Attributes.Add("placeholder", this.SearchPlaceHolder);
            pnl_search.Visible = true;
        }
        else
            pnl_search.Visible = false;

        if (!Page.IsPostBack)
        {
            this.PageSize = Site_Settings.Channel_Page_Size;

            // Pagination
            if (Request.Params["p"] != null && Request.Params["p"] != "")
            {
                this.PageNumber = Convert.ToInt32(Request.Params["p"]);
            }

            if(Request.Params["query"]!=null)
            {
                this.Term = Request.Params["query"].ToString();
            }

            if (Request.Params["char"] != null)
            {
                this.Character = Request.Params["char"].ToString();
            }
                     

            // Set Top Navigation
            if (this.isNavigation)
            {
                // Set Active Navigation
                Set_Nav_CSS();
                isnav.Visible = true;
            }
            else
                isnav.Visible = false;

            // Bind List
            BindList();

            Set_Title();
        }
    }

    private void BindList()
    {
        StringBuilder str = new StringBuilder();
        this.TotalRecords = 0;
        this.TotalRecords = _members.Count_Channels(this.Term, this.Character, this.AccountType, this.Country, this.Gender, this.HavePhoto, this.Filter, this.isCache);
        if (this.TotalRecords == 0)
        {
            // no records found
            pg.Visible = false;
            str.Append("<div class=\"bx_norecord\">");
            str.Append("<h4>" + NoRecordFound + "</h4>");
            str.Append("</div>");
            lst.InnerHtml = str.ToString();
            return;
        }
        List<Member_Struct> _list = null;
        _list = _members.Load_Channels_ADV(this.Term, this.Character, this.AccountType, this.Country, this.Gender, this.HavePhoto, this.Filter, this.Order, this.PageNumber, this.PageSize, this.AdvList, this.isCache);
        if (_list.Count > 0)
        {
            if (Site_Settings.Pagination_Type == 2)
            {
                // sql server 2000 compatibility
                this.TotalRecords = _list.Count;
            }

            // Maximum Pagination Restriction for Main Listing
            int maximum_allowed_records = Site_Settings.Pagination_Links * this.PageSize;
            if (this.TotalRecords > maximum_allowed_records)
                this.TotalRecords = maximum_allowed_records;

            if (this.isListStatus)
            {
                // List Statistic Display
                list_stat1.TotalRecords = this.TotalRecords;
                list_stat1.PageSize = this.PageSize;
                list_stat1.PageNumber = this.PageNumber;
            }
            else
            {
                // disable showing list status on top of page
                lstat.Visible = false;
            }

            if (this.TotalRecords > this.PageSize)
            {
                pagination1.TotalRecords = this.TotalRecords;
                pagination1.PageSize = this.PageSize;
                pagination1.PageNumber = this.PageNumber;
                pagination1.Default_Url = this.Default_Url;
                pagination1.Pagination_Url = this.Pagination_Url;
                if (this.Filter > 0)
                {
                    pagination1.isFilter = true;
                    pagination1.Filter_Default_Url = this.Filter_DefaultUrl;
                    pagination1.Filter_Pagination_Url = this.Filter_Pagination_Url;
                }
                pagination1.BindPagination();
            }
            else
            {
                pg.Visible = false;
            }

           
            // generate channel listing 
            int i = 0;
            int counter = 0;
            //int total_columns = 6;

            ChannelItem itm = new ChannelItem();
            if (this.AdvList)
            {
                // advance listing options
                itm.ContentLayout = 1; // left / right side listing type
                itm.LeftWidth = 13;
                itm.RightWidth = 86;
                itm.NormalLinkCssClass = "xxmedium-text bold";
                itm.AltLinkCssClass = "normal-text light";
                itm.BoxCssClass = "bx_br_bt item_pad_4";
                itm.isHoverEffect = true;
                itm.ShowLocationInfo = true;
                itm.Height = 64;
                itm.Width = 64;
                itm.ShowViews = true;
                itm.ShowDate = true;
            }
            else
            {
                // normal listing options
                itm.ShowDate = false;
                itm.isHoverEffect = false;
                itm.NormalLinkCssClass = "mini-text bold";
                itm.AltLinkCssClass = "mini-text light";
                itm.BoxWidth = 75;
                itm.BoxSpace = 10;
                itm.Width = 62;
                itm.Height = 62;
                itm.TitleLength = 8;
                itm.ShowViews = true;
            }
            for (i = 0; i <= _list.Count - 1; i++)
            {
                counter = counter + 1;
                str.Append(itm.Process(_list[i]));
            }
            str.Append("<div class=\"clear\"></div>"); // clear floating items
        }
        else
        {
            pg.Visible = false;
            str.Append("<div class=\"bx_norecord\">");
            str.Append("<h4>" + NoRecordFound + "</h4>");
            str.Append("</div>");
        }

        lst.InnerHtml = str.ToString();
    }

    protected void MyList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

        }
    }

    private void Set_Nav_CSS()
    {
        switch (this.TabIndex)
        {
            case 0:
                list_nav1.ActiveLink = 0;
                break;
            case 1:
                list_nav1.ActiveLink = 1;

                break;

        }
    }

    private void Set_Title()
    {
        string accounttitle = "";
        if (Request.Params["type"] != null)
            accounttitle = UtilityBLL.ReplaceHyphinWithSpace(Request.Params["type"].ToString()).Trim();
        string _accounttype = UtilityBLL.UppercaseFirst(accounttitle);

        string _filter = "";
        switch (this.Filter)
        {
            case 1:
                _filter =" - " + Resources.vsk.todayadded; // "Added Today";
                break;
            case 2:
                _filter =" - " + Resources.vsk.thisweekadded; // "Added This Week";
                break;
            case 3:
                _filter =" - " + Resources.vsk.thismonthadded; // "Added This Month";
                break;
        }

        string _order = "Recent Profiles";
        string _keyword = "recently added profiles";
        switch (this.TabIndex)
        {
            case 1:
                _order = "Most Viewed Profiles"; // "Most Viewed Profiles";
                _keyword = "most viewed profiles";
                break;
        }

        string _title = _order;
        if (_accounttype != "")
            _title = _order + " in " + _accounttype + " " + _filter;
        else
            _title = _order + " " + _filter;
        if (this.PageNumber > 1)
            _title = _title + " - Page " + this.PageNumber;

        if (this.HeadingTitle != "")
            _title = this.HeadingTitle;
        
        lbl_status.InnerHtml = _title;
        string meta_title = _title;

        string meta_description = ""; 
        
        if(this.PageDescription!="")
            meta_description = this.PageDescription;
        else
            meta_description = "Browse " + _keyword + " in " + _accounttype + " " + _filter;
        if (PageNumber > 1)
            meta_description = meta_description + ", Page: " + PageNumber;

        string _turl = "";
        if (this.Thumb_Url != "")
            _turl = this.Thumb_Url;
        else
            _turl = UrlConfig.Return_Website_Logo_Url();
        MetaTagsBLL.META_StaticPage(this.Page, (HtmlHead)Page.Header, meta_title, meta_description, UrlConfig.Return_Website_Logo_Url(), Config.Return_Current_Page());
        
    }

    protected void btn_search_Click1(object sender, EventArgs e)
    {
        if (this.SearchUrl != "" && txt_search.Text != "")
        {
            string separator = "?";
            if (this.SearchUrl.Contains("?"))
                separator = "&";
            Response.Redirect(this.SearchUrl + "" + separator + "query=" + Server.UrlEncode(UtilityBLL.CleanSearchTerm(txt_search.Text)));
        }
    }
}