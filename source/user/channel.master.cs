using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class user_channel : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //base.OnInit(e);
        base.OnInit(e);
        // UtilityBLL.GZipEncodePage();
        string _root = Config.GetUrl();

        // Core JQUERY JS Call       
        //HtmlGenericControl jsquery = new HtmlGenericControl("script");
        //jsquery.Attributes.Add("type", "text/javascript");
        //jsquery.Attributes.Add("src", "https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js");
        //Page.Header.Controls.Add(jsquery);

        // Twitter Bootstrap      
        HtmlGenericControl tstrap = new HtmlGenericControl("script");
        tstrap.Attributes.Add("type", "text/javascript");
        tstrap.Attributes.Add("src", Config.GetUrl("twitter/bootstrap.min.js"));
        Page.Header.Controls.Add(tstrap);

        // Jquery Validator     
        HtmlGenericControl jvalidate = new HtmlGenericControl("script");
        jvalidate.Attributes.Add("type", "text/javascript");
        jvalidate.Attributes.Add("src", Config.GetUrl("javascript/jquery.validate.js"));
        Page.Header.Controls.Add(jvalidate);

        //// jquery user interface related js call
        //HtmlGenericControl ui_core = new HtmlGenericControl("script");
        //ui_core.Attributes.Add("type", "text/javascript");
        //ui_core.Attributes.Add("src", "https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js");
        //Page.Header.Controls.Add(ui_core);

        //// Dialog Menu JS Call
        //HtmlGenericControl dialogbgi = new HtmlGenericControl("script");
        //dialogbgi.Attributes.Add("type", "text/javascript");
        //dialogbgi.Attributes.Add("src", "http://jquery-ui.googlecode.com/svn/tags/latest/external/jquery.bgiframe-2.1.2.js");
        //Page.Header.Controls.Add(dialogbgi);

        // Autocomplete JS Call
        HtmlGenericControl autocompletejs = new HtmlGenericControl("script");
        autocompletejs.Attributes.Add("type", "text/javascript");
        autocompletejs.Attributes.Add("src", _root + "jquery/plugin/jquery.autocomplete.js");
        Page.Header.Controls.Add(autocompletejs);

        //// Potato Menu JS Call
        //HtmlGenericControl potatojs = new HtmlGenericControl("script");
        //potatojs.Attributes.Add("type", "text/javascript");
        //potatojs.Attributes.Add("src", _root + "jquery/plugin/jquery.ui.potato.menu.js");
        //Page.Header.Controls.Add(potatojs);

        // VSK OWN JS Call
        HtmlGenericControl vskjs = new HtmlGenericControl("script");
        vskjs.Attributes.Add("type", "text/javascript");
        vskjs.Attributes.Add("src", _root + "javascript/vsk.js");
        Page.Header.Controls.Add(vskjs);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //HttpContext context = HttpContext.Current;
        //context.Response.Filter = new System.IO.Compression.GZipStream(context.Response.Filter, System.IO.Compression.CompressionMode.Compress);
        //HttpContext.Current.Response.AppendHeader("Content-encoding", "gzip");
        //HttpContext.Current.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
    }
}
