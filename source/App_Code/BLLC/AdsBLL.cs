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
public class AdsBLL
{
    // Note: AdsBLL handle advertisement on website

    // This BLL handles admin related activities on control panel.

    // Type:
    // ........... 1: Adult Section Ads -/ will popup if user access adult content
    // ........... 0: Non Adult Section Ads -/ will popup if user access normal content.


	public AdsBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Add_Script(string name, string adscript, int type)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into advertisement(name,adscript,type)values(@name,@adscript,@type)", new SqlParameter("@name", name), new SqlParameter("@adscript", adscript), new SqlParameter("@type", type));
        return true;
    }

    public static bool Update_Script(int adid, string name, string adscript)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update advertisement set name=@name,adscript=@adscript where adid=@adid", new SqlParameter("@adscript", adscript), new SqlParameter("@adid", adid), new SqlParameter("@name", name));
        return true;
    }

    public static bool Count_Script()
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(adid) from advertisement"));
        if (result > 0)
            return true;
        else
           return false;
    }

    public static DataSet Load_Ads(int type)
    {
        return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from advertisement where type=@type",new SqlParameter("@type",type));
    }

    public static string Fetch_Ad_Script(int id)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT adscript from advertisement where adid=@id",new SqlParameter("@id",id)).ToString();
    }

    public static string Return_Ad_Script(int id)
    {
        if (HttpContext.Current.Application["a" + id] == null)
        {
            HttpContext.Current.Application["a" + id] = Fetch_Ad_Script(id);
        }
        return HttpContext.Current.Application["a" + id].ToString();
    }

}
