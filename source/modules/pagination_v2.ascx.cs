using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;

public partial class modules_pagination_v2 : System.Web.UI.UserControl
{
    public int PageNumber = 1;
    public int PageSize = 20;
    public int TotalRecords = 0;
    public string Default_Url;
    public string Pagination_Url;
    public bool isFilter;
    public string Filter_Default_Url;
    public string Filter_Pagination_Url;
   
    // CssClasses

    private string showing = Resources.vsk.showing;
    private string records = Resources.vsk.records;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindPagination()
    {
        StringBuilder str = new StringBuilder();
        int TotalPages = (int)Math.Ceiling((double)this.TotalRecords / this.PageSize);
        int firstbound = 0;
        int lastbound = 0;
        string _rooturl = Config.GetUrl();
        string ToolTip = "";
        if (this.PageNumber > 1)
        {
            firstbound = 1;
            lastbound = firstbound + this.PageSize - 1;
            ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
            // First Link
            string FirstLinkUrl = _rooturl + "" + this.Default_Url;
            str.Append("<li><a href=\"" + FirstLinkUrl + "\" title=\"" + ToolTip + "\"><i class=\"icon-backward\"></i></a></li>\n");
            firstbound = ((TotalPages - 1) * this.PageSize);
            lastbound = firstbound + this.PageSize - 1;
            if (lastbound > this.TotalRecords)
            {
                lastbound = this.TotalRecords;
            }
            ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
            // Previous Link Enabled
            string PreviousNavigationUrl = "";
            int _prevpage = PageNumber - 1;
            if (this.isFilter)
            {
                if (this.PageNumber > 2)
                    PreviousNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Filter_Pagination_Url, _prevpage.ToString());
                else
                    PreviousNavigationUrl = _rooturl + "" + this.Filter_Default_Url;
            }
            else
            {
                if (this.PageNumber > 2)
                    PreviousNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Pagination_Url, _prevpage.ToString());
                else
                    PreviousNavigationUrl = _rooturl + "" + this.Default_Url;
            }
            str.Append("<li><a href=\"" + PreviousNavigationUrl + "\" title=\"" + ToolTip + "\"><i class=\"icon-arrow-left\"></i></a></li>\n");

            // Normal Links
            str.Append(Generate_Pagination_Links(TotalPages,_rooturl));

            if (this.PageNumber < TotalPages)
            {
                str.Append(Generate_Previous_Last_Links(TotalPages, _rooturl));
            }
        }
        else
        {
            // Normal Links
            str.Append(Generate_Pagination_Links(TotalPages, _rooturl));
            // Next Last Links
            str.Append(Generate_Previous_Last_Links(TotalPages, _rooturl));
        }
        plnks.InnerHtml = "<ul>\n" + str.ToString() + "</ul>\n";
    }

    private string Generate_Pagination_Links(int TotalPages, string _rooturl)
    {
        StringBuilder str = new StringBuilder();
        int firstbound = 0;
        int lastbound = 0;
        string ToolTip = "";


        ArrayList arr = Pagination_Process.Return_Pagination_Link_v2(TotalPages, this.PageNumber);
        if (arr.Count > 0)
        {
            int i = 0;
            string LinkURL = "";
            string Item = "";
            for (i = 0; i <= arr.Count - 1; i++)
            {
                Item = arr[i].ToString();
                firstbound = ((int.Parse(Item) - 1) * this.PageSize) + 1;
                lastbound = firstbound + this.PageSize - 1;
                if (lastbound > this.TotalRecords)
                    lastbound = this.TotalRecords;
                ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " " + Resources.vsk.of + " " + this.TotalRecords + " " + records;
                // url settings
                // normal search
                if (Item == "1")
                {
                    if (this.isFilter) // videos/MostViewed/ThisWeek.aspx
                        LinkURL = Config.GetUrl() + "" + this.Filter_Default_Url;
                    else // videos/MostViewed.aspx
                        LinkURL = Config.GetUrl() + "" + this.Default_Url;
                }
                else
                {
                    if (this.isFilter) // videos/MostViewed/232/ThisWeek.aspx
                        LinkURL = Config.GetUrl() + "" + UtilityBLL.Add_PageNumber(this.Filter_Pagination_Url, Item);
                    else // videos/232/MostViewed.aspx
                        LinkURL = Config.GetUrl() + "" + UtilityBLL.Add_PageNumber(this.Pagination_Url, Item);
                }
                string _css = "";
                if (arr[i].ToString() == this.PageNumber.ToString())
                    _css = "class=\"active\"";
                str.Append("<li " + _css + "><a href=\"" + LinkURL + "\" title=\"" + ToolTip + "\">" + arr[i].ToString() + "</a></li>\n");
            }
        }

        return str.ToString();
    }

    private string Generate_Previous_Last_Links(int TotalPages, string _rooturl)
    {
        StringBuilder str = new StringBuilder();
        string LastNavigationUrl = "";
        string NextNavigationUrl = "";
        int _nextpage = PageNumber + 1;
     
        if (this.isFilter)
        {
            LastNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Filter_Pagination_Url, TotalPages.ToString());
            NextNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Filter_Pagination_Url, _nextpage.ToString());
        }
        else
        {
            LastNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Pagination_Url, TotalPages.ToString());
            NextNavigationUrl = _rooturl + "" + UtilityBLL.Add_PageNumber(this.Pagination_Url, _nextpage.ToString());
        }

        int firstbound = ((TotalPages - 1) * this.PageSize) + 1;
        int lastbound = firstbound + this.PageSize - 1;
        if (lastbound > this.TotalRecords)
        {
            lastbound = this.TotalRecords;
        }
        string ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
        // Next Link
        str.Append("<li><a href=\"" + NextNavigationUrl + "\" title=\"" + ToolTip + "\"><i class=\"icon-arrow-right\"></i></a></li>\n");
        // Last Link
        ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
        str.Append("<li><a href=\"" + LastNavigationUrl + "\"  title=\"" + ToolTip + "\"><i class=\"icon-forward\"></i></a></li>\n");

        return str.ToString();

    }
}