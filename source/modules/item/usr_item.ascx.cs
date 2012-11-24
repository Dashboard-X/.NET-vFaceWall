using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Text;

public partial class modules_item_usr_item : System.Web.UI.UserControl
{
    private UtilityBLL _utility = new UtilityBLL();

    #region Properties
      
    private string _username = "";
    private string _picturename = "";
    private bool _isadmin = false;
    
    public string UserName
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }

    }

    public string PictureName
    {
        get
        {
            return _picturename;
        }
        set
        {
            _picturename = value;
        }

    }

    public bool isAdmin
    {
        get
        {
            return _isadmin;
        }
        set
        {
            _isadmin = value;
        }
    }
   
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {

        Set_User_Item();
    }

    private void Set_User_Item()
    {
        // user url
        string _user_url = "";
        if (this.isAdmin)
          _user_url =  Config.GetUrl("adm/sc/MemberDetail.aspx?user=" + this.UserName);
        else
            _user_url = UrlConfig.Prepare_User_Profile_Url(this.UserName, this.isAdmin);
            

        // username setup
        string _user = this.UserName;
        if (_user.Length > 10)
            _user = _user.Substring(0, 7) + "...";
        string user_link = "<a href=\"" + _user_url + "\" title=\"" + this.UserName + "\">" + _user + "</a>";
        u_nm.InnerHtml = user_link;
       
        // username thumb setup
        StringBuilder u_str = new StringBuilder();
        u_str.Append("<a href=\"" + _user_url + "\">");
        string avator_url = "";
        avator_url = UrlConfig.Retun_User_Profile_Thumb_Url(this.UserName, this.PictureName, 0);

        u_str.Append("<img src=\"" + avator_url + "\" class=\"thumbnail\" width=\"60px\" height=\"60px\"/>");
               
        u_str.Append("</a>");
        u_img.InnerHtml = u_str.ToString();

    }


}
