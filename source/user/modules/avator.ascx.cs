using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class user_modules_avator : System.Web.UI.UserControl
{
    private int _width = 90;
    private int _height = 90;

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
                return "none";
        }
        set
        {
            ViewState["PhotoName"] = value;
        }
    }
      
    public int Height
    {
        set { _height = value; }
        get { return _height; }
    }

    public int Width
    {
        set { _width = value; }
        get { return _width; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
       
        avator.Width = this.Width;
        avator.Height = this.Height;

       
        if (this.PhotoName == "")
        {
            // get user photo info from db
            this.PhotoName = members.Get_Picture(this.UserName);
        }

        avator.Src = UrlConfig.Retun_User_Profile_Thumb_Url(this.UserName, this.PhotoName,0);
                 
    }
   
}
