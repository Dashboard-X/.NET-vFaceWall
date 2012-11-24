using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using BCrypt.Net;
public partial class modules_createaccount : System.Web.UI.UserControl
{
    public bool isAdmin
    {
        get
        {
            if (ViewState["isAdmin"] != null)
                return (bool)ViewState["isAdmin"];
            else
                return false;
        }
        set
        {
            ViewState["isAdmin"] = value;
        }
    }
   
    private members _memberprocess = new members();
    protected string Redirect_Url = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        
        // globalization settings
        btn_register.Text = Resources.vsk.register;
        if (Request.Params["ReturnUrl"] != null)
        {
            this.Redirect_Url = Request.Params["ReturnUrl"].ToString();
        }
        // Add terms agreement validation via javascript
        btn_register.OnClientClick = "return validate_agreement('#" + chk_agree.ClientID + "');";

        if (!Page.IsPostBack)
        {
            if (this.isAdmin)
                adm.Visible = true;
            else
                adm.Visible = false;

            // Load countries
            Load_Countries();

            // load parent / child categories
            //int category_type = 2; // represent member channels
            //CategoriesBLL.BindCategories(drp_accounttype, 0, category_type, " " + Resources.vsk.selectcategory + " ", " ", true);
            // Load Days / Months / Years
            //Load_Days();
            //Load_Year();

        }

    }

    protected void btn_register_Click1(object sender, EventArgs e)
    {
        if (!chk_agree.Checked)
        {
            Config.ShowMessageV2(msg, Resources.vsk.message_reg_01, "Error!", 0); // "Accept terms of use &amp; privacy policy before continue."
            return;
        }
        // birth date processing
        //string _birth_date = drp_birthday_month.SelectedValue + "/" + drp_birthday_day.SelectedValue + "/" + drp_year.SelectedValue;
        //DateTime birth_day = Convert.ToDateTime(_birth_date);
        //int date_diff = DateTime.Now.Year - birth_day.Year;
        //if (date_diff < 10)
        //{
        //    Config.ShowMessage(msg, Resources.vsk.message_reg_02, 0, 0); // Age must be greater than 10 years before registering on this website.
        //    return;
        //}
        // check for restricted usernames
        string res_values = DictionaryBLL.Return_RestrictedUserNames();
        if (res_values != "")
        {
            if (DictionaryBLL.isMatch(lUserName.Text, res_values))
            {
                Config.ShowMessageV2(msg, Resources.vsk.message_reg_03, "Error!", 0); // User name not available, please choose another one.
                return;
            }
        }

        // IP Address tracking and processing
        string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (BlockIPBLL.Validate_IP(ipaddress))
        {
            Response.Redirect(Config.GetUrl("IPBlocked.aspx"));
            return;
        }


        if (_memberprocess.Check_UserName(lUserName.Text))
        {
            Config.ShowMessageV2(msg, Resources.vsk.message_reg_03, "Error!", 0); // User name not available, please choose another one.
            return;
        }
        if (_memberprocess.Check_Email(Email.Text))
        {
            Config.ShowMessageV2(msg, Resources.vsk.message_reg_04, "Error!", 0); // "Email address is already exist."
            return;
        }

        string gender = "Male";
        if (r_female.Checked)
            gender = "Female";

        // validation key processing
        string val_key = "none";
        int isenabled = 1; // user account activated
        if (Config.isRegistrationValidation() && !this.isAdmin)
        {
            val_key = Guid.NewGuid().ToString().Substring(0, 10);
            isenabled = 0; // user account deactivated
        }
        // Add Member
        int type = 0; // normal member
        if (this.isAdmin)
        {
            type = Convert.ToInt32(drp_acc.SelectedValue);
        }

        int userrole_id = 0;

        // encrypt password
        //int BCRYPT_WORK_FACTOR = 10;
        string encrypted_password = BCrypt.Net.BCrypt.HashPassword(lPassword.Text);
        members.Add(0, lUserName.Text, encrypted_password , Email.Text, drp_country.SelectedValue.ToString(), isenabled, gender, DateTime.Now, val_key, type, userrole_id);
        // Create Required Directories
        Directory_Process.CreateRequiredDirectories(Server.MapPath(Request.ApplicationPath) + "/contents/member/" + lUserName.Text.ToLower());

        if (this.isAdmin)
        {
            Response.Redirect(Config.GetUrl("adm/sc/members/Default.aspx?status=created"));
        }
        else
        {
            // Send Mail
            MailTemplateProcess(Email.Text, lUserName.Text, lPassword.Text, val_key);

            if (Config.isRegistrationValidation())
            {
                Response.Redirect("Validate.aspx?user=" + lUserName.Text + "");
            }
            else
            {
                // authorize user
                FormsAuthentication.SetAuthCookie(lUserName.Text, false);
                // Store IP Address Log 
                User_IPLogBLL.Process_Ipaddress_Log(lUserName.Text, ipaddress);
                if(Config.GetMembershipAccountUpgradeRedirect()==1)
                    Response.Redirect("myaccount/Packages.aspx?status=success");
                else
                    Response.Redirect("myaccount/Default.aspx?status=success");
            }
        }
    }
  


    private void Load_Countries()
    {
        drp_country.DataSource = UtilityBLL.GetCountries(true);
        drp_country.DataTextField = "Value";
        drp_country.DataValueField = "Key";
        drp_country.DataBind();
    }

    // mail processing
    private void MailTemplateProcess(string emailaddress, string username, string password, string key)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRREG");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);
                contents = MailProcess.Process2(contents, "\\[password\\]", password);
                string validation_url = Config.GetUrl("validate.aspx?key=" + key.Trim() + "&user=" + username.Trim());
                string url = "<a href=\"" + validation_url + "\">" + validation_url + "</a>";
                contents = MailProcess.Process2(contents, "\\[key_url\\]", url);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
    
}