<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="adm" Theme="adm" MasterPageFile="~/adm/sc/admin.master"
    CodeFile="Default.aspx.cs" Inherits="adm_sc_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="headContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Control Panel</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mini-layout">
        <div id="msg" runat="server">
        </div>
       
        <div id="admin_welcome_message">
            <h1>
                <%= Site_Settings.Website_Title %></h1>
            <br />
            <h3>
                Control Panel</h3>
        </div>
    </div>
</asp:Content>
