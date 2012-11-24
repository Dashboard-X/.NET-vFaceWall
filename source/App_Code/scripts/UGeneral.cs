using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
/// <summary>
/// This class is used to generate script for handling generate items e.g tags
/// </summary>
public class UGeneral
{
	public UGeneral()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // ***************************************************
    // CORE LAYOUT FUNCTIONS
    // ***************************************************
    public string Return_Navigation_Class()
    {
        string cssClass = "chnl_left";
        switch (Site_Settings.NavigationSection)
        {
            case 0:
                // left side navigation
                // two column layout
                cssClass = "chnl_left";
                break;
            case 1:
                // right side navigation
                // two column layout
                cssClass = "chnl_right_nav";
                break;
            case 2:
                // two side navigation (left and right) - content in center
                // three column layout
                cssClass = "bd_left";
                break;
        }
        return cssClass;
    }

    public string Return_Body_Class()
    {
        string cssClass = "chnl_right";
        switch (Site_Settings.NavigationSection)
        {
            case 0:
                // left side navigation
                // two column layout
                cssClass = "chnl_right";
                break;
            case 1:
                // right side navigation
                // two column layout
                cssClass = "chnl_left_mn";
                break;
            case 2:
                // two side navigation (left and right) - content in center
                // three column layout
                cssClass = "bd_main";
                break;
        }
        return cssClass;
    }

    public string Return_RightNav_Class()
    {
        // only appear in three column layout
        string cssClass = "";
        switch (Site_Settings.NavigationSection)
        {
            case 0:
                // left side navigation
                // two column layout
                cssClass = "";
                break;
            case 1:
                // right side navigation
                // two column layout
                cssClass = "";
                break;
            case 2:
                // two side navigation (left and right) - content in center
                // three column layout
                cssClass = "bd_right";
                break;
        }
        return cssClass;
     }
    //****************************************************
    // CORE THEME FUNCTIONS
    //****************************************************
    public static string Return_Box_Css(int template)
    {
        int selected_template = 0;
        if (template > 0) // force template
            selected_template = template;
        else
        {
            if (Site_Settings.Site_Templates != 100)
                selected_template = Site_Settings.Site_Templates; // choose admin selected template if 100 is choosen
            else
            {
                // if admin choose default
                if (Site_Settings.NavigationSection == 2 && Site_Settings.Site_Templates == 0)
                {
                    // its not supported
                    // three column and web 2.0 style
                    selected_template = 2;
                }
                else
                    selected_template = template;
            }
        }
        string cssClass = "";
        if(selected_template ==0)
            cssClass = "module ui-corner-all";
        else
            cssClass = "module_pd2 ui-corner-all";

        return cssClass;
    }

     public static string Return_Heading_Css(int template)
    {
        int selected_template = 0;
        if (template > 0) // force template
            selected_template = template;
        else
        {
            if (Site_Settings.Site_Templates != 100)
                selected_template = Site_Settings.Site_Templates; // choose admin selected template if 100 is choosen
            else
            {
                // if admin choose default
                if (Site_Settings.NavigationSection == 2 && Site_Settings.Site_Templates == 0)
                {
                    // its not supported
                    // three column and web 2.0 style
                    selected_template = 2;
                }
                else
                    selected_template = template;
            }
        }
        string cssClass = "";
        switch (selected_template)
        {
            case 0:
                // Default web 2.0 style clean
               cssClass = "heading";
                break;
            case 1:
                // jquery ui header style
                cssClass = "heading-round btn-primary";
                break;
            case 2:
                // jquery ui default state style
                cssClass = "heading-round btn-warning";
                break;
            case 3:
                // jquery ui default hover style
                cssClass = "heading-round btn-danger";
                break;
            case 4:
                // jquery ui default active style
                cssClass = "heading-round btn-inverse";
                break;
        }
        return cssClass;
    }
    //****************************************************
    // GENERAL UTILITY FUNCTIONS
    //****************************************************

    // Utility function to prepare description of any record
     public static string Prepare_Description(string description, int length)
     {  
         string desc = UtilityBLL.CompressCode(description);
         if (desc == "")
             return desc;

         if (desc.Length > length && length > 0)
             desc = desc.Substring(0, length) + "..";
         
         return desc;
     }

  
}