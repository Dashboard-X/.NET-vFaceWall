using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for Abuse_Struct
/// </summary>
public class Abuse_Struct
{
	public Abuse_Struct()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private long _reportid = 0;
    private long _contentid = 0;
    private string _username = "";
    private string _ipaddress = "";
    private string _reason = "";
    private DateTime _addeddate = DateTime.Now;
    private int _type = 0;

    public long ReportID
    {
        set { _reportid = value; }
        get { return _reportid; }
    }

    public long ContentID
    {
        set { _contentid = value; }
        get { return _contentid; }
    }

    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }

    public string IPAddress
    {
        set { _ipaddress = value; }
        get { return _ipaddress; }
    }

    public string Reason
    {
        set { _reason = value; }
        get { return _reason; }
    }

    public DateTime AddedDate
    {
        set { _addeddate = value; }
        get { return _addeddate; }
    }

    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }
}
