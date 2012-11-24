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
using System.IO;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class FacebookBLLC
{
	public FacebookBLLC()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // create account for facebook user
    public static bool Add(string UserName, string FirstName, string LastName, string Password, string Email, string Country, int isEnabled, string gender, DateTime birthdate, string picturename, string fb_uid, int roleid)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert Into users(UserName,FirstName,LastName,Password,Email,CountryName,Register_Date,isEnabled,Last_Login,gender,birthdate,picturename,fb_uid,credits,remained_video,remained_audio,remained_gallery,remained_photos,remained_blogs,space_video,space_audio,space_photos,roleid)values(@UserName,@FirstName,@LastName,@Password,@Email,@CountryName,@Register_Date,@isEnabled,@Last_Login,@gender,@birthdate,@picturename,@fb_uid,@credits,@remained_video,@remained_audio,@remained_gallery,@remained_photos,@remained_blogs,@space_video,@space_audio,@space_photos,@roleid)", new SqlParameter("@UserName", UserName), new SqlParameter("@FirstName", FirstName), new SqlParameter("@LastName", LastName), new SqlParameter("@Email", Email), new SqlParameter("@CountryName", Country), new SqlParameter("@Password", Password), new SqlParameter("@Register_Date", DateTime.Now), new SqlParameter("@isEnabled", isEnabled), new SqlParameter("@Last_Login", DateTime.Now), new SqlParameter("@birthdate", birthdate), new SqlParameter("@gender", gender), new SqlParameter("@picturename", picturename), new SqlParameter("@fb_uid", fb_uid), new SqlParameter("@roleid",roleid));
        // send notification mail to admin
        members.MailTemplateProcess(UserName, Email);
        return true;
    }

    // check whether facebook uid exist in user table
    public static bool Check_UID(string fb_uid)
    {
        int result= Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT count(fb_uid) from users where fb_uid=@fbuid",new SqlParameter("@fbuid",fb_uid)));
        if (result > 0)
            return true;
        else 
            return false;
    }

    // get username via facebook uid
    public static string Get_UserName(string fb_uid)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT username from users where fb_uid=@fbuid",new SqlParameter("@fbuid",fb_uid)).ToString();
       
    }
}