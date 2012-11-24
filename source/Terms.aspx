<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Terms.aspx.cs" MasterPageFile="~/main.master"
    Inherits="Terms" %>

<%@ Register Src="~/modules/policies/terms_of_use.ascx" TagName="terms_of_use" TagPrefix="uc8" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="module ui-corner-all">
            <uc8:terms_of_use ID="Terms_of_use1" runat="server" />
        </div>
    </div>
</asp:Content>
