using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class DictionaryBLL
{
    // File Updated: April 18 2010
    // VSK Version 5.2

    // Note: DictionaryBLL Important Terms
    // Type:
    // .............. 0 :-> Content Restricted Values
    // .............. 1 :-> UserName Restricted Values

    // isrestrict:
    // .............. 1:-> screen data and no restriction mean screen data and highlight it on control panel
    // .............. 2:-> screen data and restrict it e.g fuck -> f**k, boobs -> b***s

    private static int isRestrict = Config.Get_ScreeningOption();
    public DictionaryBLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool Add(string value, int type)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert Into dictionary(value,type)values(@value,@type)", new SqlParameter("@value", value), new SqlParameter("@type", type));
        HttpContext.Current.Application["ld_screen_" + type] = Fetch_Values(type); // Update values in application state
        Update_Values(); // Update values in application state
        return true;
    }

    public static bool Delete(int id)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Delete from dictionary where id=@id",new SqlParameter("@id",id));
        return true;
    }

    public static DataSet Load(int type)
    {
        return SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from dictionary where type=@type",new SqlParameter("@type",type));
    }

    // old compatibility function
    public static List<Dictionary_Struct> Return_DictionaryValues()
    {
        return Return_Values(0); // type 0:-> dictionary value
    }

    // old compatibility function
    public static string Return_RestrictedUserNames()
    {
        List<Dictionary_Struct> _lst = Return_Values(1);
        StringBuilder keywords = new StringBuilder();
        if (_lst.Count > 0)
        {
            int i = 0;
            for (i = 0; i <= _lst.Count - 1; i++)
            {
                if (i == 0)
                    keywords.Append(_lst[i].Value);
                else
                    keywords.Append("," + _lst[i].Value);
            }
            return keywords.ToString();
        }
        else
        {
            return "";
        }
    }

    public static void Update_Values()
    {
        int type = 0;
        HttpContext.Current.Application["ld_screen_" + type] = Fetch_Values(type);
        type = 1;
        HttpContext.Current.Application["ld_screen_" + type] = Fetch_Values(type);
    }

    public static List<Dictionary_Struct> Return_Values(int type)
    {
        if (HttpContext.Current.Application["ld_screen_" + type] == null)
        {
            HttpContext.Current.Application["ld_screen_" + type] = Fetch_Values(type);
        }

        List<Dictionary_Struct> _list = (List<Dictionary_Struct>)(HttpContext.Current.Application["ld_screen_" + type]);
        return _list;
    }

    private static List<Dictionary_Struct> Fetch_Values(int type)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        List<Dictionary_Struct> _items = new List<Dictionary_Struct>();
        Dictionary_Struct _item;
        // generate sql query
        StringBuilder str = new StringBuilder();
        str.Append("SELECT value from dictionary where type=" + type);
        // execute sql query
        SqlCommand cmd = new SqlCommand(str.ToString(), con);
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            _item = new Dictionary_Struct();
            _item.Value = reader["value"].ToString();
            _items.Add(_item);
        }
        reader.Close();
        con.Close();

        return _items;
    }

    // Core function to process screening data
    public static string Process_Screening(string text)
    {
        List<Dictionary_Struct> _lst = Return_Values(0);
        if (_lst.Count == 0)
            return text;

        StringBuilder keywords = new StringBuilder();
        if (_lst.Count > 0)
        {
            int i = 0;
            // create output like (apple|banana|mango)
            for (i = 0; i <= _lst.Count - 1; i++)
            {
                if (_lst.Count == 1)
                    keywords.Append("(" + @"\b" + _lst[i].Value.Trim() + @"\b)");
                else if (i == 0)
                    keywords.Append("(" + @"\b" + _lst[i].Value.Trim() + @"\b");
                else if (i == _lst.Count - 1)
                    keywords.Append("|" + @"\b" + _lst[i].Value.Trim() + @"\b)");
                else
                    keywords.Append("|" + @"\b" + _lst[i].Value.Trim() + @"\b");
            }
            string key = keywords.ToString();
            //// Swap out the ,<space> for pipes and add the braces
            Regex r = new Regex(", @");
            //string keyword = "(" + r.Replace(keywords.ToString(), "|") + ")";

            // Get ready to replace the keywords
            MatchEvaluator _match = null;
            if (isRestrict == 0)
                _match = new MatchEvaluator(MatchEval);
            else
                _match = new MatchEvaluator(RestrictMatchEval);

            r = new Regex(keywords.ToString(), RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //// Do the replace
            return r.Replace(text, _match);
        }
        else
        {
            return "";
        }
    }

    public static string MatchEval(Match match)
    {
        if (match.Groups[1].Success)
        {
            return "<span class=\"yel_bk\">" + match.ToString() + "</span>";
        }
        else
        {
            // no match
            return "";
        }
    }

    public static string RestrictMatchEval(Match match)
    {
        if (match.Groups[1].Success)
        {
            return UtilityBLL.Restrict_Word(match.ToString());
        }
        else
        {
            // no match
            return "";
        }
    }

    // check for match content with provided keywords
    public static bool isMatch(string text, string keywords)
    {
        bool flg = false;
        if (!string.IsNullOrEmpty(keywords))
        {
            string[] str;
            str = keywords.ToString().Split(char.Parse(","));
            int i = 0;
            for (i = 0; i <= str.Length - 1; i++)
            {
                if (text.Contains(str[i]))
                {
                    if (flg == false)
                    {
                        flg = true;
                    }
                }
            }
        }
        return flg;
    }

    // validate search terms with bad user searches
    public static bool Validate_Search_Word(string text)
    {
        bool flag = true; // valid word
        List<Dictionary_Struct> _lst = Return_Values(0);
        if (_lst.Count > 0)
        {
            int i = 0;
            // create output like (apple|banana|mango)
            for (i = 0; i <= _lst.Count - 1; i++)
            {
                if (flag)
                {
                    if (text.Contains(_lst[i].Value))
                        flag = false;
                }
            }
        }

        return flag;
    }
}
