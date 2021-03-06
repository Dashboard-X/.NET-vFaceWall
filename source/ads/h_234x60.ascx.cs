﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class ads_h_234x60 : System.Web.UI.UserControl
{
    private string _script = "";
    private bool _isadult = false;
    public string Script
    {
        set { _script = value; }
        get { return _script; }
    }
    public bool isAdult
    {
        set { _isadult = value; }
        get { return _isadult; }
    }

    protected void Page_Load(object send, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Config.isAdsEnabled())
            {
                // Horizontal Ad - 234x60
                // Adult ID : 6
                // Non Adult ID : 5
                if (this.isAdult)
                {
                    // adult section
                    this.Script = AdsBLL.Return_Ad_Script(6);
                }
                else
                {
                    // non adult section
                    this.Script = AdsBLL.Return_Ad_Script(5);
                }
            }
        }
        
    }

}
