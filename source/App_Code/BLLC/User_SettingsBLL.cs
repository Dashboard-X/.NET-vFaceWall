using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class User_SettingsBLL
{
	public User_SettingsBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // update user mail options
    public static bool Update_User_Mail_Settings(string username, int isautomail, int mail_vcomment,int mail_ccomment,int mail_pmessage,int mail_finvite,int mail_subscribe)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set isautomail=@isautomail,mail_vcomment=@mail_vcomment,mail_ccomment=@mail_ccomment,mail_pmessage=@mail_pmessage,mail_finvite=@mail_finvite,mail_subscribe=@mail_subscribe where username=@username",new SqlParameter("@username",username),new SqlParameter("@isautomail",isautomail),new SqlParameter("@mail_vcomment",mail_vcomment),new SqlParameter("@mail_ccomment",mail_ccomment),new SqlParameter("@mail_pmessage",mail_pmessage),new SqlParameter("@mail_finvite",mail_finvite),new SqlParameter("@mail_subscribe",mail_subscribe));
        return true;
    }

    // update user privacy options
    public static bool Update_User_Privacy_Settings(string username, int privacy_fmessages)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set privacy_fmessages=@privacy_fmessages where username=@username", new SqlParameter("@username", username), new SqlParameter("@privacy_fmessages", privacy_fmessages));
        return true;
    }

    // Load user mail options
    public static List<Member_Struct> Fetch_User_Mail_Options(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT email,isautomail,mail_vcomment,mail_ccomment,mail_pmessage,mail_finvite,mail_subscribe from users WHERE isEnabled=1 AND username=@username");

        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.isAutoMail = int.Parse(reader["isautomail"].ToString());
            _item.Mail_VComment = int.Parse(reader["mail_vcomment"].ToString());
            _item.Mail_CComment = int.Parse(reader["mail_ccomment"].ToString());
            _item.Mail_PMessage = int.Parse(reader["mail_pmessage"].ToString());
            _item.Mail_FInvite = int.Parse(reader["mail_finvite"].ToString());
            _item.Mail_Subscribe = int.Parse(reader["mail_subscribe"].ToString());
            _item.Email = reader["email"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close();

        return _items;
    }

    // load user privacy options
    public static List<Member_Struct> Fetch_User_Privacy_Options(string username)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        List<Member_Struct> _items = new List<Member_Struct>();
        Member_Struct _item;

        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT privacy_fmessages from users WHERE isEnabled=1 AND username=@username");

        // execute sql query
        SqlCommand user_cmd = new SqlCommand(str.ToString(), con);
        user_cmd.Parameters.Add(new SqlParameter("@username", username));
        SqlDataReader reader = user_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Member_Struct();
            _item.Privacy_FMessages = int.Parse(reader["privacy_fmessages"].ToString());
            _items.Add(_item);
        }
        reader.Close();
        con.Close();

        return _items;
    }

    // return true if user allow sending mail when someone comment on video or post video response
    public static bool isMail_VideoComemnt(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND mail_vcomment=1 AND username=@username",new SqlParameter("@username",username)));
        if (result > 0)
            return true;
        else
            return false;
    }

    // return true if user allow sending mail when someone comment on his/her channel
    public static bool isMail_ChannelComemnt(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND mail_ccomment=1 AND username=@username",new SqlParameter("@username",username)));
        if (result > 0)
            return true;
        else
            return false;
    }

    // return true if user allow sending mail when someone send private message
   
    public static bool isMail_PrivateMessage(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND mail_pmessage=1 AND username=@username", new SqlParameter("@username", username)));
        if (result > 0)
            return true;
        else
            return false;
    }

    // return true if user allow sending mail when someone send friend invitation
    public static bool isMail_FriendInvite(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND mail_finvite=1 AND username=@username", new SqlParameter("@username", username)));
        if (result > 0)
            return true;
        else
            return false;
    }

    // return true if user allow sending mail when user subscribe their channel
    public static bool isMail_ChannelSubscribe(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND mail_subscribe=1 AND username=@username", new SqlParameter("@username", username)));
        if (result > 0)
            return true;
        else
            return false;
    }

    // return true if user allow only friends only to send private messages
    // return false if user allow to send private messages by all users.
    public static bool isMessages_AllowFriendsOnly(string username)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "Select Count(username) from users where isautomail=1 AND privacy_fmessages=1 AND username=@username", new SqlParameter("@username", username)));
        if (result > 0)
            return true;
        else
            return false;
    }
    


}
