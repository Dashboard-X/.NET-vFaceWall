using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class user_modules_nav : System.Web.UI.UserControl
{
    public string UserName = ""; // profile username
    public bool ShowVideos = true;
    public bool ShowAudio = true;
    public bool ShowPhotos = true;
    public bool ShowQA = true;
    public bool ShowForumPosts = true;
    public bool ShowFavorites = true;
    public bool ShowFriends = true;
    public bool ShowBlogs = true;
    public bool ShowGroups = true;

    public int CountVideos = 0;
    public int CountAudio = 0;
    public int CountPhotos = 0;
    public int CountQA = 0;
    public int CountBlogs = 0;
    public int CountFavorites=0;
    public int CountFriends = 0;
    public int CountGroups = 0;

    public int ActiveIndex = 0;
    public bool isAdmin = false;
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PrePare_Links();
        }
    }

    private void PrePare_Links()
    {
        StringBuilder str = new StringBuilder();

        string profilecss = "";


        switch (ActiveIndex)
        {
            
            case 10:
                profilecss = " class=\"active\"";
                break;

        }
        str.Append("<ul class=\"nav nav-list\">\n");
  
        str.Append("<li class=\"nav-header\">Profile</li>\n");
        str.Append("<li" + profilecss + "><a href=\"" + UrlConfig.Prepare_User_Profile_Url(this.UserName, "profile", isAdmin) + "\">Profile</a></li>\n");

        
        str.Append("</ul>\n");

        lst.InnerHtml = str.ToString();
    }
}