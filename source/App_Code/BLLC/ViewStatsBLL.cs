using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class ViewStatsBLL
{
	public ViewStatsBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Note: Important View Stat Terms

    // Table Used:
    // i: view_stat_today // handles today viewed contents
    // ii: view_stat_thisweek // handles thisweek viewed contents
    // iii: view_stat_thismonth // handles this month viewed contents

    // itemtype:
    // ........... 1: Videos | Audio Files
    // ........... 2: Photos
    // ........... 3: Blogs
    // ........... 4: Galleries
    // ........... 5: Forum
    // ........... 6: Groups
    /// <summary>
    /// Handles view statistic of contents
    /// </summary>
    public static void Process_View_Stats(long contentid, int itemtype, int views)
    { 
        //*******************************************************
        // Comment advance view statistic section if you not want advance view statistic
        // By commenting this section today, thisweek, this month section will not load data
        // Advance View Statistic Section
        //************************************************************
        // Clear old statistics :-> Scalable approach is to call this function from admin section in daily basis manually
        Clear_Old_Stats();

        // increment views
        views = views + 1;
        // increment today views
        Increment_Views_Today(contentid, itemtype);
        // increment thisweek views
        Increment_Views_ThisWeek(contentid, itemtype);
        // increment thismonth views
        Increment_Views_ThisMonth(contentid, itemtype);

        //*****************************************************
        // Close Advance View Statistic Section
        //*****************************************************
        switch (itemtype)
        {
            case 1:
                // increment alltime video views
                Increment_Video_Views_AllTime(contentid, views);
                break;
            case 2:
                // increment alltime photo views
                Increment_Photo_Views_AllTime(contentid, views);
                break;
            case 3:
                // increment alltime blog views
                Increment_Blog_Views_AllTime(contentid, views);
                break;
        }
    }
    
    /// <summary>
    /// Delete all older stats
    /// </summary>
    public static void Clear_Old_Stats()
    {
        if (HttpContext.Current.Cache["view_stat_clear_data"] == null)
        {
            // Today :-> clear all records from view_stat_today which is older than today
            string query = "Delete from view_stat_today where";
            //query = query + " DateDiff(added_date,CURDATE())<=-1"; // mysql version
            query = query + " DateDiff(DAY,added_date,getdate())>=1"; // sql server
            SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, query);
            // This Week :-> clear all records from view_stat_thisweek which isolder than this week
            query = "Delete from view_stat_thisweek where";
            //query = query + " DateDiff(added_date,CURDATE())<=-7"; // mysql version
            query = query + " DateDiff(DAY,added_date,getdate())>=7"; //sql server
            SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, query);
            // This Month :-> clear all records from view_stat_thismonth which is older than this month
            query = "Delete from view_stat_thismonth where";
            //query = query + " DateDiff(added_date,CURDATE())<=-31"; // mysql version
            query = query + " DateDiff(DAY,added_date,getdate())>=31"; // sql server
            SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, query);
            // populate cache
            HttpContext.Current.Cache.Add("view_stat_clear_data", "ClearOK", null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }

    private static void Increment_Views_Today(long contentid, int itemtype)
    {
        if (!Check_Today(contentid, itemtype))
        {
            // Add record
            Add_Views(contentid, itemtype, "view_stat_today");
        }
        else
        {
            // Increment record
            // retrieve view stats for selected record
            int views = Get_Views(contentid, itemtype, "view_stat_today");
            views = views + 1;
            // update view stats
            Update_Views(contentid, itemtype, views, "view_stat_today");
        }
    }

    private static void Increment_Views_ThisWeek(long contentid, int itemtype)
    {
        if (!Check_ThisWeek(contentid, itemtype))
        {
            // Add record
            Add_Views(contentid, itemtype, "view_stat_thisweek");
        }
        else
        {
            // Increment record
            // retrieve view stats for selected record
            int views = Get_Views(contentid, itemtype, "view_stat_thisweek");
            views = views + 1;
            // update view stats
            Update_Views(contentid, itemtype, views, "view_stat_thisweek");
        }
    }

    private static void Increment_Views_ThisMonth(long contentid, int itemtype)
    {
        if (!Check_ThisMonth(contentid, itemtype))
        {
            // Add record
            Add_Views(contentid, itemtype, "view_stat_thismonth");
        }
        else
        {
            // Increment record
            // retrieve view stats for selected record
            int views = Get_Views(contentid, itemtype, "view_stat_thismonth");
            views = views + 1;
            // update view stats
            Update_Views(contentid, itemtype, views, "view_stat_thismonth");
        }
    }
        

    private static void Increment_Video_Views_AllTime(long contentid, int views)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update videos set views=" + views + " where VideoID=" + contentid + "");
    }

    private static void Increment_Photo_Views_AllTime(long contentid, int views)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update photos set views=" + views + " where ImageID=" + contentid + "");
    }

    private static void Increment_Blog_Views_AllTime(long contentid, int views)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update blogs set views=" + views + " where PostID=" + contentid + "");
    }

    // Check contents
    private static bool Check_Today(long contentid, int itemtype)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(contentid) from view_stat_today WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
        if (result > 0)
            return true;
        else
            return false;
    }

    private static bool Check_ThisWeek(long contentid, int itemtype)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(contentid) from view_stat_thisweek WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
        if (result > 0)
            return true;
        else
            return false;
    }

    private static bool Check_ThisMonth(long contentid, int itemtype)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(contentid) from view_stat_thismonth WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
        if (result > 0)
            return true;
        else
            return false;
    }

    // Add Views
    private static void Add_Views(long contentid, int itemtype, string tablename)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into " + tablename + "(contentid,itemtype,added_date)values(@contentid,@itemtype,@added_date)", new SqlParameter("@contentid", contentid), new SqlParameter("@itemtype", itemtype), new SqlParameter("@added_date", DateTime.Now));
    }
    // Get Views
    private static int Get_Views(long contentid, int itemtype, string tablename)
    {
       return (int)SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT views FROM " + tablename + " WHERE ContentID=" + contentid + " AND itemtype=" + itemtype);
    }
    // Update Views
    private static void Update_Views(long contentid, int itemtype, int views, string tablename)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update " + tablename + " set views=" + views + " WHERE ContentID=" + contentid + " AND itemtype=" + itemtype);
    }

    // Get Content Views
    public static int Get_Views_Today(long contentid, int itemtype)
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT views from view_stat_today WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
    }

    public static int Get_Views_ThisWeek(long contentid, int itemtype)
    {
       return  Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT views from view_stat_thisweek WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
    }

    public static int Get_Views_ThisMonth(long contentid, int itemtype)
    {
       return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT views from view_stat_thismonth WHERE ContentID=" + contentid + " AND itemtype=" + itemtype + ""));
    }
}
