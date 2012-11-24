using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Runtime.InteropServices;
/// <summary>
/// Summary description for UtilityBLL
/// </summary>
public class UtilityBLL
{
    // CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1);

    public UtilityBLL()
    { }

    /// <summary>
    /// Validate keywork in list of keywords
    /// </summary>

    public static bool Check_File_Extension(string keywords, string filename)
    {
        string[] arr;

        arr = keywords.ToString().Split(char.Parse(","));
        int i;
        bool flag = false;
        for (i = 0; i <= arr.Length - 1; i++)
        {
            if (filename.EndsWith(arr[i]))
            {
                flag = true;
            }
        }
        if (flag == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Validate long character words
    /// </summary>
    public static bool isLongWordExist(string text)
    {
        bool flag = false;
        if (text.Contains(" "))
        {
            string[] arr;
            arr = text.ToString().Split(char.Parse(" "));
            int i;

            for (i = 0; i <= arr.Length - 1; i++)
            {
                if (arr[i].Length > 30)
                    flag = true;
            }
        }
        else
        {
            if (text.Length > 30)
                flag = true;
            else
                flag = false;
        }
        return flag;
    }

    public static bool isImage(string contenttype)
    {
        bool status = false;
        switch (contenttype)
        {
            //case "image/bmp":
            //    status = true;
            //    break;
            case "image/gif":
                status = true;
                break;
            case "image/jpeg":
                status = true;
                break;
            case "image/png":
                status = true;
                break;
        }

        return status;
    }

    /// <summary>
    /// Generate and return country list
    /// </summary>
    private static string[] _countries = new string[] { 
         "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", 
         "Angola", "Anguilla", "Antarctica", "Antigua And Barbuda", "Argentina", 
         "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
		   "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
		   "Belgium", "Belize", "Benin", "Bermuda", "Bhutan",
		   "Bolivia", "Bosnia Hercegovina", "Botswana", "Bouvet Island", "Brazil",
		   "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Byelorussian SSR",
		   "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands",
		   "Central African Republic", "Chad", "Chile", "China", "Christmas Island",
		   "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands",
		   "Costa Rica", "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus",
		   "Czech Republic", "Czechoslovakia", "Denmark", "Djibouti", "Dominica",
		   "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador",
		   "England", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia",
		   "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France",
		   "Gabon", "Gambia", "Georgia", "Germany", "Ghana",
		   "Gibraltar", "Great Britain", "Greece", "Greenland", "Grenada",
		   "Guadeloupe", "Guam", "Guatemela", "Guernsey", "Guiana",
		   "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Islands",
		   "Honduras", "Hong Kong", "Hungary", "Iceland", "India",
		   "Indonesia", "Iran", "Iraq", "Ireland", "Isle Of Man",
		   "Israel", "Italy", "Jamaica", "Japan", "Jersey",
		   "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, South",
		   "Korea, North", "Kuwait", "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia",
		   "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein",
		   "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar",
		   "Malawi", "Malaysia", "Maldives", "Mali", "Malta",
		   "Mariana Islands", "Marshall Islands", "Martinique", "Mauritania", "Mauritius",
		   "Mayotte", "Mexico", "Micronesia", "Moldova", "Monaco",
		   "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar",
		   "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles",
		   "Neutral Zone", "New Caledonia", "New Zealand", "Nicaragua", "Niger",
		   "Nigeria", "Niue", "Norfolk Island", "Northern Ireland", "Norway",
		   "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea",
		   "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland",
		   "Polynesia", "Portugal", "Puerto Rico", "Qatar", "Reunion",
		   "Romania", "Russian Federation", "Rwanda", "Saint Helena", "Saint Kitts",
		   "Saint Lucia", "Saint Pierre", "Saint Vincent", "Samoa", "San Marino",
		   "Sao Tome and Principe", "Saudi Arabia", "Scotland", "Senegal", "Seychelles",
		   "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
		   "Somalia", "South Africa", "Spain", "Sri Lanka",
		   "Sudan", "Suriname", "Svalbard", "Swaziland", "Sweden",
		   "Switzerland", "Syria", "Taiwan", "Tajikista", "Tanzania",
		   "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago",
		   "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu",
		   "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States",
		   "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela",
		   "Vietnam", "Virgin Islands", "Wales", "Western Sahara", "Yemen",
		   "Yugoslavia", "Zaire", "Zambia", "Zimbabwe"};


    /// <summary>
    /// Returns an array with all countries
    /// </summary>     
    public static StringCollection GetCountries()
    {
        StringCollection countries = new StringCollection();
        countries.AddRange(_countries);
        return countries;
    }

    public static SortedList GetCountries(bool insertEmpty)
    {
        SortedList countries = new SortedList();
        if (insertEmpty)
            countries.Add("", "Please Select One");
        foreach (String country in _countries)
            countries.Add(country, country);
        return countries;
    }

    public static string ReplaceSpaceWithUnderscore(string input)
    {
        string str = "";
        // replace all  spaces with underscrore
        str = Regex.Replace(input, " ", "_");
        // remove special characters
        return Regex.Replace(str, @"[\[\]\\\^\$\.\/\|\?\*\+\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
    }

    public static string ReplaceSpaceWithHyphin(string input)
    {
        string str = "";
        // replace all spaces with hypin
        str = Regex.Replace(input, " ", "-");
        str = Regex.Replace(str, @"[\-]+", "-");
        // remove special characters
        // str = Regex.Replace(str, @"[\[\]\\\^\$\.\|\/\?\*\+\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
        Regex r = new Regex("(?:[^a-z0-9-_]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        return r.Replace(str, String.Empty);
        //str = Regex.Replace(str, @"^[\w\d:#@%/;$()~_?\+-=\\\.&]", "");
        //return str;
    }
    // without (.) filter using in tags, categories
    public static string ReplaceSpaceWithHyphin_v2(string input)
    {
        string str = "";
        // replace all spaces with hypin
        str = Regex.Replace(input, " ", "-");
        str = Regex.Replace(str, @"[\-]+", "-");
        // remove special characters
        // str = Regex.Replace(str, @"[\[\]\\\^\$\|\/\?\*\+\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
        Regex r = new Regex("(?:[^a-z0-9-._]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        return r.Replace(str, String.Empty);
    }

    public static string ReplaceHyphinWithSpace(string input)
    {
        string str = Regex.Replace(input, @"[\-]", " ");
        return str;
    }

    public static string Strip_Special_Characters(string str)
    {
        return Regex.Replace(str, @"[\[\]\\\^\$\.\|\/\?\*\+\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
    }

    public static string Strip_Search_Characters(string str)
    {
        return Regex.Replace(str, @"[\[\]\\\^\$\|\/\?\*\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
    }

    public static string Strip_RSS_Characters(string str)
    {
        return Regex.Replace(str, "nbsp|amp", " ");
    }

    public static string Add_NoFollow_Tag(string input)
    {
        return Regex.Replace(input, "<a", "<a rel='nofollow'");
    }

    // replace [p] with page number value
    public static string Add_PageNumber(string input, string value)
    {
        return Regex.Replace(input, @"\[p\]", value);
    }

    // apple -> a***e
    public static string Restrict_Word(string word)
    {
        int length = word.Length;
        int count = 0;
        StringBuilder str = new StringBuilder();
        foreach (char c in word)
        {
            if (count > 0 && count < length - 1)
                str.Append("*");
            else
                str.Append(c);

            count++;
        }
        return str.ToString();
    }

    public static string StripHTML(string htmlstring)
    {
        // string pattern = @"<(.|\n)*?>";
        string pattern = @"</?(?i:script|embed|object|font|div|span|p|frameset|frame|iframe|meta|link|style)(.|\n)*?>";
        htmlstring = Regex.Replace(htmlstring, pattern, string.Empty);
        return Regex.Replace(htmlstring, @"<[^>]+>", "");
        //  str = Regex.Replace(str, @"</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\n)*?>", "");
        // return str;
    }

    public static string RemoveImages(string htmlstring)
    {
        string pattern = @"</?(?i:img)(.|\n)*?>";
        return Regex.Replace(htmlstring, pattern, string.Empty);
    }

    // strip html except <p>
    public static string StripHTML_v2(string htmlstring)
    {
        string pattern = @"</?(?i:script|font|span|frameset|frame|iframe|meta|link|style)(.|\n)*?>";
        return Regex.Replace(htmlstring, pattern, string.Empty);
    }

    // strip every type of html except a
    public static string StripHTML_v3(string htmlstring)
    {
        string pattern = @"<(?!\/?a(?=>|\s.*>))\/?.*?>";
        return Regex.Replace(htmlstring, pattern, string.Empty);
        // return str;
    }
    // strip every type of html except a, p
    public static string StripHTML_v4(string htmlstring)
    {
        string pattern = @"<(?!\/?(a|p)(?=>|\s.*>))\/?.*?>";
        return Regex.Replace(htmlstring, pattern, string.Empty);
        // return str;
    }
    // match urls and replace it with <a href="" /> urls
    public static string Prepare_Urls(string html)
    {
        string tStr = html;
        string http_url = "";
        string short_url = "";
        string http_pattern = @"(\[)(?<url>(http(s)?):(([a-zA-Z0-9._\\\.\/\?\+\%#&\+-=])+)?)(\])"; // match url [http://www.abc.com] , [] is used to protect urls used within src or href urls
        Match VDMatch = Regex.Match(html, http_pattern);
        while (VDMatch.Success)
        {
            http_url = VDMatch.Groups["url"].Value;
            //http_url = VDMatch.Value;
            if (http_url.Length > 50)
                short_url = http_url.Substring(0, 50) + "...";
            else
                short_url = http_url;

            tStr = tStr.Replace(http_url, "<a href=\"" + http_url + "\" target=\"_blank\" rel=\"nofollow\">" + short_url + "</a>");
            tStr = tStr.Replace(@"[", "");
            tStr = tStr.Replace(@"]", "");
            VDMatch = VDMatch.NextMatch();
        }
        return tStr;
    }

    public static ArrayList Extract_JPEGS(string input)
    {
        RegexOptions m_options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
        Regex regexMatch = new Regex("<img[^>]+>|<a[^>]+>", m_options);
        // Regex regexMatch = new Regex(@"https?://(?:[a-z 0-9\-]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|jpeg)", m_options);
        ArrayList arr = new ArrayList();
        Match m = regexMatch.Match(input);
        while (m.Success)
        {
            arr.Add(m.Value);
            input = input.Replace(m.Value, "done");
            m = regexMatch.Match(input);
        }
        return arr;
    }

    public static string CleanBlogHTML(string html)
    {
        html = System.Text.RegularExpressions.Regex.Replace(html, @"<(.|\n)*?>", ""); // remove <.... >
        html = System.Text.RegularExpressions.Regex.Replace(html, @"(\[(\w)+\]([a-zA-Z0-9_\\\^\$\.\/\|\?\*\+\(\)\{\}%,:'""`«»“”‘’\;>!@#&\+\s-?])+(\[/(\w)+\]))", ""); // remove [abc]...[/abc]
        return Regex.Replace(html, @"[\[\]\\\^\$\|\/\?\*\+\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
    }


    public static string CleanSearchTerm(string html)
    {
        html = System.Text.RegularExpressions.Regex.Replace(html, @"<(.|\n)*?>", ""); // remove <.... >
        //return System.Text.RegularExpressions.Regex.Replace(html, @"(\[(\w)+\]([a-zA-Z0-9_\\\^\$\.\/\|\?\*\+\(\)\{\}%,:'""\;>!@#&\+\s-?])+(\[/(\w)+\]))", ""); // remove [abc]...[/abc]
        return Regex.Replace(html, @"[\[\]\\\^\$\|\/\?\*\(\)\{\}%,:'""`«»“”‘’;><!@#&\+]?", "");
    }

    /// <summary>
    /// Validate whether current value is in digit format
    /// </summary>
    public static bool isDigit(string value)
    {
        System.Text.RegularExpressions.Match m1 = System.Text.RegularExpressions.Regex.Match(value, @"(\d+)");
        if (m1.Success)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Customize Date
    /// </summary>
    public static string CustomizeDate(DateTime startdate, DateTime date)
    {
        string time = "";
        TimeSpan diffdate = date.Subtract(startdate);
        int days = diffdate.Days;
        if (days >= 365)
        {
            double yr = (double)((int)days / 365);
            int years = (int)((double)Math.Ceiling(yr));
            if (years > 1)
                time = years + " years ago";
            else
                time = years + " year ago";
        }
        else if (days >= 31 && days < 365)
        {
            double mn = (double)((int)days / 31);
            int months = (int)((double)Math.Ceiling(mn));
            if (months > 1)
                time = months + " months ago";
            else
                time = months + " month ago";
        }
        else if (days >= 7 && days < 31)
        {
            double wk = (double)((int)days / 7);
            int week = (int)((double)Math.Ceiling(wk));
            if (week > 1)
                time = week + " weeks ago";
            else
                time = week + " week ago";
        }
        else if (days < 7 && days > 0)
        {
            if (days > 1)
                time = days + " days ago";
            else
                time = days + " day ago";
        }
        else if (days == 0)
        {
            int hours = diffdate.Hours;
            if (hours == 0)
            {
                int minutes = diffdate.Minutes;
                if (minutes > 1)
                    time = minutes + " mins ago";
                else
                    time = minutes + " min ago";
            }
            else
            {
                if (hours > 1)
                    time = hours + " hours ago";
                else
                    time = hours + " hour ago";
            }

        }
        return time;
    }

    /// <summary>
    /// Customize Duration
    /// </summary>
    public static string Customize_Duration(string Duration)
    {
        try
        {
            TimeSpan _span = TimeSpan.Parse(Duration);
            int seconds = _span.Seconds;
            int minutes = _span.Minutes;
            int hours = _span.Hours;
            string str = "";
            if (hours > 0)
            {
                if (hours < 10)
                    str = str + "0" + hours + ":";
                else
                    str = str + "" + hours + ":";
            }
            if (minutes > 0)
            {
                str = str + "" + minutes + ":";
            }
            else
            {
                str = str + "0:";
            }
            if (seconds > 0)
            {
                if (seconds < 10)
                    str = str + "0" + seconds + "";
                else
                    str = str + "" + seconds + "";
            }
            else
            {
                str = str + "00";
            }
            return str;
        }
        catch (Exception ex)
        {
            return Duration;
        }

    }

    public static int GetDateDiff(DateTime startdate, DateTime lastdate)
    {
        TimeSpan diffdate = lastdate.Subtract(startdate);
        return diffdate.Days;
    }

    public static string FixCode(string html)
    {
        html = html.Replace("  ", "&nbsp; ");
        html = html.Replace("  ", " &nbsp;");
        html = html.Replace("\t", "&nbsp;&nbsp;&nbsp;");
        html = html.Replace("[", "&#91;");
        html = html.Replace("]", "&#93;");
        html = html.Replace("<", "&lt;");
        html = html.Replace(">", "&gt;");
        html = html.Replace("\n\n\n\n", "<br /><br />");
        html = html.Replace("\n\n\n", "<br /><br />");
        html = html.Replace("\n\n", "<br /><br />");
        html = html.Replace("\n", "<br />");

        // href
        html = Prepare_Urls(html);

        return html;
    }

    public static string CompressCode(string html)
    {
        html = html.Replace("  ", "");
        html = html.Replace("\t", "");
        html = html.Replace("\n\n\n\n", "");
        html = html.Replace("\n\n\n", "");
        html = html.Replace("\n\n", "");
        html = html.Replace("\n", "");
        return html;
    }

    public static string CompressCodeBreak(string html)
    {
        html = html.Replace("  ", "");
        html = html.Replace("\t", "");
        html = html.Replace("\n\n\n\n", "<br />");
        html = html.Replace("\n\n\n", "<br />");
        html = html.Replace("\n\n", "<br />");
        html = html.Replace("\n", "<br />");
        return html;
    }

    public static string DeCompressCodeBreak(string html)
    {
        html = html.Replace("<br />", "\n");
        return html;
    }

    public static string ReturnMonthName(int month)
    {
        string mn = "";
        switch (month)
        {

            case 1:
                mn = "January";
                break;
            case 2:
                mn = "February";
                break;
            case 3:
                mn = "March";
                break;
            case 4:
                mn = "April";
                break;
            case 5:
                mn = "May";
                break;
            case 6:
                mn = "June";
                break;
            case 7:
                mn = "July";
                break;
            case 8:
                mn = "August";
                break;
            case 9:
                mn = "September";
                break;
            case 10:
                mn = "October";
                break;
            case 11:
                mn = "November";
                break;
            case 12:
                mn = "December";
                break;
        }
        return mn;
    }


    //// Fetch gallery id from string
    public static ArrayList Fetch_Gallery_ID(string html)
    {
        RegexOptions m_options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
        Regex r_gid = new Regex("\\[GID\\](?<inner>(.*?))\\[/GID\\]", m_options);
        Match VDMatch = r_gid.Match(html);
        ArrayList _arr = new ArrayList();
        while (VDMatch.Success)
        {
            _arr.Add(VDMatch.Groups["inner"].Value);
            VDMatch = VDMatch.NextMatch();
        }
        return _arr;
    }

    public static string Replace_Term(string html, string term, string url)
    {
        RegexOptions m_options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
        Regex r_gid = new Regex(term, m_options);
        Match VDMatch = r_gid.Match(html);
        while (VDMatch.Success)
        {
            html = html.Replace(term, url);
        }
        return html;
    }

    public static string UppercaseFirst(string s, bool isallcharacters)
    {
        StringBuilder str = new StringBuilder();
        if (isallcharacters)
        {
            if (s.Contains(" "))
            {
                string[] arr = s.ToString().Split(char.Parse(" "));
                int i;
                for (i = 0; i <= arr.Length - 1; i++)
                {
                    if (arr[i].ToString().Length > 0)
                    {
                        str.Append(UppercaseFirst(arr[i].ToString()) + " ");
                    }
                }
            }
            else
            {
                str.Append(UppercaseFirst(s));
            }
        }
        else
        {
            str.Append(UppercaseFirst(s));
        }
        return str.ToString();
    }

    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static void RenameFile(string oldpath, string newpath)
    {
        if (File.Exists(oldpath))
        {
            File.Move(oldpath, newpath);
            if (File.Exists(newpath))
                File.Delete(oldpath);
        }
    }

    /// <summary>
    /// Generate Date in Different Formats
    /// </summary>
    public static string Generate_Date(DateTime _date, int FormatID)
    {
        string _date_output = "";
        //switch (Blog_Settings.PostDateTemplate)
        switch (FormatID)
        {
            case 0:
                // 0:  21 May, 2011
                _date_output = _date.Day + " " + UtilityBLL.ReturnMonthName(_date.Month) + ", " + _date.Year;
                break;
            case 1:
                // 1: May 30th, 2011
                string cday = _date.Day + "th";
                if (_date.Day == 1)
                    cday = _date.Day + "st";
                else if (_date.Day == 2)
                    cday = _date.Day + "nd";
                _date_output = UtilityBLL.ReturnMonthName(_date.Month) + " " + cday + ", " + _date.Year;
                break;
            case 2:
                // 2: May 11 2011
                _date_output = UtilityBLL.ReturnMonthName(_date.Month) + " " + _date.Day + ", " + _date.Year;
                break;
            case 3:
                // 3: 2 days ago 
                _date_output = UtilityBLL.CustomizeDate(_date, DateTime.Now);
                break;
            case 4:
                // Today 10:54 PM
                string suffix = "AM";
                int hours = 0;
                if (_date.Hour > 12)
                {
                    suffix = "PM";
                    hours = _date.Hour - 12;
                }
                TimeSpan diffdate = DateTime.Now.Subtract(_date);
                int days = diffdate.Days;
                string date_suffix = "";
                if (days == 0)
                    date_suffix = "Today";
                else if (days == 1)
                    date_suffix = "Yesterday";
                else
                    date_suffix = UtilityBLL.ReturnMonthName(_date.Month) + " " + _date.Day + ", " + _date.Year;

                _date_output = date_suffix + " " + hours + ":" + _date.Minute + " " + suffix;
                break;
        }
        return _date_output;
    }

    public static string Process_Content_Text(string html)
    {
        // compress code :-> replace \n - <br />
        html = CompressCodeBreak(html);
        // process bbcode
        html = BBCode.MakeHtml(html, true);
        // prepare urls
        html = UtilityBLL.GenerateLink(html, true);
        // html decode at the end
        //html = HttpContext.Current.Server.HtmlDecode(html);

        return html;
    }

    // remove html
    // compress code breaks
    // generate links
    // no bbcode processing
    // used in comments etc processing
    public static string Process_Content(string html)
    {
        string _cmt = UtilityBLL.StripHTML(html).Trim(); // remove all html
        if (_cmt == "")
            return "";
        // generate urls
        // fix html code
        _cmt = UtilityBLL.CompressCodeBreak(_cmt);
        _cmt = UtilityBLL.GenerateLink(_cmt, true);

        return _cmt;
    }

    public static bool ValidateUrl(string URL)
    {
        Match VDMatch = Regex.Match(URL, @"(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?");
        if (VDMatch.Success)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Determines if GZip is supported
    /// </summary>
    /// <returns></returns>
    public static bool IsGZipSupported()
    {
        string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
        if (!string.IsNullOrEmpty(AcceptEncoding) &&
                (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
            return true;
        return false;
    }

    /// <summary>
    /// Sets up the current page or handler to use GZip through a Response.Filter
    /// IMPORTANT:  
    /// You have to call this method before any output is generated!
    /// </summary>
    public static void GZipEncodePage()
    {
        HttpResponse Response = HttpContext.Current.Response;

        if (IsGZipSupported())
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (AcceptEncoding.Contains("deflate"))
            {
                Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                            System.IO.Compression.CompressionMode.Compress);
                Response.Headers.Remove("Content-Encoding");
                Response.AppendHeader("Content-Encoding", "deflate");
            }
            else
            {
                Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                            System.IO.Compression.CompressionMode.Compress);
                Response.Headers.Remove("Content-Encoding");
                Response.AppendHeader("Content-Encoding", "gzip");
            }

        }
    }

    // Utility Function To Replace String With Hyperlink
    public static string GenerateLink(string text, bool nofollow)
    {
        string pattern = @"(?<hrefurl><(a|iframe)[^>]*>.*?</(a|iframe)>)|(?<imgurl><img.*?/>)|(?<httpurl>(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";
        if (nofollow)
            return Regex.Replace(text, pattern, new MatchEvaluator(ComputeReplacementNoFollow));
        else
            return Regex.Replace(text, pattern, new MatchEvaluator(ComputeReplacement));
    }

    public static String ComputeReplacement(Match m)
    {
        if (m.Groups["imgurl"].Success)
            return m.Groups["imgurl"].Value;
        else if (m.Groups["httpurl"].Success)
            return "<a href=\"" + m.Groups["httpurl"].Value + "\">" + m.Groups["httpurl"].Value + "</a>";
        else
            return m.Groups["hrefurl"].Value;
    }

    // Computer Replacement With No Follow Text
    public static String ComputeReplacementNoFollow(Match m)
    {
        if (m.Groups["imgurl"].Success)
            return m.Groups["imgurl"].Value;
        else if (m.Groups["httpurl"].Success)
            return "<a href=\"" + m.Groups["httpurl"].Value + "\" rel=\"nofollow\">" + m.Groups["httpurl"].Value + "</a>";
        else
            return m.Groups["hrefurl"].Value;
    }
}