using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Collections.Generic;


public partial class user_settings_Default : System.Web.UI.Page
{
    protected string ChannelTheme_Url = "";
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

    public string Channel_Theme
    {
        get
        {
            if (ViewState["Channel_Theme"] != null)
                return ViewState["Channel_Theme"].ToString();
            else
                return "blue";
        }
        set
        {
            ViewState["Channel_Theme"] = value;
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Site_Settings.Channel_Custom_Theme)
            Page.Theme = "empty";
        else
            Page.Theme = Page.StyleSheetTheme;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                string redirect_url = Config.GetUrl() + "user/settings/";
                Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                return;
            }

            this.UserName = User.Identity.Name;
           



            // load user profile 
            Load_User_Profile_Data(this.UserName);
            // if custom theme is enabled
            if (Site_Settings.Channel_Custom_Theme)
                this.ChannelTheme_Url = "<link href=\"" + Config.GetUrl("themes/channel/" + this.Channel_Theme + "/ui.all.css") + "\" rel=\"stylesheet\" type=\"text/css\" media=\"all\" />";

        }
    }

    //load user profile data
    private void Load_User_Profile_Data(string username)
    {
        List<Member_Struct> _list = members.Fetch_User_Channel(username);
        if (_list.Count > 0)
        {
            // channel theme
            if (_list[0].Channel_Theme != "")
                this.Channel_Theme = _list[0].Channel_Theme;


            // load user profile
            ch_profile1.UserName = username;
            ch_profile1.FirstName = _list[0].FirstName;
            ch_profile1.LastName = _list[0].LastName;
            ch_profile1.CountryName = _list[0].CountryName;
            ch_profile1.Gender = _list[0].Gender;
            ch_profile1.RelationshipStatus = _list[0].RelationshipStatus;
            ch_profile1.Register_Date = _list[0].RegisterDate;
            ch_profile1.Last_Login = _list[0].Last_Login;
            ch_profile1.Views = _list[0].Views;


            // increment profile views
            members.Increment_Views(username, _list[0].Views);

            nav1.UserName = username;
          

            if (_list[0].Channel_isFriends == 0 || !Config.isFriends())
                nav1.ShowFriends = false;

          

          
            nav1.CountPhotos = _list[0].Count_Photos;
           
            nav1.CountFriends = _list[0].Count_Friends;
           
            nav1.ActiveIndex = 11; // settings

            //connect information
            connect_v21.UserName = username;
            connect_v21.PictureName = _list[0].PictureName;
            connect_v21.FirstName = _list[0].FirstName;
            connect_v21.LastName = _list[0].LastName;
            connect_v21.Gender = _list[0].Gender;
            connect_v21.RelationshipStatus = _list[0].RelationshipStatus;
            connect_v21.HomeTown = _list[0].HometTown;
            connect_v21.CurrentCity = _list[0].CurrentCity;
            connect_v21.Zipcode = _list[0].Zipcode;
            connect_v21.Country = _list[0].CountryName;


            // Theme Settings
            themes1.UserName = username;
            themes1.Channel_Theme = _list[0].Channel_Theme;

            // Module Settings
            cmodules1.UserName = username;
          
            cmodules1.Channel_isFriends = _list[0].Channel_isFriends;
           
            // Page Title and Meta Information
            string title = "Channel Settings";

            // set meta information
            string meta_description = "You can set channel themes and restrict user access to certain type and part of your channel profile from here";
            string profile_url = Config.GetUrl("user/settings/");
            MetaTagsBLL.META_StaticPage(this.Page, (HtmlHead)Page.Header, title, meta_description, "", profile_url);
        }
        else
        {
            Response.Redirect(Config.GetUrl() + "channel/Default.aspx?status=accountnotexist", true);
        }
    }
}