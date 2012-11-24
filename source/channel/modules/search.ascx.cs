using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class channel_modules_search : System.Web.UI.UserControl
{
    private string Character = "";
    public string acomplete_clientid; // autocomplete client id
    public string acomplete_serviceurl = ""; // autocomplete service url

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // autocomplete search setup
        //acomplete_clientid = search.ClientID;
        asearch.HRef = Config.GetUrl() + "channel/searchoptions.aspx";
        acomplete_serviceurl = Config.GetUrl() + "channel/modules/autocomplete.ashx";
        if (!Page.IsPostBack)
        {
            if (Request.Params["char"] != null)
            {
                this.Character = Request.Params["char"].ToString();
            }
            Generate_Filter_Links();
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (search.Text != "")
        {
            Response.Redirect(Config.GetUrl("channel/default.aspx?query=" + Server.UrlEncode(search.Text)));
        }
    }

    private void Generate_Filter_Links()
    {
        StringBuilder str = new StringBuilder();
        string _url = Config.GetUrl("channel/default.aspx");
        str.Append("<li><a class=\"active\" href=\"" + _url + "\" title=\"Browse all channels\">All</a></li>\n");

        if (this.Character == "a")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=a\" title=\"Browse channels starting with character A\">A</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=a\" title=\"Browse channels starting with character A\">A</a></li>\n");

        if (this.Character == "b")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=b\" title=\"Browse channels starting with character B\">B</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=b\" title=\"Browse channels starting with character B\">B</a></li>\n");

        if (this.Character == "c")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=c\" title=\"Browse channels starting with character C\">C</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=c\" title=\"Browse channels starting with character C\">C</a></li>\n");

        if (this.Character == "d")
            str.Append("<li class=\"active\"><a  href=\"" + _url + "?char=d\" title=\"Browse channels starting with character D\">D</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=d\" title=\"Browse channels starting with character D\">D</a></li>\n");

        if (this.Character == "e")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=e\" title=\"Browse channels starting with character E\">E</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=e\" title=\"Browse channels starting with character E\">E</a></li>\n");

        if (this.Character == "f")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=f\" title=\"Browse channels starting with character F\">F</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=f\" title=\"Browse channels starting with character F\">F</a></li>\n");

        if (this.Character == "g")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=g\" title=\"Browse channels starting with character G\">G</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=g\" title=\"Browse channels starting with character G\">G</a></li>\n");

        if (this.Character == "h")
            str.Append("<li class=\"active\"><a  href=\"" + _url + "?char=h\" title=\"Browse channels starting with character H\">H</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=h\" title=\"Browse channels starting with character H\">H</a></li>\n");

        if (this.Character == "i")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=i\" title=\"Browse channels starting with character I\">I</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=i\" title=\"Browse channels starting with character I\">I</a></li>\n");

        if (this.Character == "j")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=j\" title=\"Browse channels starting with character J\">J</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=j\" title=\"Browse channels starting with character J\">J</a></li>\n");

        if (this.Character == "k")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=k\" title=\"Browse channels starting with character K\">K</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=k\" title=\"Browse channels starting with character K\">K</a></li>\n");

        if (this.Character == "l")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=l\" title=\"Browse channels starting with character L\">L</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=l\" title=\"Browse channels starting with character L\">L</a></li>\n");

        if (this.Character == "m")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=m\" title=\"Browse channels starting with character M\">M</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=m\" title=\"Browse channels starting with character M\">M</a></li>\n");

        if (this.Character == "n")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=n\" title=\"Browse channels starting with character N\">N</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=n\" title=\"Browse channels starting with character N\">N</a></li>\n");

        if (this.Character == "o")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=o\" title=\"Browse channels starting with character O\">O</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=o\" title=\"Browse channels starting with character O\">O</a></li>\n");

        if (this.Character == "p")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=p\" title=\"Browse channels starting with character P\">P</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=p\" title=\"Browse channels starting with character P\">P</a></li>\n");

        if (this.Character == "q")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=q\" title=\"Browse channels starting with character Q\">Q</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=q\" title=\"Browse channels starting with character Q\">Q</a></li>\n");

        if (this.Character == "r")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=r\" title=\"Browse channels starting with character R\">R</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=r\" title=\"Browse channels starting with character R\">R</a></li>\n");

        if (this.Character == "s")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=s\" title=\"Browse channels starting with character S\">S</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=s\" title=\"Browse channels starting with character S\">S</a></li>\n");

        if (this.Character == "t")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=t\" title=\"Browse channels starting with character T\">T</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=t\" title=\"Browse channels starting with character T\">T</a></li>\n");

        if (this.Character == "u")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=u\" title=\"Browse channels starting with character U\">U</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=u\" title=\"Browse channels starting with character U\">U</a></li>\n");

        if (this.Character == "v")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=v\" title=\"Browse channels starting with character V\">V</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=v\" title=\"Browse channels starting with character V\">V</a></li>\n");

        if (this.Character == "w")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=w\" title=\"Browse channels starting with character W\">W</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=w\" title=\"Browse channels starting with character W\">W</a></li>\n");

        if (this.Character == "x")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=x\" title=\"Browse channels starting with character X\">X</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=x\" title=\"Browse channels starting with character X\">X</a></li>\n");

        if (this.Character == "y")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=y\" title=\"Browse channels starting with character Y\">Y</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=y\" title=\"Browse channels starting with character Y\">Y</a></li>\n");

        if (this.Character == "z")
            str.Append("<li class=\"active\"><a href=\"" + _url + "?char=z\" title=\"Browse channels starting with character Z\">Z</a></li>\n");
        else
            str.Append("<li><a href=\"" + _url + "?char=z\" title=\"Browse channels starting with character Z\">Z</a></li>\n");
       

        filter.InnerHtml = str.ToString();
    }
}