<%@ WebHandler Language="C#" Class="post" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

public class post : IHttpHandler {

    public long GroupID = 0;
    public string Poster_UserName = "";
    public string Profile_UserName = "";
    public string Data = "";
    public string UserProfilePictureName = "none";
    public string Note = "";
    public int Privacy = 0;

    private int _leftwidth = 10; // %; for post author photo preview
    private int _rightwidth = 89; // %; for original post section
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        if (context.Request.Params["pic"] != null)
            this.UserProfilePictureName = context.Request.Params["pic"].ToString();

        if (context.Request.Params["usr"] != null)
            this.Profile_UserName = context.Request.Params["usr"].ToString();

        if (context.User.Identity.IsAuthenticated)
            this.Poster_UserName = context.User.Identity.Name;

        if (context.Request.Params["d"] != null)
            this.Data = HttpContext.Current.Server.UrlDecode(context.Request.Params["d"].ToString());
        
        if (context.Request.Params["pic"] != null)
            this.UserProfilePictureName = context.Request.Params["pic"].ToString();

        if (context.Request.Params["note"] != null)
            this.Note = context.Request.Params["note"].ToString();

        if (context.Request.Params["prc"] != null)
            this.Privacy = Convert.ToInt32(context.Request.Params["prc"]);

        //*********************************
        // Security Check
        //*********************************
        if (context.Request.UrlReferrer == null)
        {
            context.Response.Write("Authorization Failed!");
        }
        else
        {
            context.Response.Write(Process_Data(context));
        }
    }

    private string Process_Data(HttpContext context)
    {
        StringBuilder str = new StringBuilder();

        if (this.Poster_UserName == "")
        {
            return "Sign In or Sign Up";
        }
        if (this.Data != "" && this.Profile_UserName !="")
        {
            if (this.UserProfilePictureName == "none" || this.UserProfilePictureName == "")
                this.UserProfilePictureName = members.Get_Picture_No_Session(this.Poster_UserName);
            // Normal Text -> No Parsing Required)
            string _note = "";
            if (this.Note != "")
            {
                _note = UtilityBLL.StripHTML(this.Note);
            }

            string _username = this.Poster_UserName;
            string _usr_link = "<a class=\"bold\" href=\"" + UrlConfig.Prepare_User_Profile_Url(_username, false) + "\"  title=\"" + _username + "\">" + _username + "</a>";
            StringBuilder gstr = new StringBuilder();
            gstr.Append("<div class=\"item_pad_2\">\n");
            gstr.Append(this.Data);
            gstr.Append("<div class=\"clear\"></div>\n");
            gstr.Append("</div>\n");
            
            UserActivity_Struct ustr = new UserActivity_Struct();
            ustr.UserName = Profile_UserName;
            ustr.PosterUserName = Poster_UserName;
            ustr.PictureName = this.UserProfilePictureName;
            ustr.Title = _note;
            ustr.Activity = gstr.ToString(); // generate video or audio preview item
            ActivityBLL.Add(ustr);

            ActivityItem act = new ActivityItem();
            act.LeftWidth = this._leftwidth; // profile layout style
            act.RightWidth = this._rightwidth;
            act.Width = 50; // profile thumb height
            act.Height = 50;
            act.isHoverEffect = true;
            act.ShowProfileThumb = true; // show user profile thumb on right side.
            act.LoggedInUserName = this.Poster_UserName;
            str.Append(HttpContext.Current.Server.HtmlDecode(act.Process(ustr)));
           
        }
        else
        {
            str.Append("No data found!"); 
        }
         
        return str.ToString();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

    //// send mail to owner of group if someone post new post on his / her group
    //private void MailTemplateProcess(string ownerusername, string username, string inboxurl)
    //{
    //    //if sending mail option enabled
    //    if (Config.isEmailEnabled())
    //    {
    //        System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("GPDPOST");
    //        if (lst.Count > 0)
    //        {
    //            string subject = MailProcess.Process2(lst[0].Subject, "\\[ownerusername\\]", ownerusername);
    //            subject = MailProcess.Process2(subject, "\\[username\\]", username);

    //            string contents = MailProcess.Process2(lst[0].Contents, "\\[ownerusername\\]", ownerusername);
    //            contents = MailProcess.Process2(contents, "\\[username\\]", ownerusername);

    //            string group_url = Config.GetUrl("group/" + this.GroupID + ".aspx");
    //            string url = "<a href=\"" + group_url + "\">" + group_url + "</a>";

    //            contents = MailProcess.Process2(contents, "\\[url\\]", url);

    //            contents = MailProcess.Process2(contents, "\\[inboxurl\\]", inboxurl);

    //            string emailaddress = members.Return_Value(ownerusername, "email");

    //            MailProcess.Send_Mail(emailaddress, subject, contents);
    //        }
    //    }
    //}

}