<%@ WebHandler Language="C#" Class="dpost" %>

using System;
using System.Web;
using System.Text;

public class dpost : IHttpHandler {
    
     public long ActivityID = 0; 
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["aid"] != null)
            this.ActivityID = Convert.ToInt64(context.Request.Params["aid"]);
    
        context.Response.Write(Process_Data(context));
    }

    private string Process_Data(HttpContext context)
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

        if (this.ActivityID == 0)
        {
            return "p101";
        }

        ActivityBLL.Delete(this.ActivityID);

        return "0";
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}