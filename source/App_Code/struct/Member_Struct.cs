using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// User Table Definition
/// </summary>
public class Member_Struct
{
    public Member_Struct()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    long _userid = 0;
    string _username = "";
    string _password = "";
    string _email = "";
    string _countryname = "";
    string _firstname = "";
    string _lastname = "";
    string _gender = "Male";
    DateTime _birthdate = DateTime.Now;
    DateTime _registerdate = DateTime.Now;
    DateTime _last_login = DateTime.Now;
    int _isenabled = 0;

    int _issendmessages = 0;
    int _isallowbirthday = 0;
    int _accounttype = 0;
    int _views = 0;
    string _aboutme = "";
    string _picturename = "";
    string _relationshipstatus = "single";
    string _website = "";
    string _hometown = "";
    string _currentcity = "";
    string _zipcode = "";
    string _occupations = "";
    string _companies = "";
    string _schools = "";
    string _interests = "";
    string _movies = "";
    string _musics = "";
    string _books = "";
    int _videos_watched = 0;
    int _subscribers = 0;

    //user settings
    int _isautomail = 1;
    int _mail_vcomment = 1;
    int _mail_ccomment = 1;
    int _mail_pmessages = 1;
    int _mail_finvite = 1;
    int _mail_subscribe = 1;

    // privacy
    int _privacy_fmessages = 1;

    // extra
    int _count_comments = 0; // no of user friends
    int _count_friends = 0; // no of user subscribers
    int _count_messages = 0; // no of unread user messages
    int _count_photos = 0; // no of photos uploaded by user
  
    // channel properties
    int _channel_iscomments = 0;
    int _channel_isfriends = 0;
    string _channel_name = "";
    string _channel_theme = "";

    int _type = 0; // 0: normal member, 1: administrator, 2: premium users

    public long UserID
    {
        set { _userid = value; }
        get { return _userid; }
    }

    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }

    public string Password
    {
        set { _password = value; }
        get { return _password; }
    }

    public string Email
    {
        set { _email = value; }
        get { return _email; }
    }

    public string CountryName
    {
        set { _countryname = value; }
        get { return _countryname; }
    }

    public string FirstName
    {
        set { if (_firstname != "") _firstname = ""; _firstname = value; }
        get { return _firstname; }
    }
    public string LastName
    {
        set { if (_lastname != "") _lastname = ""; _lastname = value; }
        get { return _lastname; }
    }

    public DateTime RegisterDate
    {
        set { _registerdate = value; }
        get { return _registerdate; }
    }

    public int isEnabled
    {
        set { _isenabled = value; }
        get { return _isenabled; }
    }

    public int AccountType
    {
        set { _accounttype = value; }
        get { return _accounttype; }
    }

    public int Views
    {
        set { _views = value; }
        get { return _views; }
    }

    public string PictureName
    {
        set { _picturename = value; }
        get { return _picturename; }
    }

    public string Gender
    {
        set { _gender = value; }
        get { return _gender; }
    }

    public DateTime BirthDate
    {
        set { _birthdate = value; }
        get { return _birthdate; }
    }

    public int isAutoMail
    {
        set { _isautomail = value; }
        get { return _isautomail; }
    }

    public int isAllowBirthDay
    {
        set { _isallowbirthday = value; }
        get { return _isallowbirthday; }
    }

    public int isSendMessages
    {
        set { _issendmessages = value; }
        get { return _issendmessages; }
    }

    public string RelationshipStatus
    {
        set { _relationshipstatus = value; }
        get { return _relationshipstatus; }
    }

    public string Website
    {
        set { _website = value; }
        get { return _website; }
    }

    public string HometTown
    {
        set { _hometown = value; }
        get { return _hometown; }
    }

    public string CurrentCity
    {
        set { _currentcity = value; }
        get { return _currentcity; }
    }

    public string Zipcode
    {
        set { _zipcode = value; }
        get { return _zipcode; }
    }

    public string Occupations
    {
        set { _occupations = value; }
        get { return _occupations; }
    }

    public string Companies
    {
        set { _companies = value; }
        get { return _companies; }
    }

    public string Schools
    {
        set { _schools = value; }
        get { return _schools; }
    }

    public string Interests
    {
        set { _interests = value; }
        get { return _interests; }
    }

    public string Movies
    {
        set { _movies = value; }
        get { return _movies; }
    }

    public string Musics
    {
        set { _musics = value; }
        get { return _musics; }
    }

    public string Books
    {
        set { _books = value; }
        get { return _books; }
    }


    public string AboutMe
    {
        set { _aboutme = value; }
        get { return _aboutme; }
    }

    public int Mail_VComment
    {
        set { _mail_vcomment = value; }
        get { return _mail_vcomment; }
    }

    public int Mail_CComment
    {
        set { _mail_ccomment = value; }
        get { return _mail_ccomment; }
    }

    public int Mail_PMessage
    {
        set { _mail_pmessages = value; }
        get { return _mail_pmessages; }
    }

    public int Mail_FInvite
    {
        set { _mail_finvite = value; }
        get { return _mail_finvite; }
    }

    public int Mail_Subscribe
    {
        set { _mail_subscribe = value; }
        get { return _mail_subscribe; }
    }

    public int Privacy_FMessages
    {
        set { _privacy_fmessages = value; }
        get { return _privacy_fmessages; }
    }

    public int Videos_Watched
    {
        set { _videos_watched = value; }
        get { return _videos_watched; }
    }

    public int Subscribers
    {
        set { _subscribers = value; }
        get { return _subscribers; }
    }

    public DateTime Last_Login
    {
        set { _last_login = value; }
        get { return _last_login; }
    }

    public int Count_Comments
    {
        set { _count_comments = value; }
        get { return _count_comments; }
    }

    public int Count_Friends
    {
        set { _count_friends = value; }
        get { return _count_friends; }
    }

   
    public int Count_Messages
    {
        set { _count_messages = value; }
        get { return _count_messages; }
    }

    public int Count_Photos
    {
        set { _count_photos = value; }
        get { return _count_photos; }
    }

  

    public int Channel_isComments
    {
        set { _channel_iscomments = value; }
        get { return _channel_iscomments; }
    }

    public int Channel_isFriends
    {
        set { _channel_isfriends = value; }
        get { return _channel_isfriends; }
    }

    


    public string Channel_Name
    {
        set { _channel_name = value; }
        get { return _channel_name; }
    }

    public string Channel_Theme
    {
        set { _channel_theme = value; }
        get { return _channel_theme; }
    }

    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }
}
