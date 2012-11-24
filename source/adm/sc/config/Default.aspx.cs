using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_sc_config_Default : System.Web.UI.Page
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

   
        ConfigurationBLL.Update_Value(1, txt_title.Text);
        ConfigurationBLL.Update_Value(2, txt_description.Text);
      
        ConfigurationBLL.Update_Value(4, txt_email.Text);
        ConfigurationBLL.Update_Value(5, txt_emaildisplayname.Text);

        ConfigurationBLL.Update_Value(7, drp_databasetype.SelectedValue);
        ConfigurationBLL.Update_Value(8, drp_screening.SelectedValue);
        ConfigurationBLL.Update_Value(9, drp_approvaltype.SelectedValue);
        ConfigurationBLL.Update_Value(10, txt_abusereport_count.Text);
        ConfigurationBLL.Update_Value(11, txt_cache.Text);
      
        ConfigurationBLL.Update_Value(17, txt_channel_psize.Text);


        ConfigurationBLL.Update_Value(183, txt_tracking.Text);
        ConfigurationBLL.Update_Value(186, drp_template.SelectedValue);
        ConfigurationBLL.Update_Value(171, drp_navigationside.SelectedValue);
        ConfigurationBLL.Update_Value(26, txt_max_url_length.Text);
        
     
        Load_Settings();

        Config.ShowMessageV2(msg, "General Configuration Settings Updated Successfully", "Success!", 1);

    }

    private void Load_Settings()
    {
        txt_title.Text = Site_Settings.Website_Title;
        txt_description.Text = Site_Settings.Website_Description;
       
        txt_email.Text = Site_Settings.Admin_Mail;
        txt_emaildisplayname.Text = Site_Settings.Admin_Mail_DisplayName;
      
        drp_screening.SelectedValue = Site_Settings.Screen_Content.ToString();
        drp_approvaltype.SelectedValue = Site_Settings.Content_Approval.ToString();
        txt_abusereport_count.Text = Site_Settings.Spam_Count.ToString();
        txt_cache.Text = Site_Settings.Cache_Duration.ToString();
      
        txt_channel_psize.Text = Site_Settings.Channel_Page_Size.ToString();
      
        txt_tracking.Text = Site_Settings.Site_Analytics;
        drp_template.SelectedValue = Site_Settings.Site_Templates.ToString();
        drp_navigationside.SelectedValue = Site_Settings.NavigationSection.ToString();
        txt_max_url_length.Text = Site_Settings.Maxiumum_Dynamic_Url_Length.ToString();
       
    }
}