<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="ManageAccount.aspx.cs"
    Inherits="user_ManageAccount" %>

<%@ Register Src="modules/macc.ascx" TagName="macc" TagPrefix="uc1" %>
<%@ Register Src="modules/macc_menu.ascx" TagName="macc_menu" TagPrefix="uc2" %>
<%@ Register Src="modules/nav/left_nav.ascx" TagName="left_nav" TagPrefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Resources.vsk.manageaccount %>
        -
        <%= Site_Settings.Website_Title %>
        </title>
</asp:Content>
<asp:Content ID="pagecontent" ContentPlaceHolderID="MC" runat="server">
    <div id="msg" runat="server">
    </div>
    <div class="item">
        <uc2:macc_menu ID="macc_menu1" runat="server" />
    </div>
    <div class="item_pad_4">
        <div style="padding-left: 215px;">
            <h3>
                <%= Resources.vsk.manageaccount %></h3>
        </div>
    </div>
    <div class="item_pad_2">
        <div class="msg_b">
            <table>
                <tr>
                    <td class="lft">
                        <div id="msg_l_lst">
                            <uc3:left_nav ID="left_nav1" runat="server" />
                        </div>
                    </td>
                    <td class="md">
                    </td>
                    <td class="rt">
                        <uc1:macc ID="macc1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
