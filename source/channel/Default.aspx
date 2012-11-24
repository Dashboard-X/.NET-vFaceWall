<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" MasterPageFile="~/main.master"
    CodeFile="Default.aspx.cs" Inherits="channel_Default" %>

<%@ Register Src="~/channel/modules/nav/navigation_sm.ascx" TagName="navigation_sm"
    TagPrefix="uc1" %>
<%@ Register Src="~/channel/modules/nav/navigation.ascx" TagName="navigation" TagPrefix="uc4" %>
<%@ Register Src="~/channel/modules/nav/third_nav.ascx" TagName="third_nav" TagPrefix="uc5" %>
<%@ Register Src="~/modules/list/channels_v2.ascx" TagName="channel_list" TagPrefix="uc2" %>
<%@ Register src="modules/search.ascx" tagname="search" tagprefix="uc3" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="canonical" href="<%= Config.GetUrl("channel/") %>" />
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="<%= NavigationClass %>">
            <uc4:navigation ID="navigation1" runat="server" />
            <uc1:navigation_sm ID="navigation_sm1" runat="server" />
        </div>
        <div class="<%= BodyClass %>">
            <div class="item">
                 <uc3:search ID="search2" runat="server" />
            </div>
            <div class="item_pad">
                <uc2:channel_list ID="channel_list1" runat="server" />
            </div>
        </div>
        <div class="<%= BodyRight %>">
            <uc5:third_nav ID="third_nav1" runat="server" />
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
