using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_config_Features : System.Web.UI.Page
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
            Response.Redirect( Config.GetUrl("adm/sc/Default.aspx?action_error=true"));
            return;
        }

        // update configuration values in database
        string value = "0";
       
        if (r_channels_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(74, value);

      

        if (r_comments_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(84, value);

     

        if (r_ads_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(90, value);

        if (r_email_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(91, value);

        if (r_email_ver_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(92, value);

       

        if (r_adult_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(94, value);

        if (r_rating_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(100, value);

        if (r_views_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(95, value);

        if (r_date_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(96, value);

        if (r_username_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(99, value);

        if (r_registration_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(97, value);

        if (r_registration_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(150, value);


        if (r_mchannel_on.Checked)
            value = "true";
        else
            value = "false";
        ConfigurationBLL.Update_Value(24, value);

        if (r_ip_on.Checked)
            value = "true";
        else
            value = "false";
        ConfigurationBLL.Update_Value(12, value);

      

        if (r_login_rating_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(101, value);

        if (r_login_comment_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(102, value);

        if (r_advice_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(103, value);

        if (r_cviews_on.Checked)
            value = "1";
        else
            value = "0";
        ConfigurationBLL.Update_Value(23, value);
      
        Load_Settings();
        Config.ShowMessageV2(msg, "Feature Configuration Settings Updated Successfully", "Success!", 1);

    }

    private void Load_Settings()
    {
      

        if (Site_Settings.Feature_Channels == 1)
            r_channels_on.Checked = true;
        else
            r_channels_off.Checked = true;

        if (Site_Settings.Feature_Comments == 1)
            r_comments_on.Checked = true;
        else
            r_comments_off.Checked = true;

        if (Site_Settings.Feature_Advertisement == 1)
            r_ads_on.Checked = true;
        else
            r_ads_off.Checked = true;

        if (Site_Settings.Feature_Email == 1)
            r_email_on.Checked = true;
        else
            r_email_off.Checked = true;

        if (Site_Settings.Feature_Email_Verification == 1)
            r_email_ver_on.Checked = true;
        else
            r_email_ver_off.Checked = true;

        if (Site_Settings.Feature_Adult_Verification == 1)
            r_adult_on.Checked = true;
        else
            r_adult_off.Checked = true;

        if (Site_Settings.Feature_Rating == 1)
            r_rating_on.Checked = true;
        else
            r_rating_off.Checked = true;

        if (Site_Settings.Feature_Views == 1)
            r_views_on.Checked = true;
        else
            r_views_off.Checked = true;

        if (Site_Settings.Channel_Views == 1)
            r_cviews_on.Checked = true;
        else
            r_cviews_off.Checked = true;

        if (Site_Settings.Feature_Date == 1)
            r_date_on.Checked = true;
        else
            r_date_off.Checked = true;

        if (Site_Settings.Feature_UserName == 1)
            r_username_on.Checked = true;
        else
            r_username_off.Checked = true;

        if (Site_Settings.Feature_MemberRegistration == 1)
            r_registration_on.Checked = true;
        else
            r_registration_off.Checked = true;

       

        if (Site_Settings.Channel_Custom_Theme)
            r_mchannel_on.Checked = true;
        else
            r_mchannel_off.Checked = true;

        if (Site_Settings.Store_IPAddress)
            r_ip_on.Checked = true;
        else
            r_ip_off.Checked = true;

       
        if (Site_Settings.Feature_Login_Rating==1)
            r_login_rating_on.Checked = true;
        else
            r_login_rating_off.Checked = true;

        if (Site_Settings.Feature_Login_Comment == 1)
            r_login_comment_on.Checked = true;
        else
            r_login_comment_off.Checked = true;

        if (Site_Settings.Feature_Login_Advice == 1)
            r_advice_on.Checked = true;
        else
            r_advice_off.Checked = true;

    }
}