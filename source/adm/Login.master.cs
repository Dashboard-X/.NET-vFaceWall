using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

public partial class adm_Login : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Core JQUERY JS Call       
        HtmlGenericControl jsquery = new HtmlGenericControl("script");
        jsquery.Attributes.Add("type", "text/javascript");
        jsquery.Attributes.Add("src", "https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js");
        Page.Header.Controls.Add(jsquery);

        // Twitter Bootstrap      
        HtmlGenericControl tstrap = new HtmlGenericControl("script");
        tstrap.Attributes.Add("type", "text/javascript");
        tstrap.Attributes.Add("src", Config.GetUrl("twitter/bootstrap.min.js"));
        Page.Header.Controls.Add(tstrap);

        // VSK OWN JS Call
        HtmlGenericControl vskjs = new HtmlGenericControl("script");
        vskjs.Attributes.Add("type", "text/javascript");
        vskjs.Attributes.Add("src", Config.GetUrl() + "javascript/vsk.js");
        Page.Header.Controls.Add(vskjs);

        // Activate for enabling round corners in IE Browser
        // Note: You must remove round corner behavriour from buttons, textbox and other controls. it will disturb these controls 
        // while applying round corners.
        HtmlGenericControl js_ie_round_corner = new HtmlGenericControl("script");
        js_ie_round_corner.Attributes.Add("type", "text/javascript");
        js_ie_round_corner.Attributes.Add("src", Config.GetUrl("javascript/DD_roundies.uicornerfix.js"));
        Page.Header.Controls.Add(js_ie_round_corner);

    }
}
