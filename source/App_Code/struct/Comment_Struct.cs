using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for Comment_Struct
/// </summary>
public class Comment_Struct
{
	public Comment_Struct()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private long _commentid = 0;
    private long _videoid = 0;
    private string _username = "";
    private string _comment = "";
    private DateTime _added_date = DateTime.Now;
    private int _isenabled = 0;
    private int _type = 0;
    private int _points = 0;
    private string _picturename = "none";
    private int _isapproved = 1;
    private long _replyid = 0;
    private string _profileid = ""; // in case if item identity is in char like username instead of normal record id (bigid) -> _videoid;

    public long CommentID
    {
        set { _commentid = value; }
        get { return _commentid; }
    }

    public long VideoID
    {
        set { _videoid = value; }
        get { return _videoid; }
    }

    public string ProfileID
    {
        set { _profileid = value; }
        get { return _profileid; }
    }

    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }

    public string Comment
    {
        set { _comment = value; }
        get { return _comment; }
    }

    public DateTime Added_Date
    {
        set { _added_date = value; }
        get { return _added_date; }
    }

    public int isEnabled
    {
        set { _isenabled = value; }
        get { return _isenabled; }
    }
    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }

    public int Points
    {
        set { _points = value; }
        get { return _points; }
    }

    public string PictureName
    {
        set { _picturename = value; }
        get { return _picturename; }
    }

    public int isApproved
    {
        set { _isapproved = value; }
        get { return _isapproved; }
    }

    public long ReplyID
    {
        set { _replyid = value; }
        get { return _replyid; }
    }
}
