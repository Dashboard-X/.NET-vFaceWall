using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class user_profile_Default : System.Web.UI.Page
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
            

            // increment profile views
            members.Increment_Views(username, _list[0].Views);

            nav1.UserName = username;
          

            if (_list[0].Channel_isFriends == 0 || !Config.isFriends())
                nav1.ShowFriends = false;


         
            
            nav1.CountPhotos = _list[0].Count_Photos;
        
            nav1.CountFriends = _list[0].Count_Friends;

            nav1.ActiveIndex = 10; // profile

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

            // Fetch User Detail Profile
            StringBuilder str = new StringBuilder();
            str.Append("<div class=\"heading\">\n");
            str.Append("<div style=\"float:left; width:50%;\">\n");
            str.Append("<h3>Detail Profile</h3>\n");
            str.Append("</div>\n");
            if (User.Identity.IsAuthenticated)
            {
                if (Page.User.Identity.Name == username)
                {
                    // own profile
                    str.Append("<div class=\"item_r\" style=\"float:right; width:10%;\">\n");
                    str.Append("<a href=\"" + Config.GetUrl("myaccount/profile.aspx") + "\">Edit Profile</a>\n");
                    str.Append("</div>\n");
                }
            }
            str.Append("<div class=\"clear\"></div>\n");
            str.Append("</div>\n");
            str.Append("<div class=\"item\">\n");
            List<Member_Struct> _mem = members.Fetch_User_DetailProfile(username);
            if(_mem.Count>0)
            {
                // name settings
                string _name = "";
                if (_list[0].FirstName != "" || _list[0].LastName != "")
                    _name = _list[0].FirstName + " " + _list[0].LastName;
                else
                    _name = username;
                Set_Extended_Item(str, "Name", _name);
                // Age 
                if (_mem[0].isAllowBirthDay == 1)
                {
                    int age = DateTime.Now.Year - _mem[0].BirthDate.Year;
                    Set_Extended_Item(str, Resources.vsk.age, age.ToString());
                }
                // Joined
                Set_Extended_Item(str, Resources.vsk.joined, string.Format("{0:Y}", _list[0].RegisterDate));
                //// Videos Watched
                // Website
                if (_list[0].Website.Trim() != "")
                {
                    string websitename = _list[0].Website;
                    if (websitename.Length > 35)
                        websitename = websitename.Substring(0, 35) + "...";
                    string url = "<a href=\"" + _list[0].Website + "\" alt=\"" + _list[0].Website + "\" target=\"_blank\">" + websitename + "</a>";

                    Set_Extended_Item(str, Resources.vsk.website, url);
                }
                // About Me
                if (_mem[0].AboutMe.Trim() != "")
                {
                    Set_Extended_Item(str, Resources.vsk.aboutme, _mem[0].AboutMe);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.aboutme, "<div class=\"item_c light\">---- No Information ----</div>");
                }

                // Home Town
                if (_list[0].HometTown.Trim() != "")
                {
                    Set_Extended_Item(str, Resources.vsk.hometown, _list[0].HometTown);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.hometown, "<div class=\"item_c light\">---- No Information ----</div>");
                }
                // Country
                if (_list[0].CountryName.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.country, _list[0].CountryName);
                }
                // Occupations
                if (_mem[0].Occupations.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.occupations, _mem[0].Occupations);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.occupations, "<div class=\"item_c light\">---- No Information ----</div>");
                }
                // Companies
                if (_mem[0].Companies.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.companies, _mem[0].Occupations);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.companies, "<div class=\"item_c light\">---- No Information ----</div>");
                }
                // Hobbies
                if (_mem[0].Interests.Trim() != "")
                {
                    Set_Extended_Item(str, Resources.vsk.hobbies, _mem[0].Interests);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.hobbies, "<div class=\"item_c light\">---- No Information ----</div>");
                }

                // Movies
                if (_mem[0].Movies.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.movies, _mem[0].Movies);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.movies, "<div class=\"item_c light\">---- No Information ----</div>");
                }
                // Musics
                if (_mem[0].Musics.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.musics, _mem[0].Musics);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.musics, "<div class=\"item_c light\">---- No Information ----</div>");
                }
                // Books
                if (_mem[0].Books.Trim() != "")
                {
                   Set_Extended_Item(str, Resources.vsk.books, _mem[0].Books);
                }
                else
                {
                    Set_Extended_Item(str, Resources.vsk.books, "<div class=\"item_c light\">---- No Information ----</div>");
                }
            }
            str.Append("</div>\n");
            cprofile.InnerHtml = str.ToString();

            string title = username + "'s complete profile";

            // set meta information
            string meta_description = title + ", date joined: " + _list[0].RegisterDate;
            string thumb_url = UrlConfig.Retun_User_Profile_Thumb_Url(_list[0].UserName, _list[0].PictureName, 0);
            string profile_url = UrlConfig.Prepare_User_Profile_Url(_list[0].UserName,"profile", false);
            MetaTagsBLL.META_StaticPage(this.Page, (HtmlHead)Page.Header, title, meta_description, thumb_url, profile_url);

        }
        else
        {
            Response.Redirect(Config.GetUrl() + "channel/Default.aspx?status=accountnotexist", true);
        }
    }

    // Create extended item
    private void Set_Extended_Item(StringBuilder str, string label, string value)
    {
        str.Append("<div class=\"bx_br_bt item_pad_4\">\n");
        str.Append("<strong>" + label + ":</strong><br />");
        str.Append(value);
        str.Append("</div>\n");
    }
}