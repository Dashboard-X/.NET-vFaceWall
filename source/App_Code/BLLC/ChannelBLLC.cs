using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class ChannelBLLC
{
    // ChannelBLLC handles user actions on his/her channel/profile.
  
	public ChannelBLLC()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Update user channel settings

    public static bool Update_Channel_Settings(string username, string channel_name)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set channel_name=@channel_name WHERE username=@username",new SqlParameter("@channel_name",channel_name),new SqlParameter("@username",username));
        return true;
    }

    public static bool Update_Channel_Theme(string username, string channel_theme)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update users set channel_theme=@channel_theme WHERE username=@username", new SqlParameter("@channel_theme", channel_theme), new SqlParameter("@username", username));
        return true;
    }

    public static bool Update_Module_Settings(string username, int channel_iscomments,int channel_isfriends,int channel_issubscribers,int channel_isgroups,int channel_isphotos,int channel_isblogs,int channel_isaudios)
    {
        // nothing to update
        return true;
    }

   
}
