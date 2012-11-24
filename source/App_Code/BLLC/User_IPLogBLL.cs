using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.IO;
using Sql.DataAccessLayer;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class User_IPLogBLL
{
  
	public User_IPLogBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Add_Ipaddress(string username, string ipaddress)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into users_ipaddress(username,ipaddress,date_added)values(@username,@ipaddress,@date_added)", new SqlParameter("@username", username), new SqlParameter("@ipaddress", ipaddress), new SqlParameter("@date_added", DateTime.Now));
        return true;
    }

    public static bool Delete_IPAddress(string username)
    {
        long serialno = Get_Old_SerialID(username);
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from users_ipaddress where serialno=@seriorno", new SqlParameter("@seriorno", serialno));
        return true;
    }

    public static long Get_Old_SerialID(string username)
    {
        return long.Parse(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT top 1 serialno from users_ipaddress where username=@username order by serialno asc", new SqlParameter("@username", username)).ToString());
    }

    public static int Count_Ipaddress(string username)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT COUNT(username) from users_ipaddress where username=@username", new SqlParameter("@username", username)));
    }

    public static bool Process_Ipaddress_Log(string username, string ipaddress)
    {
        int count = Count_Ipaddress(username);
        // keep top 5 login ip logs of each user
        if (count > 5)
        {
            // delete old ip address log
            Delete_IPAddress(username);
            // add ip address log
            Add_Ipaddress(username, ipaddress);
        }
        else
        {
            Add_Ipaddress(username, ipaddress);
        }
        return true;
    }

    public static DataSet load_ipaddress(string username)
    {
        return SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from users_ipaddress where username=@username",new SqlParameter("@username",username));
    }

}
