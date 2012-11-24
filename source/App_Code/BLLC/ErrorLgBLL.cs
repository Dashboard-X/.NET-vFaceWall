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
public class ErrorLgBLL
{
    // ErrorLog store error occurs when user browse website.
	public ErrorLgBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Add_Log(string description, string url, string stack_trace)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into error_log(description,url,stack_trace,added_date)values(@description,@url,@stack_trace,@added_date)", new SqlParameter("@description", description), new SqlParameter("@url", url), new SqlParameter("@stack_trace", stack_trace), new SqlParameter("@added_date", DateTime.Now));
        return true;
    }

    public static bool Delete_Log(int id)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from error_log where id=@id", new SqlParameter("@id", id));
        return true;
    }

    public static bool Delete_Log()
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from error_log");
        return true;
    }

    public static DataSet Load_Log()
    {
        return SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from error_log order by added_date desc");
    }

    public static DataSet Get_Log(int id)
    {
        return SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from error_log where id=" + id + " order by added_date desc");
    }

}
