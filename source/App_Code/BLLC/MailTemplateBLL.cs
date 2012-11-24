using System;
using System.Data;

using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Sql.DataAccessLayer;
using System.Collections.Generic;

/// <summary>
/// Provided by MediaSoftPro (www.mediasoftpro.com)
/// Twitter: @mediasoftpro
/// Facebook: facebook.com/mediasoftpro
/// </summary>
public class MailTemplateBLL
{
	public MailTemplateBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Add(string TemplateKey,string Description,string subjecttags,string tags,string subject, string contents,string type)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Insert into mailtemplates(templatekey,description,tags,subjecttags,subject,contents,type)values(@templatekey,@description,@tags,@subjecttags,@subject,@contents,@type)", new SqlParameter("@templatekey", TemplateKey), new SqlParameter("@description", Description), new SqlParameter("@tags", tags), new SqlParameter("@subjecttags", subjecttags), new SqlParameter("@subject", subject), new SqlParameter("@contents", contents), new SqlParameter("@type", type));
        return true;
    }

    public static bool Validate_TemplateKey(string TemplateKey)
    {
        int result = Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, CommandType.Text, "SELECT Count(templatekey) from mailtemplates where templatekey=@templatekey",new SqlParameter("@templatekey",TemplateKey)));
        if (result > 0)
            return true;
        else
            return false;
    }
    public static DataSet Get_Information(string type)
    {
        if (type == "all")
           return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from mailtemplates order by type");
        else
           return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from mailtemplates where type=@type order by type",new SqlParameter("@type",type));
               
    }

    public static DataSet Get_Value(int id)
    {
        return (DataSet)SqlHelper.ExecuteDataset(Config.ConnectionString, CommandType.Text, "SELECT * from mailtemplates where id=@id",new SqlParameter("@id",id));
    }

    public static List<Struct_MailTemplates> Get_Record(string templatekey)
    {
        if (HttpContext.Current.Cache["ld_mailtemplates_" + templatekey] == null)
        {
            HttpContext.Current.Cache["ld_mailtemplates_" + templatekey] = Fetch_Record(templatekey);
        }
        return (List<Struct_MailTemplates>)HttpContext.Current.Cache["ld_mailtemplates_" + templatekey];
    }

    public static List<Struct_MailTemplates> Fetch_Record(string templatekey)
    {
        SqlConnection con = new SqlConnection(Config.ConnectionString);
        con.Open();
        System.Collections.Generic.List<Struct_MailTemplates> items = new System.Collections.Generic.List<Struct_MailTemplates>();
        Struct_MailTemplates str_ct = default(Struct_MailTemplates);
        SqlCommand cmd = new SqlCommand("SELECT subject,contents from mailtemplates where templatekey=@templatekey", con);
        cmd.Parameters.Add(new SqlParameter("@templatekey", templatekey));
        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
            str_ct = new Struct_MailTemplates();
            str_ct.Subject = reader["subject"].ToString();
            str_ct.Contents = reader["contents"].ToString();
            items.Add(str_ct);
        }
        reader.Close();
        con.Close();
        return items;
    }


    public static bool Update_Record(int id, string subject, string contents)
    {
        SqlHelper.ExecuteNonQuery(Config.ConnectionString, CommandType.Text, "Update mailtemplates set subject=@subject,contents=@contents where id=@id", new SqlParameter("@id", id), new SqlParameter("@contents", contents), new SqlParameter("@subject", subject));
        return true;
    }

}
