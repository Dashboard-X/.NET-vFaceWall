<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Web.Security;

public class login : IHttpHandler {

    public string UserName = "";
    public string Password = "";
    public bool RememberCheck = false;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        if (context.Request.Params["usr"] != null)
            this.UserName = context.Request.Params["usr"].ToString();

        if (context.Request.Params["pas"] != null)
            this.Password = context.Request.Params["pas"].ToString();

        if (context.Request.Params["rem"] != null)
            this.RememberCheck = true;
        //"http://localhost/vsk70/handlers/login.ashx?usr=tommy01&pas=2222222&rem=1";
        context.Response.Write(Generate_Output(context));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
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
        //if (context.Request.UrlReferrer == null)
        //{
        //    return "p400"; // Authorization Failed"
        //}
        //if (!context.Request.UrlReferrer.Host.Contains("URLMATCHINGWORD"))
        //{
        //    return "401"; // Double Authorization
        //}

        if (this.UserName == "" && this.Password == "")
        {            
            return "p100";
        }

        List<Member_Struct> _lst = members.Get_Hash_Password(this.UserName);
        if (_lst.Count == 0)
        {
            return "p101";  // "Login failed, please enter correct user name &amp; password."
        }
        // check encrypted password
        if (_lst[0].Password.Length < 20)
        {
            // backward compatibility
            members _memberprocess = new members();
            // check existing user passwords with old system
            if (!_memberprocess.Validate_Member(this.UserName, this.Password, false))
            {
                return "p101";  // "Login failed, please enter correct user name &amp; password."
            }
        }
        else
        {
            // check encrypted password with user typed password
            bool matched = BCrypt.Net.BCrypt.Verify(this.Password, _lst[0].Password);
            if (!matched)
            {
                return "p101";  // "Login failed, please enter correct user name &amp; password."
            }
        }
        // IP Address tracking and processing
        string ipaddress = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (BlockIPBLL.Validate_IP(ipaddress))
        {
            return "p102"; 
        }

        if (Site_Settings.Store_IPAddress)
        {
            // Store IP Address Log 
            User_IPLogBLL.Process_Ipaddress_Log(this.UserName, ipaddress);
        }

        // Update Last Login Activity of User
        members.Update_Value(this.UserName, "last_login", DateTime.Now);
        // member is validated
        FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(1, this.UserName, DateTime.Now, DateTime.Now.AddMonths(1), this.RememberCheck, this.UserName, FormsAuthentication.FormsCookiePath);
        string encTicket = FormsAuthentication.Encrypt(_ticket);
        HttpCookie _cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        if (this.RememberCheck)
            _cookie.Expires = DateTime.Now.AddMonths(1);
        context.Response.Cookies.Add(_cookie);

        // Generate Comment Output
        return "0";
        //return cmt.CommentID.ToString();
    }

}