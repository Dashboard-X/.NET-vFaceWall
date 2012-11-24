<%@ WebHandler Language="C#" Class="autocomplete" %>

using System;
using System.Web;

public class autocomplete : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string query = "";
        if (context.Request.Params["query"] != null && context.Request.Params["query"] != "")
            query = context.Request.Params["query"];

        string jsonoutput = members.Load_User_AutoComplete(query);
        
        context.Response.Write(jsonoutput);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}