<%@ WebHandler Language="C#" Class="postpreview" %>

using System;
using System.Web;
using System.Text;

public class postpreview : IHttpHandler {

    public string URL = "";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        if (context.Request.Params["u"] != null)
            this.URL = context.Request.Params["u"].ToString();

        //ErrorLgBLL.Add_Log("Point 1", "///", "Url Value is: " + this.URL);
        context.Response.Write(Process_Data());
    }

    private string Process_Data()
    {
        StringBuilder str = new StringBuilder();
        if (this.URL != "" && UtilityBLL.ValidateUrl(this.URL))
        {
            // Type: (Post Type)
            // ........... 0: General Text (website internal)
            // ........... 1: Video (website internal)
            // ........... 2: Video Album (website) internal)  Audio Album (website internal)
            // ........... 3: Photo (website internal)
            // ........... 4: Photo Album (website internal)
            // ........... 5: Blog Post (website internal)
            // ........... 6: Audio (website internal)
            // ........... 8: Forum Post (website internal)
            // ........... 9: User Channel (website internal)
            // ........... 10: All external links
            // ........... 11: Youtube Link
            // ........... 12: Vimeo Link
            // ........... 13: Ask Question
            // ........... 14: Answer Question
            UItemPreview itm = new UItemPreview();
            itm.ContentUrl = this.URL;
            itm.isRoundCorners = false;
            itm.DescriptionLength = 100;
            if (this.URL.StartsWith(Config.GetUrl()))
            {
                if (this.URL.Contains("/photo/"))
                {
                    itm.Type = 3; // type internal photo url

                }
                else if (this.URL.Contains("/video/"))
                {
                    itm.Type = 1; // type internal video url
                }
                else if (this.URL.Contains("/gallery/"))
                {
                    itm.Type = 4; // type internal photo gallery url
                    itm.ShowUrl = false; // disable showing url as its not looking good
                    itm.Width = 130; // width of gallery thumb
                    itm.Height = 85;  // height of gallery thumb
                }
                else if (this.URL.Contains("/audio/"))
                {
                    itm.Type = 6; // type internal audio type
                }
                else if (this.URL.Contains("/post/"))
                {
                    itm.Type = 5;

                }
                else if (this.URL.Contains("/user/"))
                {
                    itm.Type = 9;
                }
                else if (this.URL.Contains("/question/"))
                {
                    itm.Type = 13; // Q&amp;A
                }
                else if (this.URL.Contains("/album/"))
                {
                    itm.Type = 2;
                    itm.ShowUrl = false; // disable showing url as its not looking good
                    itm.Width = 130; // width of gallery thumb
                    itm.Height = 85;  // height of gallery thumb
                }
            }
            else
            {
                if (this.URL.Contains("youtu.be") || this.URL.Contains("youtube.com"))
                {
                    itm.Type = 11; // youtube video
                    itm.Width = 400;
                    itm.Height = 235;
                }
                else if (this.URL.Contains("vimeo.com"))
                {
                    itm.Type = 12; // vimeo video
                    itm.Width = 400;
                    itm.Height = 235;
                }
                else
                {
                    itm.Type = 10; // all external links
                }
            }
            str.Append(itm.Process());
        }
        else
        {
            str.Append("No data found!");
        }

        // string hidden box unique id - > this will create box with close link, and will be close when user click on close link
        string boxid = "bxprvdata";
        return Config.SetHiddenMessage_v2("<div id=\"postcontent\">\n" + str.ToString() + "</div>", boxid, true, 4);
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}