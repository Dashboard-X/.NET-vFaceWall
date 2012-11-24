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
public class User_Ratings
{
   
    //**********************************************************************************************************************************
    //**********************************************************************************************************************************
    // Store User Ratings in Database.
    //**********************************************************************************************************************************
    //**********************************************************************************************************************************

    // Note: Rating Important Terms

    // Type:
    // ............ 0:- Video Rating
    // ............ 1:- Video Comment Advice Rating
    // ............ 2:- Photo Rating
    // ............ 3:- Blog Rating
    // ............ 9:- Photo Galleries
    // ............ 10:- Group Posts
    // ............ 11: - Group Po
    // ............ 12: - QA Votes
    // ............ 13: - QA Answer Votes
    // ............ 14: - User Activities

    // Rating:
    // ........... video | audio,  0: liked, 1: disliked
	public User_Ratings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Add(string username, long itemid, int type, int rating)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert Into user_ratings(itemid,username,type,rating)values(@itemid,@username,@type,@rating)", new SqlParameter("@itemid", itemid), new SqlParameter("@username", username), new SqlParameter("@type", type), new SqlParameter("@rating", rating));
        return true;
    }

    public static bool Delete(long itemid,int type)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from user_ratings where itemid=@itemid and type=@type",new SqlParameter("@type",type),new SqlParameter("@itemid",itemid));
        return true;
    }

    public static bool Delete(string username) 
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString,CommandType.Text,"Delete from user_ratings where username=@username",new SqlParameter("@username",username));
        return true;
    }

    public static bool Delete(string username,long itemid,int type)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from user_ratings where username=@username AND itemid=@itemid AND type=@type",new SqlParameter("@username",username),new SqlParameter("@itemid",itemid),new SqlParameter("@type",type));
        return true;
    }
    public static bool Check(string username,long itemid,int type)
     {
         int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(itemid) from user_ratings where username=@username AND itemid=@itemid AND type=@type",new SqlParameter("@username",username),new SqlParameter("@itemid",itemid),new SqlParameter("@type",type)));
         if (result>0)
             return true;
         else
             return false;
     }

    public static DataSet Load(string username)
    {
        return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from user_ratings where username=@username order by id desc", new SqlParameter("@username", username));
    }
    
}
