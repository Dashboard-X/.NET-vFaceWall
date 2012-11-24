using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// This class is used to generate meta tags for different static and dynamic pages
/// </summary>
public class MetaTagsBLL
{
	public MetaTagsBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Main
    // Meta Tags for Default Page
    public static void META_StaticPage(System.Web.UI.Page _pg, System.Web.UI.HtmlControls.HtmlHead _hd, string title, string description, string imageurl, string url)
    {      
        _pg.Title = title + " - " + Site_Settings.Website_Title;
        PAGE_META(_hd, "", description);
        FB_META(_hd, title, description, imageurl, url);
    }
        
    #endregion
        

    private static void PAGE_META(HtmlHead _hd, string keywords, string description)
    {

        HtmlMeta hm1 = new HtmlMeta();
        hm1.Name = "description";
        hm1.Content = description;
        _hd.Controls.Add(hm1);
    }

    private static void FB_META(HtmlHead _hd, string title, string description, string imageurl, string url)
    {
        if (Social_Settings.ShowFacebookMetaTags)
        {
            // og title
            HtmlMeta ogtitle = new HtmlMeta();
            ogtitle.Attributes.Add("property", "og:title");
            ogtitle.Attributes.Add("content", title);
            _hd.Controls.Add(ogtitle);
            // og description
            HtmlMeta ogdescription = new HtmlMeta();
            ogdescription.Attributes.Add("property", "og:description");
            ogdescription.Attributes.Add("content", description);
            _hd.Controls.Add(ogdescription);
            // og image
            if (imageurl != "")
            {
                HtmlMeta ogimage = new HtmlMeta();
                ogimage.Attributes.Add("property", "og:image");
                ogimage.Attributes.Add("content", imageurl);
                _hd.Controls.Add(ogimage);
            }
            // og type
            HtmlMeta ogtype = new HtmlMeta();
            ogtype.Attributes.Add("property", "og:type");
            ogtype.Attributes.Add("content", "article");
            _hd.Controls.Add(ogtype);

            // og url
            if (url != "")
            {
                HtmlMeta ogurl = new HtmlMeta();
                ogurl.Attributes.Add("property", "og:url");
                ogurl.Attributes.Add("content", url);
                _hd.Controls.Add(ogurl);
            }
            // og site name
            HtmlMeta ogsitename = new HtmlMeta();
            ogsitename.Attributes.Add("property", "og:site_name");
            ogsitename.Attributes.Add("content", Site_Settings.Website_Title);
            _hd.Controls.Add(ogsitename);

            // fb app id
            HtmlMeta fbappid = new HtmlMeta();
            fbappid.Attributes.Add("property", "fb:app_id");
            fbappid.Attributes.Add("content", Social_Settings.FB_AppID);
            _hd.Controls.Add(fbappid);
        }
    }
}