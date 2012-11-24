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
public class BlockIPBLL
{
    // Note: BlockIPBLL used to take actions on certain ip address.
    
 
	public BlockIPBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  
    public static bool Add_IP(string ipaddress)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into blockip(ipaddress,date_added)values(@ipaddress,@date_added)", new SqlParameter("@ipaddress", ipaddress), new SqlParameter("@date_added", DateTime.Now));
        return true;
    }

    public static bool Delete_IP(int id)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from blockip where id=@id",new SqlParameter("@id",id));
        return true;
    }

    public static DataSet Load_IP()
    {
        return SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from blockip order by id desc");
    }

    public static bool Validate_IP(string ipaddress)
    {
        int result =Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(id) from blockip where ipaddress=@ipaddress",new SqlParameter("@ipaddress",ipaddress)));
        if (result > 0)
            return true;
        else
           return false;
    }

}
