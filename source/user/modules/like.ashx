<%@ WebHandler Language="C#" Class="like" %>

using System;
using System.Web;
using System.Text;

public class like : IHttpHandler {

    private int VoteType = 0; // 0: negative, 1: positive
    private int Points = 0;
    private long ContentID = 0;
    private string PosterName = ""; // Comment Poster Name
    private string AuthorName = ""; // Comment Author Name
    private bool NegativeVotes = true;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["pts"] != null)
            this.Points = Convert.ToInt32(context.Request.Params["pts"]);
        if (context.Request.Params["vt"] != null)
            this.VoteType = Convert.ToInt32(context.Request.Params["vt"]);
        if (context.Request.Params["id"] != null)
            this.ContentID = Convert.ToInt64(context.Request.Params["id"]);
        if (context.Request.Params["ausr"] != null)
            this.AuthorName = context.Request.Params["ausr"].ToString();

        if (context.Request.Params["nvt"] != null)
            this.NegativeVotes = Convert.ToBoolean(context.Request.Params["nvt"]);

        if (context.User.Identity.IsAuthenticated)
            this.PosterName = context.User.Identity.Name;

        context.Response.Write(Generate_Output(context));
    }

    private string Generate_Output(HttpContext context)
    {

        //**********************************************************
        // Generate Output Panel
        //**********************************************************
        StringBuilder str = new StringBuilder();

        //*********************************
        // Security Check
        //*********************************
        if (context.Request.UrlReferrer == null)
        {
            return "p400"; // Authorization Failed"
        }
        //if (!context.Request.UrlReferrer.Host.Contains("URLMATCHINGWORD"))
        //{
        //    return "401"; // Double Authorization
        //}

        if (this.ContentID == 0)
        {
            return "p100"; //
        }

        //if (this.AuthorName == this.PosterName)
        //{
        //    return "p101"; // can't vote your own activity
        //}

        this.Points = this.Points + 1;
        string _field = "";
        if (this.VoteType == 1) // liked
            _field = "liked";
        else
            _field = "disliked";

        if (Config.isLoginRequired_Points())
        {
            int type = 14; // user activity

            // if not authorized
            if (this.PosterName == "")
                return "p102"; // sign in or sign up

            if (!User_Ratings.Check(this.PosterName, this.ContentID, type))
            {
                User_Ratings.Add(this.PosterName, this.ContentID, type, 1);
                ActivityBLL.Update_Field(this.ContentID, this.Points.ToString(), _field);
            }
            else
            {
                return "p103"; // you already post vote on this comment
            }
        }
        else
        {
            ActivityBLL.Update_Field(this.ContentID, this.Points.ToString(), _field);
        }

        return this.Points.ToString();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}