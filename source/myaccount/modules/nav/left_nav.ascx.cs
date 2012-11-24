using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class myaccount_modules_left_nav : System.Web.UI.UserControl
{
    public int ActiveIndex = 0;
    protected void Page_PreRender(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            // setup left navigation links
            string url = Config.GetUrl();
            StringBuilder str = new StringBuilder();
            string overview_class= "";
            string psetup_class="";
            string privacy_class="";
            string email_class="";
            string manage_class="";
            switch (ActiveIndex)
            {
                case 0:
                    overview_class = " class=\"active\"";
                    break;
                case 1:
                     psetup_class = " class=\"active\"";
                    break;
                case 2:
                     privacy_class = " class=\"active\"";
                    break;
                case 3:
                     email_class = " class=\"active\"";
                    break;
                case 5:
                    manage_class = " class=\"active\"";
                    break;
            }
            str.Append("<ul class=\"nav nav-pills nav-stacked\">\n");

            str.Append("<li" + overview_class + "><a href=\"" + url + "myaccount/\">" + Resources.vsk.overview + "</a></li>\n");
            str.Append("<li" + psetup_class + "><a href=\"" + url + "myaccount/profile.aspx\">" + Resources.vsk.profilesetup + "</a></li>\n");
            str.Append("<li" + email_class + "><a href=\"" + url + "myaccount/emailoptions.aspx\">" + Resources.vsk.emailoptions + "</a></li>\n");
            str.Append("<li" + privacy_class + "><a href=\"" + url + "myaccount/privacy.aspx\">" + Resources.vsk.privacy + "</a></li>\n");
           
            str.Append("<li" + manage_class + "><a href=\"" + url + "myaccount/manageaccount.aspx\">" + Resources.vsk.manageaccount + "</a></li>\n");
            str.Append("</ul>\n");
            nav.InnerHtml = str.ToString();
        }
    }
}