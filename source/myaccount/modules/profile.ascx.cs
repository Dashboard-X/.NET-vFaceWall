using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Collections.Generic;

public partial class myaccount_modules_profile : System.Web.UI.UserControl
{

    public string UserName
    {
        get
        {
            if (ViewState["UserName"] != null)
                return ViewState["UserName"].ToString();
            else
                return "";
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Globalization Text
        btn_bsave.Text = Resources.vsk.savechanges; // Save Changes
        btn_save.Text = Resources.vsk.savechanges; // Save Changes

        if (!Page.IsPostBack && this.UserName != "")
        {
            // load countries
            Load_Countries();
            // Load user profile information
            Load_Profile_Info(this.UserName);
        }

    }
    
    private void Load_Countries()
    {
        drp_country.DataSource = UtilityBLL.GetCountries(true);
        drp_country.DataTextField = "Value";
        drp_country.DataValueField = "Key";
        drp_country.DataBind();
    }
    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if(this.UserName!="")
            Update_User_Profile_Info(this.UserName);
    }
    protected void btn_bsave_Click(object sender, EventArgs e)
    {
        if(this.UserName!="")
           Update_User_Profile_Info(this.UserName);
    }

    private void Load_Profile_Info(string username)
    {
        List<Member_Struct> _list = members.Fetch_User_Profile(username);
        if (_list.Count > 0)
        {
            txt_aboutme.Text = _list[0].AboutMe;
            txt_website.Text = _list[0].Website;
            txt_firstname.Text = _list[0].FirstName;
            txt_lastname.Text = _list[0].LastName;
            drp_gender.SelectedValue = _list[0].Gender;
            drp_rstatus.SelectedValue = _list[0].RelationshipStatus;
            if (_list[0].isAllowBirthDay == 1)
                btn_dis.Checked = true;
            else
                btn_nodis.Checked = true;
            txt_hometown.Text = _list[0].HometTown;
            txt_curcity.Text = _list[0].CurrentCity;
            txt_zipcode.Text = _list[0].Zipcode;
            drp_country.SelectedValue = _list[0].CountryName;
            txt_occupations.Text = _list[0].Occupations;
            txt_companies.Text = _list[0].Companies;
            txt_schools.Text = _list[0].Schools;
            txt_interests.Text = _list[0].Interests;
            txt_movies.Text = _list[0].Movies;
            txt_musics.Text = _list[0].Musics;
            txt_books.Text = _list[0].Books;

            // load user avator info
            avator_upd1.UserName = username;
            avator_upd1.PhotoName = _list[0].PictureName;

        }
    }

    private void Update_User_Profile_Info(string username)
    {
        int isallowbirthdate = 1;
        if (btn_nodis.Checked)
            isallowbirthdate = 0;

        // check for long words
        if (UtilityBLL.isLongWordExist(txt_aboutme.Text) || UtilityBLL.isLongWordExist(txt_occupations.Text) || UtilityBLL.isLongWordExist(txt_companies.Text) || UtilityBLL.isLongWordExist(txt_schools.Text) || UtilityBLL.isLongWordExist(txt_interests.Text) || UtilityBLL.isLongWordExist(txt_movies.Text) || UtilityBLL.isLongWordExist(txt_musics.Text) || UtilityBLL.isLongWordExist(txt_books.Text))
        {
            Config.ShowMessageV2(msg, Resources.vsk.message_comment_01, "Error!", 0);  // You typed some invalid words, correct and post again.
            return;
        }
        // update user profile
        members.Update_User_Profile(username, txt_firstname.Text, txt_lastname.Text, drp_country.SelectedValue, drp_gender.SelectedValue, drp_rstatus.SelectedValue, txt_aboutme.Text, txt_website.Text, txt_hometown.Text, txt_curcity.Text, txt_zipcode.Text, txt_occupations.Text, txt_companies.Text, txt_schools.Text, txt_interests.Text, txt_movies.Text, txt_musics.Text, txt_books.Text, isallowbirthdate);

        // send mail 
        MailTemplateProcess(username);

        Response.Redirect( Config.GetUrl("myaccount/Profile.aspx?status=updated"));
    }

    private void MailTemplateProcess(string username)
    {
        //if sending mail option enabled
        if (Config.isEmailEnabled())
        {
            string emailaddress = members.Return_Value(username, "email");
            System.Collections.Generic.List<Struct_MailTemplates> lst = MailTemplateBLL.Get_Record("USRPROFUPD");
            if (lst.Count > 0)
            {
                string subject = MailProcess.Process2(lst[0].Subject, "\\[username\\]", username);
                string contents = MailProcess.Process2(lst[0].Contents, "\\[username\\]", username);

                MailProcess.Send_Mail(emailaddress, subject, contents);
            }
        }
    }
}
