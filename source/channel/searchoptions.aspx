<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="searchoptions.aspx.cs"
    Inherits="channel_searchoptions" %>

<%@ Register Src="~/channel/modules/nav/navigation_sm.ascx" TagName="navigation_sm"
    TagPrefix="uc1" %>
<%@ Register Src="~/channel/modules/nav/navigation.ascx" TagName="navigation" TagPrefix="uc4" %>
<%@ Register Src="~/channel/modules/nav/third_nav.ascx" TagName="third_nav" TagPrefix="uc5" %>
<%@ Register Src="modules/advsearch.ascx" TagName="advsearch" TagPrefix="uc2" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="<%= NavigationClass %>">
            <uc4:navigation ID="navigation1" runat="server" />
            <uc1:navigation_sm ID="navigation_sm1" runat="server" />
        </div>
        <div class="<%= BodyClass %>">
            <ul class="breadcrumb" id="bread" runat="server">
            </ul>
            <uc2:advsearch ID="advsearch1" runat="server" />
        </div>
        <div class="<%= BodyRight %>">
            <uc5:third_nav ID="third_nav1" runat="server" />
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
