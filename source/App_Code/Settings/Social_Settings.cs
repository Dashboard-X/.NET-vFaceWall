using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Social_Settings
/// </summary>
public class Social_Settings
{
    public Social_Settings()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // Social Links
    public static string Feedburner_url { get { return ConfigurationBLL.Return_Value(109); } } // Feedburner or feed url e.g http://feeds.feedburner.com/FEEDBURNER_URL - Shown for RSS Icon
    public static string Facebook_url { get { return ConfigurationBLL.Return_Value(110); } } // facebook page url e.g http://www.facebook.com/USERNAME
    public static string Twitter_url { get { return ConfigurationBLL.Return_Value(111); } } // Twitter page url e.g http://twitter.com/#!/TWITTER_UNAME 
    // FACEBOOK Settings
    public static string FB_AppID { get { return ConfigurationBLL.Return_Value(112); } } // Facebook App ID
    public static string FB_AppKey { get { return ConfigurationBLL.Return_Value(113); } } // Facebook App Key (Optional)
    public static string FB_AppSecret { get { return ConfigurationBLL.Return_Value(114); } } // Facebook App Secrete (Optional)
    public static bool Share_Buttons { get { return Convert.ToBoolean(ConfigurationBLL.Return_Value(115)); } } // Show facebook and other if exist share buttons on main listing pages e.g Facebook Like Button
    public static string Addthis_PubID { get { return ConfigurationBLL.Return_Value(116); } }
    public static string Twitter_UID { get { return ConfigurationBLL.Return_Value(117); } }

    public static bool ShowFacebookMetaTags { get { return Convert.ToBoolean(ConfigurationBLL.Return_Value(118)); } } // add facebook meta tags on page
}