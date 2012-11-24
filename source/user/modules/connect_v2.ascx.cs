using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;

public partial class user_modules_connect_v2 : System.Web.UI.UserControl
{
    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }

    public string PictureName = "none";
    public string FirstName = "";
    public string LastName = "";

    public string Gender = "Male";
    public string RelationshipStatus = "";
    public string HomeTown = "";
    public string CurrentCity = "";
    public string Zipcode = "";
    public string Country = "";
    //public string PictureName
    //{
    //    get
    //    {
    //        if (ViewState["PictureName"] != null)
    //            return ViewState["PictureName"].ToString();
    //        else
    //            return "none";
    //    }
    //    set
    //    {
    //        ViewState["PictureName"] = value;
    //    }
    //}

    protected void Page_PreRender(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            LoadInformation();
        }
    }

    private void LoadInformation()
    {

        StringBuilder str = new StringBuilder();
        str.Append("<div class=\"item\">\n");
        str.Append("<div style=\"float:left;width:30%;\">\n");
        if (this.PictureName == "")
        {
            // get user photo info from db
            this.PictureName = members.Get_Picture(this.UserName);
        }
        str.Append("<a href=\"" + UrlConfig.Prepare_User_Profile_Url(this.UserName, false) + "\" class=\"thumbnail\" title=\"" + this.UserName + " channel\">\n");
        str.Append("<img src=\"" + UrlConfig.Retun_User_Profile_Thumb_Url(this.UserName, this.PictureName, 0) + "\" alt=\"" + this.UserName + " channel\" style=\"width:90px;height:90px;\" />");
        str.Append("</a>\n");
        str.Append("</div>\n");
        str.Append("<div style=\"float:right;width:68%;\">\n");
        string _name = "";
        if(this.FirstName !="" || this.LastName !="" )
            _name = this.FirstName + " " + this.LastName;
        else
            _name = this.UserName;

        str.Append("<span class=\"xxmedium-text bold\">" + _name + "</span><br />");
        StringBuilder info = new StringBuilder();
        info.Append(this.Gender);
        if (this.RelationshipStatus != "")
            info.Append(", " + this.RelationshipStatus);
        if (this.HomeTown != "" || this.CurrentCity != "")
        {
            if (this.CurrentCity != "")
                info.Append(", " + this.HomeTown);
            else
                info.Append(", " + this.CurrentCity);
        }
        if (this.Zipcode != "")
            info.Append(", " + this.Zipcode);
        info.Append(", " + this.Country);
        str.Append("<span class=\"mini-text light\">" + info.ToString() + "</span>\n");
        if (Page.User.Identity.IsAuthenticated)
        {
            if (Page.User.Identity.Name == this.UserName)
            {
                // own profile
                str.Append("<div class=\"item_pad_2\">\n");
                str.Append("<a class=\"normal-text reverse bold\" href=\"" + Config.GetUrl("myaccount/profile.aspx") + "\" title=\"Edit profile\">Edit Profile</a>&nbsp;|&nbsp;");
                str.Append("<a class=\"normal-text reverse bold\" href=\"" + Config.GetUrl("user/settings/") + "\" title=\"Edit channel settings\">Settings</a>\n");
                str.Append("</div>\n");
            }
            else
            {
                // other user profile
                OtherUserProfileLinks(str);
            }
        }
        else
        {
            // other user profile
            OtherUserProfileLinks(str);
        }
        str.Append("</div>\n");
        str.Append("<div class=\"clear\"></div>\n");
        str.Append("</div>\n");

        widget.InnerHtml = str.ToString();
    }

    private void OtherUserProfileLinks(StringBuilder str)
    {
        //str.Append("<div class=\"item_pad_2\">\n");
       
        //if (Config.isFeature_Messaging())
        //{
        //    str.Append("<a href=\"" + Config.GetUrl("myaccount/inbox/Default.aspx?uid=" + this.UserName + "") + "\" class=\"normal-text reverse bold\">" + Resources.vsk.sendmessage + "</a>\n");
        //}
        //str.Append("</div>\n");
    }

}
