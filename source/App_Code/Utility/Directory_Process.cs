using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.IO;
/// <summary>
/// Summary description for Directory_Process
/// </summary>
public class Directory_Process
{
	public Directory_Process()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void CreateRequiredDirectories(string strPath)
    {
        Directory.CreateDirectory(strPath);
        Directory.CreateDirectory(strPath + "\\default");
        Directory.CreateDirectory(strPath + "\\flv");
        Directory.CreateDirectory(strPath + "\\mp3");
        Directory.CreateDirectory(strPath + "\\thumbs");
        Directory.CreateDirectory(strPath + "\\photos");
        Directory.CreateDirectory(strPath + "\\photos\\thumbs");
        Directory.CreateDirectory(strPath + "\\photos\\midthumbs");
        Directory.CreateDirectory(strPath + "\\groups");
        Directory.CreateDirectory(strPath + "\\groups\\thumbs");
    }

     public static void CreatePhotoDirectories(string strPath)
     {
         if (!Directory.Exists(strPath))
             Directory.CreateDirectory(strPath);
         if (!Directory.Exists(strPath + "\\midthumbs"))
             Directory.CreateDirectory(strPath + "\\midthumbs");
         if (!Directory.Exists(strPath + "\\thumbs"))
             Directory.CreateDirectory(strPath + "\\thumbs");
     }

     // Create directory to store blog optional photos
     public static void CreateBlogDirectory(string strPath)
     {
         if (!Directory.Exists(strPath))
             Directory.CreateDirectory(strPath);
         if (!Directory.Exists(strPath + "\\thumbs\\"))
             Directory.CreateDirectory(strPath + "\\medium\\");
     }
}
