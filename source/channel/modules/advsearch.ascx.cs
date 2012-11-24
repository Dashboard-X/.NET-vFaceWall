using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class channel_modules_advsearch : System.Web.UI.UserControl
{
    public string HeaderTitle = "Advance Channel Search";
    public int CategoryType = 2; // for channels
    public string SearchUrl = "channel/search.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        // globalization text
        btn_update.Text = Resources.vsk.submit; // Submit

        if (!Page.IsPostBack)
        {
            cmt.InnerHtml = this.HeaderTitle;

            Load_Countries();

        }
    }

    protected void btn_post_Click(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
 

        // if(txt_term.Text !="" || txt_user.Text !="" || _categories != "" || )
        str.Append("?o=" + drp_order.SelectedValue);
        if (txt_term.Text != "")
            str.Append("&query=" + Server.UrlEncode(UtilityBLL.CleanSearchTerm(txt_term.Text)));
        if (drp_country.SelectedValue!="")
            str.Append("&cnt=" + drp_country.SelectedValue);
        if (drp_gender.SelectedValue != "")
            str.Append("&gender=" + drp_gender.SelectedValue);
        
        if (chk_onlyphoto.Checked)
            str.Append("&ponly=true");

        Response.Redirect(Config.GetUrl(SearchUrl + "" + str.ToString()), true);
    }

    private void Load_Countries()
    {
        drp_country.DataSource = UtilityBLL.GetCountries(true);
        drp_country.DataTextField = "Value";
        drp_country.DataValueField = "Key";
        drp_country.DataBind();
    }

   
}