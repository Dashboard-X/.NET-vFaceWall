<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="EmailOptions.aspx.cs"
    Inherits="user_EmailOptions" %>

<%@ Register src="modules/emailo.ascx" tagname="emailo" tagprefix="uc1" %>
<%@ Register src="modules/macc_menu.ascx" tagname="macc_menu" tagprefix="uc2" %>
<%@ Register src="modules/nav/left_nav.ascx" tagname="left_nav" tagprefix="uc3" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title><%= Resources.vsk.emailoptions %> - <%= Site_Settings.Website_Title %></title>
    
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
                <%= Resources.vsk.emailoptions %></h3>
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
                        <uc1:emailo ID="emailo1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
