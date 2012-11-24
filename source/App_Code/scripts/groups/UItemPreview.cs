using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// This class is responsible for generating different types of content previews (facebook style) within posts.
/// </summary>
public class UItemPreview
{
    public UItemPreview()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region MyRegion
    private int _width = 130; // width of thumb inside box
    private int _leftwidth = 30; // in % in case of showing thumb left and content on right
    private int _rightwidth = 69; // in % in case of showing thumb left and content on right
    private int _height = 100; // 0: default height of thumb inside box
      
    private bool _isadmin = false; // admin mode
    // Show Options
    private bool _isroundcorners = true; // enable, disable round corners on thumb item
    private int _titlelength = 0; // maximum allowed length of title (0: unlimited)
    private bool _showtitle = true; // show title of photo
    private bool _showdescription = true; // show description of photo
    private int _descriptionlength = 0; // 0: show complete description
    private bool _showdate = true;
    private int _dateformat = 3; //  // 0:  21 May, 2011, 1: May 30th, 2011, 2: May 11 2011, 3: 2 days ago, 4: Today 10:54 PM
    private bool _showauthor = true;
    private bool _showurl = true; // show url of actual content
    // urls
    private string _authorurl = ""; // url of author of photo, if empty default url will be used.
    private string _previewurl = ""; // url of title or thumb preview, if empty default url will be used
    // css styles
    private string _thumbcssclass = "thumbnail";
    private string _thumbroundcssclass = "thumbnail";
    private string _boldlinkcssclass = "normal-text bold";
    private string _normallinkcssclass = "";

    private int _type = 0; // Type of content
    private string _contenturl = "";
    // Type: (Post Type)
    // ........... 0: General Text (website internal)
    // ........... 1: Video (website internal)
    // ........... 2: Video Album (website) internal)
    // ........... 3: Photo (website internal)
    // ........... 4: Photo Album (website internal)
    // ........... 5: Blog Post (website internal)
    // ........... 6: Audio (website internal)
    // ........... 7: Audio Album (website internal)
    // ........... 8: Forum Post (website internal)
    // ........... 9: User Channel (website internal)
    // ........... 10: All external links
    // ........... 11: Youtube Link
    public int Width
    {
        set { _width = value; }
        get { return _width; }
    }

    public int LeftWidth
    {
        set { _leftwidth = value; }
        get { return _leftwidth; }
    }

    public int RightWidth
    {
        set { _rightwidth = value; }
        get { return _rightwidth; }
    }

    public int Height
    {
        set { _height = value; }
        get { return _height; }
    }

    public bool ShowUrl
    {
        set { _showurl = value; }
        get { return _showurl; }
    }

    public bool isRoundCorners
    {
        set { _isroundcorners = value; }
        get { return _isroundcorners; }
    }
    public string PreviewUrl
    {
        set { _previewurl = value; }
        get { return _previewurl; }
    }
    public bool isAdmin
    {
        set { _isadmin = value; }
        get { return _isadmin; }
    }

    public int TitleLength
    {
        set { _titlelength = value; }
        get { return _titlelength; }
    }

    public bool ShowTitle
    {
        set { _showtitle = value; }
        get { return _showtitle; }
    }

    public bool ShowDescription
    {
        set { _showdescription = value; }
        get { return _showdescription; }
    }
    public int DescriptionLength
    {
        set { _descriptionlength = value; }
        get { return _descriptionlength; }
    }

    public bool ShowDate
    {
        set { _showdate = value; }
        get { return _showdate; }
    }
    public int DateFormat
    {
        set { _dateformat = value; }
        get { return _dateformat; }
    }

    public bool ShowAuthor
    {
        set { _showauthor = value; }
        get { return _showauthor; }
    }
      
    public string AuthorUrl
    {
        set { _authorurl = value; }
        get { return _authorurl; }
    }

    public string ThumbCssClass
    {
        set { _thumbcssclass = value; }
        get { return _thumbcssclass; }
    }

    public string ThumbRoundCssClass
    {
        set { _thumbroundcssclass = value; }
        get { return _thumbroundcssclass; }
    }
    public string BoldLinkCssClass
    {
        set { _boldlinkcssclass = value; }
        get { return _boldlinkcssclass; }
    }
    public string NormalLinkCssClass
    {
        set { _normallinkcssclass = value; }
        get { return _normallinkcssclass; }
    }
    public int Type
    {
        set { _type = value; }
        get { return _type; }
    }

    public string ContentUrl
    {
        set { _contenturl = value; }
        get { return _contenturl; }
    }
    #endregion

    public string Process()
    {      

        StringBuilder str = new StringBuilder();
        // Main Item Box
        if (this.ContentUrl != "")
        {
            str.Append("<div class=\"item\" style=\"width:95%;\">\n");
            switch (this.Type)
            {
                //case 1:
                //    // Internal Video Preview
                //    str.Append(Prepare_Internal_Video_Preivew_Content(0));
                //    break;
                //case 3:
                //    // Internal Photo Preview
                //    str.Append(Prepare_Internal_Photo_Preivew_Content());
                //    break;
                //case 2:
                //    // Internal audio or video album preview
                //    str.Append(Prepare_Internal_Album_Preivew_Content());
                //    break;
                //case 4:
                //    // Internal Photo Gallery Preview
                //    str.Append(Prepare_Internal_Gallery_Preivew_Content());
                //    break;
                //case 5:
                //    // Internal Blog Preview
                //    str.Append(Prepare_Internal_Blog_Preivew_Content());
                //    break;
                //case 6:
                //    // Internal Audio Gallery Preview
                //    str.Append(Prepare_Internal_Video_Preivew_Content(1));
                //    break;
                //case 9:
                //    // Internal Channel Preview
                //    str.Append(Prepare_Internal_Channel_Preivew_Content());
                //    break;
                case 11:
                    // External Youtube Preview Url
                    str.Append(Prepare_Youtube_Preview_Url());
                    break;
                case 12:
                     // External Vimeo Preview Url
                     str.Append(Prepare_Vimeo_Preview_Url());
                    break;
                case 10:
                     // External Urls
                    str.Append(Prepare_External_Content_Source());
                    break;
                //case 13:
                //    // Ask Question
                //    str.Append(Prepare_Internal_QA_Preivew_Content());
                //    break;
                case 14:
                    // Answer Question
                    str.Append(Prepare_Vimeo_Preview_Url());
                    break;     
                  
            }
            str.Append("</div>\n");
        }
        return str.ToString();
    }
    
    private string Prepare_Youtube_Preview_Url()
    {
        StringBuilder str = new StringBuilder();
        string YtID = Fetch_YoutubeID(this.ContentUrl);
        if (YtID != "")
        {
           // Fetch Youtube Video Information
            str.Append("<iframe width=\"" + this.Width + "\" height=\"" + this.Height + "\" src=\"http://www.youtube.com/embed/" + YtID + "\" frameborder=\"0\" allowfullscreen></iframe>");
        }
        else
        {
            str.Append("No youtube video preview available!");
        }
        this.PreviewUrl = "";
        return str.ToString();
    }

    private string Prepare_Vimeo_Preview_Url()
    {        
        StringBuilder str = new StringBuilder();
        string YtID = Fetch_Vimeo_ID(this.ContentUrl);
        if (YtID != "")
        {
           // Fetch Youtube Video Information
            str.Append("<iframe src=\"http://player.vimeo.com/video/" + YtID + "?title=0&byline=0&portrait=0\" width=\"" + this.Width + "\" height=\"" + this.Height + "\" frameborder=\"0\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>");
        }
        else
        {
            str.Append("No vimeo video preview available!");
        }
        return str.ToString();
    }

    private string Prepare_External_Content_Source()
    {
        try
        {
            string source = Process_External_Url_Content();
            if (source != "")
            {
                // Generate Post from OG Content
                string post = Generate_OG_Post(source);
                if (post != "")
                {
                    return post;
                }
                // Crawl for post title, description, image
                post = Generate_Meta_Post(source);
                if (post != "")
                {
                    return post;
                }
                // If no meta or description information available, then create post from general content
                post = Generate_General_Post(source);
                if (post != "")
                {
                    return post;
                }
                else
                {
                    return "No information retrieved from specified url.";
                }
            }
            else
            {
                return "No information retrieved from specified url.";
            }
        }
        catch (Exception ex)
        {
            return "No information retrieved from specified url.";
        }
    }

    // Generate Post from general website page content
    private string Generate_General_Post(string source)
    {
        StringBuilder str = new StringBuilder();
        string post = UtilityBLL.StripHTML(source);
        if (post != "")
        {
            string image = Return_Post_Image(source);
            if (image != "")
            {
                // post contain image.
                str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
                str.Append(Process_Image(image, ""));
                str.Append("</div>\n");
                str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
                str.Append(Generate_General_Post_Content(post, source));
                str.Append("</div>\n");
                str.Append("<div class=\"clear\"></div>\n");
            }
            else
            {
                // post without image
                str.Append(Generate_General_Post_Content(post, source));
            }
        }
        
        return str.ToString();

    }

    private string Generate_General_Post_Content(string description, string source)
    {
        StringBuilder str = new StringBuilder();
        // url fetch from canonical link
        string url = Fetch_Canonical_Link(source);
        if (url == "")
            url = this.ContentUrl;

        if (this.ShowUrl && url != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _url = url;
            if (_url.Length > 50)
                _url = _url.Substring(0, 50) + "..";
            str.Append("<a target=\"_blank\" href=\"" + this.ContentUrl + "\" class=\"" + this.NormalLinkCssClass + "\">" + _url + "</a>\n");
            str.Append("</div>\n");
        }

        // Description
        if (this.ShowDescription && description != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            str.Append("<span class=\"light\">" + UGeneral.Prepare_Description(description, this.DescriptionLength) + "</span>");
            str.Append("</div>\n");
        }
        return str.ToString();
    }
    // Generate post from page meta information
    private string Generate_Meta_Post(string source)
    {
        StringBuilder str = new StringBuilder();
        string title = Fetch_Page_Title(source);
        string description = Fetch_Page_Description(source);

        if (title != "" || description != "")
        {
            // check for image .jpg image
            string image = Return_Post_Image(source);
            if (image != "")
            {
                // post contain image.
                str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
                str.Append(Process_Image(image, title));
                str.Append("</div>\n");
                str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
                str.Append(Process_General_Post_Content(title, description,source));
                str.Append("</div>\n");
                str.Append("<div class=\"clear\"></div>\n");
            }
            else
            {
                // post without image
                str.Append(Process_General_Post_Content(title, description, source));
            }
        }
        return str.ToString();
        
    }

    // generate post content from page meta information
    private string Process_General_Post_Content(string title, string description, string source)
    {
        StringBuilder str = new StringBuilder();
        // title
        if (this.ShowTitle && title != "")
        {
            string _title = title;
            if (_title.Length > this.TitleLength && this.TitleLength > 0)
                _title = _title.Substring(0, this.TitleLength) + "..";
            if (_title != "")
            {
                str.Append("<div class=\"item_pad_2\">\n");
                string _title_url = "<a target=\"_blank\" href=\"" + this.ContentUrl + "\" class=\"" + this.BoldLinkCssClass + "\" title=\"" + title + "\">" + _title + "</a>";
                str.Append("</div>\n");
                str.Append(_title_url);
            }
        }
        // url fetch from canonical link
        string url = Fetch_Canonical_Link(source);
        if (url == "")
            url = this.ContentUrl;

        if (this.ShowUrl && url != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _url = url;
            if (_url.Length > 50)
                _url = _url.Substring(0, 50) + "..";
            str.Append("<a target=\"_blank\" href=\"" + this.ContentUrl + "\" class=\"" + this.NormalLinkCssClass + "\">" + _url + "</a>\n");
            str.Append("</div>\n");
        }

        // Description
        if (this.ShowDescription && description != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            str.Append("<span class=\"light\">" + UGeneral.Prepare_Description(description, this.DescriptionLength) + "</span>");
            str.Append("</div>\n");
        }

        return str.ToString();
    }
    // Generate post content from og source if source contains og tag
    private string Generate_OG_Post(string source)
    {
        StringBuilder str = new StringBuilder();
        // Fetch OG Title
        string title = Fetch_OGTitle(source, "title");
        string description = Fetch_OGTitle(source, "description");
        string url = Fetch_OGTitle(source, "url");
        string image = Fetch_OGTitle(source, "image");

        if (title != "" || description != "" || url != "" || image != "")
        {
            if (image != "")
            {
                // post contain image.
                str.Append("<div style=\"float:left; width:" + this.LeftWidth + "%;\">\n");
                str.Append(Process_Image(image,title));
                str.Append("</div>\n");
                str.Append("<div style=\"float:right; width:" + this.RightWidth + "%;\">\n");
                str.Append(Process_OG_Content(title, url, description));                
                str.Append("</div>\n");
                str.Append("<div class=\"clear\"></div>\n");
            }
            else
            {
                str.Append(Process_OG_Content(title, url, description));                
            }
        }

        return str.ToString();
    }

    private string Process_OG_Content(string title, string url, string description)
    {
        StringBuilder str = new StringBuilder();
        // title
        if (this.ShowTitle && title != "")
        {
            string _title = title;
            if (_title.Length > this.TitleLength && this.TitleLength > 0)
                _title = _title.Substring(0, this.TitleLength) + "..";
            if (_title != "")
            {
                str.Append("<div class=\"item_pad_2\">\n");
                string _title_url = "<a target=\"_blank\" href=\"" + this.ContentUrl + "\" class=\"" + this.BoldLinkCssClass + "\" title=\"" + title + "\">" + _title + "</a>";
                str.Append("</div>\n");
                str.Append(_title_url);
            }
        }
        // url
        if (this.ShowUrl && url != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            string _url = url;
            if (_url.Length > 50)
                _url = _url.Substring(0, 50) + "..";
            str.Append("<a target=\"_blank\" href=\"" + this.ContentUrl + "\" class=\"" + this.NormalLinkCssClass + "\">" + _url + "</a>\n");
            str.Append("</div>\n");
        }
        // Description
        if (this.ShowDescription && description != "")
        {
            str.Append("<div class=\"item_pad_2\">\n");
            str.Append("<span class=\"light\">" + UGeneral.Prepare_Description(description, this.DescriptionLength) + "</span>");
            str.Append("</div>\n");
        }
        return str.ToString();
    }
    private string Process_External_Url_Content()
    {
        if (this.ContentUrl != "")
        {

            System.Net.WebClient client = new System.Net.WebClient();
            System.IO.Stream data = client.OpenRead(this.ContentUrl);
            System.IO.StreamReader reader = new System.IO.StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            return s;
        }
        else
        {
            return "";
        }
    }

    private string Process_Image(string imageurl,string caption)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<a target=\"_blank\" href=\"" + imageurl + "\" title=\"" + caption + "\">"); // thumb link setup
        string border_class = this.ThumbRoundCssClass;
        if (!this.isRoundCorners)
            border_class = this.ThumbCssClass;
        str.Append("<img class=\"" + border_class + "\" src=\"" + imageurl + "\" height=\"" + this.Height + "\" width=\"" + this.Width + "\">");
        str.Append("</a>");

        return str.ToString();
    }
    // this function will parse blog content and will return image url of one image
    private string Return_Post_Image(string desc)
    {     
        string url = "";
        string jpgpatthern = @"(?<imageurl>http://([a-zA-Z0-9.\/\\_\-\+]+)?.jpg)";
        Match jpgm = Regex.Match(desc, jpgpatthern);
        if (jpgm.Success)
        {
            url = jpgm.Groups["imageurl"].Value;
        }
        return url;
    }
    private long Fetch_ID(string URL, string Match)
    {
        string reg = @"(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/" + Match + @"/(?<pid>(\d+))";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(URL, reg);
        if (VDMatch.Success)
        {
            return Convert.ToInt64(VDMatch.Groups["pid"].Value);
        }
        else
            return 0;
    }

    private string Fetch_UserName(string URL, string Match)
    {
        string reg = @"(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/" + Match + @"/(?<pid>(\w+))";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(URL, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["pid"].Value;
        }
        else
            return "";
    }

    private string Fetch_YoutubeID(string URL)
    {
        string reg = @"(?<=v(\=|\/))(?<yid>([-a-zA-Z0-9_]+))|(?<=youtu\.be\/)(?<yid2>([-a-zA-Z0-9_]+))";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(URL, reg);
        if (VDMatch.Success)
        {
            if (VDMatch.Groups["yid"].Value != "")
                return VDMatch.Groups["yid"].Value; // normal pattern
            else
                return VDMatch.Groups["yid2"].Value; // pattern like http://youtu.be/[id]
        }
        else
            return "";
    }

    private string Fetch_Vimeo_ID(string URL)
    {
        string reg = @"(http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/(?<vid>(\d+))";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(URL, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["vid"].Value;
        }
        else
            return "";
    }
       

    private string Fetch_OGTitle(string Text, string OgType)
    {       
        string reg = "<meta property=\"og:" + OgType + "\" content=\"(?<ogtitle>(.)*?\")\"";
       
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(Text, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["ogtitle"].Value;

        }
        else
            return "";
    }

    // Fetch website title from source
    private string Fetch_Page_Title(string Text)
    {
        string reg = "<title>(?<stitle>(.)*?)</title>";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(Text, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["stitle"].Value;
        }
        else
            return "";
        
    }

    // Fetch website description from source
    private string Fetch_Page_Description(string Text)
    {
        string reg = "<meta name=\"description\" content=\"(?<des>(.)*?\")";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(Text, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["des"].Value;
        }
        else
            return "";
    }

    // Fetch Canonical Link from Source
    private string Fetch_Canonical_Link(string Text)
    {
        string reg = "<link rel=\"canonical\" href=\"(?<des>(.)*?\")";
        System.Text.RegularExpressions.Match VDMatch = System.Text.RegularExpressions.Regex.Match(Text, reg);
        if (VDMatch.Success)
        {
            return VDMatch.Groups["des"].Value;
        }
        else
            return "";
    }
   
  
    
}