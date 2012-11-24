using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;

public partial class myaccount_modules_avator_upd2 : System.Web.UI.UserControl
{
    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }

    public string PhotoName
    {
        get
        {
            if (ViewState["PhotoName"] != null)
                return ViewState["PhotoName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["PhotoName"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // globalization text
        StringBuilder str = new StringBuilder();
        str.Append("<div style=\"width:98px;\" class=\"item_c\"><img width=\"95\" height=\"95\" src=\"" + UrlConfig.Retun_User_Profile_Thumb_Url(this.UserName, this.PhotoName, 0) + "\" class=\"thumbnail\" id=\"avator\"/>\n");
        str.Append("<a data-toggle=\"modal\" href=\"#myModal\">" + Resources.vsk.change + "</a></div>\n");
        lst.InnerHtml = str.ToString();

        btn_save.Text = Resources.vsk.savechanges; // Save Changes
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string strPath = Server.MapPath(Request.ApplicationPath) + "//contents//member//" + this.UserName + "//photos//";

        // delete old user profile photo if exist
        if (this.PhotoName != "none" || !this.PhotoName.Contains("http"))
        {
            try
            {
                if (File.Exists(strPath + "" + this.PhotoName))
                    File.Delete(strPath + "" + this.PhotoName);

                if (File.Exists(strPath + "thumbs/" + this.PhotoName))
                    File.Delete(strPath + "thumbs/" + this.PhotoName);
            }
            catch (Exception ex)
            {
                //Response.Redirect( Config.GetUrl("user/Profile.aspx?status=derror"));
            }

        }

        // update new photo
        string filename = Guid.NewGuid().ToString().Substring(0, 10) + "" + ph1.PostedFile.FileName.Remove(0, ph1.PostedFile.FileName.LastIndexOf("."));
        if (!UtilityBLL.isImage(ph1.PostedFile.ContentType))
        {
            Response.Redirect( Config.GetUrl("myaccount/Profile.aspx?status=invalidformat"));
            return;
        }

        ph1.PostedFile.SaveAs(strPath + "" + filename);
        ph1.PostedFile.SaveAs(strPath + "thumbs/" + filename);
        string orignalfilename = strPath + "" + filename;
        string thumbfilename = strPath + "thumbs/" + filename;
        Bitmap mp = Image_Process.CreateThumbnail(orignalfilename, 92);
        if (mp == null)
        {
            Response.Redirect( Config.GetUrl("myaccount/Profile.aspx?status=perror"));
            return;
        }

        mp.Save(thumbfilename);
        Image_Process.SaveJpeg(thumbfilename, mp, 90);
        mp.Dispose();

        members.Update_Value(this.UserName, "PictureName", filename);
        Response.Redirect( Config.GetUrl("myaccount/Profile.aspx?status=pupdated"));
    }

    
}
