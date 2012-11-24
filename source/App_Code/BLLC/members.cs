using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

public class members
{
    // Update Status : 21 Apr 2010 - 8:22 PM

    // Note: Important Member Terms

    // Role | User Type

    // type:
    // ........... 0: Normal User
    // ........... 1: Administrator
    // ........... 2: Premium User

    // isEnabled:
    // ........... 0: Disabled Member
    // ........... 1: Enabled Member

    // channel_iscomments
    // .......... 0: Member not allowed comments on his/her channel
    // .......... 1: Member allow comments on his/her channel

    // channel_isfriends
    // .......... 0: Member not show his/her friends on channel
    // .......... 1: Member show his/her friends on channel

    // channel_issubscribers
    // .......... 0: Subscribers will not show on member channel
    // .......... 1: Subscribers will show on member channel

    // channel_isgroups
    // .......... 0: Member not show his/her groups on channel
    // .......... 1: Member show his/her groups on channel

    // channel_name // customize name for channel, by default username will be the name of channel if its not defined.

    // channel_theme // current configured theme by user. // channel themes available on the following folder
    // themes/channel/black,red,blue,fire,forest,pink,princes,stealth,sunlight
    

    // Statistic related fields -/ keep track of user contents that is store in user table for scalability / performance measure.

    // stat_videos :-> no of videos uploaded by user
    // stat_friends :-> no of user friends
    // stat_subscribers :-> no of user subscribers
    // stat_favorites :-> no of user favorited videos
    // stat_comments :-> no of comments posted on user channel
    // stat_groups :-> no of user groups
    // stat_messages :-> no of user unread messages.

    // Mail Handling Related Fields -/ allow user to avoid un necessary emails by setting mail options from myaccount section
    // Note: Below mentioned fields target restriction on external mails. still user will receive internal messages. 
    // There may be chances that some restriction may not work. you can test and report us in order to fix and improve its performance.

    // isautomail :-> enable / disable receiving mails
    // mail_vcomment :-> enable / disable mail receive when user receive comment on his / her video.
    // mail_ccomment :-> enable / disable mail receive when user receive comment on his / her channel.
    // mail_pmessage :-> enable/ disable mail when user receive private message internally within site messaging board.
    // mail_finvite :-> enable / disable mail when user receive friend inviation
    // mail_subscribe :-> enable / disable mail when user subscribed their profile.

    // Privacy Option
    // privacy_fmessages :-> enable / disable allow only friends to send messages and share videos.

    // user credits, allocated space and allowed uploads
    // Credits: Credits available to purchase premium contents
    // Remained_Video = 32; Total number of videos user can upload (-1: unlimited)
    // Remained_Audio = 34; Total number of audio files user can upload (-1: unlimited)
    // Remain_Gallery = 30; Total number of photo galleries user can create (-1: unlimited)
    // Remained_photos = 200; Total number of photos user can upload (-1; unlimited)
    // Remained_Blogs = 200; Total number of blog posts user can post (-1: unlimited)
    // Space_Video = 100; Total number of space available for video upload (-1:unlimited)
    // Space_Audio = 100; Total number of space available for audio upload (-1: unlimited)
    // Space_Photo = 100; Total number of space available of photo upload (-1: unlimited)
   
    // ************************************************************************************************************************************
    // ************************************************************************************************************************************
    // Member Data Manipulation Section
    // ************************************************************************************************************************************
    // ************************************************************************************************************************************
    #region Data Manipulation

    public members()
    {
    }

    public static bool Add(int AccountType, string UserName, string Password, string Email, string Country, int isEnabled, string gender, DateTime birthdate, string val_key, int type, int roleid)
    {
        // Stored Procedure Version
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_Post", new SqlParameter("@UserName", UserName), new SqlParameter("@Email", Email), new SqlParameter("@CountryName", Country), new SqlParameter("@Password", Password), new SqlParameter("@Register_Date", DateTime.Now), new SqlParameter("@AccountType", AccountType), new SqlParameter("@isEnabled", isEnabled), new SqlParameter("@Last_Login", DateTime.Now), new SqlParameter("@birthdate", birthdate), new SqlParameter("@gender", gender), new SqlParameter("@val_key", val_key), new SqlParameter("@type", type), new SqlParameter("@credits", credits), new SqlParameter("@remained_video", remained_video), new SqlParameter("@remained_audio", remained_audio), new SqlParameter("@remained_gallery", remained_gallery), new SqlParameter("@remained_photos", remained_photos), new SqlParameter("@remained_blogs", remained_blogs), new SqlParameter("@space_video", space_video), new SqlParameter("@space_audio", space_audio), new SqlParameter("@space_photos", space_photos), new SqlParameter("@roleid", roleid));
        // SQL Version
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("Insert Into users(UserName,Password,Email,CountryName,Register_Date,isEnabled,AccountType,Last_Login,gender,birthdate,val_key,type,roleid)values(");
        str.Append("@UserName,@Password,@Email,@CountryName,@Register_Date,@isEnabled,@AccountType,@Last_Login");
        str.Append(",@gender,@birthdate,@val_key,@type,@roleid");
        str.Append(")");
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
        cmd.Parameters.Add(new SqlParameter("@Password", Password));
        cmd.Parameters.Add(new SqlParameter("@Email", Email));
        cmd.Parameters.Add(new SqlParameter("@CountryName", Country));
        cmd.Parameters.Add(new SqlParameter("@isEnabled", isEnabled));
        cmd.Parameters.Add(new SqlParameter("@AccountType", AccountType));
        cmd.Parameters.Add(new SqlParameter("@Last_Login", DateTime.Now));
        cmd.Parameters.Add(new SqlParameter("@gender", gender));
        cmd.Parameters.Add(new SqlParameter("@birthdate", birthdate));
        cmd.Parameters.Add(new SqlParameter("@val_key", val_key));
        cmd.Parameters.Add(new SqlParameter("@type", type));
     
        cmd.Parameters.Add(new SqlParameter("@roleid", roleid));

        cmd.Parameters.Add(new SqlParameter("@Register_Date", DateTime.Now));
        if (con.State != ConnectionState.Open) 
            con.Open();
        cmd.ExecuteNonQuery();

        // send notification mail to admin
        MailTemplateProcess(UserName, Email);
        return true;
    }

    public static bool Delete(string UserName)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete From users WHERE username=@username",new SqlParameter("@username",UserName));
        return true;
    }

    public static bool Update_User_Profile(string username, string firstname, string lastname, string countryname, string gender, string relationshipstatus, string aboutme, string website, string hometown, string currentcity, string zipcode, string occupations, string companies, string schools, string interests, string movies, string musics, string books, int isallowbirthday)
    {
        // *********************************************************//
        // SCREENING Data Script: Updated in VSK 5.2
        // *********************************************************//
        if (Config.Get_ScreeningOption() == 1)
        {
            // screening, matching and replacing enabled
            // the following script will screen content with matching words e.g "apple" -> "a***e"
            aboutme = DictionaryBLL.Process_Screening(aboutme);
            occupations = DictionaryBLL.Process_Screening(occupations);
            companies = DictionaryBLL.Process_Screening(companies);
            schools = DictionaryBLL.Process_Screening(schools);
            interests = DictionaryBLL.Process_Screening(interests);
            movies = DictionaryBLL.Process_Screening(movies);
            musics = DictionaryBLL.Process_Screening(musics);
            books = DictionaryBLL.Process_Screening(books);
        }
        // *********************************************************//
        // *********************************************************//
          SqlConnection con = new SqlConnection(Config.ConnectionString);
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("Update users set firstname=@firstname,lastname=@lastname,countryname=@countryname,gender=@gender,relationshipstatus=@relationshipstatus");
        str.Append(",aboutme=@aboutme,website=@website,hometown=@hometown,currentcity=@currentcity,zipcode=@zipcode,occupations=@occupations");
        str.Append(",companies=@companies,schools=@schools,interests=@interests,movies=@movies,musics=@musics,books=@books,isallowbirthday=@isallowbirthday WHERE");
        str.Append(" username=@username;");
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@firstname", firstname));
        cmd.Parameters.Add(new SqlParameter("@lastname", lastname));
        cmd.Parameters.Add(new SqlParameter("@countryname", countryname));
        cmd.Parameters.Add(new SqlParameter("@gender", gender));
        cmd.Parameters.Add(new SqlParameter("@relationshipstatus", relationshipstatus));
        cmd.Parameters.Add(new SqlParameter("@aboutme", aboutme));
        cmd.Parameters.Add(new SqlParameter("@website", website));
        cmd.Parameters.Add(new SqlParameter("@hometown", hometown));
        cmd.Parameters.Add(new SqlParameter("@currentcity", currentcity));
        cmd.Parameters.Add(new SqlParameter("@zipcode", zipcode));
        cmd.Parameters.Add(new SqlParameter("@occupations", occupations));
        cmd.Parameters.Add(new SqlParameter("@companies", companies));
        cmd.Parameters.Add(new SqlParameter("@schools", schools));
        cmd.Parameters.Add(new SqlParameter("@interests", interests));

        cmd.Parameters.Add(new SqlParameter("@movies", movies));
        cmd.Parameters.Add(new SqlParameter("@musics", musics));
        cmd.Parameters.Add(new SqlParameter("@books", books));

        cmd.Parameters.Add(new SqlParameter("@isallowbirthday", isallowbirthday));
        cmd.Parameters.Add(new SqlParameter("@username", username));
     
        if (con.State != ConnectionState.Open) 
            con.Open();
        cmd.ExecuteNonQuery();
       
        // Stored Procedure Version
        // SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_UpdateProfile", new SqlParameter("@username", username), new SqlParameter("@firstname", firstname), new SqlParameter("@lastname", lastname), new SqlParameter("@countryname", countryname), new SqlParameter("@gender", gender), new SqlParameter("@relationshipstatus", relationshipstatus), new SqlParameter("@aboutme", aboutme), new SqlParameter("@website", website), new SqlParameter("@hometown", hometown), new SqlParameter("@currentcity", currentcity), new SqlParameter("@zipcode", zipcode), new SqlParameter("@occupations", occupations), new SqlParameter("@companies", companies), new SqlParameter("@schools", schools), new SqlParameter("@interests", interests), new SqlParameter("@movies", movies), new SqlParameter("@musics", musics), new SqlParameter("@books", books), new SqlParameter("@isallowbirthday", isallowbirthday));
        return true;
    }
      

    public static bool Update_UserName(string OlderUserName, string NewUserName)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set UserName=@NewUserName WHERE UserName=@OldUserName",new SqlParameter("@NewUserName",NewUserName),new SqlParameter("@OlderUserName",OlderUserName));
        return true;

    }

    public static string Return_Value(string UserName, string fieldname)
    {
        //return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_FieldValue", new SqlParameter("@username", UserName),new SqlParameter("@FieldName",fieldname)).ToString();
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT " + fieldname + " FROM users WHERE username=@username", new SqlParameter("@username", UserName)).ToString();
    }

    public static bool Update_Value(string UserName, string fieldname, string value)
    {
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_UpdateValue", new SqlParameter("@username", UserName), new SqlParameter("@FieldValue", value),new SqlParameter("@FieldName",fieldname));
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users Set " + fieldname + "=@FieldValue WHERE username=@username", new SqlParameter("@username", UserName), new SqlParameter("@FieldValue", value));
        return true;
    }

    public static bool Update_Value(string UserName, string fieldname, DateTime value)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set " + fieldname + "=@value where username=@username", new SqlParameter("@username", UserName), new SqlParameter("@value", value));
        return true;
    }

    public static bool Update_Favorite_Stat(string UserName, int stat_favorites,int type)
    {
        string field_name = "stat_favorites";
        if (type == 1) // audio
            field_name = "stat_audiofavorites";
        Update_Value(UserName, field_name, stat_favorites.ToString());
        return true;
    }

    public static bool Update_UnRead_Messages_Stat(string UserName, int stat_messages)
    {
        Update_Value(UserName, "stat_messages", stat_messages.ToString());
       // update user cache value too
        string _cache = "usr_msg_cnt_" + UserName;
        HttpContext.Current.Cache.Add(_cache, stat_messages, null, DateTime.Now.AddMinutes(Site_Settings.Cache_Duration), TimeSpan.Zero, CacheItemPriority.High, null);
        return true;
    }

    public static int Get_UnRead_Messages_Stat(string UserName)
    {
        // make it cacheable
        string _cache = "usr_msg_cnt_" + UserName;
        if (HttpContext.Current.Cache[_cache] == null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT stat_messages FROM users where username=@username");

            int total_messages = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, str.ToString(), new SqlParameter("@username", UserName)));

            HttpContext.Current.Cache.Add(_cache, total_messages, null, DateTime.Now.AddMinutes(Site_Settings.Cache_Duration), TimeSpan.Zero, CacheItemPriority.High, null);
        }
        return Convert.ToInt32(HttpContext.Current.Cache[_cache]);
    }


    #endregion
 
    // ************************************************************************************************************************************
    // ************************************************************************************************************************************
    // Member Action Section
    // ************************************************************************************************************************************
    // ************************************************************************************************************************************
    #region Premium Membership Authorization Checks
    // 0: Normal Member
    // 1: Administrators
    // 2: Premium Users
    public static int Get_MemberType(string UserName)
    {
        string sessionid = "usr_mem_type" + UserName;
        if (HttpContext.Current.Session[sessionid] == null)
        {
            HttpContext.Current.Session.Add(sessionid, Get_MemberType_No_Session(UserName));
        }

        return Convert.ToInt32(HttpContext.Current.Session[sessionid].ToString());
    }

    public static int Get_MemberType_No_Session(string UserName)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select type from users Where UserName=@username", new SqlParameter("@username", UserName)));
    }

  
    #endregion

    #region Data Action
        
    public bool Validate_Member(string UserName, string Password,bool isadmin)
    {        
        string type = "";
        if(isadmin)
            type = " AND type=1"; // Administrator

        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(UserName) from users Where UserName=@username AND Password=@password AND isenabled=1 " + type + "",new SqlParameter("@username",UserName),new SqlParameter("@password",Password)));
        if (result > 0)
            return true;
        else
            return false;
    }

    public static bool Validate_Member(string UserName)
    {
        //int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_ValidateUser", new SqlParameter("@uname", UserName)));
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(UserName) from users Where username=@uname AND isenabled=1", new SqlParameter("@uname", UserName)));
        if (result > 0)
            return true;
        else
            return false;
    }

    public static bool Validate_Member_Email(string Email, string Password)
    {
        //int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_ValidateEmail", new SqlParameter("@pwd", Password), new SqlParameter("@eml", Email)));
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, " Select Count(UserName) from users Where email=@eml AND password=@pwd AND isenabled=1", new SqlParameter("@pwd", Password), new SqlParameter("@eml", Email)));
        if (result > 0)
            return true;
        else
            return false;
    }
    
    public bool Check_UserName(string UserName)
    {
        //int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_CheckUser", new SqlParameter("@uname", UserName)));
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select count(username) from users Where username=@uname", new SqlParameter("@uname", UserName)));
        if (result > 0)
            return true;
        else
            return false;
    }

    public bool Check_Email(string Email)
    {
        //int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Members_CheckEmail", new SqlParameter("@eml", Email)));
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select count(username) from users Where email=@eml", new SqlParameter("@eml", Email)));
        if (result > 0)
            return true;
        else
            return false;
    }

    public bool Check_Key(string UserName,string val_key)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select count(username) from users Where UserName=@username AND val_key=@val_key",new SqlParameter("@username",UserName),new SqlParameter("@val_key",val_key)));
        if (result > 0)
            return true;
        else
            return false;
    }
    
    //public static string Get_Email(string UserName)
    //{
    //    return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Email from users Where UserName=@UserName", new SqlParameter("@UserName", UserName)).ToString();
    //}

    public static string Get_Picture(string UserName)
    {
        //return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select picturename from users Where UserName='" + UserName + "'").ToString();
        // store user picture in session in order to fetch directly.
        string sessionid = "vkit_usr_" + UserName;
        if (HttpContext.Current.Session[sessionid] == null)
        {
            string picturename = SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select picturename from users Where UserName=@username",new SqlParameter("@username",UserName)).ToString();
            HttpContext.Current.Session.Add(sessionid,picturename);
        }

        return HttpContext.Current.Session[sessionid].ToString();
    }

    public static string Get_Picture_No_Session(string UserName)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select picturename from users Where UserName=@username", new SqlParameter("@username", UserName)).ToString();
    }
    public static int Increment_Views(string username,int views)
    {
        int current_views = views + 1;
        Update_Value(username, "views", current_views.ToString());
        return current_views;
    }

    public bool Update_IsEnabled(string username, int value)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users Set isEnabled=@isEnabled WHERE username=@username", new SqlParameter("@username", username), new SqlParameter("@isEnabled", value)));
        if (result == 1)
            return true;
        else
            return false;
    }

    public int Get_Isenabled(string username)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "select isenabled from users where username=@username", new SqlParameter("@username", username)));
    } 
    #endregion

    // ************************************************************************************************************************************
    // ************************************************************************************************************************************
    // Normal Load Operation - > Dataset
    // ************************************************************************************************************************************
    // ************************************************************************************************************************************

    #region Load Members - > Dataset
     
    public static DataSet Get_Information(string UserName)
    {
        return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "Select * from users where Username='" + UserName + "'");
    }

    public static List<Member_Struct> Load_Members(string search, string gender, string accounttype, string countryname, string isEnabled, string type, int filteroption, string order, string direction, int PageNumber, int PageSize)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // generate sql query
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        string _fields = "u.UserName,u.Register_Date,u.Views,u.PictureName,u.countryname,u.firstname,u.lastname,u.gender,u.relationshipstatus,u.hometown,u.currentcity,u.zipcode,u.isenabled";
        query.Append(_fields + " FROM users as u" + Load_Members_Admin_Logic(search,gender,accounttype,countryname,isEnabled,type,filteroption));
        // implement paging script for differnt data sources.
        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), order, PageNumber, PageSize));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, PageSize));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(normal_query);
                break;
        }

        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.UserName = reader["username"].ToString();
            _item.Views = (int)reader["views"];
            _item.PictureName = reader["picturename"].ToString();
            _item.RegisterDate = (DateTime)reader["Register_Date"];
            _item.CountryName = reader["countryname"].ToString();
            _item.FirstName = reader["firstname"].ToString();
            _item.LastName = reader["lastname"].ToString();
            _item.Gender = reader["gender"].ToString();
            _item.RelationshipStatus = reader["relationshipstatus"].ToString();
            _item.HometTown = reader["hometown"].ToString();
            _item.CurrentCity = reader["currentcity"].ToString();
            _item.Zipcode = reader["zipcode"].ToString();
            _item.isEnabled = Convert.ToInt32(reader["isenabled"]);
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    public static int Count_Members(string search, string gender, string accounttype, string countryname, string isEnabled, string type, int filteroption)
    {
        StringBuilder str = new StringBuilder();
        str.Append("SELECT Count(u.UserName) FROM users as u" + Load_Members_Admin_Logic(search, gender, accounttype, countryname, isEnabled, type, filteroption));
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, str.ToString()));
    }


    public static string Load_Members_Admin_Logic(string search, string gender, string accounttype, string countryname, string isEnabled, string type, int filteroption)
    {
        StringBuilder query = new StringBuilder();
        if (search != "all" || gender != "all" || accounttype != "all" || countryname != "all" || isEnabled != "all" || type != "all" || filteroption != 0)
            query.Append(" WHERE");
        if (search != "all")
        {
            query.Append(" (username like '%" + search + "%'");
            query.Append(" OR email like '%" + search + "%'");
            query.Append(" OR state like '%" + search + "%'");
            query.Append(" OR zipcode like '%" + search + "%'");
            query.Append(" OR firstname like '%" + search + "%'");
            query.Append(" OR lastname like '%" + search + "%'");
            query.Append(" OR countryname like '%" + search + "%')");
            if (gender != "all" || accounttype != "all" || countryname != "all" || isEnabled != "all" || type != "all" || filteroption != 0)
                query.Append(" AND");
        }
        if (gender != "all")
        {
            query.Append(" gender=" + int.Parse(gender) + "");
            if (accounttype != "all" || countryname != "all" || isEnabled != "all" || type != "all" || filteroption != 0)
                query.Append(" AND");
        }

        if (accounttype != "all")
        {
            query.Append(" accounttype=" + int.Parse(accounttype) + "");
            if (countryname != "all" || isEnabled != "all" || type != "all" || filteroption != 0)
                query.Append(" AND");
        }

        if (countryname != "all")
        {
            query.Append(" countryname='" + countryname + "'");
            if (isEnabled != "all" || type != "all" || filteroption != 0)
                query.Append(" AND");
        }

        if (isEnabled != "all")
        {
            query.Append(" isenabled=" + int.Parse(isEnabled) + "");
            if (type != "all" || filteroption != 0)
                query.Append(" AND");
        }

        if (type != "all")
        {
            query.Append(" type=" + int.Parse(type) + "");
            if (filteroption != 0)
                query.Append(" AND");
        }

        if (filteroption != 0)
        {
            switch (filteroption)
            {
                case 1:
                    // today records
                    query.Append(" " + Config.Prepare_DateDiff("register_date", 0) + " ");

                    break;
                case 2:
                    // this week records
                    query.Append(" " + Config.Prepare_DateDiff("register_date", 1) + " ");
                    break;
                case 3:
                    // this month records
                    query.Append(" " + Config.Prepare_DateDiff("register_date", 2) + " ");
                    break;
            }
        }
        return query.ToString();
    }
        
    #endregion

    //**********************************************************************************************************************************
    //**********************************************************************************************************************************
    // ReadOnly Data Retrieval and Cache Management
    //**********************************************************************************************************************************
    //**********************************************************************************************************************************

    #region Advance Load - > Cache , ReadOnly
    // Cache and Load Main Video Listing
    public List<Member_Struct> Load_Channels_ADV(string Term, string Character, string accounttype, string country, string gender, bool havephoto, int filteroption, string order, int PageNumber, int PageSize, bool AdvList,bool isCache)
    {
          // cache implementation
        int cache_duration = Config.GetCacheDuration();
        if (cache_duration == 0 || !isCache) // no cache
            return Fetch_Channels(Term, Character, accounttype, country, gender, havephoto, filteroption, order, PageNumber, PageSize, AdvList);
        else
        {
            string cache_key = "ld_chn" + accounttype + "" + filteroption + "" + order + "" + PageNumber + "" + Term + "" + Character + "" + gender + "" + country + "" + havephoto + "" + AdvList;
            if (HttpContext.Current.Cache[cache_key] == null)
                HttpContext.Current.Cache.Add(cache_key, Fetch_Channels(Term, Character, accounttype, country, gender, havephoto, filteroption, order, PageNumber, PageSize, AdvList), null, DateTime.Now.AddMinutes(cache_duration), TimeSpan.Zero, CacheItemPriority.High, null);

            List<Member_Struct> _list = (List<Member_Struct>)(HttpContext.Current.Cache[cache_key]);
            return _list;
        }

    }
    // Fetch Video Data for Main Video Listing
    private List<Member_Struct> Fetch_Channels(string Term, string Character, string accounttype, string country, string gender, bool havephoto, int filteroption, string order,int PageNumber,int PageSize,bool AdvList)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;
              
        // generate sql query
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        string _fields = "u.UserName,u.Register_Date,u.Views,u.PictureName";
        if (AdvList)
            _fields = "u.UserName,u.Register_Date,u.Views,u.PictureName,u.countryname,u.firstname,u.lastname,u.gender,u.relationshipstatus,u.hometown,u.currentcity,u.zipcode";

        query.Append(_fields + " FROM users as u WHERE u.isEnabled=1 " + Process_Channels_Logic(Term, Character, accounttype,country,gender,havephoto, filteroption));
        // implement paging script for differnt data sources.
        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), order, PageNumber, PageSize));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, PageSize));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(normal_query);
                break;
        }
     
        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        if (Term != "")
            user_cmd.Parameters.Add(new SqlParameter("@term", '%' + Term + '%'));
        if (Character != "")
            user_cmd.Parameters.Add(new SqlParameter("@character", Character + '%'));
        if (accounttype != "all")
            user_cmd.Parameters.Add(new SqlParameter("@accounttype", accounttype));
        if (gender != "")
            user_cmd.Parameters.Add(new SqlParameter("@gender", gender));
        if (country != "")
            user_cmd.Parameters.Add(new SqlParameter("@countryname", country));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.UserName = reader["username"].ToString();
            _item.Views = (int)reader["views"];
            _item.PictureName = reader["picturename"].ToString();
            _item.RegisterDate = (DateTime)reader["Register_Date"];
            if (AdvList)
            {
                _item.CountryName = reader["countryname"].ToString();
                _item.FirstName = reader["firstname"].ToString();
                _item.LastName = reader["lastname"].ToString();
                _item.Gender = reader["gender"].ToString();
                _item.RelationshipStatus = reader["relationshipstatus"].ToString();
                _item.HometTown = reader["hometown"].ToString();
                _item.CurrentCity = reader["currentcity"].ToString();
                _item.Zipcode = reader["zipcode"].ToString();
            }
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    public int Count_Channels(string Term, string Character, string accounttype, string country, string gender, bool havephoto, int filteroption,bool isCache)
    {
        if (!isCache)
            return Fetch_Total_Channel(Term, Character, accounttype, country, gender, havephoto, filteroption);
        else
        {
            string cache_key = "cnt_chnl" + accounttype + "" + filteroption + "" + Term + "" + Character;
            if (HttpContext.Current.Cache[cache_key] == null)
            {
                StringBuilder str = new StringBuilder();
                str.Append("SELECT Count(u.UserName) FROM users as u WHERE u.isEnabled=1 " + Process_Channels_Logic(Term, Character, accounttype, country, gender, havephoto, filteroption));

                int total_records = Fetch_Total_Channel(Term, Character, accounttype, country, gender, havephoto, filteroption);

                HttpContext.Current.Cache.Add(cache_key, total_records, null, DateTime.Now.AddMinutes(Config.GetCacheDuration()), TimeSpan.Zero, CacheItemPriority.High, null);
            }
            return Convert.ToInt32(HttpContext.Current.Cache[cache_key]);
        }
        
    }

    private int Fetch_Total_Channel(string Term, string Character, string accounttype, string country, string gender, bool havephoto, int filteroption)
    {
        StringBuilder str = new StringBuilder();
        str.Append("SELECT Count(u.UserName) FROM users as u WHERE u.isEnabled=1 " + Process_Channels_Logic(Term, Character, accounttype, country, gender, havephoto, filteroption));
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, str.ToString(), new SqlParameter("@term", '%' + Term + '%'), new SqlParameter("@character", Character + '%'), new SqlParameter("@accounttype", accounttype), new SqlParameter("@gender", gender), new SqlParameter("@countryname", country)));
    }
    private string Process_Channels_Logic(string Term, string Character, string accounttype, string country, string gender, bool havephoto, int filteroption)
    {
        StringBuilder query = new StringBuilder();
        if (Term != "" || Character != "" || accounttype != "all" || gender != "" || country != "" || havephoto || filteroption != 0)
            query.Append(" AND");
        if (havephoto)
        {
            query.Append(" u.pictureName <> 'none'");
            if (Term != "" || Character != "" || accounttype != "all" || gender != "" || country != "" || filteroption != 0)
                query.Append(" AND");
        }
        if (gender != "")
        {
            query.Append(" u.gender = @gender");
            if (Term != "" || Character != "" || accounttype != "all" || country != "" || filteroption != 0)
                query.Append(" AND");
        }
        if (country != "")
        {
            query.Append(" u.countryname = @countryname");
            if (Term != "" || Character != "" || accounttype != "all" || filteroption != 0)
                query.Append(" AND");
        }
        if (Term != "")
        {
            query.Append(" (u.username like @term OR u.countryname like @term OR u.hometown like @term OR u.zipcode like @term)");
            if (Character != "" || accounttype != "all" || filteroption != 0)
                query.Append(" AND");
        }
        if (Character != "")
        {
            query.Append(" u.username like @character"); // '" + Character + "%'"); //@character");
            if (accounttype != "all" || filteroption != 0)
                query.Append(" AND");
        }
        if (accounttype != "all")
        {
            query.Append(" u.accounttype=@accounttype");
            if (filteroption != 0)
                query.Append(" AND");
        }

        if (filteroption != 0)
        {
            switch (filteroption)
            {
                case 1:
                    // today records
                    query.Append(" " + Config.Prepare_DateDiff("u.register_date", 0) + " ");

                    break;
                case 2:
                    // this week records
                    query.Append(" " + Config.Prepare_DateDiff("u.register_date", 1) + " ");
                    break;
                case 3:
                    // this month records
                    query.Append(" " + Config.Prepare_DateDiff("u.register_date", 2) + " ");
                    break;

            }
        }

        return query.ToString();
    }
      

    // channel auto complete
    public static string Load_User_AutoComplete(string term)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
      
        // generate sql query
        string query = "";
        // Mysql compatible
        query = "SELECT TOP 10 username from users WHERE isenabled=1 AND username like @term ORDER BY username asc";
        // SQL SERVER compatible
        //query = "SELECT Distinct TOP 10 username from users where isenabled=1 AND username like '" + term.Replace("'", "") + "%' ORDER BY username DESC";
        // execute sql query
        SqlCommand video_cmd = new SqlCommand(query, con);
        video_cmd.Parameters.Add(new SqlParameter("@term", term + '%'));
        SqlDataReader reader = video_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        StringBuilder suggestions = new StringBuilder();
        StringBuilder data = new StringBuilder();
        int counter = 0;
        suggestions.Append("suggestions:[");
        data.Append("data:[");
        while (reader.Read())
        {
            if (counter == 0)
            {
                //suggestions.Append("'" + reader["tagname"] + " - " + reader["records"] + " records'");
                suggestions.Append("'" + reader["username"] + "'");
                data.Append("'" + reader["username"] + "'");
            }
            else
            {
                // suggestions.Append(",'" + reader["tagname"] + " - " + reader["records"] + " records'");
                suggestions.Append(",'" + reader["username"] + "'");
                data.Append(",'" + reader["username"] + "'");
            }
            counter = counter + 1;
        }
        reader.Close();
        con.Close(); con.Dispose();
        suggestions.Append("]");
        data.Append("]");

        StringBuilder jsonoutput = new StringBuilder();
        jsonoutput.Append("{query:'" + term + "'," + suggestions.ToString() + "," + data.ToString() + "}");

        return jsonoutput.ToString();
    }

    
    // Load user profile data
    public static List<Member_Struct> Fetch_User_Profile(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;
        // execute sql query
        //SqlCommand user_cmd = new SqlCommand("VSK_Members_FetchProfileSM", con);
        //user_cmd.CommandType =CommandType.StoredProcedure;
        SqlCommand user_cmd = new SqlCommand("SELECT u.firstname,u.lastname,u.countryname,u.gender,u.picturename,u.relationshipstatus,u.aboutme,u.website,u.hometown,u.currentcity,u.zipcode,u.occupations,u.companies,u.schools,u.interests,u.movies,u.musics,u.books,u.isallowbirthday FROM users as u WHERE u.isEnabled=1 AND u.username=@username", con);
        user_cmd.CommandType =CommandType.Text;
        user_cmd.Parameters.Add(new SqlParameter("@username",username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.FirstName = reader["firstname"].ToString();
            _item.LastName = reader["lastname"].ToString();
            _item.CountryName = reader["countryname"].ToString();
            _item.Gender = reader["gender"].ToString();
            _item.PictureName = reader["picturename"].ToString();
            _item.RelationshipStatus = reader["relationshipstatus"].ToString();
            _item.AboutMe = reader["aboutme"].ToString();
            _item.Website = reader["website"].ToString();
            _item.HometTown = reader["hometown"].ToString();
            _item.CurrentCity = reader["currentcity"].ToString();
            _item.Zipcode = reader["zipcode"].ToString();
            _item.Occupations = reader["occupations"].ToString();
            _item.Companies = reader["companies"].ToString();
            _item.Schools = reader["schools"].ToString();
            _item.Movies = reader["movies"].ToString();
            _item.Musics = reader["musics"].ToString();
            _item.Interests = reader["interests"].ToString();
            _item.Books = reader["books"].ToString();
            _item.isAllowBirthDay = int.Parse(reader["isallowbirthday"].ToString());
            
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    // Fetch user channel and channel settings
    public static List<Member_Struct> Fetch_User_Channel(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // execute sql query
        //SqlCommand user_cmd = new SqlCommand("VSK_Members_FetchProfile", con);
        //user_cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand user_cmd = new SqlCommand("SELECT u.firstname,u.lastname,u.birthdate,u.countryname,u.gender,u.picturename,u.relationshipstatus,u.website,u.hometown,u.currentcity,u.zipcode,u.stat_comments,u.last_login,u.views,u.register_date,u.channel_iscomments,u.channel_name,u.channel_theme FROM users as u WHERE u.isEnabled=1 AND u.username=@username", con);

        user_cmd.CommandType = CommandType.Text;
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.FirstName = reader["firstname"].ToString();
            _item.LastName = reader["lastname"].ToString();
            _item.CountryName = reader["countryname"].ToString();
            _item.Gender = reader["gender"].ToString();
            _item.PictureName = reader["picturename"].ToString();
            _item.RelationshipStatus = reader["relationshipstatus"].ToString();
            
            _item.Website = reader["website"].ToString();
            _item.HometTown = reader["hometown"].ToString();
            _item.CurrentCity = reader["currentcity"].ToString();
            _item.Zipcode = reader["zipcode"].ToString();
            string llogin = reader["last_login"].ToString();
            if (llogin != "")
                _item.Last_Login = DateTime.Parse(llogin);
            else
                _item.Last_Login = DateTime.Now;
            _item.RegisterDate = (DateTime)reader["register_date"];
            _item.Views = Convert.ToInt32(reader["views"]);
            // count data
        
            _item.Count_Comments =  Convert.ToInt32(reader["stat_comments"]);
            
            // channel settings
            _item.Channel_isComments = Convert.ToInt32(reader["channel_iscomments"]);
           
            _item.Channel_Name = reader["channel_name"].ToString();
            _item.Channel_Theme = reader["channel_theme"].ToString();

            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

      // Fetch user detail profile
    public static List<Member_Struct> Fetch_User_DetailProfile(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // execute sql query
        //SqlCommand user_cmd = new SqlCommand("VSK_Members_DetailProfile", con);
        //user_cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand user_cmd = new SqlCommand("SELECT u.aboutme,u.occupations,u.companies,u.schools,u.movies,u.musics,u.interests,u.books,u.isallowbirthday,u.birthdate,channel_iscomments FROM users as u WHERE u.isEnabled=1 AND u.username=@username", con);
        user_cmd.CommandType = CommandType.Text;
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.AboutMe = reader["aboutme"].ToString();
            _item.Occupations = reader["occupations"].ToString();
            _item.Companies = reader["companies"].ToString();
            _item.Schools = reader["schools"].ToString();
            _item.Movies = reader["movies"].ToString();
            _item.Musics = reader["musics"].ToString();
            _item.Channel_isComments = Convert.ToInt32(reader["channel_iscomments"]);
            _item.Interests = reader["interests"].ToString();
            _item.Books = reader["books"].ToString();
            _item.isAllowBirthDay = Convert.ToInt32(reader["isallowbirthday"]);
            string birthdate = reader["birthdate"].ToString();
            if (birthdate != "")
                _item.BirthDate = DateTime.Parse(birthdate);
            else
                _item.Last_Login = DateTime.Now;

            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }
    
    // Fetch user channel sm information
    // Cache Version
    public static List<Member_Struct> Fetch_User_Channel_SM(string username)
    {
        // cache implementation
        int cache_duration = Config.GetCacheDuration();
        if (cache_duration == 0) // no cache
            return members.Fetch_User_Channel_SM_NoCache(username);
        else
        {
            if (HttpContext.Current.Cache["ld_mem_sm_" + username] == null)
            {
                HttpContext.Current.Cache.Add("ld_mem_sm_" + username, members.Fetch_User_Channel_SM_NoCache(username), null, DateTime.Now.AddMinutes(cache_duration), TimeSpan.Zero, CacheItemPriority.High, null);
            }

            List<Member_Struct> _list = (List<Member_Struct>)(HttpContext.Current.Cache["ld_mem_sm_" + username]);
            return _list;
        }

    }
    // Fetch user channel sm information
    // NoCache Version
    public static List<Member_Struct> Fetch_User_Channel_SM_NoCache(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        //// generate sql query
        StringBuilder str = new StringBuilder();
        // load user profile
        str.Append("SELECT u.picturename,u.stat_comments,u.channel_iscomments,u.channel_name,u.channel_theme");
        // remaining query
        str.Append(" FROM users as u WHERE u.isEnabled=1 AND u.username=@username");

        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.PictureName = reader["picturename"].ToString();
            // count data
          
            _item.Count_Comments = Convert.ToInt32(reader["stat_comments"]);
           
          
            // channel settings
            _item.Channel_isComments = Convert.ToInt32(reader["channel_iscomments"]);
          
            _item.Channel_Name = reader["channel_name"].ToString();
            _item.Channel_Theme = reader["channel_theme"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }
    
    // get account status information for admin use
    public static List<Member_Struct> Fetch_User_Status_Info(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT isEnabled,Type,roleid FROM users where username=@username");

        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.isEnabled = Convert.ToInt32(reader["isEnabled"]);
            _item.Type = Convert.ToInt32(reader["type"]);
          
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    // load all user usernames
    public static List<Member_Struct> Fetch_User_UserNames(int type)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // Query Building
        StringBuilder str = new StringBuilder();
        str.Append("SELECT username FROM users WHERE type=@type");
        // execute sql query
        SqlCommand video_cmd = new SqlCommand(str.ToString(), con);
        video_cmd.Parameters.Add(new SqlParameter("@type", type));
        SqlDataReader reader = video_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.UserName = reader["username"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    public static int Return_User_Role(string username)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT roleid from users where username=@username",new SqlParameter("@username",username)));
    }

    public static bool Update_User_Role(string UserName, int roleid)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set roleid=@roleid WHERE username=@username", new SqlParameter("@username", UserName), new SqlParameter("@roleid", roleid));
        return true;
    }
    #endregion

    
    // Get Encrypted Password For Encoding Purpose
    public static List<Member_Struct> Get_Hash_Password(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // generate sql query
        StringBuilder str = new StringBuilder();
        // execute sql query
        SqlCommand user_cmd = new SqlCommand("SELECT username, password FROM users WHERE isEnabled=1 AND username=@uname", con);
        user_cmd.CommandType = CommandType.Text;
        //SqlCommand user_cmd = new SqlCommand("VSK_Members_GetPassword", con);
        //user_cmd.CommandType = CommandType.StoredProcedure;
        user_cmd.Parameters.Add(new SqlParameter("@uname", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.UserName = reader["username"].ToString();
            _item.Password = reader["password"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }


    // fetch user information for generating preview
    public static List<Member_Struct> Fetch_User_Info(string UserName)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT countryname,firstname,lastname,gender,picturename,relationshipstatus,aboutme,website,hometown,currentcity FROM users WHERE username=@username AND isenabled=1");
        // execute sql query
        SqlCommand video_cmd = new SqlCommand(str.ToString(), con);
        video_cmd.Parameters.Add(new SqlParameter("@username", UserName));
        SqlDataReader reader = video_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.CountryName = reader["countryname"].ToString();
            _item.FirstName = reader["firstname"].ToString();
            _item.LastName = reader["lastname"].ToString();
            _item.Gender = reader["gender"].ToString();
            _item.PictureName = reader["picturename"].ToString();
            _item.RelationshipStatus = reader["relationshipstatus"].ToString();
            _item.AboutMe = reader["aboutme"].ToString();
            _item.Website = reader["website"].ToString();
            _item.HometTown = reader["hometown"].ToString();
            _item.CurrentCity = reader["currentcity"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    // Send Mail To Admin When User Complete Registration
    public static void MailTemplateProcess(string username, string email)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRREGADM");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[email\\]", email);

                string emailaddress = Site_Settings.Admin_Mail;
                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }


    // load all user users info for sending email
    public static List<Member_Struct> Fetch_User_UserNames()
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // Query Building
        StringBuilder str = new StringBuilder();
        str.Append("SELECT username,email,isautomail FROM users where isEnabled=1");
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.UserName = reader["username"].ToString();
            _item.Email = reader["email"].ToString();
            _item.isAutoMail = Convert.ToInt32(reader["isautomail"].ToString());
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }
}
