using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Sql.DataAccessLayer;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class ConfigurationBLL
{
  
	public ConfigurationBLL()
	{}

    public static void Update_Value(int id, string value)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "UPDATE configurations set value=@value where id=" + id, new SqlParameter("@value", value));
        // Refresh Application Stat Value
        string cache_key = "config_" + id;
        HttpContext.Current.Cache[cache_key] = value;
    }

    public static string Return_Value(int id)
    {
        // Implement Cache Approach To Store Configuration Values
        string cache_key = "config_" + id;
        if (HttpContext.Current.Cache[cache_key] == null)
            HttpContext.Current.Cache.Add(cache_key, Return_NoCache_Value(id), null, DateTime.Now.AddHours(6), TimeSpan.Zero, CacheItemPriority.High, null);

        return HttpContext.Current.Cache[cache_key.ToString()].ToString();
    }

    public static string Return_NoCache_Value(int id)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT value from configurations where id=" + id + "").ToString();
    }
}