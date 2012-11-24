using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Collections.Generic;
using System.Web.Caching;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class ActivityBLL
{
	public ActivityBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static long Add(UserActivity_Struct gal)
    {       
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("Insert Into useractivities(username,title,activity,added_date,poster_username)values(@username,@title,@activity,@added_date,@poster_username);Select SCOPE_IDENTITY();");
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@username", gal.UserName));
        cmd.Parameters.Add(new SqlParameter("@title", gal.Title));
        cmd.Parameters.Add(new SqlParameter("@activity", gal.Activity));
        cmd.Parameters.Add(new SqlParameter("@added_date", DateTime.Now));
        cmd.Parameters.Add(new SqlParameter("@poster_username", gal.PosterUserName));
        if (con.State != ConnectionState.Open) 
            con.Open();
        return Convert.ToInt64(cmd.ExecuteScalar());
    }

    public static bool Delete(long ActivityID)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete From useractivities WHERE activityid=@activityid", new SqlParameter("@activityid", ActivityID));
        return true;
    }

    public static bool Update_Field(long ActivityID, string value, string FieldName)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update useractivities Set " + FieldName + "=@FieldValue WHERE activityid=@activityid", new SqlParameter("@FieldValue", value), new SqlParameter("@activityid", ActivityID));
        return true;
    }

    public static string Get_Field_Value(long ActivityID, string FieldName)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT " + FieldName + " FROM useractivities WHERE activityid=@activityid", new SqlParameter("@activityid", ActivityID)).ToString();
    }

    #region Core Loading Script
    //**************************************************************************************
    // Core Engine for generating cached and non cached photo listings
    //**************************************************************************************

    public static List<UserActivity_Struct> Load_Activities(string term, string UserName, int Month, int Year, string order, int records, int datefilter, bool iscache, int PageNumber)
    {
        if (!iscache || Config.GetCacheDuration() == 0)
        {
            return Fetch_Activities(term, UserName, Month, Year, order, records, datefilter,PageNumber);
        }
        else
        {
            // cache implementation
            StringBuilder cache = new StringBuilder();
            string lang = "";
           
            cache.Append("ft_uactivity_lm_" + lang + UserName + "" + Month + "" + Year + "" + records + "" + PageNumber + "" + datefilter + "" + UtilityBLL.ReplaceSpaceWithHyphin(order.ToLower()));
            if (term != "")
                cache.Append(UtilityBLL.ReplaceSpaceWithHyphin(term.ToLower()));


            if (HttpContext.Current.Cache[cache.ToString()] == null)
                HttpContext.Current.Cache.Add(cache.ToString(), Fetch_Activities(term, UserName, Month, Year, order, records, datefilter, PageNumber), null, DateTime.Now.AddMinutes(Config.GetCacheDuration()), TimeSpan.Zero, CacheItemPriority.High, null);

            List<UserActivity_Struct> _list = (List<UserActivity_Struct>)(HttpContext.Current.Cache[cache.ToString()]);
            return _list;
        }
    }

    // The following function load specific number of records without any pagination. e.g Fetching top 8 recently added photos
    // Non Cache Version
    public static List<UserActivity_Struct> Fetch_Activities(string term, string UserName, int Month, int Year, string order, int records, int datefilter, int PageNumber)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<UserActivity_Struct> _items = new List<UserActivity_Struct>();
        UserActivity_Struct _item;

        // generate sql query
        string logic = ActivityBLL.Process_Activity_V3_Logic(term, UserName, Month, Year, datefilter);
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        query.Append("u.picturename,p.activityid,p.username,p.poster_username,p.activity,p.title,p.added_date,p.liked,p.disliked,u.picturename,p.comments FROM useractivities as p Inner join users as u on u.username=p.poster_username " + logic);

        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), order, PageNumber, records));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, records));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(normal_query);
                break;
        }
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.Parameters.Add(new SqlParameter("@username", UserName));
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new UserActivity_Struct();
            _item.ActivityID = (long)reader["activityid"];

            _item.UserName = reader["username"].ToString();
            _item.Title = reader["title"].ToString();
            _item.Activity = reader["activity"].ToString();
            _item.Added_Date = (DateTime)reader["added_date"];
            _item.Liked = Convert.ToInt32(reader["liked"]);
            _item.Disliked = Convert.ToInt32(reader["disliked"]);
            _item.Comments = Convert.ToInt32(reader["comments"]);
            _item.PictureName = reader["picturename"].ToString();
            _item.PosterUserName = reader["poster_username"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }

    // Count Group Posts
    public static int Count_Activities(string term, string UserName, int Month, int Year, string order, int records, int datefilter)
    {
        // generate sql query
        string logic = ActivityBLL.Process_Activity_V3_Logic(term, UserName, Month, Year, datefilter);
        StringBuilder query = new StringBuilder();
        query.Append("Select count(p.activityid) from useractivities as p " + logic);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, query.ToString(), new SqlParameter("@username", UserName)));
    }

    private static string Process_Activity_V3_Logic(string Term, string username, int Month, int Year, int datefilter)
    {
        StringBuilder script = new StringBuilder();
        if (Term != "" || username != "" || Month > 0 || datefilter > 0)
        {
            script.Append("WHERE"); // no filter isenabled
        }
        
        if (Month > 0 && Year > 0)
        {
            // archive option (month and year both)
            script.Append(" Year(p.added_date)=" + Year + " AND MONTH(p.added_date)= " + Month);
            if (Term != "" || username != "" || datefilter > 0)
                script.Append(" AND");
        }
        else if (Year > 0)
        {
            // archive option (only year)
            script.Append(" Year(p.added_date)=" + Year + "");
            if (Term != "" || username != "" || datefilter > 0)
                script.Append(" AND");
        }
        if (username != "")
        {
            script.Append(" p.username=@username");
            if (Term != "" || datefilter > 0)
                script.Append(" AND");
        }
        if (datefilter != 0)
        {
            switch (datefilter)
            {
                case 1:
                    // today records
                    script.Append(" " + Config.Prepare_DateDiff("p.added_date", 0) + " ");
                    break;
                case 2:
                    // this week records
                    script.Append(" " + Config.Prepare_DateDiff("p.added_date", 1) + " ");
                    break;
                case 3:
                    // this month records
                    script.Append(" " + Config.Prepare_DateDiff("p.added_date", 2) + " ");
                    break;
            }
            if (Term != "")
                script.Append(" AND");
        }
        if (Term != "")
        {
            script.Append(" p.activity like '%" + Term + "%'");
        }
        return script.ToString();
    }

    //**************************************************************************************
    // Close Core Engine for generating cached and non cached photo listings
    //************************************************************************************** 
    #endregion

    // Fetch user activies
    public static List<UserActivity_Struct> Fetch_Activies(string UserName)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<UserActivity_Struct> _items = new List<UserActivity_Struct>();
        UserActivity_Struct _item;
        // generate sql query
        StringBuilder str = new StringBuilder();
        // execute sql query
        SqlCommand cmd = new SqlCommand("SELECT p.activityid,p.username,p.poster_username,p.title,p.activity,p.added_date,p.liked,p.disliked,u.picturename FROM useractivities as p INNER JOIN users as u on u.username=p.poster_username WHERE p.username=@uname AND u.isenabled=1 Order By p.added_date desc, p.activityid desc", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@uname", UserName));
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new UserActivity_Struct();
            _item.ActivityID = (long)reader["activityid"];
            _item.UserName = reader["username"].ToString();
            _item.Activity = reader["activity"].ToString();
            _item.Added_Date = (DateTime)reader["added_date"];
            _item.Liked = Convert.ToInt32(reader["liked"]);
            _item.Disliked = Convert.ToInt32(reader["disliked"]);
            _item.PosterUserName = reader["poster_username"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;
    }
}
