using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for Struct_MailTemplates
/// </summary>
public class Struct_MailTemplates
{
	public Struct_MailTemplates()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _id = 0;
    private string _templatekey = "";
    private string _tags = "";
    private string _contents = "";
    private string _type = "";
    private string _description = "";
    private string _subjecttags = "";
    private string _subject = "";


    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string TemplateKey
    {
        get { return _templatekey; }
        set { _templatekey = value; }
    }
    public string Tags
    {
        get { return _tags; }
        set { _tags = value; }
    }
    public string Contents
    {
        get { return _contents; }
        set { _contents = value; }
    }

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string SubjectTags
    {
        get { return _subjecttags; }
        set { _subjecttags = value; }
    }

    public string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }

}
