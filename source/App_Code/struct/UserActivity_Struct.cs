using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for UserActivity_Struct
/// </summary>
public class UserActivity_Struct
{
	public UserActivity_Struct()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private long _activityid = 0;
    private string _username = "";
    private string _posterusername = "";
    private string _title = "";
    private string _activity = "";
    private DateTime _added_date = DateTime.Now;
    private int _liked = 0;
    private int _disliked = 0;
    private int _comments = 0;
    private string _picturename = "none";

    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }

    public string PosterUserName
    {
        set { _posterusername = value; }
        get { return _posterusername; }
    }

    public long ActivityID
    {
        set { _activityid = value; }
        get { return _activityid; }
    }

    public string Title
    {
        set { _title = value; }
        get { return _title; }
    }

    public string Activity
    {
        set { _activity = value; }
        get { return _activity; }
    }

    public DateTime Added_Date
    {
        set { _added_date = value; }
        get { return _added_date; }
    }

    public int Liked
    {
        set { _liked = value; }
        get { return _liked; }
    }

    public int Disliked
    {
        set { _disliked = value; }
        get { return _disliked; }
    }
    
    public int Comments
    {
        set { _comments = value; }
        get { return _comments; }
    }

    public string PictureName
    {
        set { _picturename = value; }
        get { return _picturename; }
    }
}