using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text;

public partial class user_modules_ch_profile : System.Web.UI.UserControl
{
    private string _firstname = "";
    private string _lastname = "";
    private string _countryname = "";
    private string _gender = "";
    private string _relationshipstatus = "";
    //private string _aboutme = "";
    //private string _website = "";
    //private string _hometown = "";
    //private string _currentcity = "";
    //private string _zipcode = "";
    //private string _occupations = "";
    //private string _companies = "";
    // private string _schools = "";
    //private string _movies = "";
    //private string _musics = "";
    //private string _interests = "";
    //private string _books = "";
    //private int _isallowbirthday = 1;
    //private DateTime _birthdate = DateTime.Now;

    private DateTime _register_date = DateTime.Now;
    private DateTime _last_login = DateTime.Now;
    private int _views = 0;
    //private int _videos_watched = 0;
    //private int _subscribers = 0;

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

    public string FirstName
    {
        get
        {
            return _firstname;
        }
        set
        {
           _firstname = value;
        }
    }

    public string LastName
    {
        get
        {
            return _lastname;
        }
        set
        {
            _lastname = value;
        }
    }

    public string CountryName
    {
        get
        {
            return _countryname;
        }
        set
        {
           _countryname = value;
        }
    }

    public string Gender
    {
        get {return _gender;}
        set{_gender = value;}
    }

   

    public string RelationshipStatus
    {
        set { _relationshipstatus = value; }
        get { return _relationshipstatus; }
    }

    //public string AboutMe
    //{
    //    set { _aboutme = value; }
    //    get { return _aboutme; }
    //}
    //public string Website
    //{
    //    set { _website = value; }
    //    get { return _website; }
    //}

    //public string HomeTown
    //{
    //    set { _hometown = value; }
    //    get { return _hometown; }
    //}

    //public string CurrentCity
    //{
    //    set { _currentcity = value; }
    //    get { return _currentcity; }
    //}

    //public string ZipCode
    //{
    //    set { _zipcode = value; }
    //    get { return _zipcode; }
    //}

    //public string Occupations
    //{
    //    set { _occupations = value; }
    //    get { return _occupations; }
    //}

    //public string Companies
    //{
    //    set { _companies = value; }
    //    get { return _companies; }
    //}
    
    //public string Schools
    //{
    //    set { _schools = value; }
    //    get { return _schools; }
    //}

    //public string Movies
    //{
    //    set { _movies = value; }
    //    get { return _movies; }
    //}

    //public string Musics
    //{
    //    set { _musics = value; }
    //    get { return _musics; }
    //}

    //public string Interests
    //{
    //    set { _interests = value; }
    //    get { return _interests; }
    //}

    //public string Books
    //{
    //    set { _books = value; }
    //    get { return _books; }
    //}

    //public int isAllowBirthDay
    //{
    //    set { _isallowbirthday = value; }
    //    get { return _isallowbirthday; }
    //}

    //public DateTime BirthDate
    //{
    //    set { _birthdate = value; }
    //    get { return _birthdate; }
    //}

    public DateTime Register_Date
    {
        set { _register_date = value; }
        get { return _register_date; }
    }

    public DateTime Last_Login
    {
        set { _last_login = value; }
        get { return _last_login; }
    }

    //public int Subscribers
    //{
    //    set { _subscribers = value; }
    //    get { return _subscribers; }
    //}

    //public int Videos_Watched
    //{
    //    set { _videos_watched = value; }
    //    get { return _videos_watched; }
    //}

    public int Views
    {
        set { _views = value; }
        get { return _views; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Set_User_Data(this.UserName);
        }
    }

    private void Set_User_Data(string username)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<table style=\"width:100%;\">");
        // name settings
        string _name = "";
        if (this.FirstName != "" || this.LastName != "")
            _name = this.FirstName + " " + this.LastName;
        else
            _name = username;
        Set_Normal_Item(str, "Name", _name);

        // channel views
        Set_Normal_Item(str, Resources.vsk.channelviews, string.Format("{0:#,###}", this.Views));
        //// Age 
        //if (this.isAllowBirthDay == 1)
        //{
        //    int age = DateTime.Now.Year - this.BirthDate.Year;
        //    Set_Normal_Item(str, Resources.vsk.age, age.ToString());
        //}
        // Joined
        Set_Normal_Item(str, Resources.vsk.joined, string.Format("{0:Y}", this.Register_Date));
        // Last Signed In
        Set_Normal_Item(str, Resources.vsk.lastsignin, UtilityBLL.CustomizeDate(this.Last_Login, DateTime.Now));
        //// Videos Watched
        //string videos_watched = "0";
        //if (this.Videos_Watched > 0)
        //    videos_watched = string.Format("{0:#,###}", this.Videos_Watched);
        //Set_Normal_Item(str, Resources.vsk.videoswatched, videos_watched);
        //// Subscribers
        //string subscribe = "0";
        //if (this.Subscribers > 0)
        //    subscribe = string.Format("{0:#,###}", this.Subscribers);
        //Set_Normal_Item(str, Resources.vsk.subscribers, subscribe);
        // Website
        //if (this.Website.Trim() != "")
        //{
        //    string websitename = this.Website;
        //    if (websitename.Length > 35)
        //        websitename = websitename.Substring(0, 35) + "...";
        //    string url = "<a href=\"" + this.Website + "\" alt=\"" + this.Website + "\" target=\"_blank\">" + websitename + "</a>";

        //    if (this.Website.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.website, url);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.website, url);
        //}
        //// About Me
        //if (this.AboutMe.Trim() != "")
        //{
        //    if (this.AboutMe.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.aboutme, this.AboutMe);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.aboutme, this.AboutMe);
        //}
        //// Home Town
        //if (this.HomeTown.Trim() != "")
        //{
        //    if (this.HomeTown.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.hometown, this.HomeTown);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.hometown, this.HomeTown);

        //}
        // Country
        if (this.CountryName.Trim() != "")
        {
            if (this.CountryName.Length < 26)
                Set_Normal_Item(str, Resources.vsk.country, this.CountryName);
            else
                Set_Extended_Item(str, Resources.vsk.country, this.CountryName);
        }
        //// Occupations
        //if (this.Occupations.Trim() != "")
        //{
        //    if (this.Occupations.Length < 26)
        //        Set_Normal_Item(str,Resources.vsk.occupations, this.Occupations);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.occupations, this.Occupations);
        //}
        //// Companies
        //if (this.Companies.Trim() != "")
        //{
        //    if (this.Companies.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.companies, this.Companies);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.companies, this.Companies);
        //}
        //// Hobbies
        //if (this.Interests.Trim() != "")
        //{
        //    if (this.Interests.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.hobbies, this.Interests);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.hobbies, this.Interests);
        //}
        //// Movies
        //if (this.Movies.Trim() != "")
        //{
        //    if (this.Movies.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.movies, this.Movies);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.movies, this.Movies);
        //}
        //// Musics
        //if (this.Musics.Trim() != "")
        //{
        //    if (this.Musics.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.musics, this.Musics);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.musics, this.Musics);
        //}
        //// Books
        //if (this.Books.Trim() != "")
        //{
        //    if (this.Books.Length < 26)
        //        Set_Normal_Item(str, Resources.vsk.books, this.Books);
        //    else
        //        Set_Extended_Item(str, Resources.vsk.books, this.Books);
        //}
        str.Append("</table>");

        usr_profile.InnerHtml = str.ToString();
    }

    // Create normal profile item
    private void Set_Normal_Item(StringBuilder str,string label, string value)
    {
        str.Append("<tr><td class=\"brd_bottom\" style=\"width: 40%; padding:3px 0px 3px 0px; text-align: left; vertical-align: middle;\">");
        str.Append("<strong>" + label + ":</strong></td>");
        str.Append("<td class=\"brd_bottom\" style=\"width: 60%; text-align: right; vertical-align: middle;\">");
        str.Append(value);
        str.Append("</td></tr>");
    }

    // Create extended item
    private void Set_Extended_Item(StringBuilder str, string label, string value)
    {
        str.Append("<tr><td colspan=\"2\" class=\"brd_bottom\" style=\"width: 100%; padding:3px 0px 3px 0px; text-align: left; vertical-align: middle;\">");
        str.Append("<strong>" + label + ":</strong><br />");
        str.Append(value);
        str.Append("</td></tr>");
    }
   
}
