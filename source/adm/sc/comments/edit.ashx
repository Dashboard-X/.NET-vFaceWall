<%@ WebHandler Language="C#" Class="edit" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

public class edit : IHttpHandler {

    private long CommentID = 0;
    private string Comment = "";
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["cid"] != null)
            this.CommentID = Convert.ToInt64(context.Request.Params["cid"]);
        if (context.Request.Params["cmt"] != null)
            this.Comment = context.Request.Params["cmt"].ToString();

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
        if (this.CommentID == 0)
        {
            return "P100";
        }
        if (this.Comment == "")
        {
            return "p101";
        }
        if (UtilityBLL.isLongWordExist(this.Comment))
        {
            return "p103";
        }

        string _cmt = UtilityBLL.StripHTML(this.Comment).Trim();
        if (_cmt == "")
            return "p104";
        _cmt = UtilityBLL.CompressCodeBreak(_cmt);
        _cmt = UtilityBLL.GenerateLink(_cmt, true);
        CommentsBLL.Update(this.CommentID, _cmt);

        return "0"; // succeed
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}