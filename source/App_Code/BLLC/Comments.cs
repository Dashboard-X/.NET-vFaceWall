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
public class CommentsBLL
{ 

    // Important User Comment Terms

    // isEnabled:
    // ........... 0: Disable comment
    // ........... 1: Enable comment

    // Type:
    // ........... 0: Videos | Audio Files
    // ........... 1: Blogs // 
    // ........... 2: Photos //
    // ........... 3: Photo Gallery
    // ........... 11: QA Question
    // ........... 12: QA Answers
    // ........... 13: Group Post Comments
    // ........... 14: User Channel Activities
   
    // isapproved:
    // ........... 0: Not approved
    // ........... 1: Approved

    public CommentsBLL()
	{
	
	}


    // Post Comment

    public static long Add(Comment_Struct cmt, int Comments)
    {

        // *********************************************************//
        // SCREENING Data Script: Updated in VSK 5.2
        // *********************************************************//
        if (Config.Get_ScreeningOption() == 1)
        {
            // screening, matching and replacing enabled
            // the following script will screen content with matching words e.g "apple" -> "a***e"
            cmt.Comment = DictionaryBLL.Process_Screening(cmt.Comment);
        }
        //// Stored Procedure Version
        //SqlParameter CommentID = new SqlParameter("@CommentID", MySqlDbType.Int64);
        //CommentID.Direction = ParameterDirection.Output;
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_Post", CommentID, new SqlParameter("@VideoID", cmt.VideoID), new SqlParameter("@username", cmt.UserName), new SqlParameter("@comments", cmt.Comment), new SqlParameter("@date_added", DateTime.Now), new SqlParameter("@type", cmt.Type), new SqlParameter("@replyid", cmt.ReplyID), new SqlParameter("@profileid", cmt.ProfileID));
         SqlConnection con = new SqlConnection(Config.ConnectionString);
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("Insert Into comments(VideoID,username,_comment,added_date,type,replyid,profileid)values(@VideoID,@UserName,@Comments,@Date_Added,@Type,@ReplyID,@ProfileID);Select SCOPE_IDENTITY();");
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@VideoID", cmt.VideoID));
        cmd.Parameters.Add(new SqlParameter("@UserName", cmt.UserName));
        cmd.Parameters.Add(new SqlParameter("@Comments", cmt.Comment));
        cmd.Parameters.Add(new SqlParameter("@Type", cmt.Type));
        cmd.Parameters.Add(new SqlParameter("@ReplyID", cmt.ReplyID));
        cmd.Parameters.Add(new SqlParameter("@ProfileID", cmt.ProfileID));
        cmd.Parameters.Add(new SqlParameter("@Date_Added", DateTime.Now));
        if (con.State != ConnectionState.Open) 
            con.Open();
        long id = Convert.ToInt64(cmd.ExecuteScalar());

        string level = id.ToString();
        if(cmt.ReplyID>0)
        {
            long c = cmt.ReplyID - 1; // in order to order replies based on comment id / replies
            level = c + "." + id.ToString();
        }
        // Update Level
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_UpdateLevel", CommentID, new SqlParameter("@_CommentID", CommentID.Value), new SqlParameter("@_Level", level));
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update comments set level=@Level WHERE CommentID=@CommentID;", new SqlParameter("@CommentID", id), new SqlParameter("@Level", level));
        Comments++;
        if (cmt.ProfileID != "")
        {
            members.Update_Value(cmt.ProfileID, "comments", Comments.ToString());
        }
        else
        {
            Update_Comment_Statistics(cmt.VideoID, cmt.Type, Comments);
        }
        return id;
    }

    public static bool Update(long commentid,string value)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update Comments set _comment=@comment WHERE commentid=@commentid",new SqlParameter("@commentid",commentid),new SqlParameter("@comment",value));
        return true;
    }

    // Delete Comment
    public static bool Delete(long commentid,int type, long contentid,string profileid, int Comments)
    {
        // Stored Procedure Version
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete FROM Comments WHERE commentid=@CID;", new SqlParameter("@CID", commentid));
        Comments--;
        if (Comments < 0)
            Comments = 0;
        if (profileid != "")
        {
            members.Update_Value(profileid, "comments", Comments.ToString());
        }
        else
        {
            Update_Comment_Statistics(contentid, type, Comments);
        }
        return true;
    }

    // Delete comment without updating statistics
    public static bool Delete(long commentid)
    {
        // Stored Procedure Version
        // SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_Delete", new SqlParameter("@CID", commentid));
        // Normal SQL VERSION
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete FROM Comments WHERE commentid=@commentid",new SqlParameter("@commentid",commentid));
        return true;
    }

    // delete single video comment
    public static bool Delete(long commentid, long id,int type)
    {
        // Stored Procedure Version
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_Delete", new SqlParameter("@CID", commentid));
        // Normal SQL Version
         SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete FROM Comments WHERE commentid=@commentid", new SqlParameter("@commentid", commentid));
        // Update comment statistic information
        int TotalComments = Fetch_Total_Comments(id, "", type);
        TotalComments--;
        Update_Comment_Statistics(id, type,TotalComments);
        return true;
    }

    public static bool Delete(long id,int type, bool updatestats)
    {
        // Stored Procedure Version
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_Delete_2", new SqlParameter("@VID", id),new SqlParameter("@tp",type));
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete FROM Comments WHERE videoid=@VID AND Type=@tp;", new SqlParameter("@VID", id),new SqlParameter("@tp",type));
     
        // Update comment statistic information
        if(updatestats)
            Update_Comment_Statistics(id, type,0);
        return true;
    }

    // update action
    // Core function responsible for enable, disable, approve, disapprove contents and update their record statictics
    public static bool Update_Action(long commentid,int OldValue, int NewValue, long id, string profileid, int type, string field, bool updatestats)
    {
        // Stored Procedure Version
        //SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.StoredProcedure, "VSK_Comments_UpdateAction", new SqlParameter("@FieldName", field), new SqlParameter("@FieldValue", NewValue), new SqlParameter("@CID", commentid));
        // SQL Version
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update comments Set " + field + "=@FieldValue WHERE commentid=@commentid", new SqlParameter("@commentid", commentid), new SqlParameter("@FieldValue", NewValue));
        // Update comment statistic information
        if (updatestats)
        {
            int TotalComments = Fetch_Total_Comments(id,profileid,type); // get total comments posted on selected record or profile id
            if(OldValue != NewValue)
            {
                // changes happend
                if(NewValue==1)
                    TotalComments++; // Content is enabled
                else
                    TotalComments--; // Content is disabled
            }
            Update_Comment_Statistics(id, type, TotalComments);
        }

        return true;
    }
    
    // update comment statistics version 2.0 -> more scalable
    public static void Update_Comment_Statistics(long id, int type, int comments)
    {
        switch (type)
        {
            //case 0:
            //    // videos;
            //    // update video comment statistic
            //    VideoBLL.Update_Field(id, comments.ToString(), "comments");
            //    break;
            //case 1:
            //    // blogs
            //    BlogsBLL.Update_Field(id, comments.ToString(),"comments");
            //    break;
            //case 2:
            //    // photos
            //    // update photo comment statistic
            //    PhotosBLLC.Update_Field(id, comments.ToString(), "comments"); // update photo comments
            //    break;
            //case 3:
            //    // photo gallerys
            //    GalleryBLLC.Update_Field(id, comments.ToString(),"comments"); // update gallery comments
            //    break;
            //case 11:
            //    // QA Question
            //    QABLL.Update_Field(id, comments.ToString(), "comments");
            //    break;
            //case 13:
            //    // group posts
            //    Group_Post_BLL.Update_Field(id, comments,"comments");
            //    break;
            //case 12:
            //    // QA Anser
            //    QAnswersBLL.Update_Field(id, comments.ToString(), "comments");
            //    break;
            case 14:
                // User Channel Activities
                ActivityBLL.Update_Field(id, comments.ToString(),"comments");
                break;
        }

    }
   
    public static bool Update_Field(long CommentID, string value, string FieldName)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update comments set " + FieldName + "=@value where CommentID=@CommentID", new SqlParameter("@CommentID", CommentID), new SqlParameter("@value", value));
        return true;
    }

    public static string Get_Field_Value(long CommentID, string FieldName)
    {
        return SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT " + FieldName + " FROM comments where CommentID=@CommentID",new SqlParameter("@CommentID",CommentID)).ToString();
    }

    public static int Fetch_Total_Comments(long id, string profileid, int type)
    {
        int TotalComments = 0;
        if (profileid != "")
            TotalComments = Convert.ToInt32(members.Return_Value(profileid, "comments"));
        else
        {
            switch (type)
            {
                //case 0:
                //    // videos;
                //    // update video comment statistic
                //    TotalComments = Convert.ToInt32(VideoBLL.Get_Field_Value(id, "comments"));
                //    break;
                //case 1:
                //    // blogs
                //    TotalComments = Convert.ToInt32(BlogsBLL.Get_Field_Value(id, "comments"));
                //    break;
                //case 2:
                //    // photos
                //    TotalComments = Convert.ToInt32(PhotosBLLC.Get_Field_Value(id, "comments"));
                //    break;
                //case 3:
                //    // photo gallerys
                //    TotalComments = Convert.ToInt32(GalleryBLLC.Get_Field_Value(id, "comments"));
                //    break;
                //case 11:
                //    // QA Question
                //    TotalComments = Convert.ToInt32(QABLL.Get_Field_Value(id, "comments"));
                //    break;
                //case 13:
                //    // group posts
                //    TotalComments = Convert.ToInt32(Group_Post_BLL.Get_Field_Value(id, "comments"));
                //    break;
                //case 12:
                //    // QA Answer
                //    TotalComments = Convert.ToInt32(QAnswersBLL.Get_Field_Value(id, "comments"));
                //    break;
                case 14:
                    // User Activities
                    TotalComments = Convert.ToInt32(ActivityBLL.Get_Field_Value(id, "comments"));
                    break;
            }
        }

        return TotalComments;
    }

    // Fetch Recent Video comments
    public static List<Comment_Struct> Fetch_Comments(long id, int type,int total_records)
    { 
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Comment_Struct> items = new System.Collections.Generic.List<Comment_Struct>();
        Comment_Struct str_ct = default(Comment_Struct);
        //// generate query
        string Query = "SELECT TOP " + total_records + " commentid,username,_comment,videoid,added_date,points,replyid from comments WHERE videoid=" + id + " AND type=" + type + " AND isenabled=1  AND isApproved=1 ORDER BY commentid DESC";
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Comment_Struct();
            str_ct.CommentID = (long)reader["CommentID"];
            str_ct.UserName = reader["username"].ToString();
            str_ct.Added_Date = (DateTime)reader["added_date"];
            str_ct.Comment = reader["_comment"].ToString();
            str_ct.VideoID = Convert.ToInt64(reader["videoid"]);
            str_ct.Points = (int)reader["points"];
            str_ct.ReplyID = Convert.ToInt64(reader["replyid"]);
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }

    // Fetch Comments With User Profile Picture
    public static List<Comment_Struct> Fetch_Comments_V2(long id, string profileid, int type, int total_records, string Order, bool ShowAuthoImage)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Comment_Struct> items = new System.Collections.Generic.List<Comment_Struct>();
        Comment_Struct str_ct = default(Comment_Struct);
        //// generate query
        string _filter = "";
        if (profileid != "")
            _filter = "c.profileid=" + profileid;
        else
            _filter = "c.videoid=" + id;
        string Query = "";
        if(!ShowAuthoImage)
            Query = "SELECT TOP " + total_records + " c.commentid,c.username,c._comment,c.videoid,c.added_date,c.points,c.replyid from comments as c WHERE " + _filter + " AND c.type=" + type + " AND c.isenabled=1  AND c.isApproved=1 ORDER BY " + Order + " DESC";
        else
            Query = "SELECT TOP " + total_records + " c.commentid,c.username,c._comment,c.videoid,c.added_date,c.points,u.picturename,c.replyid from comments as c inner join users as u on u.username=c.username WHERe " + _filter + " AND c.type=" + type + " AND c.isenabled=1  AND c.isApproved=1 ORDER BY " + Order + "";
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Comment_Struct();
          
            str_ct.CommentID = (long)reader["CommentID"];
            str_ct.UserName = reader["username"].ToString();
            str_ct.Added_Date = (DateTime)reader["added_date"];
            str_ct.Comment = reader["_comment"].ToString();
            str_ct.VideoID = Convert.ToInt64(reader["videoid"]);
            str_ct.Points = (int)reader["points"];
            str_ct.ReplyID = Convert.ToInt64(reader["replyid"]);
            if(profileid !="")
                str_ct.PictureName = reader["picturename"].ToString();
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }

    // Fetch all video comments
    public static List<Comment_Struct> Fetch_Comments(long id,int type,int PageNumber,int PageSize)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Comment_Struct> items = new System.Collections.Generic.List<Comment_Struct>();
        Comment_Struct str_ct = default(Comment_Struct);
        //// generate query
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        query.Append("commentid,username,_comment,videoid,added_date,points from comments WHERE VideoID=" + id + " AND type=" + type + " AND isenabled=1 AND isApproved=1");
        // implement paging script for differnt data sources.
        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), "commentid DESC", PageNumber, PageSize));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY commentid DESC";
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, PageSize));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY commentid DESC";
                str.Append(normal_query);
                break;
        }
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Comment_Struct();
            str_ct.CommentID = (long)reader["CommentID"];
            str_ct.UserName = reader["username"].ToString();
            str_ct.Added_Date = (DateTime)reader["added_date"];
            str_ct.Comment = reader["_comment"].ToString();
            str_ct.VideoID = Convert.ToInt64(reader["videoid"]);
            str_ct.Points = (int)reader["points"];
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }

    // Fetch Comments With User Profile Picture
    public static List<Comment_Struct> Fetch_Comments_V2(long id, string profileid, int type, int PageNumber, int PageSize, string Order, bool ShowAuthoImage)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Comment_Struct> items = new System.Collections.Generic.List<Comment_Struct>();
        Comment_Struct str_ct = default(Comment_Struct);
        //// generate query
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        //// generate query
        string _filter = "";
        if (profileid != "")
            _filter = "c.profileid=@profileid"; //_filter = "c.profileid=" + profileid.Replace("'","");
        else
            _filter = "c.videoid=@id"; // _filter = "c.videoid=" + id;
        query.Append("c.commentid,c.username,c._comment,c.videoid,c.added_date,c.points,c.replyid"); // remove ,
        if(ShowAuthoImage)
            query.Append(",u.picturename"); // remove,
        query.Append(" FROM comments c "); // WHERE p.videoid=555 ORDER BY level;
        if (ShowAuthoImage)
            query.Append("inner join users as u on u.username=c.username ");
        query.Append(" WHERE " + _filter + " AND c.type=@type AND c.isenabled=1  AND c.isApproved=1");
        
        // implement paging script for differnt data sources.
        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), Order, PageNumber, PageSize));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY " + Order;
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, PageSize));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY " + Order;
                str.Append(normal_query);
                break;
        }
        SqlParameter profileparam = new SqlParameter("@profileid", profileid);
        SqlParameter contentidparam = new SqlParameter("@id", id);
        SqlParameter typeparam = new SqlParameter("@type", type);

        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        cmd.Parameters.Add(profileparam);
        cmd.Parameters.Add(contentidparam);
        cmd.Parameters.Add(typeparam);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Comment_Struct();
            str_ct.CommentID = (long)reader["CommentID"];
            str_ct.UserName = reader["username"].ToString();
            str_ct.Added_Date = (DateTime)reader["added_date"];
            str_ct.Comment = reader["_comment"].ToString();
            str_ct.VideoID = Convert.ToInt64(reader["videoid"]);
            str_ct.Points = (int)reader["points"];
            str_ct.ReplyID = Convert.ToInt64(reader["replyid"]);
            if (ShowAuthoImage)
                str_ct.PictureName = reader["picturename"].ToString();
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }
    // Fetch All usersname who post comment on existing content type
    public static List<Member_Struct> Fetch_Comment_UserNames(long id,int type)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Member_Struct> items = new System.Collections.Generic.List<Member_Struct>();
        Member_Struct str_ct = default(Member_Struct);
        //// generate query
        string Query = "SELECT Distinct c.username,u.email,u.isautomail from comments as c inner join users as u on u.username=c.username WHERE c.VideoID=@videoid AND c.type=@type";
        SqlCommand cmd = new SqlCommand(Query, con);
        cmd.Parameters.Add(new SqlParameter("@videoid", id));
        cmd.Parameters.Add(new SqlParameter("@type", type));
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Member_Struct();
            str_ct.UserName = reader["username"].ToString();
            str_ct.Email = reader["email"].ToString();
            str_ct.isAutoMail = Convert.ToInt32(reader["isautomail"]);
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }


    // Get UserName, Comment, isEnable for Abuse Reporting Purpose
    // Added 12th Oct 2010
    public static List<Comment_Struct> Get_SM_Info(long ID, int type)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        List<Comment_Struct> _items = new List<Comment_Struct>();
        Comment_Struct _item;
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT username,_comment,isenabled FROM comments WHERE videoid=" + ID + " AND type=" + type + "");
        // execute sql query
        SqlCommand video_cmd = new SqlCommand(str.ToString(), con);
        SqlDataReader reader = video_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Comment_Struct();
            _item.Comment = reader["_comment"].ToString();
            _item.UserName = reader["username"].ToString();
            _item.isEnabled = Convert.ToInt32(reader["isenabled"]);
            _items.Add(_item);
        }
        reader.Close();
        con.Close();

        return _items;
    }

    #region Admin Loading Script

    // Load photos in admin section
    public static List<Comment_Struct> Load_Comments(string search, string username, string type, int isEnabled, int isApproved, int filteroption, string order, int PageNumber, int PageSize)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        if (con.State != ConnectionState.Open) con.Open();
        List<Comment_Struct> _items = new List<Comment_Struct>();
        Comment_Struct _item;

        // Query Building
        StringBuilder str = new StringBuilder();
        StringBuilder query = new StringBuilder();
        string logic = CommentsBLL.Return_Comment_Admin_Logic(search, username, type, isEnabled, isApproved, filteroption);
        query.Append("commentid,videoid,username,_comment,added_date,isenabled,type,points,isapproved,replyid,profileid FROM comments" + logic);
        // implement paging script for differnt data sources.
        switch (Site_Settings.Pagination_Type)
        {
            case 0:
                // SQL SERVER 2005 or Later Supported Query
                str.Append(Pagination_Process.Prepare_SQLSERVER2005_Pagination(query.ToString(), order, PageNumber, PageSize));
                break;
            case 1:
                // MySQL Supported Query
                string mysql_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(Pagination_Process.Prepare_MySQL_Pagination(mysql_query, PageNumber, PageSize));
                break;
            case 2:
                // SQL SERVER 2000 Supported Query
                string normal_query = "SELECT " + query.ToString() + " ORDER BY " + order;
                str.Append(normal_query);
                break;
        }
        // execute sql query
        SqlCommand video_cmd = new SqlCommand(str.ToString(), con);
        video_cmd.Parameters.Add(new SqlParameter("@search",search));
        video_cmd.Parameters.Add(new SqlParameter("@username", username));
        video_cmd.Parameters.Add(new SqlParameter("@type", type));
        video_cmd.Parameters.Add(new SqlParameter("@isenabled", isEnabled));
        video_cmd.Parameters.Add(new SqlParameter("@isapproved", isApproved));
        SqlDataReader reader = video_cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Comment_Struct();
            _item.CommentID = (long)reader["commentid"];
            _item.Comment = reader["_comment"].ToString();
            _item.UserName = reader["username"].ToString();
            _item.VideoID = (long)reader["videoid"];
            _item.isEnabled = Convert.ToInt32(reader["isenabled"]);
            _item.isApproved = Convert.ToInt32(reader["isapproved"]);
            _item.Type = Convert.ToInt32(reader["type"]);
            _item.ReplyID = (long)reader["replyid"];
            _item.Points = Convert.ToInt32(reader["points"]);
            _item.Added_Date = (DateTime)reader["added_date"];
            _item.ProfileID = reader["profileid"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close(); con.Dispose();

        return _items;

    }

    // advance counting with different settings
    public static int Count_Comments(string search, string username, string type, int isEnabled, int isApproved, int filteroption)
    {
        StringBuilder query = new StringBuilder();
        string logic = CommentsBLL.Return_Comment_Admin_Logic(search, username, type, isEnabled, isApproved, filteroption);
        query.Append("SELECT Count(commentid) FROM comments" + logic);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, query.ToString(),new SqlParameter("@search",search),new SqlParameter("@username",username),new SqlParameter("@type",type),new SqlParameter("@isenabled",isEnabled),new SqlParameter("@isapproved",isApproved)));
    }

    // Admin photo search script
    public static string Return_Comment_Admin_Logic(string search, string username, string type, int isEnabled, int isapproved, int filteroption)
    {
        StringBuilder query = new StringBuilder();

        if (search != "" || username != "" || type != "all" || isEnabled != 2 ||  isapproved != 2 || filteroption != 0)
            query.Append(" WHERE");
        if (search != "")
        {
            query.Append(" (comment like '%@search%'");
            query.Append(" OR description like '%@search%'");
            query.Append(" OR tags like '%@search%')");
            if (username != "" || type != "all" || isEnabled != 2 || isapproved != 2 || filteroption != 0)
                query.Append(" AND");
        }
        if (username != "")
        {
            query.Append(" username=@username");
            if (username != "" || type != "all" || isEnabled != 2 || isapproved != 2 || filteroption != 0)
                query.Append(" AND");
        }
        if (type != "all")
        {
            query.Append(" type=@type");
            if (isEnabled != 2 || isapproved != 2 || filteroption != 0)
                query.Append(" AND");
        }
        if (isEnabled != 2)
        {
            query.Append(" isenabled=@isenabled");
            if (isapproved != 2 || filteroption != 0)
                query.Append(" AND");
        }
        if (isapproved != 2)
        {
            query.Append(" isapproved=@isapproved");
            if (filteroption != 0)
                query.Append(" AND");
        }
        if (filteroption != 0)
        {
            switch (filteroption)
            {
                case 1:
                    // today records
                    query.Append(" " + Config.Prepare_DateDiff("added_date", 0) + " ");
                    break;
                case 2:
                    // this week records
                    query.Append(" " + Config.Prepare_DateDiff("added_date", 1) + " ");
                    break;
                case 3:
                    // this month records
                    query.Append(" " + Config.Prepare_DateDiff("added_date", 2) + " ");
                    break;
            }
        }
        return query.ToString();
    }

    #endregion

   
}
