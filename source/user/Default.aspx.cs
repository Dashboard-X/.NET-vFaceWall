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

public partial class user_Default : System.Web.UI.Page
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

    //private members _memberdalc = new members();
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
            string redirect_url = Config.GetUrl() + "myaccount/Default.aspx";
            if (Request.Params["user"] == null)
            {
                // no user key provided - check whether user authorized
                if (User.Identity.IsAuthenticated)
                {
                    this.UserName = User.Identity.Name;
                }
                else
                {
                    Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                    return;
                }
            }
            else
            {
                // user key provided
                this.UserName = Request.Params["user"].ToString();
                if (this.UserName == "Default")
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        this.UserName = User.Identity.Name;
                    }
                    else
                    {
                        Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                        return;
                    }
                }
            }

            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "chnl_setting_changed":
                        // channel settings changed
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_01, "Success!",1); // Channel setting has been changed
                        break;
                    case "chnl_theme_changed":
                        // channel theme changed
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_02, "Success!",1); // Channel theme has been changed
                        break;
                    case "chnl_module_changed":
                        // channel module changed
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_03, "Success!",1); // Channel module settings have been changed
                        break;
                    case "fvalfailed":
                        // friend invitation :-> failed to find request key / uid with friend request
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_04, "Success!",1); // "Friend request validation failed!"
                        break;
                    case "fcancelled":
                        // friend invitation :-> request cancelled
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_05, "Success!",1); // Friend request cancelled!
                        break;
                    case "fusrvalfailed":
                        // friend invitation :-> user validation failed
                        Config.ShowMessageV2(msg, Resources.vsk.message_channel_06, "Success!",1); // Friend request, user validation failed!
                        break;
                    case "accepted":
                        // friend inviation :-> user accepted friend inviation
                        if (Request.Params["uid"] != null)
                            Config.ShowMessageV2(msg, Request.Params["uid"].ToString() + " become your friend", "Success!", 1);
                        else
                            Config.ShowMessageV2(msg, Resources.vsk.message_channel_07, "Success!",1); // User has been added in your friend list successfully
                        break;
                }
            }



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
            if(_list[0].Channel_Theme != "")
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

            // user activity list
            post1.AuthorUserName = username;
            activities1.UserName = username;
            if (Request.Params["search"] != null)
                activities1.Term = Server.UrlDecode(Request.Params["search"].ToString());

            string title = username + "'s channel";

            // set meta information
            string meta_description = title + ", date joined: " + _list[0].RegisterDate;
            string thumb_url = UrlConfig.Retun_User_Profile_Thumb_Url(_list[0].UserName, _list[0].PictureName,0);
            string profile_url = UrlConfig.Prepare_User_Profile_Url(_list[0].UserName,false);
            MetaTagsBLL.META_StaticPage(this.Page, (HtmlHead)Page.Header, title, meta_description, thumb_url, profile_url);
        }
        else
        {
            Response.Redirect(Config.GetUrl() + "channel/Default.aspx?status=accountnotexist", true);
        }
    }

    protected void btn_search_Click1(object sender, EventArgs e)
    {
        if (UserName != "" && txt_search.Text != "")
        {
            Response.Redirect(Config.GetUrl("user/" + UserName + ".aspx?search=" + Server.UrlEncode(UtilityBLL.CleanSearchTerm(txt_search.Text))));
        }

    }
}
