<%@ WebHandler Language="C#" Class="rcmt" %>

using System;
using System.Web;
using System.Text;

public class rcmt : IHttpHandler {
    
    private long ContentID = 0;
    private string ProfileID = "";
    private long CommentID = 0;
    private int Type = 0;
    private int TotalComments = 0;
    private string PosterID = ""; // Currently Logged IN User
    private string UserProfilePictureName = "none"; // default setting for user profile photo
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        if (context.Request.Params["cid"] != null)
            this.CommentID = Convert.ToInt64(context.Request.Params["cid"]);
        if (context.Request.Params["tp"] != null)
            this.Type = Convert.ToInt32(context.Request.Params["tp"]);
        if (context.Request.Params["tcmt"] != null)
            this.TotalComments = Convert.ToInt32(context.Request.Params["tcmt"]);
        if (context.Request.Params["prf"] != null)
            this.ProfileID = context.Request.Params["prf"].ToString();
        if (context.User.Identity.IsAuthenticated)
            this.PosterID = context.User.Identity.Name;
        context.Response.Write(Generate_Output(context));
    }

    private string Generate_Output(HttpContext context)
    {
        //**********************************************************
        // Generate Output Panel
        //**********************************************************
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

        if (this.ContentID == 0)
        {
            //return Config.SetHiddenMessage("Error occured while posting your comment", this.ElementID, true); // wrap output in round box
            return "p100"; //
        }

        if (this.CommentID == 0)
        {
            return "p101";
        }

        if (this.PosterID == "")
        {
            return "p102";
        }
        
        // Delete Comment
        CommentsBLL.Delete(this.CommentID, this.Type, this.ContentID, this.ProfileID, this.TotalComments);

        return "0";
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

}