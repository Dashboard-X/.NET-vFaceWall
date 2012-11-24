using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Site_Settings
/// </summary>
public class Site_Settings
{
	public Site_Settings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //****************************************************************************
    // General Settings
    //****************************************************************************
    public static string Website_Title { get { return ConfigurationBLL.Return_Value(1); } }
    public static string Website_Description { get { return ConfigurationBLL.Return_Value(2); } }
    public static string Admin_Mail { get { return ConfigurationBLL.Return_Value(4); } }
    public static string Admin_Mail_DisplayName { get { return ConfigurationBLL.Return_Value(5); } }
    public static int Screen_Content { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(8)); } }
    public static int Content_Approval { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(9)); } }
    public static int Spam_Count { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(10)); } }
    public static int Cache_Duration { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(11)); } }
    public static bool Store_IPAddress { get { return Convert.ToBoolean(ConfigurationBLL.Return_Value(12)); } }
    public static int Pagination_Type { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(7)); } } // database type
    public static int Channel_Page_Size { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(17)); } }
    public static int Channel_Item_Priority_Show { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(22)); } }
    public static int Channel_Views { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(23)); } }
    public static bool Channel_Custom_Theme { get { return Convert.ToBoolean(ConfigurationBLL.Return_Value(24)); } }
    public static int Pagination_Links { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(6)); } }
    public static int Maxiumum_Dynamic_Url_Length { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(26)); } }
    
    // Navigation 
    // 0: left side (two column)
    // 1: right side (two column)
    // 2: three column two navigation on left and right side and body within center
    public static int NavigationSection { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(171)); } } // Adjust left or right side navigation

    //**************************************************************************
    // Feature Settings
    //**************************************************************************
    // Enable or Disable Features
    public static int Feature_Channels { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(74)); } }  // 1: on, 0: off
    public static int Feature_Friends { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(78)); } }  // 1: on, 0: off
  
    // Section settings
    public static int Feature_Comments { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(84)); } } // 1: on, 0: off
    public static int Feature_Advertisement { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(90)); } }  // 1: on, 0: off
    public static int Feature_Email { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(91)); } }  // 1: on, 0: off (Send auto email)
    public static int Feature_Email_Verification { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(92)); } }  // 1: on, 0: off // Enable to verify user after registration via email.
    public static int Feature_Adult_Verification { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(94)); } }  // 1: on, 0: off
    public static int Feature_Views { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(95)); } } //  // 1: on, 0: off (display media views with each item
    public static int Feature_Date { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(96)); } }  // 1: on, 0: off (display date with each item) e.g 2 days ago
    public static int Feature_MemberRegistration { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(97)); } } // 1: on, 0: off  Member registration available
    public static int Feature_UserName { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(99)); } }  // 1: on, 0: off - (display username with each item)
    public static int Feature_Rating { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(100)); } } // 1: on, 0: off
    public static int Feature_LikeBox { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(150)); } }  // 1: on, 0: off
    // login settings for posting / actions

    public static int Feature_Login_Rating { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(101)); } } // 1: on, 0: off - Login required to post like / dislike
    public static int Feature_Login_Comment { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(102)); } }  // 1: on, 0: off - Login required to post comment
    public static int Feature_Login_Advice { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(103)); } }  // 1: on, 0: off - Login required to like / dislike comment post
  
    //********************************
    // Site Contents
    //********************************
    public static string Site_Analytics { get { return ConfigurationBLL.Return_Value(183); } }
    public static string Site_Terms { get { return ConfigurationBLL.Return_Value(108); } }

    // Template
    // 0: none (default website settings)
    // 1: default web 2.0 style (three column layout not supported)
    // 2: jquery ui header style
    // 3: jquery ui default style
    // 4: jquery ui hover style
    // 5: jquery ui active style
    public static int Site_Templates { get { return Convert.ToInt32(ConfigurationBLL.Return_Value(186)); } } // different site templates

}