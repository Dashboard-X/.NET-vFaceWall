<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Privacy.aspx.cs" MasterPageFile="~/main.master"
    Inherits="Privacy" %>

<%@ Register Src="~/modules/policies/privacy_policy.ascx" TagName="privacy_policy"
    TagPrefix="uc8" %>
<asp:Content ID="hd" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        - Privacy Policy.</title>
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div class="main_block ui-corner-all">
        <div class="module ui-corner-all">
            <div class="pd_5">
                <uc8:privacy_policy ID="Privacy_policy1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
