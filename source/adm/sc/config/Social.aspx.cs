using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_config_Social : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Load_Settings();
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        // Check whether admin section is in readonly mode.
        if (!Config.isAdminActionAllowed())
        {
            Response.Redirect(Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }


        ConfigurationBLL.Update_Value(110, txt_facebookurl.Text);
        ConfigurationBLL.Update_Value(111, txt_twitterurl.Text);
        ConfigurationBLL.Update_Value(109, txt_feedburnerurl.Text);
        ConfigurationBLL.Update_Value(112, txt_fbappid.Text);
        ConfigurationBLL.Update_Value(114, txt_fbsecretkey.Text);
        ConfigurationBLL.Update_Value(116, txt_addthispubid.Text);
        ConfigurationBLL.Update_Value(117, txt_twitter_uid.Text);
        ConfigurationBLL.Update_Value(115, drp_share.SelectedValue);
        ConfigurationBLL.Update_Value(118, drp_og.SelectedValue);
        
        Load_Settings();

        Config.ShowMessageV2(msg, "Social Configuration Settings Updated Successfully", "Success!", 1);

    }

    private void Load_Settings()
    {
        txt_facebookurl.Text = Social_Settings.Facebook_url;
        txt_twitterurl.Text = Social_Settings.Twitter_url;
        txt_feedburnerurl.Text = Social_Settings.Feedburner_url;
        txt_fbappid.Text = Social_Settings.FB_AppID;
        txt_fbsecretkey.Text = Social_Settings.FB_AppSecret;
        txt_addthispubid.Text = Social_Settings.Addthis_PubID;
        txt_twitter_uid.Text = Social_Settings.Twitter_UID;
        drp_share.SelectedValue = Social_Settings.Share_Buttons.ToString();
        drp_og.SelectedValue = Social_Settings.ShowFacebookMetaTags.ToString();
        
    }
}