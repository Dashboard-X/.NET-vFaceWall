using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
/// <summary>
/// Summary description for SocialScripts
/// </summary>
public class SocialScripts
{
	public SocialScripts()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string Generate_Social_Box()
    {
        StringBuilder str = new StringBuilder();
        str.Append("<div class=\"item_r\">\n");
        str.Append("<table style=\"width:240px;\">\n");
        str.Append("<tr>\n");
        str.Append("<td style=\"width:80px; text-align:left; vertical-align:middle;\">\n");
        str.Append("<g:plusone size=\"medium\"></g:plusone>\n");
        str.Append("</td>\n");
        str.Append("<td style=\" width:80px; text-align:left; vertical-align:middle;\">\n");
        str.Append("<fb:like href=\"\" layout=\"button_count\" show_faces=\"true\" width=\"40\" font=\"\"></fb:like>\n");
        str.Append("</td>\n");
        str.Append("<td style=\"width:80px; text-align:left; vertical-align:middle;\">\n");
       // str.Append("<a href=\"http://twitter.com/share\" class=\"twitter-share-button\" data-count=\"horizontal\" data-via=\"" + Social_Settings.Twitter_UID + "\">Tweet</a>\n");
        str.Append("</td>\n");
        str.Append("</tr>\n");
        str.Append("</table>\n");
        str.Append("</div>\n"); // end of bx
        return str.ToString();
    }
}