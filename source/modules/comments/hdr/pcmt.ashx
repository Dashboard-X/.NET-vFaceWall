<%@ WebHandler Language="C#" Class="pcmt" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Specialized;

public class pcmt : IHttpHandler {
    
    // This handler is responsible for posting comment on item.
    private long ContentID = 0;
    private string ProfileID = "";
    private long ReplyID = 0;
    private string AuthorID = "";
    private string PosterID = "";
    private string Comment = "";
    private int Type = 0;
    private int TotalComments = 0;
    private string ContentUrl = "";
    private bool ShowAuthorPhoto = false;
    private int Height = 0;
    private int Width = 0;
    private bool isAdmin = false;
    private bool ShowReply = true;
    private bool ShowVotes = true;
    private bool HoverEffect = true;
    private int TemplateID = 0;
    private string UserProfilePictureName = "none"; // default setting for user profile photo
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        if (context.Request.Params["rid"] != null)
            this.ReplyID = Convert.ToInt64(context.Request.Params["rid"]);

        if (context.Request.Params["val"] != null)
            this.Comment = context.Server.HtmlDecode(context.Request.Params["val"].ToString());
          
        if (context.Request.Params["tp"] != null)
            this.Type = Convert.ToInt32(context.Request.Params["tp"]);
        if (context.Request.Params["ausr"] != null)
            this.AuthorID = context.Request.Params["ausr"].ToString();
        if (context.Request.Params["tcmt"] != null)
            this.TotalComments = Convert.ToInt32(context.Request.Params["tcmt"]);
       
        if (context.Request.Params["prf"] != null)
            this.ProfileID = context.Request.Params["prf"].ToString();
       
        if (context.Request.Params["curl"] != null)
            this.ContentUrl = context.Request.Params["curl"].ToString();
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
        if (context.Request.Params["irep"] != null)
            this.ShowReply = Convert.ToBoolean(context.Request.Params["irep"]);
        if(context.Request.Params["ivt"]!=null)
            this.ShowVotes = Convert.ToBoolean(context.Request.Params["ivt"]);
        
        if (context.Request.Params["tmlid"] != null)
            this.TemplateID = Convert.ToInt32(context.Request.Params["tmlid"]);
        
        if (context.Request.Params["ishover"] != null)
            this.HoverEffect = Convert.ToBoolean(context.Request.Params["ishover"]);
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
            //return Config.SetHiddenMessage("Error occured while posting your comment", this.ElementID, true); // wrap output in round box
            return "p100"; //
        }
        // authorization required if login enabled for poting commment
        bool isloginrequired = Config.isLoginRequired_Comment();
        string username = "guest";
        if (this.PosterID == "" && isloginrequired)
        {
            //string sign_in = "<a href=\"" + Config.GetUrl() + "Login.aspx?ReturnUrl=" + Config.Return_Current_Page() + "\" class=\"bold\">Sign In</a>";
            //string sign_up = "<a href=\"" + Config.GetUrl() + "Register.aspx\" class=\"bold\">Sign Up</a>";
            //str.Append("" + sign_in + " or " + sign_up + " now!");
            //return Config.SetHiddenMessage(str.ToString(), this.ElementID, true); // wrap output in round box
            return "p101";
        }

        if (this.PosterID != "")
            username = this.PosterID;

        if (this.Comment == "")
        {
            // return Config.SetHiddenMessage("<strong>Write comment to post.</strong>", this.ElementID, true);
            return "p102";
        }

        // Invalid characters exist in text
       
        if (UtilityBLL.isLongWordExist(this.Comment))
        {
            //return Config.SetHiddenMessage("<strong>" + Resources.vsk.message_comment_01 + "</strong>", this.ElementID, true); //You typed some invalid words, correct and post again.
            return "p103";
        }

        // comment text processing
        ////string _cmt = UtilityBLL.StripHTML_v3(this.Comment).Trim(); // remove all html except a
        //string _cmt = UtilityBLL.StripHTML(this.Comment).Trim(); // remove all html
        //if (_cmt == "")
        //    return "p103";
        //// generate urls
        //// fix html code
        //_cmt = UtilityBLL.CompressCodeBreak(_cmt);
        //_cmt = UtilityBLL.GenerateLink(_cmt,true);
        string _cmt = UtilityBLL.Process_Content(this.Comment);
        if (_cmt == "")
            return "p103";
        Comment_Struct cmt = new Comment_Struct();
        cmt.VideoID = this.ContentID;
        cmt.Comment = _cmt;
        cmt.Type = this.Type;
        cmt.UserName = this.PosterID;
        cmt.ReplyID = this.ReplyID;
        cmt.ProfileID = this.ProfileID;
                
        // Post Comment
        cmt.CommentID = CommentsBLL.Add(cmt, this.TotalComments);
        cmt.PictureName = this.UserProfilePictureName;
       
        // Generate comment output
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
        if (this.ReplyID == 0)
            postitem.ShowReplyLink = true;
        else
            postitem.ShowReplyLink = false;
        postitem.ShowVotes = true;
        postitem.AuthorUserName = this.AuthorID;
        postitem.PosterUserName = this.PosterID;
        postitem.ShowReplyLink = this.ShowReply;
        postitem.ShowVotes = this.ShowVotes;
        postitem.isHoverEffect = this.HoverEffect;
        postitem.TemplateID = this.TemplateID;
        str.Append(postitem.Process(cmt));
        
        // send email
        MailTemplateProcess(username);

        // Generate Comment Output
        return str.ToString();
        //return cmt.CommentID.ToString();
    }


    // send emailnotification to all users 
    // i: author of original content
    // ii: all users who post comment on this content.
    private void MailTemplateProcess(string cusername)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            // set mediatype
            string mediatype = "Video";
            switch (this.Type)
            {
                case 0:
                    mediatype = "Video";
                    break;
                case 1:
                    //blog
                    mediatype = "Blog Post";
                    break;
                case 2:
                    // photos
                    mediatype = "Photo";
                    break;
                case 3:
                    // galleries
                    mediatype = "Photo Gallery";
                    break;
                case 11:
                    // QA Question
                    mediatype = "Question";
                    break;
                case 13:
                    // QA Answer
                    mediatype = "Answer";
                    break;
            }
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("MEDCMT");
            if (lst.Count > 0)
            {
                // send email to author
                if (this.AuthorID != "")
                {
                    string emailaddress = members.Return_Value(this.AuthorID, "email");
                    Send_Email(cusername, this.AuthorID,emailaddress, mediatype, lst);
                }

                // send mail to all other usernames who already post comment on this content
                System.Collections.Generic.List<Member_Struct> cuserlist = CommentsBLL.Fetch_Comment_UserNames(this.ContentID,this.Type);
                if (cuserlist.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i <= cuserlist.Count - 1; i++)
                    {
                        if (this.PosterID != cuserlist[i].UserName || this.AuthorID != cuserlist[i].UserName)
                        {
                            if (cuserlist[i].isAutoMail == 1)
                            {
                                // if user allow receiving mail.
                                Send_Email(cusername, cuserlist[i].UserName, cuserlist[i].Email, mediatype, lst);
                            }
                        }
                    }
                }

            }
        }
    }

    private void Send_Email(string cusername, string rusername,string emailaddress, string mediatype, System.Collections.Generic.List<Struct_MailTemplates> lst)
    {
        if (rusername != "guest")
        {           
            string subject = MailProcess.Process2(lst[0].Subject, "\\[cusername\\]", cusername);
            subject = MailProcess.Process2(subject, "\\[rusername\\]", rusername);
            subject = MailProcess.Process2(subject, "\\[mediatype\\]", mediatype);

            string contents = MailProcess.Process2(lst[0].Contents, "\\[cusername\\]", cusername);
            contents = MailProcess.Process2(contents, "\\[rusername\\]", rusername);
            contents = MailProcess.Process2(contents, "\\[mediatype\\]", mediatype);
            contents = MailProcess.Process2(contents, "\\[comment\\]", this.Comment);
            contents = MailProcess.Process2(contents, "\\[url\\]", this.ContentUrl);

            MailProcess.Send_Mail(emailaddress, subject, contents);
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}