<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>

<script runat="server">

    //private Config _config = new Config();
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        // Add default configuration if not exist
     
    }

    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        ////// Code that runs when an unhandled error occurs
        //Exception objErr = Server.GetLastError().GetBaseException();
        //string err = "<b>Error in: </b>" + Request.Url.ToString() + "<br /><br /><b>Error Message: </b>" + objErr.Message.ToString() + "<br /><br /><b>Stack Trace:</b><br />" + objErr.StackTrace.ToString();
        //// store error report in database
        //ErrorLgBLL.Add_Log(objErr.Message.ToString(), Request.Url.ToString(), objErr.StackTrace.ToString());
        ////Session("error") = err;
        //Server.ClearError();
        //Response.Redirect(Config.GetUrl() + "Error.aspx");
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        //Response.Redirect("Website-Down.aspx");
        

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    //protected void Application_PreSendRequestHeaders()
    //{
    //    // ensure that if GZip/Deflate Encoding is applied that headers are set
    //    // also works when error occurs if filters are still active
    //    HttpResponse response = HttpContext.Current.Response;
    //    if (response.Filter is System.IO.Compression.GZipStream && response.Headers["Content-encoding"] != "gzip")
    //        response.AppendHeader("Content-encoding", "gzip");
    //    else if (response.Filter is System.IO.Compression.DeflateStream && response.Headers["Content-encoding"] != "deflate")
    //        response.AppendHeader("Content-encoding", "deflate");
    //}
</script>
