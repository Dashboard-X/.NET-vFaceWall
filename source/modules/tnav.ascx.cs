using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class modules_tnav : System.Web.UI.UserControl
{
    protected string Links = "";
    protected string Post_Handler = "";
    protected string LoginUrl = "";
    protected string IPBlockedUrl = "";
    protected string RedirectUrl = "";
    private string _rooturl = "";
    protected void Page_PreRender(object sender, EventArgs e)
    {       
        _rooturl = Config.GetUrl();
        Post_Handler = _rooturl + "handlers/login.ashx";
        LoginUrl = _rooturl + "login.aspx?status=error";
        IPBlockedUrl = _rooturl + "IPBlocked.aspx";
        Process_Links();

    }

    private void Process_Links()
    {
        StringBuilder str = new StringBuilder();
        string app_path = Config.GetUrl();
        if (Page.User.Identity.IsAuthenticated)
        {
            string profile_url = app_path + "user/" + Page.User.Identity.Name + ".aspx";
            string myaccount_url = app_path + "myaccount/";
            string signout_url = app_path + "logout.aspx";

            str.Append("<li class=\"dropdown\"><a rel=\"nofollow\" class=\"dropdown-toggle bld\" data-toggle=\"dropdown\" href=\"" + profile_url + "\">" + Page.User.Identity.Name + " <b class=\"caret\"></b></a>\n");
            str.Append("<ul class=\"dropdown-menu\">\n");
            str.Append("<li><a href=\"" + myaccount_url + "\">" + Resources.vsk.myaccount + "</a></li>\n");
            str.Append("<li><a href=\"" + profile_url + "\">" + Resources.vsk.mychannel + "</a></li>\n");
            
            str.Append("</ul>\n");
            str.Append("</li>");
            str.Append("<li><a class=\"mdt\" href=\"" + signout_url + "\">" + Resources.vsk.signout + "</a></li>");
        }
        else
        {
            string redirect_url = Server.UrlEncode(Config.Return_Current_Page());
            string register_url = app_path + "register.aspx";
            string login_url = app_path + "login.aspx?ReturnUrl=" + redirect_url.ToString();
            str.Append("<li><a class=\"bold\" rel=\"nofollow\" href=\"" + register_url + "\">" + Resources.vsk.createaccount + "</a></li>");
            str.Append("<li class=\"dropdown\"><a rel=\"nofollow\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\">" + Resources.vsk.signin + " <b class=\"caret\"></b></a>\n");
            str.Append("<ul class=\"dropdown-menu\"><li><div class=\"nav-login\">\n");
            str.Append(GenerateLoginForm());
            str.Append("</div></li></ul>\n");
            str.Append("</li>");
           
        }

        Links = str.ToString();
        //t_nav.InnerHtml = str.ToString();
    }


    private string GenerateLoginForm()
    {
        string cache = "lgnsm_cache";
        if (HttpContext.Current.Cache[cache.ToString()] == null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<div class=\"vkbox-header\">\n");
            str.Append("<h3>Login!</h3>\n");
            str.Append("</div>\n");
            str.Append("<div class=\"vkbox-body\">\n");
            str.Append("<div id=\"lgnsm_msg\"></div>\n");
            // form body section
            str.Append("<form class=\"navbar-form pull-left\">");
            str.Append("<div class=\"form-horizontal\">\n");
            str.Append("<fieldset>\n");
            str.Append("<div class=\"control-group\">\n");
            str.Append("<label class=\"control-label\" for=\"lgnsm_uname\">\n");
            str.Append("User Name:</label>\n");
            str.Append("<div class=\"controls\">\n");
            str.Append("<input name=\"lgnsm_uname\" class=\"input-medium\" placeholder=\"User Name\" title=\"Enter your username\" type=\"text\" maxlength=\"30\" id=\"lgnsm_uname\" />\n");
            str.Append("</div>\n");
            str.Append("</div>\n");
            str.Append("<div class=\"control-group\">\n");
            str.Append("<label class=\"control-label\" for=\"lgnsm_pass\">\n");
            str.Append("Password:</label>\n");
            str.Append("<div class=\"controls\">\n");
            str.Append("<input name=\"lgnsm_pass\" placeholder=\"Password\" title=\"Enter your password\" class=\"input-medium\" type=\"password\" id=\"lgnsm_pass\" />\n");
            str.Append("</div>\n");
            str.Append("</div>\n");
            str.Append("<div class=\"controls\">\n");
            str.Append("<label class=\"checkbox\">\n");
            str.Append("<input id=\"lgnsm_rem\" type=\"checkbox\" name=\"lgnsm_rem\" /> Remember me next time</label>\n");
            str.Append("</div>\n");

            str.Append("</fieldset>\n");
            str.Append("</div>\n");
            str.Append("</form>\n");
            // close form body section
            str.Append("</div>\n");
            str.Append("<div class=\"vkbox-footer-sm row-fluid\">\n");
            str.Append("<div style=\"float:left; width:45%; padding-top:5px;\">\n");
            str.Append("<a class=\"bold\" rel=\"tooltip\" title=\"Click here if you forgot your password\" href=\"" + _rooturl + "forgotpassword.aspx\">Forgot Password!</a>\n");
            str.Append("</div>\n");
            str.Append("<div style=\"float:right; width:54%;\" class=\"item r\">\n");
            str.Append("<input type=\"submit\" name=\"submit\" class=\"btn btn-primary vtoplogin\" value=\"Login\" />");
            str.Append("&nbsp;|&nbsp;");
            if (Request.Params["ReturnUrl"] != null)
                RedirectUrl = Request.Params["ReturnUrl"].ToString();
            else
                RedirectUrl = _rooturl + "myaccount/";

            str.Append("<a rel=\"nofollow\" class=\"btn\" href=\"#\" rel=\"tooltip\"  onclick=\"fb_login('" + _rooturl + "','" + RedirectUrl + "');\" title=\"" + Resources.vsk.loginviafacebook + "\">Facebook</a>");
            str.Append("</div>\n");
            str.Append("</div>\n");
            HttpContext.Current.Cache.Add(cache.ToString(), str.ToString(), null, DateTime.Now.AddMinutes(Config.GetCacheDuration()), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
        }
   
        return HttpContext.Current.Cache[cache.ToString()].ToString();
    }
   
}