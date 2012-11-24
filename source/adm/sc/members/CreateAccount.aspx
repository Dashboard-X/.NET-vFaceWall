<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_members_CreateAccount" %>

<%@ Register src="../../../modules/createaccount.ascx" tagname="createaccount" tagprefix="uc1" %>

<asp:Content ID="HeadContent2" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Member Management | Create Account</title>
</asp:Content>
<asp:Content ID="MainConten1t" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Create Account</h3>
    </div>
    <div id="msg" runat="server">
    </div>
    <div class="item">
        <uc1:createaccount ID="createaccount1" isAdmin="true" runat="server" />
    </div>
</asp:Content>
