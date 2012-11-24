using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Threading;
using System.Globalization;

public partial class main : System.Web.UI.MasterPage
{

    protected string JScript = "";
   
    protected override void OnInit(EventArgs e)
    {       
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

        //// Jquery Validator     
        //HtmlGenericControl jvalidate = new HtmlGenericControl("script");
        //jvalidate.Attributes.Add("type", "text/javascript");
        //jvalidate.Attributes.Add("src", Config.GetUrl("javascript/jquery.validate.js"));
        //Page.Header.Controls.Add(jvalidate);

              
        // Autocomplete JS Call
        HtmlGenericControl autocompletejs = new HtmlGenericControl("script");
        autocompletejs.Attributes.Add("type", "text/javascript");
        autocompletejs.Attributes.Add("src", _root + "jquery/plugin/jquery.autocomplete.js");
        Page.Header.Controls.Add(autocompletejs);

    
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
             
    }



    
}
