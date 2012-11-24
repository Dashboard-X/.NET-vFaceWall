<%@ WebHandler Language="C#" Class="load" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

public class load : IHttpHandler {
    
    // This handler is responsible for posting comment on item.
    private long ContentID = 0;
    private string ProfileID = "";
    private string AuthorID = "";
    private string PosterID = "";
    private int Type = 0;
    private bool ShowAuthorPhoto = false;
    private string UserProfilePictureName = "none"; // default setting for user profile photo
    private int Height = 0;
    private int Width = 0;
    private bool isAdmin = false;
    private int PageNumber = 1;
    private int PageSize = 20;
    private string Order = "";
    private int TotalPages = 0;
    private bool ShowReply = true;
    private bool ShowVotes = true;
    private bool HoverEffect = true;
    private int TemplateID = 0;

    private int _content_left_width = 0;
    private int _content_right_width = 0;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        if (context.Request.Params["tp"] != null)
            this.Type = Convert.ToInt32(context.Request.Params["tp"]);
        if (context.Request.Params["ausr"] != null)
            this.AuthorID = context.Request.Params["ausr"].ToString();
        if (context.Request.Params["prf"] != null)
            this.ProfileID = context.Request.Params["prf"].ToString();
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
        if (context.User.Identity.IsAuthenticated)
            this.PosterID = context.User.Identity.Name;

        if (context.Request.Params["p"] != null)
            this.PageNumber = Convert.ToInt32(context.Request.Params["p"]);
        if (context.Request.Params["ps"] != null)
            this.PageSize = Convert.ToInt32(context.Request.Params["ps"]);
        if (context.Request.Params["o"] != null)
            this.Order = context.Request.Params["o"].ToString();
        if (context.Request.Params["tpages"] != null)
            this.TotalPages = Convert.ToInt32(context.Request.Params["tpages"]);

        if (context.Request.Params["irep"] != null)
            this.ShowReply = Convert.ToBoolean(context.Request.Params["irep"]);

        if (context.Request.Params["ivt"] != null)
            this.ShowVotes = Convert.ToBoolean(context.Request.Params["ivt"]);

        if (context.Request.Params["tmlid"] != null)
            this.TemplateID = Convert.ToInt32(context.Request.Params["tmlid"]);
        
        if (context.Request.Params["ishover"] != null)
            this.HoverEffect = Convert.ToBoolean(context.Request.Params["ishover"]);
        
        if (context.Request.Params["clwidth"] != null)
            this._content_left_width = Convert.ToInt32(context.Request.Params["clwidth"]);

        if (context.Request.Params["crwidth"] != null)
            this._content_right_width = Convert.ToInt32(context.Request.Params["crwidth"]);
        
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

        List<Comment_Struct> _list = CommentsBLL.Fetch_Comments_V2(this.ContentID, this.ProfileID, this.Type,this.PageNumber, this.PageSize, this.Order, this.ShowAuthorPhoto);
        if (_list.Count > 0)
        {
            // load comments
            int i = 0;
            for (i = 0; i <= _list.Count - 1; i++)
            {
                // set post layout
                CmtItem postitem = new CmtItem();
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
                postitem.AuthorUserName = this.AuthorID; // Author Of Post
                postitem.PosterUserName = this.PosterID; // Currently logged in User
                postitem.ShowReplyLink = this.ShowReply;
                postitem.ShowVotes = this.ShowVotes;
                postitem.isHoverEffect = this.HoverEffect;
                postitem.TemplateID = this.TemplateID;
                str.Append(postitem.Process(_list[i]));
                if(_content_left_width>0)
                    postitem._content_left_width = 76; // %
                if(_content_right_width>0)
                    postitem._content_right_width = 118; // px
            }
        }
        else
        {
            if (this.PageNumber == 1)
                str.Append("<h4>No comments yet posted!</h4>");
            else
            {
                str.Append("<div class=\"bx_msg bx_br_tp\">");
                str.Append("<h4>No more comments!</h4>");
                str.Append("</div>");
            }
        }
        // Generate Comment Output
        return str.ToString();
        //return cmt.CommentID.ToString();
    }


    
    public bool IsReusable {
        get {
            return false;
        }
    }

}