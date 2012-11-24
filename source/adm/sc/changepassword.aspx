<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Theme="adm"
    StylesheetTheme="adm" MasterPageFile="~/adm/sc/admin.master" Inherits="adm_sc_changepassword" %>

<%@ Register src="../../myaccount/modules/macc.ascx" tagname="macc" tagprefix="uc1" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Site_Settings.Website_Title %>
        | Administration | Control Panel | Manage Account</title>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="top_heading">
        <h3>
            Manage Account</h3>
    </div>
    <div class="separator_10px">
    </div>
     <div id="msg" runat="server"></div>
    <div class="item_pad_2">
        <div id="bx">
            <div class="bx_hd">
                Manage Account...
            </div>
            <div class="bx_bd">
                
                <uc1:macc ID="macc1" runat="server" />
            </div>
        </div>
    </div>
 
</asp:Content>
