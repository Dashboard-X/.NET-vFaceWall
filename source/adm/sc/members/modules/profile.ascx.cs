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

public partial class adm_sc_members_modules_profile : System.Web.UI.UserControl
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
        if (!Page.IsPostBack && this.UserName != "")
        {
            // load countries
            Load_Countries();
            // Load user profile information
            Load_Profile_Info(this.UserName);
            // Recent login activity of user
            recent_login1.UserName = this.UserName;
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
        if (this.UserName != "")
            Update_User_Profile_Info(this.UserName);
    }
    protected void btn_bsave_Click(object sender, EventArgs e)
    {
        if (this.UserName != "")
            Update_User_Profile_Info(this.UserName);
    }

    private void Load_Profile_Info(string username)
    {
        DataSet ds = members.Get_Information(username);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_aboutme.Text = ds.Tables[0].Rows[0]["AboutMe"].ToString();
            txt_website.Text = ds.Tables[0].Rows[0]["Website"].ToString();
            txt_firstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
            txt_lastname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
            drp_gender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
            drp_rstatus.SelectedValue = ds.Tables[0].Rows[0]["RelationshipStatus"].ToString();
            int isallowbirthday = int.Parse(ds.Tables[0].Rows[0]["isAllowBirthDay"].ToString());
            if (isallowbirthday == 1)
                btn_dis.Checked = true;
            else
                btn_nodis.Checked = true;
            txt_hometown.Text = ds.Tables[0].Rows[0]["hometown"].ToString();
            txt_curcity.Text = ds.Tables[0].Rows[0]["CurrentCity"].ToString();
            txt_zipcode.Text = ds.Tables[0].Rows[0]["Zipcode"].ToString();
            drp_country.SelectedValue = ds.Tables[0].Rows[0]["CountryName"].ToString();
            txt_occupations.Text = ds.Tables[0].Rows[0]["Occupations"].ToString();
            txt_companies.Text = ds.Tables[0].Rows[0]["Companies"].ToString();
            txt_schools.Text = ds.Tables[0].Rows[0]["Schools"].ToString();
            txt_interests.Text = ds.Tables[0].Rows[0]["Interests"].ToString();
            txt_movies.Text = ds.Tables[0].Rows[0]["Movies"].ToString();
            txt_musics.Text = ds.Tables[0].Rows[0]["Musics"].ToString();
            txt_books.Text = ds.Tables[0].Rows[0]["Books"].ToString();

            // load user avator info
            avator_upd1.UserName = username;
            avator_upd1.PhotoName = ds.Tables[0].Rows[0]["PictureName"].ToString();

            // stats
            st_comments.InnerHtml = ds.Tables[0].Rows[0]["stat_comments"].ToString();
            st_favorites.InnerHtml = ds.Tables[0].Rows[0]["stat_favorites"].ToString();
            st_friends.InnerHtml = ds.Tables[0].Rows[0]["stat_friends"].ToString();
            st_groups.InnerHtml = ds.Tables[0].Rows[0]["stat_groups"].ToString();
            st_messages.InnerHtml = ds.Tables[0].Rows[0]["stat_messages"].ToString();
            st_videos.InnerHtml = ds.Tables[0].Rows[0]["stat_videos"].ToString();
            st_videos.HRef = Config.GetUrl("adm/sc/videos/?user=" + username);
            st_qa.InnerHtml = ds.Tables[0].Rows[0]["stat_qa"].ToString();
            st_qa.HRef = Config.GetUrl("adm/sc/qa/?user=" + username);
            st_qaanswers.InnerHtml = ds.Tables[0].Rows[0]["stat_qanswers"].ToString();
            st_qafav.InnerHtml = ds.Tables[0].Rows[0]["stat_qafavorites"].ToString();
            st_photos.InnerHtml = ds.Tables[0].Rows[0]["stat_photos"].ToString();
            st_photos.HRef = Config.GetUrl("adm/sc/photos/?user=" + username);
            st_audio.InnerHtml = ds.Tables[0].Rows[0]["stat_audios"].ToString();
            st_audio.HRef = Config.GetUrl("adm/sc/videos/?user=" + username + "&mdp=1");
            st_audiofavorites.InnerHtml = ds.Tables[0].Rows[0]["stat_audiofavorites"].ToString();
            st_blogs.InnerHtml = ds.Tables[0].Rows[0]["stat_blogs"].ToString();
            st_blogs.HRef = Config.GetUrl("adm/sc/blogs/?user=" + username);
            st_forumtopics.InnerHtml = ds.Tables[0].Rows[0]["stat_blogs"].ToString();
            int ismail = Convert.ToInt32(ds.Tables[0].Rows[0]["stat_forum_posts"]);
            if (ismail == 1)
            {
                isautomail.InnerHtml = "Yes";
                isautomail.Attributes.Add("class", "green bold");
            }
            else
            {
                isautomail.InnerHtml = "No";
                isautomail.Attributes.Add("class", "red bold");
            }
            int mtype = Convert.ToInt32(ds.Tables[0].Rows[0]["type"]);
            if (mtype == 1)
            {
                memtype.InnerHtml = "Administrator";
                memtype.Attributes.Add("class", "green bold");
            }
            else if (mtype==2)
            {
                memtype.InnerHtml = "Premium Member";
                memtype.Attributes.Add("class", "green bold");
            }
            else
            {
                memtype.InnerHtml = "Normal Member";
                memtype.Attributes.Add("class", "red bold");
            }

            if (mtype == 1)
            {
                int reado = Convert.ToInt32(ds.Tables[0].Rows[0]["readonly"]);
                if (reado == 1)
                {
                    ronly.InnerHtml = "Admin Access Read Only";
                    ronly.Attributes.Add("class", "red bold");
                }
                else
                {
                    ronly.InnerHtml = "Admin Full Access";
                    ronly.Attributes.Add("class", "green bold");
                }
            }
            else
            {
                ronly.InnerHtml = "No Admin Permission";
                ronly.Attributes.Add("class", "red bold");
            }

            int fbud = Convert.ToInt32(ds.Tables[0].Rows[0]["fb_uid"]);
            if (fbud > 0)
            {
                fbuid.InnerHtml = "Registerred Via Facebook - FB UID: " + fbud;
                fbuid.Attributes.Add("class", "green bold");
            }
            else
            {
                fbuid.InnerHtml = "Direct Registered";
                fbuid.Attributes.Add("class", "red bold");
            }
            eml.InnerHtml = ds.Tables[0].Rows[0]["email"].ToString();
                     

            llogin.InnerHtml = UtilityBLL.CustomizeDate((DateTime)ds.Tables[0].Rows[0]["last_login"], DateTime.Now);
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
            Config.ShowMessageV2(msg, "Some invalid or long word exist, fix and save profile again.", "Error!", 0); 
            return;
        }
        // update user profile
        members.Update_User_Profile(username, txt_firstname.Text, txt_lastname.Text, drp_country.SelectedValue, drp_gender.SelectedValue, drp_rstatus.SelectedValue, txt_aboutme.Text, txt_website.Text, txt_hometown.Text, txt_curcity.Text, txt_zipcode.Text, txt_occupations.Text, txt_companies.Text, txt_schools.Text, txt_interests.Text, txt_movies.Text, txt_musics.Text, txt_books.Text, isallowbirthdate);

        Response.Redirect( Config.GetUrl("adm/sc/members/memberdetail.aspx?user=" + this.UserName + "&status=updated"));
    }
}
