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
public class Administration
{
    // Note: administration Important Terms

    // This BLL handles admin related activities on control panel.

    // readonly:
    // ........... 1: ReadOnly Access to current admin in control panel.
    // ........... 0: Full Access to current admin in control panel

    public bool isReadOnly(string UserName)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT readonly from users where username=@username",new SqlParameter("@username",UserName)));
        if (result == 1)
            return true;
        else
            return false;
    }

}
