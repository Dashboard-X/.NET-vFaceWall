<%@ WebHandler Language="C#" Class="pagination" %>

using System;
using System.Web;
using System.Text;
using System.Collections;

public class pagination : IHttpHandler {
   
    // Main handler responsible for loading ajax pagination
    public int PageNumber = 1;
    public int PageSize = 20;
    public int TotalRecords = 0;

    public string LoadContainer = ""; // Container id where to load reccords.
    public string LoadStatusContainer = ""; // Container where to show loading status.
    public string LoadHandler = ""; // Handler to call for fetching records.
    public string LoadParams = ""; // Handler Parameters to pass

    public bool ShowFirst = true;
    public bool ShowLast = true;
    // CssClasses
    private string showing = Resources.vsk.showing;
    private string records = Resources.vsk.records;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["p"] != null)
            this.PageNumber = Convert.ToInt32(context.Request.Params["p"]);
        if (context.Request.Params["ps"] != null)
            this.PageSize = Convert.ToInt32(context.Request.Params["ps"]);
        if (context.Request.Params["tr"] != null)
            this.TotalRecords = Convert.ToInt32(context.Request.Params["tr"]);
        if (context.Request.Params["lc"] != null)
            this.LoadContainer = context.Request.Params["lc"].ToString();
        if (context.Request.Params["lstat"] != null)
            this.LoadStatusContainer = context.Request.Params["lstat"].ToString();
        
        if (context.Request.Params["lhandler"] != null)
            this.LoadHandler = context.Request.Params["lhandler"].ToString();

        if (context.Request.Params["lparam"] != null)
            this.LoadParams = context.Request.Params["lparam"].ToString();
        
        if (context.Request.Params["sf"] != null)
            this.ShowFirst = Convert.ToBoolean(context.Request.Params["sf"]);

        if (context.Request.Params["sl"] != null)
            this.ShowLast = Convert.ToBoolean(context.Request.Params["sl"]);

        context.Response.Write(Generate_Output(context)); 
    }

    private string Generate_Output(HttpContext context)
    {
        //**********************************************************
        // Generate Output Panel
        //**********************************************************
        StringBuilder str = new StringBuilder();

        //*********************************
        // Security Check
        //*********************************
        if (context.Request.UrlReferrer == null)
        {
            return "p400"; // Authorization Failed"
        }
   
        int TotalPages = (int)Math.Ceiling((double)this.TotalRecords / this.PageSize);
        str.Append("<span style=\"display:none;\" id=\"p_trec\">" + this.TotalRecords + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"p_tpages\">" + TotalPages + "</span>\n"); // store total pages info in <span>
        str.Append("<span style=\"display:none;\" id=\"p_psize\">" + this.PageSize + "</span>\n"); // store page size infor in <span>
        str.Append("<span style=\"display:none;\" id=\"p_pnum\">" + this.PageNumber + "</span>\n"); // current page index <span>
        int firstbound = 0;
        int lastbound = 0;
        string ToolTip = "";
        if (this.PageNumber > 1)
        {
            if (this.ShowFirst)
            {
                firstbound = 1;
                lastbound = firstbound + this.PageSize - 1;
                ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
                // First Link
                str.Append("<li><a id=\"p_1\" href=\"#\" class=\"pagination-css\" title=\"" + ToolTip + "\">" + Resources.vsk.first + "</a></li>\n");
            }
            firstbound = ((TotalPages - 1) * this.PageSize);
            lastbound = firstbound + this.PageSize - 1;
            if (lastbound > this.TotalRecords)
            {
                lastbound = this.TotalRecords;
            }
            ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
            // Previous Link Enabled
            str.Append("<li><a id=\"pp_" + (this.PageNumber - 1) + "\" href=\"#\" class=\"pagination-css\" title=\"" + ToolTip + "\">" + Resources.vsk.prev + "</a></li>\n");

            // Normal Links
            str.Append(Generate_Pagination_Links(TotalPages));

            if (this.PageNumber < TotalPages)
            {
                str.Append(Generate_Previous_Last_Links(TotalPages));
            }
        }
        else
        {
            // Normal Links
            str.Append(Generate_Pagination_Links(TotalPages));
            // Next Last Links
            str.Append(Generate_Previous_Last_Links(TotalPages));
        }
        str.Append("<div class=\"clear\"></div>\n");
        // Generate Comment Output
        return str.ToString();
        //return cmt.CommentID.ToString();
    }
    
    private string Generate_Pagination_Links(int TotalPages)
    {
        StringBuilder str = new StringBuilder();
        int firstbound = 0;
        int lastbound = 0;
        string ToolTip = "";


        ArrayList arr = Pagination_Process.Return_Pagination_Link_v2(TotalPages, this.PageNumber);
        if (arr.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= arr.Count - 1; i++)
            {
                firstbound = ((int.Parse(arr[i].ToString()) - 1) * this.PageSize) + 1;
                lastbound = firstbound + this.PageSize - 1;
                if (lastbound > this.TotalRecords)
                    lastbound = this.TotalRecords;
                ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " " + Resources.vsk.of + " " + this.TotalRecords + " " + records;
                string _css = "";
                if (arr[i].ToString() == this.PageNumber.ToString())
                    _css = " class=\"active\"";
                str.Append("<li" + _css + "><a id=\"pg_" + arr[i].ToString() + "\" href=\"#\" class=\"pagination-css\" title=\"" + ToolTip + "\">" + arr[i].ToString() + "</a></li>");
            }
        }

        return str.ToString();
    }

    private string Generate_Previous_Last_Links(int TotalPages)
    {
        StringBuilder str = new StringBuilder();
        int firstbound = ((this.PageNumber) * this.PageSize) + 1;
        int lastbound = firstbound + this.PageSize - 1;
        if (lastbound > this.TotalRecords)
        {
            lastbound = this.TotalRecords;
        }
        string ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
        // Next Link
        str.Append("<li><a id=\"pn_" + (this.PageNumber + 1) + "\" href=\"#\" class=\"pagination-css\" title=\"" + ToolTip + "\">" + Resources.vsk.next + "</a></li>");
        if (this.ShowLast)
        {
            // Last Link
            firstbound = ((TotalPages - 1) * this.PageSize) + 1;
            lastbound = firstbound + this.PageSize - 1;
            if (lastbound > TotalPages)
            {
                lastbound = TotalPages;
            }
            ToolTip = showing + " " + firstbound + " - " + lastbound + " " + records + " of " + this.TotalRecords + " " + records;
            str.Append("<li><a id=\"pl_" + TotalPages + "\" href=\"#\" class=\"pagination-css\" title=\"" + ToolTip + "\">" + Resources.vsk.last + "</a></li>");
        }
        return str.ToString();

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}