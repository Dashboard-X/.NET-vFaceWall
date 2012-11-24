using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Xml;
/// <summary>
/// General configuration of  <%= Site_Settings.Website_Title %>.
/// </summary>
public class Config
{
    private static UtilityBLL _utility = new UtilityBLL();

    public Config()
    { }

    #region General Settings
    /// <summary>
    /// Get Database connection string
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            //return ConfigurationManager.AppSettings["connectionString"].ToString();  // MySQL Compatible
            return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString; // SQL SERVER Compatible
        }
    }

    /// <summary>
    /// Get current domain url
    /// </summary>
    public static string GetUrl()
    {
        string _url = ConfigurationManager.AppSettings["url"];
        if (_url.EndsWith("/") || _url.EndsWith(@"\"))
            return _url;
        else
            return _url + "/";
    }

    /// <summary>
    /// Get current domain url
    /// </summary>
    public static string GetUrl(string url)
    {
        string _url = ConfigurationManager.AppSettings["url"];
        if (_url.EndsWith("/") || _url.EndsWith(@"\"))
            return _url + "" + url;
        else
            return _url + "/" + url;
    }

    /// <summary>
    /// Membership Account Upgrade Options
    /// </summary>
    public static int GetMembershipAccountUpgradeType()
    {
        int type = Convert.ToInt32(ConfigurationManager.AppSettings["Member_Account_Upgrade_Type"]);
        if (type > 1)
            type = 1;
        return type;
    }
        
    /// <summary>
    /// Membership Account Upgrade Redirect Option - 0: Redirect User To MyAccount After Registeration, 1: Redirect User To Upgrade Account Section After Registeration
    /// </summary>
    public static int GetMembershipAccountUpgradeRedirect()
    {
        int type = Convert.ToInt32(ConfigurationManager.AppSettings["Member_Account_Upgrade_Redirect"]);
        if (type > 1)
            type = 1;
        return type;
    }
   
    /// <summary>
    /// Prepare DateDiff for SQL SERVER / MYSQL
    /// </summary>
    public static string Prepare_DateDiff(string field, int type)
    {
        string value = "";
        // type 0: today, 1: this week, 2: this month
        int servertype = Site_Settings.Pagination_Type;
        // servertype 0: sql server 2005, 1: mysql, 2: sql server 2000
        switch (servertype)
        {
            case 0:
                value = Prepare_DateDiff_SQLSERVER(field, type);
                break;
            case 1:
                value = Prepare_DateDiff_MYSQL(field, type);
                break;
            case 2:
                value = Prepare_DateDiff_SQLSERVER(field, type);
                break;
        }
        return value;
    }
    /// <summary>
    /// Prepare DateDiff for SQL SERVER
    /// </summary>
    private static string Prepare_DateDiff_SQLSERVER(string field, int type)
    {
        string value = "";
        switch (type)
        {
            case 0:
                // today
                value = "DateDiff(DAY," + field + ",getdate())=0";
                break;
            case 1:
                // this week 
                value = "DateDiff(DAY," + field + ",getdate())>=0 AND DateDiff(DAY," + field + ",getdate())<7";
                break;
            case 2:
                // this month
                value = "DateDiff(DAY," + field + ",getdate())>=0 AND DateDiff(DAY," + field + ",getdate())<31";
                break;
            case 3:
                // all records within this week
                value = "DateDiff(DAY," + field + ",getdate())<7";
                break;
            case 4:
                // all records within this month
                value = "DateDiff(DAY," + field + ",getdate())<31";
                break;
        }
        return value;
    }

    /// <summary>
    /// Prepare DateDiff for MYSQL
    /// </summary>
    private static string Prepare_DateDiff_MYSQL(string field, int type)
    {
        string value = "";
        switch (type)
        {
            case 0:
                // today
                value = "DateDiff(" + field + ",CURDATE())=0";
                break;
            case 1:
                // this week 
                value = "DateDiff(" + field + ",CURDATE())<=0 AND DateDiff(" + field + ",CURDATE())>-7";
                break;
            case 2:
                // this month
                value = "DateDiff(" + field + ",CURDATE())<=0 AND DateDiff(" + field + ",CURDATE())>-31";
                break;
            case 3:
                // all records within this week
                value = "DateDiff(" + field + ",CURDATE())>-7";
                break;
            case 4:
                // all records within this month
                value = "DateDiff(" + field + ",CURDATE())>-31";
                break;
        }
        return value;
    }

    // Broad Search Logic
    public static string BroadSearchScript(string _term, string ext, string title)
    {
        StringBuilder script = new StringBuilder();
        // broad match
        if (_term.Contains(","))
        {
            script.Append(" (");
            string[] arr = _term.ToString().Split(char.Parse(","));
            int i = 0;
            if (arr.Length > 0)
            {
                for (i = 0; i <= arr.Length - 1; i++)
                {
                    string _name = arr[i].ToString().Trim();
                    if (i > 0)
                        script.Append(" OR ");

                    script.Append(" " + ext + "categories like '%" + _name + "%' OR " + ext + "tags like '%" + _name + "%' OR " + title + " like '%" + _name + "%'");
                }
            }
            script.Append(")");
        }
        else
        {
            script.Append(" (" + ext + "categories like '%" + _term + "%' OR " + ext + "tags like '%" + _term + "%' OR " + title + " like '%" + _term + "%' OR " + ext + "description like '%" + _term + "%')");
        }

        return script.ToString();
    }

    // Tag Search Login
    public static string TagSearchScript(string _term, string ext)
    {
        StringBuilder script = new StringBuilder();
        if (_term.Contains(","))
        {
            // multiple tags
            script.Append(" (");
            string[] arr = _term.ToString().Split(char.Parse(","));
            int i = 0;
            if (arr.Length > 0)
            {

                for (i = 0; i <= arr.Length - 1; i++)
                {
                    string _name = arr[i].ToString().Trim();
                    if (i > 0)
                        script.Append(" OR ");

                    script.Append("" + ext + "tags like '%" + _name + "%'");
                }
            }
            script.Append(")");

        }
        else
        {
            script.Append(" " + ext + "tags like '%" + _term + "%'");
        }
        return script.ToString();
    }

    // Category Search Logic
    public static string CategorySearchScript(string _term, string ext)
    {
        StringBuilder script = new StringBuilder();
        if (_term.Contains(","))
        {
            // multiple categories
            script.Append(" (");
            string[] arr = _term.ToString().Split(char.Parse(","));
            int i = 0;
            if (arr.Length > 0)
            {

                for (i = 0; i <= arr.Length - 1; i++)
                {
                    string _name = arr[i].ToString().Trim();
                    if (i > 0)
                        script.Append(" OR ");

                    script.Append("" + ext + "categories like '%" + _name + "%'");
                }
            }
            script.Append(")");
        }
        else
        {
            script.Append(" " + ext + "categories like '%" + _term + "%'");
        }
        return script.ToString();
    }
    /// <summary>
    /// Validate administrator readonly status.
    /// </summary>
    public static bool isAdminActionAllowed()
    {
        if (HttpContext.Current.Session["AccessLevel"] != null)
        {
            if (HttpContext.Current.Session["AccessLevel"].ToString() == "Full")
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Return current page url and append querystring
    /// </summary>
    public static string Return_Current_Page(string value)
    {
        string prot = "http";
        string https = HttpContext.Current.Request.ServerVariables["HTTPS"];
        if (https != "off")
            prot = "https";

        string domainname = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        string filename = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        //string querystring = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        return prot + "://" + domainname + filename + "?" + value;
    }

    /// <summary>
    /// Return current page url.
    /// </summary>
    public static string Return_Current_Page()
    {
        string prot = "http";
        string https = HttpContext.Current.Request.ServerVariables["HTTPS"];
        if (https != "off")
            prot = "https";

        string domainname = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        string filename = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        //string querystring = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        //if (querystring != "")
        //    return prot + "://" + domainname + filename + "?" + querystring;
        //else
        return prot + "://" + domainname + filename;
    }
    #endregion

    
    #region Website_Settings
  
    /// <summary>
    /// Get website title
    /// </summary>
    public static string GetWebsiteTitle()
    {
        return Site_Settings.Website_Title;
    }

    /// <summary>
    /// Get website description
    /// </summary>
    public static string GetWebsiteDescription()
    {
        return Site_Settings.Website_Description;
    }
    
   
    
    /// <summary>
    /// Get Cache Duration
    /// </summary>
    public static int GetCacheDuration()
    {
        return Site_Settings.Cache_Duration;
    }

   
    /// <summary>
    /// Return screen option 0-> screen use for highlight words, 1:-> screen and replace (e.g apple -> a***e)
    /// </summary>
    public static int Get_ScreeningOption()
    {
        return Site_Settings.Screen_Content;
    }

    /// <summary>
    /// Get approval type
    /// </summary>
    public static bool isAutomativeApproval()
    {
        int value = Site_Settings.Content_Approval;
        if (value == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Get abuse | spam maximum count setting
    /// </summary>
    public static int Return_AbuseReport_Count()
    {
        return Site_Settings.Spam_Count;
    }
    
    /// <summary>
    /// Get admin email address for sending mails
    /// </summary>
    public static string Return_Email()
    {
        return Site_Settings.Admin_Mail;
    }

    /// <summary>
    /// Get admin email display name for sending mails
    /// </summary>
    public static string Return_EmailDisplayName()
    {
        return Site_Settings.Admin_Mail_DisplayName;
    }
    #endregion


    #region Feature_Settings

    
    /// <summary>
    /// Toggle On / Off Video Feature in Website
    /// </summary>
    public static bool isSignUp()
    {
        int status = Site_Settings.Feature_MemberRegistration;
        if (status == 1)
            return true;
        else
            return false;
    }
    
    
    /// <summary>
    ///  Toggle On / Off Channel Feature in Website
    /// </summary>
    public static bool isChannels()
    {
        int status = Site_Settings.Feature_Channels;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toglle on | off friend section
    /// </summary>
    public static bool isFriends()
    {
        int status = Site_Settings.Feature_Friends;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toglle on | off comment section
    /// </summary>
    public static bool isFeature_Comments()
    {
        int status =Site_Settings.Feature_Comments;
        if (status == 1)
            return true;
        else
            return false;
    }

    

    /// <summary>
    /// Toglle on | off views
    /// </summary>
    public static bool isFeature_Views()
    {
        int status = Site_Settings.Feature_Views;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toglle on | off uploaded date
    /// </summary>
    public static bool isFeature_Date()
    {
        int status = Site_Settings.Feature_Date;
        if (status == 1)
            return true;
        else
            return false;
    }


    /// <summary>
    /// Toggle on | off advertisement
    /// </summary>
    public static bool isAdsEnabled()
    {
        int status = Site_Settings.Feature_Advertisement;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toggle on | off email processing
    /// </summary>
    public static bool isEmailEnabled()
    {
        int status = Site_Settings.Feature_Email;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toggle on | off adult validation
    /// </summary>
    public static bool isAdultValidation()
    {
        int status = Site_Settings.Feature_Adult_Verification;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Choose whether registration validation required at time of user registration
    /// </summary>
    public static bool isRegistrationValidation()
    {
        int status = Site_Settings.Feature_Email_Verification;
        if (status == 1)
            return true;
        else
            return false;
    }


    /// <summary>
    /// Toglle on | off username
    /// </summary>
    public static bool isFeature_UserName()
    {
        int status = Site_Settings.Feature_UserName;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Toglle on | off comment section
    /// </summary>
    public static bool isFeature_Rating()
    {
        int status = Site_Settings.Feature_Rating;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Login required for rating content, by disabling rating no record tracking will store, mean single user can post multiple ratings
    /// </summary>
    public static bool isLoginRequired_Rating()
    {
        int status = Site_Settings.Feature_Login_Rating;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Login required for posting comment, by disabling rating user can post comment as guest user.
    /// </summary>
    public static bool isLoginRequired_Comment()
    {
        int status = Site_Settings.Feature_Login_Comment;
        if (status == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Login required for point good or bad points, by disabling this feature no tracking will store, single user can post multiple points.
    /// </summary>
    public static bool isLoginRequired_Points()
    {
        int status = Site_Settings.Feature_Login_Advice;
        if (status == 1)
            return true;
        else
            return false;
    }


    #endregion
    

    #region Message Layouts
    
    /// <summary>
    /// Return loading message
    /// </summary>
    public static void SetLoadingMessage(System.Web.UI.HtmlControls.HtmlGenericControl pnl, string elementid, bool isvisible)
    {
        StringBuilder str = new StringBuilder();
        string hidden = "";
        if (isvisible == false)
            hidden = "style=\"display:none;\"";
        str.Append("<div id=\"" + elementid + "\" " + hidden + "  class=\"ajax_out_box ui-corner-all\">loading....</div>");
        pnl.InnerHtml = str.ToString();
    }

    /// <summary>
    /// Display message with close link, by default 
    /// </summary>
    public static void SetHiddenMessage_v2(System.Web.UI.HtmlControls.HtmlGenericControl pnl, string message, string elementid, bool isvisible, int messagetype)
    {
        pnl.InnerHtml = SetHiddenMessage_v2(message, elementid, isvisible, messagetype);
    }
    /// <summary>
    /// Display message with close link, by default 
    /// </summary>
    public static string SetHiddenMessage_v2(string message, string elementid, bool isvisible, int messagetype)
    {
        // type: 4 normal
        string css = "ajax_out_box ui-corner-all";
        switch (messagetype)
        {
            case 0:
                css = "alert alert-error";
                break;
            case 1:
                css = "alert alert-success";
                break;
            case 2:
                css = "alert alert-info";
                break;
            case 3:
                css = "alert alert-block";
                break;
        }
        string alert_event = "";
        if (messagetype != 4)
            alert_event = " data-dismiss=\"alert\"";

        StringBuilder str = new StringBuilder();
        string hidden = "";
        if (isvisible == false)
            hidden = "style=\"display:none;\"";
        str.Append("<div id=\"" + elementid + "\" " + hidden + "  class=\"" + css + "\"><button id=\"aclose\"" + alert_event + " class=\"close\">&times;</button>" + message + "</div>");
        return str.ToString();
    }
    ///// <summary>
    ///// Return loading message
    ///// </summary>
    //public static void SetLoadingMessage(System.Web.UI.HtmlControls.HtmlGenericControl pnl, string elementid, string message, bool isvisible, bool isroundbox)
    //{
    //    StringBuilder str = new StringBuilder();
    //    string hidden = "";
    //    string roundboxclass = "";
    //    if (isvisible == false)
    //        hidden = "style=\"display:none;\"";

    //    if (isroundbox)
    //        roundboxclass = "class=\"ajax_out_box ui-corner-all\"";

    //    str.Append("<div id=\"" + elementid + "\" " + hidden + " " + roundboxclass + ">" + message + "</div>");
    //    pnl.InnerHtml = str.ToString();
    //}

  
    ///// <summary>
    ///// Return message with close link, by default 
    ///// </summary>
    //public static string SetHiddenMessage(string message, string elementid, bool isvisible)
    //{
    //    StringBuilder str = new StringBuilder();
    //    string hidden = "";
    //    if (isvisible == false)
    //        hidden = "style=\"display:none;\"";
    //    str.Append("<div id=\"" + elementid + "\" " + hidden + " class=\"ajax_out_box ui-corner-all\"><div style=\"float:left; width:85%;\">" + message + "</div><div style=\"float:right; width:10%; text-align:right;\"><a href=\"#\" onclick=\"toggle_panel(2,'#" + elementid + "');\">close</a></div><div class=\"clear\"></div></div>");
    //    return str.ToString();
    //}

    ///// <summary>
    ///// Display message with close link, by default 
    ///// </summary>
    //public static void SetHiddenMessage(System.Web.UI.HtmlControls.HtmlGenericControl pnl, string message, string elementid, bool isvisible)
    //{
    //    StringBuilder str = new StringBuilder();
    //    string hidden = "";
    //    if (isvisible == false)
    //        hidden = "style=\"display:none;\"";
    //    str.Append("<div id=\"" + elementid + "\" " + hidden + "  class=\"ajax_out_box ui-corner-all\"><div style=\"float:left; width:85%;\">" + message + "</div><div style=\"float:right; width:10%; text-align:right;\"><a href=\"#\" onclick=\"toggle_panel(2,'#" + elementid + "');\">close</a></div><div class=\"clear\"></div></div>");
    //    pnl.InnerHtml = str.ToString();
    //}
    ///// <summary>
    ///// Return message with close link, by default 
    ///// </summary>
    //public static string SetAjaxMessage(string message, string elementid, bool isvisible, bool isroundbox)
    //{
    //    StringBuilder str = new StringBuilder();
    //    string hidden = "";
    //    if (isvisible == false)
    //        hidden = "style=\"display:none;\"";
    //    string roundbox = "class=\"ajax_out_box  ui-corner-all\""; // box with gray border
    //    if (isroundbox)
    //        roundbox = "class=\"module_t_mrg  ui-corner-all\""; // box without any border
    //    str.Append("<div id=\"" + elementid + "\" " + hidden + " " + roundbox + "><div style=\"float:left; width:85%;\">" + message + "</div><div style=\"float:right; width:10%; text-align:right;\"><a href=\"javascript:void(0)\" onclick=\"toggle_panel(2,'#shw_lgn');\">close</a></div><div class=\"clear\"></div></div>");
    //    return str.ToString();
    //}  
    #endregion

    // Twitter Bootstrap Message Structuring
    #region Twitter Bootstrap

    public static void ShowMessageV2(System.Web.UI.HtmlControls.HtmlGenericControl pnl, string message, string headingmessage, int messagetype)
    {
        // Message Types
        // 0: Error
        // 1: Success
        // 2: Info
        // 3: Warning
        string css = "alert-error";
        switch (messagetype)
        {
            case 1:
                css = "alert-success";
                break;
            case 2:
                css = "alert-info";
                break;
            case 3:
                css = "alert-block";
                break;
        }
        StringBuilder str = new StringBuilder();
        string headingmsg = "";
        if (headingmessage != "")
            headingmsg = "<strong>" + headingmessage + "</strong>";
        if (messagetype != 3)
        {
            str.Append("<div class=\"alert " + css + "\">\n");
            str.Append("<button class=\"close\" data-dismiss=\"alert\">\n&times;</button>\n");
            str.Append(headingmsg + " " + message + "</div>\n");
        }
        else
        {
            str.Append("<div class=\"alert " + css + "\">\n");
            str.Append("<a class=\"close\" data-dismiss=\"alert\" href=\"#\">×</a>\n");
            if (headingmessage != "")
                str.Append("<h4 class=\"alert-heading\">" + headingmessage + "</h4>\n");
            str.Append(message + "</div>\n");
        }
         pnl.InnerHtml = str.ToString();
    }

    public static string ShowMessageV2(string message, string headingmessage, int messagetype)
    {
        // Message Types
        // 0: Error
        // 1: Success
        // 2: Info
        // 3: Warning
        string css = "alert-error";
        switch (messagetype)
        {
            case 1:
                css = "alert-success";
                break;
            case 2:
                css = "alert-info";
                break;
            case 3:
                css = "alert-block";
                break;
        }
        StringBuilder str = new StringBuilder();
        string headingmsg = "";
        if (headingmessage != "")
            headingmsg = "<strong>" + headingmessage + "</strong>";
        if (messagetype != 3)
        {
            str.Append("<div class=\"alert " + css + "\">\n");
            str.Append("<button class=\"close\" data-dismiss=\"alert\">\n&times;</button>\n");
            str.Append(headingmsg + " " + message + "</div>\n");
        }
        else
        {
            str.Append("<div class=\"alert " + css + "\">\n");
            str.Append("<a class=\"close\" data-dismiss=\"alert\" href=\"#\">×</a>\n");
            if (headingmessage != "")
                str.Append("<h4 class=\"alert-heading\">" + headingmessage + "</h4>\n");
            str.Append(message + "</div>\n");
        }
        return str.ToString();
    } 
    #endregion
}
