<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/main.master" CodeFile="Profile.aspx.cs"
    Inherits="user_Profile" %>

<%@ Register Src="modules/profile.ascx" TagName="profile" TagPrefix="uc2" %>
<%@ Register Src="modules/macc_menu.ascx" TagName="macc_menu" TagPrefix="uc2" %>
<%@ Register Src="modules/nav/left_nav.ascx" TagName="left_nav" TagPrefix="uc1" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>
        <%= Resources.vsk.profilesetup %>
        -
        <%= Site_Settings.Website_Title %>
        .</title>
    <meta name="keywords" content="video, my account, profile, upload, share">
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
                <%= Resources.vsk.profilesetup %></h3>
        </div>
    </div>
    <div class="item_pad_2">
        <div class="msg_b">
            <table>
                <tr>
                    <td class="lft">
                        <div id="msg_l_lst">
                            <uc1:left_nav ID="left_nav1" runat="server" />
                        </div>
                    </td>
                    <td class="md">
                    </td>
                    <td class="rt">
                        <uc2:profile ID="profile1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
