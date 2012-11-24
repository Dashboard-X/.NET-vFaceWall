using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Text;

public partial class myaccount_Default : System.Web.UI.Page
{ 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Myaccount top navigation setting
            macc_menu1.ActiveIndex = 6; // Account Settings

            if (!Page.User.Identity.IsAuthenticated)
            {
                string redirect_url = Config.GetUrl() + "myaccount/Default.aspx";
                Response.Redirect(Config.GetUrl() + "Login.aspx?ReturnUrl=" + redirect_url);
                return;
            }
            string username = Page.User.Identity.Name;

            // status / error messages
            if (Request.Params["status"] != null)
            {
                switch (Request.Params["status"].ToString())
                {
                    case "success":
                        Config.ShowMessageV2(msg, Resources.vsk.message_myaccount_01, "Success!", 1); //Your account has been created
                        break;
                    case "activated":
                        Config.ShowMessageV2(msg, Resources.vsk.message_myaccount_02, "Success!", 1); //Your account has been activated
                        break;
                }
            }
            

            left_nav1.ActiveIndex = 0;
                        
            // Load user overview
            Load_OverView(username);
        }
    }

    private void Load_OverView(string username)
    {
        List<Member_Struct> _list = members.Fetch_User_Channel(username);
        if (_list.Count > 0)
        {
            // avator setup
            avator_upd21.UserName = username;
            avator_upd21.PhotoName = _list[0].PictureName;

            string usr_channel_url = UrlConfig.Prepare_User_Profile_Url(username, false);
            StringBuilder prf_lf = new StringBuilder();

            string _name = "";
            if (_list[0].FirstName != "" || _list[0].LastName != "")
                _name = _list[0].FirstName + " " + _list[0].LastName;
            else
                _name = username;

            prf_lf.Append("<div class=\"item_pad_2\">\n");
            prf_lf.Append("<a href=\"" + UrlConfig.Prepare_User_Profile_Url(username,false) + "\" class=\"xxmedium-text bold\">" + _name + "</a>");
            prf_lf.Append("</div>\n");

            StringBuilder info = new StringBuilder();
            info.Append(_list[0].Gender);
            if (_list[0].RelationshipStatus != "")
                info.Append(", " + _list[0].RelationshipStatus);
            if (_list[0].HometTown != "" || _list[0].CurrentCity != "")
            {
                if (_list[0].CurrentCity != "")
                    info.Append(", " + _list[0].HometTown);
                else
                    info.Append(", " + _list[0].CurrentCity);
            }
            if (_list[0].Zipcode != "")
                info.Append(", " + _list[0].Zipcode);
            info.Append(", " + _list[0].CountryName);

            prf_lf.Append("<div class=\"item\">\n");
            prf_lf.Append("<span class=\"normal-text light\">" + info.ToString() + "</span>\n");
            string views = "0";
            if (_list[0].Views > 0)
                views = string.Format("{0:#,###}", _list[0].Views);
            prf_lf.Append("<br /><span class=\"normal-text light\">" + Resources.vsk.channelviews + "</span> <strong>" + views + "</strong><br />");
            prf_lf.Append("</div>\n");
            
            ovr_first.InnerHtml = prf_lf.ToString();
        }
    }
}
