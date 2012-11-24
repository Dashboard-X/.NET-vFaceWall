using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



/// <summary>
/// Summary description for DictionaryBLL
/// </summary>
public class Dictionary_Struct
{
    public Dictionary_Struct()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _id = 0;
    private string _value = "";
    private int _type = 0;
  
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }

    public string Value
    {
        set { _value = value; }
        get { return _value; }
    }

    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }
}
