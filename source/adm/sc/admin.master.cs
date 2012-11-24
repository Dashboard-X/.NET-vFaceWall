using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class adm_sc_admin : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        // UtilityBLL.GZipEncodePage();
        string _root = Config.GetUrl();

        // Core JQUERY JS Call       
        HtmlGenericControl jsquery = new HtmlGenericControl("script");
        jsquery.Attributes.Add("type", "text/javascript");
        jsquery.Attributes.Add("src", "https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js");
        Page.Header.Controls.Add(jsquery);

        // Twitter Bootstrap      
        HtmlGenericControl tstrap = new HtmlGenericControl("script");
        tstrap.Attributes.Add("type", "text/javascript");
        tstrap.Attributes.Add("src", Config.GetUrl("twitter/bootstrap.min.js"));
        Page.Header.Controls.Add(tstrap);

         HtmlGenericControl jcss = new HtmlGenericControl("link");
         jcss.Attributes.Add("type", "text/css");
         jcss.Attributes.Add("rel", "stylesheet");
         jcss.Attributes.Add("href", Config.GetUrl("adm/jquery/dcverticalmegamenu.css"));
         Page.Header.Controls.Add(jcss);

        HtmlGenericControl jsquery_menu = new HtmlGenericControl("script");
        jsquery_menu.Attributes.Add("type", "text/javascript");
        jsquery_menu.Attributes.Add("src", Config.GetUrl("adm/jquery/jquery.hoverIntent.minified.js"));
        Page.Header.Controls.Add(jsquery_menu);

         HtmlGenericControl jvert = new HtmlGenericControl("script");
         jvert.Attributes.Add("type", "text/javascript");
         jvert.Attributes.Add("src", Config.GetUrl("adm/jquery/jquery.dcverticalmegamenu.1.3.js"));
         Page.Header.Controls.Add(jvert);
        
        // VSK OWN JS Call
        HtmlGenericControl vskjs = new HtmlGenericControl("script");
        vskjs.Attributes.Add("type", "text/javascript");
        vskjs.Attributes.Add("src", _root + "javascript/vsk.js");
        Page.Header.Controls.Add(vskjs);
    }
   
}
