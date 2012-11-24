using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for UrlConfig
/// </summary>
public class UrlConfig
{
	public UrlConfig()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Generate Public User Profile Url
    public static string Prepare_User_Profile_Url(string username, bool isadmin)
    {
        string _url="";
        if(isadmin)
            _url = Config.GetUrl() + "adm/sc/members/MemberDetail.aspx?user=" + username + "";
        else
            _url = Config.GetUrl() + "user/" + username.ToLower() + ".aspx";

        return _url;
    }

    public static string Prepare_User_Profile_Url(string username, string section, bool isadmin)
    {
        string _url = "";
        if (isadmin)
            _url = Config.GetUrl() + "adm/sc/members/MemberDetail.aspx?user=" + username + "";
        else
        {
            if (section != "")
                section = section + "/";
            _url = Config.GetUrl() + "user/" + section + "" + username.ToLower() + ".aspx";
        }

        return _url;
    }
    
     
    // Return website logo url - url used for sending to Facebook (share
    public static string Return_Website_Logo_Url()
    {
        return Config.GetUrl("images/logo.gif");
        
    }

    public static string Retun_User_Profile_Thumb_Url(string username, string picturename, int iscloud)
    {
        string URL = "";
        if (picturename.Contains("http"))
            URL = picturename;
        else if (picturename != "none")
            URL = UrlConfig.Return_Photo_Url(username, picturename, 0, iscloud);
        else
            URL = Config.GetUrl() + "images/dmember.png";

        return URL;
    }

    public static string Return_Photo_Url(string username, string picturename, int type, int iscloud)
    {
        if (iscloud == 1) // media stored in cloud storage
            return ""; // cloud settings disabled
        else
        {
            // type = 0: thumb, 1: mid thumb, 2: original
            string URL = "";
            string Imagetype = ""; // original
            switch (type)
            {
                case 0:
                    Imagetype = "thumbs/";
                    break;
                case 1:
                    Imagetype = "midthumbs/";
                    break;
            }
            if (picturename.Contains("http"))
                URL = picturename;
            else if (picturename != "none")
                URL = Config.GetUrl() + "contents/member/" + username + "/photos/" + Imagetype + "" + picturename;
            else
                URL = Config.GetUrl() + "images/dmember.png";
            return URL;
        }
    }

    public static string Return_User_Profile_Photo(string username, string picturename, int type, int iscloud)
    {
        if (iscloud == 1) // media stored in cloud storage
            return ""; // no cloud configuration
        else
        {
            // type = 0: thumb, 1: mid thumb, 2: original
            string URL = "";
            string Imagetype = ""; // original
            switch (type)
            {
                case 0:
                    Imagetype = "thumbs/";
                    break;
                case 1:
                    Imagetype = "midthumbs/";
                    break;
            }
            if (picturename.Contains("http"))
                URL = picturename;
            else if (picturename != "none")
                URL = Config.GetUrl() + "contents/member/" + username + "/photos/" + Imagetype + "" + picturename;
            else
                URL = Config.GetUrl() + "images/dmember.png";
            return URL;
        }
    }

}