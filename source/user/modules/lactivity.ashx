<%@ WebHandler Language="C#" Class="lactivity" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;


public class lactivity : IHttpHandler {

    // This handler is responsible for posting comment on item.
    private long ContentID = 0;
    private string UserName = "";
    private string Term = "";
    private int Month = 0;
    private int Year = 0;
    private int Type = 0;
    private int AddedFilter = 0;
    private bool ShowAuthorPhoto = false;
    private string UserProfilePictureName = "none"; // default setting for user profile photo
    private int Height = 0;
    private int Width = 0;
    private bool isAdmin = false;
    private int PageNumber = 1;
    private int PageSize = 20;
    private int TotalRecords = 0;
    private string Order = "";
    private int TotalPages = 0;
    private bool HoverEffect = true;
    private bool isCache = false;
    private int _content_left_width = 0;
    private int _content_right_width = 0;

    private string PostUrl = "";
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        if (context.Request.Params["tp"] != null)
            this.Type = Convert.ToInt32(context.Request.Params["tp"]);
        if (context.Request.Params["usr"] != null)
            this.UserName = context.Request.Params["usr"].ToString();
        if (context.Request.Params["term"] != null)
            this.UserName = context.Request.Params["term"].ToString();
        if (context.Request.Params["mn"] != null)
            this.Month = Convert.ToInt32(context.Request.Params["mn"]);
        if (context.Request.Params["yr"] != null)
            this.Year = Convert.ToInt32(context.Request.Params["yr"]);
        if (context.Request.Params["wd"] != null)
            this.Width = Convert.ToInt32(context.Request.Params["wd"]);
        if (context.Request.Params["ht"] != null)
            this.Height = Convert.ToInt32(context.Request.Params["ht"]);
        if (context.Request.Params["sphoto"] != null)
            this.ShowAuthorPhoto = Convert.ToBoolean(context.Request.Params["sphoto"]);
        if (context.Request.Params["photo"] != null)
            this.UserProfilePictureName = context.Request.Params["photo"].ToString();
        if (context.Request.Params["isadm"] != null)
            this.isAdmin = Convert.ToBoolean(context.Request.Params["isadm"]);
     
        if (context.Request.Params["p"] != null)
            this.PageNumber = Convert.ToInt32(context.Request.Params["p"]);
        if (context.Request.Params["ps"] != null)
            this.PageSize = Convert.ToInt32(context.Request.Params["ps"]);
        if (context.Request.Params["o"] != null)
            this.Order = context.Request.Params["o"].ToString();
        if (context.Request.Params["tpages"] != null)
            this.TotalPages = Convert.ToInt32(context.Request.Params["tpages"]);

        if (context.Request.Params["ishover"] != null)
            this.HoverEffect = Convert.ToBoolean(context.Request.Params["ishover"]);

        if (context.Request.Params["clwidth"] != null)
            this._content_left_width = Convert.ToInt32(context.Request.Params["clwidth"]);

        if (context.Request.Params["crwidth"] != null)
            this._content_right_width = Convert.ToInt32(context.Request.Params["crwidth"]);

        if (context.Request.Params["afilter"] != null)
            this.AddedFilter = Convert.ToInt32(context.Request.Params["afilter"]);
        
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

        //if (!context.Request.UrlReferrer.Host.Contains("URLMATCHINGWORD"))
        //{
        //    return "401"; // Double Authorization
        //}

        this.TotalRecords = ActivityBLL.Count_Activities(this.Term, this.UserName, this.Month, this.Year, this.Order, this.PageSize, this.AddedFilter);
        if (this.TotalRecords == 0)
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>No More User Activities Found!</h4>");
            str.Append("</div>");
            return str.ToString();
        }

        List<UserActivity_Struct> _list = ActivityBLL.Load_Activities(this.Term, this.UserName, this.Month, this.Year, this.Order, this.PageSize, this.AddedFilter, this.isCache, this.PageNumber);
        if (_list.Count > 0)
        {
            int i = 0;
            ActivityItem act = new ActivityItem();
            act.LeftWidth = this._content_left_width; // profile layout style
            act.RightWidth = this._content_right_width;
            act.Width = 50; // profile thumb height
            act.Height = 50;
            act.isHoverEffect = true;
            act.ShowProfileThumb = true; // show user profile thumb on right side.
            for (i = 0; i <= _list.Count - 1; i++)
            {
                PostUrl = UrlConfig.Prepare_User_Profile_Url(_list[i].UserName, "activity", this.isAdmin) + "?id=" + _list[i].ActivityID;
                // generate list
                str.Append(act.Process(_list[i]));
                str.Append("<div class=\"clear\"></div>"); // clear floating items
            }
            str.Append("<div class=\"clear\"></div>"); // clear floating items
            //// store comment flags
            //int TotalPages = 1;
            //if (this.TotalRecords >= this.PageSize)
            //    TotalPages = (int)Math.Ceiling((double)TotalRecords / this.PageSize);
            //str.Append("<span style=\"display:none;\" id=\"cmt_tcmts\">" + this.TotalRecords + "</span>\n"); // store total pages info in <span>
            //str.Append("<span style=\"display:none;\" id=\"cmt_tpages\">" + TotalPages + "</span>\n"); // store total pages info in <span>
            //str.Append("<span style=\"display:none;\" id=\"cmt_psize\">" + PageSize + "</span>\n"); // store page size infor in <span>
            //str.Append("<span style=\"display:none;\" id=\"cmt_pnum\">1</span>\n"); // current page mark as 0, no page loaded in start
            //str.Append("<div id=\"cmt_load_cnt\"></div>\n"); // load more comment container
            //str.Append("<div id=\"cmt_loading\"></div>\n"); // show loading progres
            //if (TotalPages > 1)
            //{
            //    str.Append("<div class=\"item_pad_4 bx_br_both actmore\" style=\"padding:5px 0px;\">\n");
            //    str.Append("<a id=\"actldmore\" href=\"#\" class=\"bold actldmore\" title=\"load more comments\"><i class=\"icon-chevron-down icon-white\"></i> Load more...</a>\n");
            //    str.Append("</div>\n");
            //}
        }
        else
        {
            str.Append("<div class=\"bx_msg\">");
            str.Append("<h4>No More User Activities Found!</h4>");
            str.Append("</div>");
        }
        // Generate Comment Output
        return str.ToString();
        //return cmt.CommentID.ToString();
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}