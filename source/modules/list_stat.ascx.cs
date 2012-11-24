using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class modules_list_stat : System.Web.UI.UserControl
{
    //globalization text
    string showing = Resources.vsk.showing;
    string records = Resources.vsk.records.ToLower();

    private int _totalrecords = 0;
    private int _pagenumber = 0;
    private int _pagesize = 0;

    public int TotalRecords
    {
        set { _totalrecords = value; }
        get { return _totalrecords; }
    }
    
    public int PageNumber
    {
        set { _pagenumber = value; }
        get { return _pagenumber; }
    }

    public int PageSize
    {
        set { _pagesize = value; }
        get { return _pagesize; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int first_boundary = 0;
            if(this.TotalRecords>0)
                first_boundary = ((this.PageNumber - 1) * this.PageSize) + 1;
            int second_boundary = ((this.PageNumber - 1) * this.PageSize) + this.PageSize;
            if (second_boundary > this.TotalRecords)
                second_boundary = this.TotalRecords;

            vd_stat.InnerHtml = showing + " <strong>" + first_boundary.ToString() + "</strong> " + Resources.vsk.to + " <strong>" + second_boundary.ToString() + "</strong> " + Resources.vsk.of + " <strong>" + this.TotalRecords.ToString() + "</strong> " + records;
        }
    }
}
