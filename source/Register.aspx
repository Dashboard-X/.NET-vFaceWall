<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" MasterPageFile="~/main.master"
    Inherits="Register" %>

<%@ Register Src="~/modules/ads/vd_sponsor.ascx" TagName="vd_sponsor" TagPrefix="uc4" %>
<%@ Register Src="ads/s_300x250.ascx" TagName="s_300x250" TagPrefix="uc2" %>
<%@ Register Src="ads/h_468x60.ascx" TagName="h_468x60" TagPrefix="uc2" %>
<%@ Register Src="modules/createaccount.ascx" TagName="createaccount" TagPrefix="uc1" %>
<%@ Register Src="widgets/nav/main_nav.ascx" TagName="main_nav" TagPrefix="uc3" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div id="msg" runat="server"></div>
        <div class="module ui-corner-all" id="mn" runat="server">
            <uc1:createaccount ID="createaccount1" runat="server" />
        </div>
    </div>
</asp:Content>
