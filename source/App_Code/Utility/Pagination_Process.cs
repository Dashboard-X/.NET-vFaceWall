using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Functions used for pagination processing
/// </summary>
public class Pagination_Process
{
	public Pagination_Process()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string Ajax_Pagination_V2(int total_records, int pagesize, int selectedpage)
    {
        StringBuilder str = new StringBuilder();
        int total_pages = (int)Math.Ceiling((double)total_records / pagesize);
        if (total_pages <= 1)
            return "";
        str.Append("<div class=\"pagination\">\n");
        ArrayList arr = Pagination_Process.Return_Pagination_Link(total_pages, 7, selectedpage);
        int i = 0;
        string _css = "";
        for (i = 0; i <= arr.Count - 1; i++)
        {
            if (Convert.ToInt32(arr[i]) == selectedpage)
                _css = " class=\"active\"";
            else
                _css = "";

            str.Append("<li" + _css + "><a href=\"#\" id=\"apg_" + arr[i].ToString() + "\" class=\"apagination-css\">" + arr[i].ToString() + "</a>");
        }
        str.Append("</div>\n");
        return str.ToString();
    }
     
    // Prepare Script Compatible with SQL SERVER 2005 or above Pagination
    public static string Prepare_SQLSERVER2005_Pagination(string query, string order,int pagenumber, int pagesize)
    {
        // fetch and return only batch of data starting from star posting to lastbound
        int startindex = (pagenumber - 1) * pagesize + 1;
        int lastbound = (startindex - 1) + pagesize;
        return "SELECT TOP " + pagesize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY " + order + ") AS RowNumber," + query + ") AS Results WHERE RowNumber >=" + startindex + " AND RowNumber <=" + lastbound;
    }

    // Prepare Script Compatible with MYSQL Pagination
    public static string Prepare_MySQL_Pagination(string query, int pagenumber, int pagesize)
    {
        // fetch and return only batch of data
        int startindex = (pagenumber - 1) * pagesize;
        return query + "  LIMIT " + startindex + "," + pagesize;
    }
    // Main Pagination Logic
    public static ArrayList Return_Pagination_Link(int TotalPages,int Total_Links,int SelectedPage) 
     {
         int i;
         ArrayList arr = new ArrayList();
         if (TotalPages<Total_Links)
         {
             for(i=1;i<=TotalPages;i++)
             {
                 arr.Add(i);
             }
         }
         else
         {
            int startindex  = SelectedPage;
            int lowerbound = startindex - (int)Math.Floor((double)Total_Links / 2);
            int upperbound = startindex + (int)Math.Floor((double)Total_Links / 2);
             if (lowerbound < 1)
             {
                 //calculate the difference and increment the upper bound
                upperbound = upperbound + (1 - lowerbound);
                lowerbound = 1;
             }
             //if upperbound is greater than total page is
             if (upperbound > TotalPages)
             {
                //calculate the difference and decrement the lower bound
                lowerbound = lowerbound - (upperbound - TotalPages);
                upperbound = TotalPages;
             }
             for(i=lowerbound;i<=upperbound;i++)
             {
                 arr.Add(i);
             }
           
           
         }
        return arr;
       
     }

    // Advance Pagination version 2.0
    public static ArrayList Return_Pagination_Link_v2(int TotalPages, int SelectedPage)
    {
        int i = 0;
        int value = 0;
        ArrayList arr = new ArrayList();
        ArrayList lower_arr = new ArrayList();
        ArrayList upper_arr = new ArrayList();

        if (SelectedPage == 1)
        {
            if (TotalPages < 11)
            {
                //// display seven links
                //// 1 2 3 4 5 6 7
                for (i = 1; i <= 7; i++)
                {
                    if (i <= TotalPages)
                    {
                        value = i;
                        arr.Add(value);
                    }
                }
            }
            else if (TotalPages >= 11 & TotalPages < 51)
            {
                //// display eight links
                //// 1 2 3 4 5 6 7(4+)11 
                for (i = 1; i <= 8; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 51 & TotalPages < 101)
            {
                //// display nine links 
                //// 1 2 3 4 5 6 7(4+)11(40+)51
                for (i = 1; i <= 9; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 101 & TotalPages < 501)
            {
                //// display ten links
                //// 1 2 3 4 5 6 7(4+)11(40+)51(50+)101
                for (i = 1; i <= 10; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 501 & TotalPages < 1001)
            {
                //// display eleven links
                //// 1 2 3 4 5 6 7(4+)11(40+)51(50+)101(400+)501
                for (i = 1; i <= 11; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else if (i == 11)
                    {
                        value = value + 400;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 1001 & TotalPages < 5001)
            {
                //// display twelve links
                for (i = 1; i <= 12; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else if (i == 11)
                    {
                        value = value + 400;
                    }
                    else if (i == 12)
                    {
                        value = value + 500;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 5001 & TotalPages < 10001)
            {
                //// display thirteen links
                for (i = 1; i <= 13; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else if (i == 11)
                    {
                        value = value + 400;
                    }
                    else if (i == 12)
                    {
                        value = value + 500;
                    }
                    else if (i == 13)
                    {
                        value = value + 4000;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 10001 & TotalPages < 50001)
            {
                //// display fourteen links
                for (i = 1; i <= 14; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else if (i == 11)
                    {
                        value = value + 400;
                    }
                    else if (i == 12)
                    {
                        value = value + 500;
                    }
                    else if (i == 13)
                    {
                        value = value + 4000;
                    }
                    else if (i == 14)
                    {
                        value = value + 5000;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
            else if (TotalPages >= 50001)
            {
                //// display fifteen links
                for (i = 1; i <= 15; i++)
                {
                    if (i == 8)
                    {
                        value = i + 3;
                    }
                    else if (i == 9)
                    {
                        value = value + 40;
                    }
                    else if (i == 10)
                    {
                        value = value + 50;
                    }
                    else if (i == 11)
                    {
                        value = value + 400;
                    }
                    else if (i == 12)
                    {
                        value = value + 500;
                    }
                    else if (i == 13)
                    {
                        value = value + 4000;
                    }
                    else if (i == 14)
                    {
                        value = value + 5000;
                    }
                    else if (i == 15)
                    {
                        value = value + 40000;
                    }
                    else
                    {
                        value = i;
                    }
                    arr.Add(value);
                }
            }
        }
        if (SelectedPage > 1)
        {
            //// Lower Bound
            if (SelectedPage < 11)
            {
                for (i = 1; i <= SelectedPage; i++)
                {
                    //// display upto seven links on lower bound side
                    if (i < 7)
                    {
                        value = SelectedPage - i;
                        if (value > 0)
                        {
                            lower_arr.Add(value);
                        }
                    }
                }
            }
            else if (SelectedPage >= 11 & SelectedPage < 51)
            {
                //// display upto eight links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 8)
                    {
                        value = (SelectedPage - 6) - 4;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 51 & SelectedPage < 101)
            {
                //// display upto nine links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 7)
                    {
                        value = (SelectedPage - 6) - 4;
                    }
                    else if (i == 8)
                    {
                        value = value - 40;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 101 & SelectedPage < 501)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = (SelectedPage - 6) - 4;
                    }
                    else if (i == 7)
                    {
                        value = value - 40;
                    }
                    else if (i == 8)
                    {
                        value = value - 50;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 501 & SelectedPage < 1001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value - 40;
                    }
                    else if (i == 7)
                    {
                        value = value - 50;
                    }
                    else if (i == 8)
                    {
                        value = value - 400;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 1001 & SelectedPage < 5001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value - 50;
                    }
                    else if (i == 7)
                    {
                        value = value - 400;
                    }
                    else if (i == 8)
                    {
                        value = value - 500;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 5001 & SelectedPage < 10001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value - 400;
                    }
                    else if (i == 7)
                    {
                        value = value - 500;
                    }
                    else if (i == 8)
                    {
                        value = value - 4000;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 10001 & SelectedPage < 50001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value - 500;
                    }
                    else if (i == 7)
                    {
                        value = value - 4000;
                    }
                    else if (i == 8)
                    {
                        value = value - 5000;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }
            else if (SelectedPage >= 50001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value - 4000;
                    }
                    else if (i == 7)
                    {
                        value = value - 5000;
                    }
                    else if (i == 8)
                    {
                        value = value - 40000;
                    }
                    else
                    {
                        value = SelectedPage - i;
                    }
                    lower_arr.Add(value);
                }
            }

            //// display upper bound
            int diff = TotalPages - SelectedPage;
            if (diff < 11)
            {
                for (i = 1; i <= 6; i++)
                {
                    //// display upto seven links on upper bound side
                    value = SelectedPage + i;
                    if (value <= TotalPages)
                    {
                        upper_arr.Add(value);
                    }
                }
            }
            else if (diff >= 11 & diff < 51)
            {
                //// display upto eight links on upper bound side
                for (i = 1; i <= 7; i++)
                {
                    if (i == 7)
                    {
                        value = (SelectedPage + 6) + 4;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 51 & diff < 101)
            {
                //// display upto nine links on upper bound side
                for (i = 1; i <= 8; i++)
                {
                    if (i == 7)
                    {
                        value = (SelectedPage + 6) + 4;
                    }
                    else if (i == 8)
                    {
                        value = value + 40;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 101 & diff < 501)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = (SelectedPage + 6) + 4;
                    }
                    else if (i == 7)
                    {
                        value = value + 40;
                    }
                    else if (i == 8)
                    {
                        value = value + 50;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 501 & diff < 1001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value + 40;
                    }
                    else if (i == 7)
                    {
                        value = value + 50;
                    }
                    else if (i == 8)
                    {
                        value = value + 400;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 1001 & diff < 5001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value + 50;
                    }
                    else if (i == 7)
                    {
                        value = value + 400;
                    }
                    else if (i == 6)
                    {
                        value = value + 500;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 5001 & diff < 10001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value + 400;
                    }
                    else if (i == 7)
                    {
                        value = value + 500;
                    }
                    else if (i == 8)
                    {
                        value = value + 4000;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (diff >= 10001 & diff < 50001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value + 500;
                    }
                    else if (i == 7)
                    {
                        value = value + 4000;
                    }
                    else if (i == 8)
                    {
                        value = value + 5000;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }
            else if (SelectedPage >= 50001)
            {
                //// display upto ten links
                for (i = 1; i <= 8; i++)
                {
                    if (i == 6)
                    {
                        value = value + 4000;
                    }
                    else if (i == 7)
                    {
                        value = value + 5000;
                    }
                    else if (i == 8)
                    {
                        value = value + 40000;
                    }
                    else
                    {
                        value = SelectedPage + i;
                    }
                    upper_arr.Add(value);
                }
            }

            //// add lower array values
            for (i = 0; i <= lower_arr.Count - 1; i++)
            {
                int rev_index = (lower_arr.Count - 1) - i;
                arr.Add(lower_arr[rev_index].ToString());
            }
            //// add selected record
            arr.Add(SelectedPage);
            //// add upper array values
            for (i = 0; i <= upper_arr.Count - 1; i++)
            {
                arr.Add(upper_arr[i].ToString());
            }
        }

        return arr;
    }

    // Normal Pagination script -/ works when user load all user records.
    public static void Process_Pagination(DataList _list, Repeater _rept, HyperLink _prev, HyperLink _next, int _size, int _pagenumber, IEnumerable _source)
    {
        PagedDataSource objPds = new PagedDataSource();
        objPds.DataSource = _source;
        objPds.AllowPaging = true;

        objPds.PageSize = _size;
        objPds.CurrentPageIndex = (_pagenumber - 1);

        if (!objPds.IsFirstPage)
            _prev.Visible = true;
        else
            _prev.Visible = false;
        if (!objPds.IsLastPage)
            _next.Visible = true;
        else
            _next.Visible = false;
        ArrayList arr = Return_Pagination_Link(objPds.PageCount, 11, _pagenumber);
        if (objPds.PageCount > 1)
        {
            _rept.Visible = true;
            _rept.DataSource = arr;
            _rept.DataBind();
        }
        else
        {
            _rept.Visible = false;
        }
        _list.DataSource = objPds;
        _list.DataBind();
    }
    public static void Process_Pagination(DataList _list, Repeater _rept, LinkButton _prev, LinkButton _next, int _size, int _pagenumber, IEnumerable _source)
    {
        PagedDataSource objPds = new PagedDataSource();
        objPds.DataSource = _source;
        objPds.AllowPaging = true;

        objPds.PageSize = _size;
        objPds.CurrentPageIndex = (_pagenumber - 1);

        if (!objPds.IsFirstPage)
            _prev.Visible = true;
        else
            _prev.Visible = false;
        if (!objPds.IsLastPage)
            _next.Visible = true;
        else
            _next.Visible = false;
        ArrayList arr = Return_Pagination_Link(objPds.PageCount, 11, _pagenumber);
        if (objPds.PageCount > 1)
        {
            _rept.Visible = true;
            _rept.DataSource = arr;
            _rept.DataBind();
        }
        else
        {
            _rept.Visible = false;
        }
        _list.DataSource = objPds;
        _list.DataBind();
    }
    // Scalable Pagination Script -/ work when user load each page only
    public static void Process_Pagination(DataList _list, Repeater _rept, HyperLink _prev, HyperLink _next, int _size, int _pagenumber, IEnumerable _source, int _totalrecords)
    {
        int total_pages = (int)Math.Ceiling((double)_totalrecords / _size);
        if (total_pages > 1)
        {
            // Previous Link Scripting
            if (_pagenumber > 1)
            {
                _prev.Visible = true;
                int firstbound = (((_pagenumber - 1) - 1) * _size) + 1;
                if (firstbound < 0)
                    firstbound = 1;
                int lastbound = firstbound + _size - 1;
                _prev.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
            }
            else
            {
                _prev.Visible = false;
            }
            // Next Link Scripting
            if (_pagenumber < total_pages)
            {
                _next.Visible = true;
                int firstbound = (((_pagenumber + 1) - 1) * _size) + 1;
                int lastbound = firstbound + _size - 1;
                if (lastbound > _totalrecords)
                    lastbound = _totalrecords;
                _next.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
            }
            else
            {
                _next.Visible = false;
            }

            // main pagination links
            ArrayList arr = Return_Pagination_Link_v2(total_pages, _pagenumber);
            _rept.Visible = true;
            _rept.DataSource = arr;
            _rept.DataBind();
        }
        else
        {
            _rept.Visible = false;
            _next.Visible = false;
            _prev.Visible = false;
        }

        _list.DataSource = _source;
        _list.DataBind();
    }
    public static void Process_Pagination(DataList _list, Repeater _rept, LinkButton _prev, LinkButton _next, int _size, int _pagenumber, IEnumerable _source, int _totalrecords)
    {
        int total_pages = (int)Math.Ceiling((double)_totalrecords / _size);
        if (total_pages > 1)
        {
            // Previous Link Scripting
            if (_pagenumber > 1)
            {
                _prev.Visible = true;
                int firstbound = (((_pagenumber - 1) - 1) * _size) + 1;
                if (firstbound < 0)
                    firstbound = 1;
                int lastbound = firstbound + _size - 1;
                _prev.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
            }
            else
            {
                _prev.Visible = false;
            }
            // Next Link Scripting
            if (_pagenumber < total_pages)
            {
                _next.Visible = true;
                int firstbound = (((_pagenumber + 1) - 1) * _size) + 1;
                int lastbound = firstbound + _size - 1;
                if (lastbound > _totalrecords)
                    lastbound = _totalrecords;
                _next.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
            }
            else
            {
                _next.Visible = false;
            }

            // main pagination links
            ArrayList arr = Return_Pagination_Link_v2(total_pages, _pagenumber);
            _rept.Visible = true;
            _rept.DataSource = arr;
            _rept.DataBind();
        }
        else
        {
            _rept.Visible = false;
            _next.Visible = false;
            _prev.Visible = false;
        }

        _list.DataSource = _source;
        _list.DataBind();
    }
    
    // Process Pagination ver 2.0 // Compatible with Hyperlinks
    // Compatible with both MySQL / SQL SERVER.
    // Support Advance Pagination
    public static void Process_Pagination_v2(DataList _list,Repeater _rept,HyperLink _prev,HyperLink _next,int _size, int _pagenumber, IEnumerable _source, int _totalrecords)
    {
        if (Site_Settings.Pagination_Type == 1)
        {
            // MySQL Compatible
            int total_pages = (int)Math.Ceiling((double)_totalrecords / _size);
            if (total_pages > 1)
            {
                // Previous Link Scripting
                if (_pagenumber > 1)
                {
                    _prev.Visible = true;
                    int firstbound = (((_pagenumber - 1) - 1) * _size) + 1;
                    if (firstbound < 0)
                        firstbound = 1;
                    int lastbound = firstbound + _size - 1;
                    _prev.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
                }
                else
                {
                    _prev.Visible = false;
                }
                // Next Link Scripting
                if (_pagenumber < total_pages)
                {
                    _next.Visible = true;
                    int firstbound = (((_pagenumber + 1) - 1) * _size) + 1;
                    int lastbound = firstbound + _size - 1;
                    if (lastbound > _totalrecords)
                        lastbound = _totalrecords;
                    _next.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
                }
                else
                {
                    _next.Visible = false;
                }

                // main pagination links
                ArrayList arr = Pagination_Process.Return_Pagination_Link_v2(total_pages, _pagenumber);
                _rept.Visible = true;
                _rept.DataSource = arr;
                _rept.DataBind();
            }
            else
            {
                _rept.Visible = false;
                _next.Visible = false;
                _prev.Visible = false;
            }

            _list.DataSource = _source;
            _list.DataBind();

        }
        else
        {
            // SQL SERVER Compatible
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = _source;
            objPds.AllowPaging = true;

            objPds.PageSize = _size;
            objPds.CurrentPageIndex = (_pagenumber - 1);

            if (!objPds.IsFirstPage)
            {
                _prev.Visible = true;
                int firstbound = (((_pagenumber - 1) - 1) * _size) + 1;
                if (firstbound < 0)
                    firstbound = 1;

                int lastbound = firstbound + _size - 1;

                _prev.ToolTip = "Showing previous " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
            }
            else
            {
                _prev.Visible = false;
            }
            if (!objPds.IsLastPage)
            {
                int firstbound = (((_pagenumber + 1) - 1) * _size) + 1;
                int lastbound = firstbound + _size - 1;
                if (lastbound > _totalrecords)
                    lastbound = _totalrecords;

                _next.ToolTip = "Showing next " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
                _next.Visible = true;
            }
            else
            {
                _next.Visible = false;
            }

            ArrayList arr = Pagination_Process.Return_Pagination_Link_v2(objPds.PageCount, _pagenumber);
            if (objPds.PageCount > 1)
            {
                _rept.Visible = true;
                _rept.DataSource = arr;
                _rept.DataBind();
            }
            else
            {
                _rept.Visible = false;
            }

            _list.DataSource = objPds;
            _list.DataBind();
        }
    }

    // Pagination Script version 3.0, support VSK 5.3 listing (SQL SERVER 2005, MySQL compatible)
    // Compatible with both MySQL / SQL SERVER.
    // Support Advance Pagination
    public static void Process_Pagination_v3(Repeater _rept, HyperLink _prev, HyperLink _next, int _size, int _pagenumber, int _totalrecords)
    {
       
            // MySQL Compatible
            int total_pages = (int)Math.Ceiling((double)_totalrecords / _size);
            if (total_pages > 1)
            {
                // Previous Link Scripting
                if (_pagenumber > 1)
                {
                    _prev.Visible = true;
                    int firstbound = (((_pagenumber - 1) - 1) * _size) + 1;
                    if (firstbound < 0)
                        firstbound = 1;
                    int lastbound = firstbound + _size - 1;
                    _prev.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
                }
                else
                {
                    _prev.Visible = false;
                }
                // Next Link Scripting
                if (_pagenumber < total_pages)
                {
                    _next.Visible = true;
                    int firstbound = (((_pagenumber + 1) - 1) * _size) + 1;
                    int lastbound = firstbound + _size - 1;
                    if (lastbound > _totalrecords)
                        lastbound = _totalrecords;
                    _next.ToolTip = "Showing " + firstbound + " - " + lastbound + " records of " + _totalrecords + " records";
                }
                else
                {
                    _next.Visible = false;
                }

                // main pagination links
                ArrayList arr = Pagination_Process.Return_Pagination_Link_v2(total_pages, _pagenumber);
                _rept.Visible = true;
                _rept.DataSource = arr;
                _rept.DataBind();
            }
            else
            {
                _rept.Visible = false;
                _next.Visible = false;
                _prev.Visible = false;
            }


        
    }

}
