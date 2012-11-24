<%@ WebHandler Language="C#" Class="spam" %>

using System;
using System.Web;
using System.Text;
public class spam : IHttpHandler {

    private long ContentID = 0;
    private string ProfileID = "";
    private string AuthorID = "";
    private string PosterID = "";
    private int Type = 4; // Comment Type
    private string Reason = ""; // Reason of posting comment
    private long CommentID = 0;

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        
        if (context.Request.Params["prf"] != null)
            this.ProfileID = context.Request.Params["prf"].ToString();
        
        if (context.Request.Params["cid"] != null)
            this.CommentID = Convert.ToInt64(context.Request.Params["cid"]);

        if (context.Request.Params["ausr"] != null)
            this.AuthorID = context.Request.Params["ausr"].ToString();
     
        if (context.User.Identity.IsAuthenticated)
            this.PosterID = context.User.Identity.Name;

        if (context.Request.Params["tp"] != null)
            this.Type = Convert.ToInt32(context.Request.Params["tp"]);
        
        if (context.Request.Params["reason"] != null)
            this.Reason = context.Request.Params["reason"].ToString();
        
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
        
        if (this.ContentID == 0)
        {
            return "p103";
        }
        if (this.PosterID == "")
        {
            //return Config.SetHiddenMessage("Sign In or Sign Up to mark selected comment as SPAM", this.ElementID, true); // wrap output in round box
            return "p100";
        }

        if (this.PosterID == this.AuthorID)
        {
            //return Config.SetHiddenMessage("Can't mark your own comment as SPAM", this.ElementID, true); // wrap output in round box
            return "p101";
        }

        //if (AbuseReport.Check_UserName(this.PosterID, this.CommentID, this.Type))
        //{
        //    //return Config.SetHiddenMessage("You already marked selected comment as SPAM", this.ElementID, true); // wrap output in round box
        //    return "p102";
        //}

        string ipaddress = context.Request.ServerVariables["REMOTE_ADDR"];
        //if (AbuseReport.Check_IPAddress(ipaddress, this.ContentID, type))
        //{
        //    return "p103";
        //}

        //AbuseReport.Add(this.CommentID, this.PosterID, ipaddress, "SPAM", this.Type);

        //int count_reports = AbuseReport.Count(this.CommentID, this.Type);
        //if (count_reports > Config.Return_AbuseReport_Count())
        //{
        //    // Disable content if more than maximum allowed spam report comes against any content
        //    int OldValue = 1; // content enabled before
        //    int NewValue = 0; // content disabled now
        //    CommentsBLL.Update_Action(this.CommentID,OldValue,NewValue, this.ContentID,this.ProfileID, this.Type, "isenabled", true);
        //}

        return "0";
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}