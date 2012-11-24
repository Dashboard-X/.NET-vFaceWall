<%@ WebHandler Language="C#" Class="signup" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Security;

public class signup : IHttpHandler {

    // Script to login / register user via facebook account
    private string fb_uid = ""; // UID fetched from user facebook account
    private string name = ""; // Facebook Name
    private string fb_username = ""; // Facebook UserName
    private string fname = "";
    private string lname = "";
    private string gender = "";
    private string user_birthday = "";
    private string email = "";
    private string picture = "";
    private string location = "";
    private string redirect_url = "";
    private string ElementID = ""; // div element id where you want to load box with close link
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Params["uid"] != null)
            this.fb_uid = context.Request.Params["uid"].ToString();
        if (context.Request.Params["uname"] != null)
            this.fb_username = context.Request.Params["uname"].ToString();
        if (context.Request.Params["nm"] != null)
            this.name = context.Request.Params["nm"].ToString();
        if (context.Request.Params["fn"] != null)
            this.fname = context.Request.Params["fn"].ToString();
        if (context.Request.Params["ln"] != null)
            this.lname = context.Request.Params["ln"].ToString();
        if (context.Request.Params["gn"] != null)
            this.gender = context.Request.Params["gn"].ToString();
        if (context.Request.Params["bt"] != null)
            this.user_birthday = context.Request.Params["bt"];
        if (context.Request.Params["eml"] != null)
            this.email = context.Request.Params["eml"];
        if (context.Request.Params["loc"] != null)
            this.location = context.Request.Params["loc"];
        if (context.Request.Params["pic"] != null)
            this.picture = context.Request.Params["pic"];
        // testing url http://localhost/vsk/handlers/signup.ashx?uid=10000&uname=tommy775&nm=shane&fn=shane&ln=michael&gn=male&bt=12/4/1992&eml=testing@mediasoftpro.com&loc=usa
        if (context.Request.Params["redirect_url"] != null)
            
            this.redirect_url = context.Request.Params["redirect_url"];
        if (context.Request.Params["elid"] != null)
            this.ElementID = context.Request.Params["elid"].ToString();

        string output = Process(context);
        context.Response.Write(output);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string Process(HttpContext context)
    {
        string status = "success";
        members _memberprocess =new members();
        //**********************************************************
        // Check Login
        //**********************************************************
        string _url = Config.GetUrl();
        if (this.fb_uid == "")
        {
            //context.Response.Redirect(_url + "facebook/error.aspx?status=nouid", true);
            status = "nouid";
            return status;
        }

        if (!FacebookBLLC.Check_UID(this.fb_uid))
        {
            // register user
            // validation of facebook data
            //if (this.name == "")
            //{
            //    // context.Response.Redirect(_url + "facebook/error.aspx?status=noname", true);
            //    status = "noname";
            //    return status;
            //}

            // Add user information in account
            string username = "";
            string firstname = this.fname;
            string lastname = this.lname;
            //if (this.name.Contains(" "))
            //{
            //    string[] arr = this.name.ToString().Split(char.Parse(" "));
            //    firstname = arr[0].ToString();
            //    lastname = arr[1].ToString();
            //}
            //else
            //{
            //    firstname = this.name;
            //}
            // generate auto username
            if (this.fb_username != "")
            {
                username = this.fb_username;
                if (username.Length>15)
                {
                    username = username.Substring(0, 15);
                }
                
            }
            else
            {
                string lname = "";
                if (firstname.Length > 5)
                    username = firstname.Substring(0, 5);
                else
                    username = firstname;

                if (lastname != "")
                {
                    lname = lastname.Replace(" ", "");
                    if (lname.Length > 5)
                        username = username + "" + lname.Substring(0, 5);
                    else
                        username = username + "" + lname;
                }
                username = username + "" + this.fb_uid.Substring(0, 5);
            }     
            
            if (_memberprocess.Check_UserName(username))
            {
                // user on this name already exist
                // assign username same as facebook id but 15 char limit
                fb_username = "";
                if (this.fb_uid.Length > 15)
                    username = this.fb_uid.Substring(0, 15);
                else
                    username = this.fb_uid;
            }

            // uncomment the following text if you want to verify user facebook email within your website database
            //if (this.email != "" && this.email.Contains("@"))
            //{
            //    if (_memberprocess.Check_Email(this.email))
            //    {
            //        // "Email address is already exist."
            //        //context.Response.Redirect(_url + "/facebook/error.aspx?status=emailexist", true);
            //        return "emailexist";
            //    }
            //}
            if (!this.email.Contains("@"))
                this.email = "";
           
            // birthday processing
            DateTime birthday = DateTime.Now.AddYears(-18); // dummy birth day if no birthday provided
            if (this.user_birthday.Contains("/") || this.user_birthday.Contains(@"\"))
            {
                birthday = DateTime.Parse(this.user_birthday);
            }

            // picture processing
            string picturename = "http://graph.facebook.com/" + this.fb_uid + "/picture?type=square"; //square, small, normal, large
            //if (!this.picture.Contains("undefined") || !this.picture.Contains("null"))
            //{
            //    picturename = this.picture;
            //}
            // gender processing
            if (gender != "male" || gender != "female")
                gender = "Male";
            
            // location processing
            string country = "";
            if (this.location != "null" || this.location != "" || this.location != "undefined")
            {
                if (this.location.Contains(","))
                {
                    string[] arr = this.location.ToString().Split(char.Parse(","));
                    country = arr[arr.Length - 1].ToString().Trim();
                }
                else
                {
                    country = this.location.Trim();
                }
            }

            int userrole_id = 0;
            FacebookBLLC.Add(username, firstname, lastname, "###", email, country, 1, gender, birthday, picturename, this.fb_uid, userrole_id);
          
            // Create Required Directories
           
            Directory_Process.CreateRequiredDirectories(context.Server.MapPath(context.Request.ApplicationPath) + "/contents/member/" + username);

            // send mail after user registeration
            if (email != "")
            {
                MailTemplateProcess(email, username, "###", "###");
            }

            // authorize user to access myaccount.
            FormsAuthentication.SetAuthCookie(username, false);
            if (Site_Settings.Store_IPAddress)
            {
                // Store IP Address Log 
                string ipaddress = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                User_IPLogBLL.Process_Ipaddress_Log(username, ipaddress);
            }
            //context.Response.Redirect(_url + "myaccount/Default.aspx?status=success", true);
            if(this.fb_username!="")
                status = "ucsuccess";
            else
                status = "csuccess";
            return status;
        }
        else
        {
            // facebook user account already exist in our website
            string username = FacebookBLLC.Get_UserName(this.fb_uid);
            if (username == "")
            {
                //context.Response.Redirect(_url + "facebook/error.aspx?status=nologinname", true);
                return "nologinname";
            }
            else
            {
                string ipaddress = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (BlockIPBLL.Validate_IP(ipaddress))
                {
                    //context.Response.Redirect(_url + "IPBlocked.aspx", true);
                    return "ipblocked";
                }

                User_IPLogBLL.Process_Ipaddress_Log(username, ipaddress);

                // Update Last Login Activity of User
                members.Update_Value(username, "last_login", DateTime.Now);

                // member is validated
                FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMonths(1), true, username, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(_ticket);
                HttpCookie _cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                _cookie.Expires = DateTime.Now.AddMonths(1);
                context.Response.Cookies.Add(_cookie);

                //if (this.redirect_url != "")
                //    context.Response.Redirect(this.redirect_url);
                //else
                //context.Response.Redirect(_url + "myaccount/Default.aspx");
                status = "success";
            }
        }

        return status;
    }

    // Send mail after user registeration via facebook
    private void MailTemplateProcess(string emailaddress, string username, string password, string key)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("FBUSRREG");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);

                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[password\\]", password);
                contents = MailProcess.Process2(contents, "\\[key_url\\]", key);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
  
}